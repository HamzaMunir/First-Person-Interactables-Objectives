using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zong.Test.Environment;

namespace Zong.Test.Gameplay
{
    public class SpherePickup : Pickup
    {
        public override void PickedUp(Player player)
        {
            SphereItem sphere = (SphereItem) _item;
            player.Pickup(sphere);
            HideCanvas();
        }
        
    }
}