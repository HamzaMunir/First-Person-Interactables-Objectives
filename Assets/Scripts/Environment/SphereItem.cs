using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zong.Test.Core;
using Zong.Test.Gameplay;

namespace Zong.Test.Environment
{
    public class SphereItem : Item
    {

        public Vector3 PositionOffset = Vector3.zero;
        public Vector3 RotationOffset = Vector3.zero;
        protected override void Attach(Player player)
        {
            base.Attach(player);
            
            transform.localScale = OriginalScale;
            transform.localPosition = PositionOffset;
            
        }

        public override void Pick(Player player)
        {
            base.Pick(player);
            MessageBroker.Instance.Publish(new ItemPickedMessage(){ Item = this, Player = player});
        }

        public override void Detach()
        {
            base.Detach();
            transform.localPosition = ItemPickup.transform.position;
        }
    }
}