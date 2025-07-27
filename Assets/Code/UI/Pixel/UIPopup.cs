using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Pixel
{
    public class UIPopup : MonoBehaviour
    {
        private static readonly int HideTrigger = Animator.StringToHash("Hide");
        [SerializeField] private Button _closeButton;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _closeButton.onClick.AddListener(Hide);
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _animator.SetTrigger(HideTrigger);
        }

        private void OnHide()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}