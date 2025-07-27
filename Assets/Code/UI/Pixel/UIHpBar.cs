using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Pixel
{
    public class UIHpBar : MonoBehaviour
    {
        private static readonly int HitTrigger = Animator.StringToHash("Hit");
        
        [SerializeField] private Image _hpBarImage;
        [SerializeField] private Button _hitButton;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private Gradient _hpBarGradient;
        [SerializeField] private float _smoothSpeed = 10f;
        
        private const float MaxHp = 100f;
        private float _currentHp;
        
        private void Start()
        {
            _hitButton.onClick.AddListener(Hit);
            _currentHp = MaxHp;
        }

        private void Hit()
        {
            _animator.SetTrigger(HitTrigger);
            _currentHp -= 10f;
            if (_currentHp < 0f)
            {
                _currentHp = MaxHp;
            }
        }

        private void Update()
        {
            UpdateHpBar();
        }

        private void UpdateHpBar()
        {

            float hpPercentage = _currentHp / MaxHp;
            float currentFillAmount = Mathf.Lerp(_hpBarImage.fillAmount, hpPercentage, Time.deltaTime * _smoothSpeed);
            _hpBarImage.fillAmount = currentFillAmount;
            _hpBarImage.color = _hpBarGradient.Evaluate(currentFillAmount);
        }
    }
}