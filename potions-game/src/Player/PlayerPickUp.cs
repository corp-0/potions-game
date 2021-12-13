using System;
using System.Collections.Generic;
using System.Text;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Managers;
using PotionsGame.Core.Utils;
using PotionsGame.Items;

// ReSharper disable SuspiciousTypeConversion.Global
namespace PotionsGame.Player
{
    
    [RegisteredType(nameof(PlayerPickUp), baseType = nameof(Node2D))]
    public class PlayerPickUp: Node2D, IDisableable, IDisplayDebuggeable
    {
        private Area2D pickUpArea;
        [Export] public int MaxInventorySize { get; private set; } = 2;
        private int selectedItemIndex;
        private Pickupable activeItem;
        private readonly List<Pickupable> inventory = new List<Pickupable>();
        private readonly List<Pickupable> itemsInReach = new List<Pickupable>();
        private Label debugInventoryLabel;

        public event Action<int> SelectedItemChanged;
        public event Action<Pickupable> ActiveItemChanged;
        public event Action<List<Pickupable>> InventoryChanged;
        
        public override void _Ready()
        {
            pickUpArea = this.GetNodeByTypeOrNull<Area2D>();
            debugInventoryLabel = this.GetNodeByTypeOrNull<Label>();
            if (pickUpArea is null)
            {
                Logger.Instance.Error(this, $"{nameof(PlayerPickUp)} needs a {nameof(Area2D)} as child " +
                                            "but none was found!");
                throw new Exception("Required child not found!");
            }
            
            pickUpArea.Connect("body_entered", this, nameof(OnEnteredReach));
            pickUpArea.Connect("body_exited", this, nameof(OnExitedReach));
            UiManager.Instance.InventoryHud.NotifyNewActivePlayerPickUp(this);
        }

        public void OnEnteredReach(Node body)
        {
            if (inventory.Count == MaxInventorySize || !(body is Pickupable pickupable)) return;
            pickupable.IsReachable = true;
            itemsInReach.Add(pickupable);
        }

        public void OnExitedReach(Node body)
        {
            if (!(body is Pickupable pickupable) || !itemsInReach.Contains(pickupable)) return;
            pickupable.IsReachable = false;
            itemsInReach.Remove(pickupable);
        }

        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                TryPickUp();
            }
            
            if (Input.IsActionJustPressed("inventory_selection_next"))
            {
                SwitchingInventorySelection();
            }
            
            if (Input.IsActionJustPressed("inventory_make_active"))
            {
                SwitchActiveItem();
            }
            
        }

        private void SwitchActiveItem()
        {
            if (selectedItemIndex > inventory.Count - 1)
            {
                SwapActiveWithEmptySlot();
                return;
            }

            var nextActive = inventory[selectedItemIndex];
            inventory.Remove(nextActive);
            
            if (activeItem != null)
            {
                inventory.Add(activeItem);
            }

            activeItem = nextActive;
            ActiveItemChanged?.Invoke(activeItem);
            InventoryChanged?.Invoke(inventory);
        }

        private void SwapActiveWithEmptySlot()
        {
            if (activeItem == null) return;
            
            inventory.Add(activeItem);
            activeItem = null;
            ActiveItemChanged?.Invoke(activeItem);
            InventoryChanged?.Invoke(inventory);
        }
        
        private void SwitchingInventorySelection()
        {
            selectedItemIndex = (selectedItemIndex + 1) % MaxInventorySize;
            SelectedItemChanged?.Invoke(selectedItemIndex);
        }

        private void TryPickUp()
        {
            var item = ClosestItemInReach();
            if (item is null)
            {
                return;
            }
            
            item.Disable();
            inventory.Add(item);
            InventoryChanged?.Invoke(inventory);
            if (itemsInReach.Contains(item))
            {
                itemsInReach.Remove(item);
            }
        }

        private Pickupable ClosestItemInReach()
        {
            var distance = float.PositiveInfinity;
            Pickupable toPickup = null;
            foreach (var item in itemsInReach)
            {
                if (item.GlobalPosition.DistanceTo(GlobalPosition) >= distance)
                {
                    continue;
                }
                
                distance = item.GlobalPosition.DistanceTo(GlobalPosition);
                toPickup = item;
            }

            return toPickup;
        }

        public void Disable()
        {
            SetProcess(false);
            SetProcessInput(false);
            SetPhysicsProcess(false);
        }

        public void Enable()
        {
            SetProcess(true);
            SetProcessInput(true);
            SetPhysicsProcess(true);
            UiManager.Instance.InventoryHud.NotifyNewActivePlayerPickUp(this);
        }

        public bool IsEnabled => IsProcessing() && IsProcessingInput() && IsPhysicsProcessing();

        [Export] public bool DisplayDebug { get; set; }

        public void DisplayDebugInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Inventory:");
            foreach (var item in inventory)
            {
                sb.AppendLine($"{item.Name}");
            }
            sb.AppendFormat("Active: {0}", activeItem?.Name ?? "None");
            
            debugInventoryLabel.Text = sb.ToString();
        }
    }
}