using Cysharp.Threading.Tasks;
using DG.Tweening;
using MongrelsTeam.Interfaces;
using UnityEngine;

namespace MongrelsTeam.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected ScreenType _screenType;
        [SerializeField] protected RectTransform _root;
        [SerializeField] protected CanvasGroup _canvasGroup;

        public virtual async UniTask Setup()
        {
        }

        public virtual async UniTask Show(IWindowArgs args = null)
        {
            switch (_screenType)
            {
                case ScreenType.Popup:
                    _root.localScale = Vector3.zero;
                    gameObject.SetActive(true);
                    await _root.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
                    break;
                case ScreenType.Window:
                    await _canvasGroup.DOFade(0f, 0f);
                    gameObject.SetActive(true);
                    await _canvasGroup.DOFade(1f, 0.6f);
                    break;
            }
        }

        public virtual async UniTask Hide()
        {
            switch (_screenType)
            {
                case ScreenType.Popup:
                    await _root.DOScale(0f, 0.3f).SetEase(Ease.InBack);
                    break;
                case ScreenType.Window:
                    await _canvasGroup.DOFade(0f, 0.6f);
                    break;
            }
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _canvasGroup.DOKill();
            _root.DOKill();
        }
    }

    public enum ScreenType
    {
        Window,
        Popup
    }
}
