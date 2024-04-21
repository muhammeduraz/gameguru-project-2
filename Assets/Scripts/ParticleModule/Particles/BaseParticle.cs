using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.ParticleModule.Particles
{
    public abstract class BaseParticle : MonoBehaviour
    {
        #region Events

        public Action<BaseParticle> OnTerminated;

        #endregion Events

        #region Variables

        private WaitForSeconds _waitForSeconds = new WaitForSeconds(1.0f);

        [Header("Components")]
        [SerializeField] protected ParticleSystem particle;

        #endregion Variables

        #region Functions

        protected void Activate()
        {
            gameObject.SetActive(true);
        }

        protected void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Play()
        {
            if (particle.isPlaying) return;

            Activate();
            particle.Play();
            StartCoroutine(TerminateCheckCoroutine());
        }

        public void Stop(bool withChildren, ParticleSystemStopBehavior stopBehavior)
        {
            StopCoroutine(TerminateCheckCoroutine());
            particle.Stop(withChildren, stopBehavior);
            Deactivate();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private IEnumerator TerminateCheckCoroutine()
        {
            while (particle.isPlaying)
                yield return _waitForSeconds;

            OnTerminated?.Invoke(this);
            Deactivate();
        }

        #endregion Functions
    }
}