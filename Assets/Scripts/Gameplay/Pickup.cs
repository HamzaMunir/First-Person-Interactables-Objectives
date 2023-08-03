using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zong.Test.Core;

namespace Zong.Test.Gameplay
{
    public abstract class Pickup : MonoBehaviour, IInteractable
    {
        public Canvas WorldCanvas;
        
        protected Player _owner;
        private Rigidbody _rigidbody;

        public GameObject ItemPrefab;
        protected Item _item;

        public abstract void PickedUp(Player player);

        public void Start()
        {
            Spawn();
            GameManager.Instance.RegisterPickup(this);
        }

        public virtual void Spawn()
        {
            if (_item != null) return;
            var itemObject = Instantiate(ItemPrefab, this.transform);
            _item = itemObject.GetComponent<Item>();
            ShowCanvas();
        }
        private void Update()
        {
            var directionToCamera = (Vector3)transform.position - WorldCanvas.worldCamera.transform.position;
            var rotationToCamera = Quaternion.LookRotation(directionToCamera, Vector3.up);
            WorldCanvas.transform.rotation = rotationToCamera;
        }

        public virtual void Interact(Player player)
        {
            PickedUp(player);
        }

        public void ShowCanvas()
        {
            WorldCanvas?.gameObject.SetActive(true);
        }

        public void HideCanvas()
        {
            WorldCanvas?.gameObject.SetActive(false);
        }
    }
}