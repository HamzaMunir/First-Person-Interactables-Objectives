using System.Threading;
using Cysharp.Threading.Tasks.Linq;
using Game;
using UnityEngine;
using Zong.Test.Gameplay;

namespace Zong.Test.Views
{
    public class HintsUiView : MonoBehaviour
    {
        public GameObject Content;
        public GameObject InteractHint;
        public GameObject DropHint;
        private readonly CancellationTokenSource _cancelSource = new CancellationTokenSource();
        public void Start()
        {
            MessageBroker.Instance.Receive<ItemPickedMessage>()
                .Subscribe(data => OnPlayerPickedItem(data), _cancelSource.Token);
            MessageBroker.Instance.Receive<ItemPlaced>()
                .Subscribe(data => OnItemPlacedOnObjective(data), _cancelSource.Token);
            MessageBroker.Instance.Receive<ItemEquippedMessage>()
                .Subscribe(data => EquipItem(data), _cancelSource.Token);
        }

        private void EquipItem(ItemEquippedMessage data)
        {
            Content.SetActive(true);
            DropHint.SetActive(true);
            InteractHint.SetActive(false);
        }

        private void OnItemPlacedOnObjective(ItemPlaced data)
        {
            Content.SetActive(true);
            DropHint.SetActive(false);
            InteractHint.SetActive(true);
        }

        private void OnPlayerPickedItem(ItemPickedMessage data)
        {
            Content.SetActive(false);
        }
    }
}