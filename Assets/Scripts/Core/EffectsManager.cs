using System.Threading;
using Cysharp.Threading.Tasks;
using Kit;
using UnityEngine;

namespace Core
{
    public class EffectsManager : MonoBehaviour
    {
        public void SpawnEffect(ParticleSystem particleSystem, Vector3 position)
        {
            var vfx = Instantiate(particleSystem);
            vfx.transform.position = position;
            vfx.Play();
            QueueForDestroy(vfx);
        }

        private void QueueForDestroy(ParticleSystem system)
        {
            if (system.main.loop)
                return;

            ControlHelper.DelayUntil(() => !system.IsAlive(true),
                () => Destroy(system),
                system.GetCancellationTokenOnDestroy());
        }
    }
}