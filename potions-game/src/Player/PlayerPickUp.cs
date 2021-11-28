using System;
using System.Collections.Generic;
using Godot;
using MonoCustomResourceRegistry;
using PotionsGame.Core.Extensions;
using PotionsGame.Core.Utils;
using PotionsGame.Items;

// ReSharper disable SuspiciousTypeConversion.Global
namespace PotionsGame.Player
{
    
    [RegisteredType(nameof(PlayerPickUp), baseType = nameof(Node2D))]
    public class PlayerPickUp: Node2D
    {
        private Area2D pickUpArea;
        private List<Pickupable> inventory = new List<Pickupable>();
        private List<Pickupable> itemsInReach = new List<Pickupable>();

        public override void _Ready()
        {
            pickUpArea = this.GetNodeByTypeOrNull<Area2D>();
            if (pickUpArea is null)
            {
                Logger.Instance.Error(this, $"{nameof(PlayerPickUp)} needs a {nameof(Area2D)} as child " +
                                            "but none was found!");
                throw new Exception("Required child not found!");
            }
            
            pickUpArea.Connect("body_entered", this, nameof(OnEnteredReach));
            pickUpArea.Connect("body_exited", this, nameof(OnExitedReach));
            
        }

        public void OnEnteredReach(Node body)
        {
            if (!(body is Pickupable pickupable)) return;
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
        }

        private void TryPickUp()
        {
            var item = ClosestItemInReach();
            item?.Disable();
            inventory.Add(item);
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
    }
}