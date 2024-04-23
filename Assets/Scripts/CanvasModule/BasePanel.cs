using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Assets.Scripts.CanvasModule
{
    public abstract class BasePanel : MonoBehaviour, IPanel, IInitializable, IDisposable
    {
        #region Variables

        private Tween _fadeTween;

        [Header("Base Components")]
        [SerializeField] protected CanvasGroup canvasGroup;

        #endregion Variables

        #region Functions

        public virtual void Initialize()
        {
            canvasGroup.alpha = 0.0f;
        }

        public virtual void Dispose()
        {
            canvasGroup = null;
        }

        public async void Appear()
        {
            await Fade(1f).AsyncWaitForCompletion();
            await OnAppear();
        }

        public async void Disappear()
        {
            await Fade(0f).AsyncWaitForCompletion();
            OnDisappear();
        }

        protected Tween Fade(float value, float duration = 0.25f, Ease ease = Ease.OutSine)
        {
            _fadeTween?.Kill();
            _fadeTween = canvasGroup.DOFade(value, duration).SetEase(ease);
            return _fadeTween;
        }

        protected async virtual Task OnAppear() { }
        protected async virtual Task OnDisappear() { }

        #endregion Functions
    }
}