using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Pixel
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private UIPopup _popup;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _popup.Show();
        }
    }
}