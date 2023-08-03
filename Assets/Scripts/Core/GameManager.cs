using System.Collections.Generic;
using System.Threading;
using Core;
using Cysharp.Threading.Tasks.Linq;
using Game;
using Kit;
using UnityEngine;
using Zong.Test.Data;
using Zong.Test.Gameplay;
using Zong.Test.Views;

namespace Zong.Test.Core
{
    public class GameManager : Singleton<GameManager>
    {
        #region Public Fields

        [SerializeField] public Transform SpawnPoint;
        public UiManager UiManager;

        public AssetManager AssetManager => _assetManager;
        public DataManager DataManager => _dataManager;
        public EffectsManager EffectsManager => _effectsManager;

        public Vector3 PlayerLastCheckPoint => _checkPoint;

        public AudioClip ObjectivePlaced;
        public AudioClip ObjectiveDenied;

        public ParticleSystem ObjectivePlacedVfx;

        #endregion

        #region PrivateFields

        [SerializeField] private Player _player;
        [SerializeField] private AssetManager _assetManager;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private EffectsManager _effectsManager;
        private Pickup _lastHoveredGameObject;
        private Vector3 _checkPoint;
        private readonly CancellationTokenSource _cancelSource = new CancellationTokenSource();
        private readonly List<Pickup> _pickups = new();

        #endregion

        protected override void Awake()
        {
            base.Awake();
            MessageBroker.Instance.Receive<ItemPickedMessage>()
                .Subscribe(data => OnPlayerPickedItem(data), _cancelSource.Token);
            MessageBroker.Instance.Receive<ItemPlaced>()
                .Subscribe(data => OnItemPlacedOnObjective(data), _cancelSource.Token);
        }

        public void RegisterPickup(Pickup pickup)
        {
            _pickups.Add(pickup);
        }

        #region Event Callbacks

        private void OnGameRestart()
        {
            _player.Reset();
            _player.ActivateInput();
            foreach (var pickup in _pickups)
            {
                pickup.Spawn();
            }
        }
        
        private void OnPlayerPickedItem(ItemPickedMessage data)
        {
            data.Player.AddToPickedItems(data.Item);
            data.Player.DeactivateInput();
            UiManager.ShowMainUI(new MainUiData()
            {
                Categories = DataManager.ItemCategories,
                Player = data.Player
            });
            _checkPoint = data.Player.transform.position;
        }

        private void OnItemPlacedOnObjective(ItemPlaced data)
        {
            if (data.IsObjectPlaced)
            {
                AudioManager.Instance.PlayGameSound(ObjectivePlaced);
                _effectsManager.SpawnEffect(ObjectivePlacedVfx, data.Item.transform.position);
                UiManager.ShowObjectiveUi(new ObjectiveUiData()
                    { ObjectiveName = data.Objective.Name, OnRestart = OnGameRestart });
            }
            else
            {
                AudioManager.Instance.PlayGameSound(ObjectiveDenied);
                data.Player.MoveToPosition(_checkPoint);
                Destroy(data.Item.gameObject);
                UiManager.ShowMainUI(new MainUiData()
                {
                    Categories = DataManager.ItemCategories,
                    Player = data.Player
                });
            }

            data.Player.AnimateToIdle();

            _player.DeactivateInput();
        }

        #endregion

        public Item SpawnItem(string name)
        {
            var itemPrefab = AssetManager.GetPrefab(name);
            if (itemPrefab != null)
            {
                var itemObject = Instantiate(itemPrefab);
                return itemObject.GetComponent<Item>();
            }

            return null;
        }
    }
}