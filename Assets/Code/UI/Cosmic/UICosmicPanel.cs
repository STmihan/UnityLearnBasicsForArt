using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Cosmic
{
    public class UICosmicPanel : MonoBehaviour
    {
        [SerializeField] private Button _checkButton;
        [SerializeField] private Slider _shieldsSlider;
        [SerializeField] private Slider _powerSlider;
        [SerializeField] private float _speed;
        private const float MaxValue = 10f;
        
        private float _shields;
        private float _power;
        
        
        private void Start()
        {
            _checkButton.onClick.AddListener(OnCheckButtonClick);
            OnCheckButtonClick();
        }
        
        private void Update()
        {
            UpdateSliders();
        }

        private void OnCheckButtonClick()
        {
            _shields = Random.Range(0, MaxValue);
            _power = Random.Range(0, MaxValue);
        }

        private void UpdateSliders()
        {
            var shieldsPercentage = _shields / MaxValue;
            var powerPercentage = _power / MaxValue;
            
            _shieldsSlider.value = Mathf.Lerp(_shieldsSlider.value, shieldsPercentage, Time.deltaTime * _speed);
            _powerSlider.value = Mathf.Lerp(_powerSlider.value, powerPercentage, Time.deltaTime * _speed);
        }
    }
}