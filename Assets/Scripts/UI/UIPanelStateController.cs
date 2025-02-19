using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanelStateController : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private float _showDuration = 0.5f;
        private float _hideDuration = 0.05f;

        public bool IsActive { get; set; }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            var startValue = 0f;
            var endValue = 1f;
            IsActive = true;
            DOTween.To(() => startValue, x => startValue = x, endValue, _showDuration)
                .OnUpdate(() =>
                {
                    _canvasGroup.alpha = startValue; 
                })
                .OnComplete(() =>
                {
                   
                    _canvasGroup.blocksRaycasts = true;
                    _canvasGroup.interactable = true;
                });
        }

        public void Hide()
        {
            var startValue = 1f;
            var endValue = 0f;
            IsActive = false;
            DOTween.To(() => startValue, x => startValue = x, endValue, _hideDuration)
               .OnUpdate(() =>
               {
                   _canvasGroup.alpha = startValue;
               })
               .OnComplete(() =>
               {
                   _canvasGroup.blocksRaycasts = false;
                   _canvasGroup.interactable = false;
               });
        }
    }
}
