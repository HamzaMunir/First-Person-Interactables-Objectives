using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Linq;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using Zong.Test.Core;

namespace Zong.Test.Gameplay
{
    public class Player : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private GameObject _playerCameraRef;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private GameObject _itemHolder;
        private static readonly Collider[] Colliders = new Collider[10];

        private Item _pickedItem;
        private Item _equippedItem;
        private Dictionary<ECategory, List<Item>> _pickedItems = new();
        private CancellationTokenSource _cancelSource = new CancellationTokenSource();
        private CharacterController _controller;

        [SerializeField] private Animator _leftHandAnimator;
        [SerializeField] private Animator _rightHandAnimator;
        private const string GRIP_ANIMATION_STATE = "Grip";

        #endregion

        #region Public Fields

        public GameObject PlayerCameraRef => _playerCameraRef;
        public GameObject ItemHolder => _itemHolder;
        public PlayerInput Input;
        public PlayerInputController InputController;
        public Dictionary<ECategory, List<Item>> PickedItems => _pickedItems;

        #endregion


        public void Start()
        {
            _controller = GetComponent<CharacterController>();
            MessageBroker.Instance.Receive<ItemEquippedMessage>()
                .Subscribe(data => EquipItem(data), _cancelSource.Token);
        }

        #region Gameplay

        public void PerformInteraction(bool value)
        {
            var interactable = GetNearestColliderObject<IInteractable>(_collider);
            if (interactable != null)
            {
                interactable.Interact(this);
            }
        }

        public void PerformObjective(bool value)
        {
            if (_equippedItem == null) return;
            var objective = GetNearestColliderObject<IObjective>(_equippedItem.Collider);

            if (objective != null)
            {
                objective.Execute(_equippedItem, this);
            }
        }

        public void Pickup(Item item)
        {
            item.Pick(this);
            _pickedItem = item;
            if (item.ECategory == ECategory.Instruments)
            {
                Destroy(item.gameObject);
            }
        }

        public void DropItem(bool value)
        {
            PerformObjective(value);
        }

        public void AddToPickedItems(Item item)
        {
            if (_pickedItems.TryGetValue(item.ECategory, out List<Item> items))
            {
                items.Add(item);
            }
            else
            {
                var newItems = new List<Item>();
                newItems.Add(item);
                _pickedItems.Add(item.ECategory, newItems);
            }
        }

        #endregion

        #region Event Callbacks

        private void EquipItem(ItemEquippedMessage data)
        {
            var item = GameManager.Instance.SpawnItem(data.Item.Name);
            item.Equip(data.Player);
            _equippedItem = item;
            ActivateInput();
            AnimateToGrip();
        }
        
        public void MoveToPosition(Vector3 position)
        {
            _controller.enabled = false;
            transform.position = position;
            _controller.enabled = true;
        }

        #endregion

        #region Animation

        public void AnimateToGrip()
        {
            _leftHandAnimator.SetFloat(GRIP_ANIMATION_STATE, 0.2f);
            _rightHandAnimator.SetFloat(GRIP_ANIMATION_STATE, 0.2f);
        }

        public void AnimateToIdle()
        {
            _leftHandAnimator.SetFloat(GRIP_ANIMATION_STATE, 0f);
            _rightHandAnimator.SetFloat(GRIP_ANIMATION_STATE, 0f);
        }

        #endregion

        #region Input

        public void DeactivateInput()
        {
            Input.DeactivateInput();
            InputController.SetCursorLock(false);
        }

        public void ActivateInput()
        {
            Input.ActivateInput();
            InputController.SetCursorLock(true);
        }

        #endregion

        public void Reset()
        {
            if (_equippedItem != null)
            {
                Destroy(_equippedItem.gameObject);
                _equippedItem = null;
            }

            _pickedItem = null;
            PickedItems.Clear();
            MoveToPosition(GameManager.Instance.SpawnPoint.position);
            AnimateToIdle();
        }

        #region Helper Methods

        private T GetNearestColliderObject<T>(Collider self)
        {
            int size = 0;
            var colliderType = self.GetType();
            if (colliderType == typeof(CapsuleCollider))
            {
                size = Physics.OverlapSphereNonAlloc(self.bounds.center, ((CapsuleCollider)self).radius, Colliders);
            }
            else if (colliderType == typeof(SphereCollider))
            {
                size = Physics.OverlapSphereNonAlloc(self.bounds.center, ((SphereCollider)self).radius, Colliders);
            }
            else
            {
                size = Physics.OverlapBoxNonAlloc(self.bounds.center, self.bounds.extents * 0.5f, Colliders);
            }

            T nearest = default(T);
            if (size <= 0)
            {
                return nearest;
            }

            float shortestDistance = float.PositiveInfinity;
            for (int i = 0; i < size; i++)
            {
                Collider collider = Colliders[i];
                T current = collider.GetComponent<T>();
                if (current == null)
                    continue;

                float distance = (transform.position - collider.transform.position).sqrMagnitude;
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearest = current;
                }
            }

            return nearest;
        }

        #endregion
    }
}