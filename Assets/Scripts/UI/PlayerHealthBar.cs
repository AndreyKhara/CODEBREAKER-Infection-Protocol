using UnityEngine;
using UnityEngine.UI;

namespace CDB.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private float _smoothSpeed = 5f;
        
        private float _targetFillAmount = 1f;
        private float _currentFillAmount = 1f;

        private void Update()
        {
            if (Mathf.Abs(_currentFillAmount - _targetFillAmount) > 0.001f)
            {
                _currentFillAmount = Mathf.Lerp(_currentFillAmount, _targetFillAmount, Time.deltaTime * _smoothSpeed);
                _healthBarFill.fillAmount = _currentFillAmount;
            }
        }

        public void UpdateHealth(float currentHealth, float maxHealth)
        {
            _targetFillAmount = Mathf.Clamp01(currentHealth / maxHealth);
        }
    }
}
