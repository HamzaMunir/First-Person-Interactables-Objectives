using System.Collections;
using System.Collections.Generic;
using Kit;
using UnityEngine;

namespace Zong.Test.Gameplay
{
    public enum ECategory
    {
        None,
        Weapons,
        Points,
        Instruments
    }
    public class Item : MonoBehaviour
    {
        public string ID => name;
        public string Name;
        public ECategory ECategory;

        public Pickup ItemPickup;
        public Player Owner { get; protected set; }
        public Vector3 OriginalScale { get; set; }

        protected new Transform transform;

        [SerializeField] private Collider _collider;

        [SerializeField] protected AudioClip _pickupAudio;
        [SerializeField] protected AudioClip _equipAudio;
        protected virtual void Awake()
        {
            transform = base.transform;
            OriginalScale = transform.localScale;
        }
        
        public virtual void Equip(Player player)
        {
            Attach(player);
            AudioManager.Instance.PlayGameSound(_equipAudio);
        }

        public virtual void Pick(Player player)
        {
            Attach(player);
            AudioManager.Instance.PlayGameSound(_pickupAudio);
        }
        
        protected virtual void Attach(Player player)
        {
            transform.parent = player.ItemHolder.transform;
            Owner = player;
            
        }

        public virtual void Detach()
        {
            transform.parent = null;
            Owner = null;
        }


        public Collider Collider => _collider;
        public virtual bool CanUse => true;
    }
}