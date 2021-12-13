using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Managers;
using PotionsGame.Items;

namespace PotionsGame.Player.UI
{
    [RegisteredType(nameof(InventoryHud), baseType = nameof(Control))]
    public class InventoryHud : Control
    {
        private HBoxContainer stashContainer;
        private InventoryEntry activeItemSlot;
        [Export] private PackedScene inventoryEntryPrefab;
        private PlayerPickUp playerPickUp;
        private readonly List<InventoryEntry> inventoryEntries = new List<InventoryEntry>();
        

        public void NotifyNewActivePlayerPickUp(PlayerPickUp node)
        {
            if (playerPickUp != null)
            {
                playerPickUp.ActiveItemChanged -= OnActiveItemChanged;
                playerPickUp.SelectedItemChanged -= OnSelectedItemChanged;
                playerPickUp.InventoryChanged -= OnInventoryChanged;
            }
            
            playerPickUp = node;
            CreateInventorySlots();
            playerPickUp.ActiveItemChanged += OnActiveItemChanged;
            playerPickUp.SelectedItemChanged += OnSelectedItemChanged;
            playerPickUp.InventoryChanged += OnInventoryChanged;
        }

        private void CreateInventorySlots()
        {
            for (int i = 0; i < playerPickUp.MaxInventorySize; i++)
            {
                var entry = inventoryEntryPrefab.Instance<InventoryEntry>();
                stashContainer.AddChild(entry);
                inventoryEntries.Add(entry);
            }
            
            OnSelectedItemChanged(0);
        }

        private void OnInventoryChanged(List<Pickupable> inventory)
        {
            foreach (var entry in inventoryEntries)
            {
                entry.ItemIcon.Texture = null;
            }
            
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] != null)
                {
                    inventoryEntries[i].ItemIcon.Texture = inventory[i]?.SpriteTexture;
                }
            }
        }

        private void OnSelectedItemChanged(int index)
        {
            foreach (var entry in inventoryEntries)
            {
                entry.Deselect();
            }
            
            inventoryEntries[index].Select();
        }

        private void OnActiveItemChanged(Pickupable activeItem)
        {
            activeItemSlot.ItemIcon.Texture = activeItem?.SpriteTexture;
        }

        public override void _Ready()
        {
            stashContainer = GetNode<HBoxContainer>("Stash");
            activeItemSlot = GetNode<HBoxContainer>("ActiveItem").GetNodeByTypeOrNull<InventoryEntry>();

            ClearPlaceHolders();

            UiManager.Instance.InventoryHud = this;
        }

        private void ClearPlaceHolders()
        {
            stashContainer.DestroyAllChidlren();
        }
        
    }
}
