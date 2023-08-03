using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Zong.Test.Gameplay
{
    public class Objective : MonoBehaviour, IObjective
    {
        public string Name;
        public Canvas WorldCanvas;
        public GameObject Holder;
        public bool CanHoldObject = true;

        [SerializeField] private Collider _collider;

        public void ShowCanvas()
        {
            WorldCanvas?.gameObject.SetActive(true);
        }

        public void HideCanvas()
        {
            WorldCanvas?.gameObject.SetActive(false);
        }

        public virtual void Execute(Item item, Player player)
        {
            if (CanHoldObject)
            {
                item.transform.parent = Holder.transform;
                item.transform.localPosition = Vector3.zero;
            }

            MessageBroker.Instance.Publish(new ItemPlaced()
                { Item = item, Objective = this, Player = player, IsObjectPlaced = CanHoldObject });
        }

        public Collider Collider => _collider;
    }
}