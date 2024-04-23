using System;
using Zenject;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.CanvasModule
{
    public abstract class BasePanel : MonoBehaviour, IPanel, IInitializable, IDisposable
    {
        #region Variables

        private Tween _fadeTween;

        [Header("Base Components")]
        [SerializeField] protected CanvasGroup canvasGroup;

        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public virtual void Initialize()
        {
            canvasGroup.alpha = 0.0f;
        }

        public virtual void Dispose()
        {
            canvasGroup = null;
        }

        public void Appear()
        {
            OnAppear();
            Fade(1f);
        }

        public void Disappear()
        {
            OnDisappear();
            Fade(0f);
        }

        protected void Fade(float value, float duration = 0.25f, Ease ease = Ease.OutSine)
        {
            _fadeTween?.Kill();
            _fadeTween = canvasGroup.DOFade(value, duration).SetEase(ease);
        }

        protected abstract void OnAppear();
        protected abstract void OnDisappear();

        #endregion Functions
    }
}