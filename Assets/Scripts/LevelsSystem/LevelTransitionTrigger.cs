using UnityEngine;
using UnityEngine.SceneManagement;

namespace CDB.LevelsSystem
{
    /// <summary>
    /// Триггер для перехода со сцены Game на сцену Levels
    /// </summary>
    public class LevelTransitionTrigger : MonoBehaviour
    {
        [Header("Scene Settings")]
        [SerializeField] private string _targetSceneName = "Levels";
        
        [Header("Trigger Settings")]
        [SerializeField] private bool _requirePlayerTag = true;
        [SerializeField] private string _playerTag = "Player";
        
        [Header("Visual Feedback")]
        [SerializeField] private GameObject _visualEffect;
        [SerializeField] private Color _gizmoColor = Color.cyan;

        private bool _isTransitioning = false;

        private void OnTriggerEnter(Collider other)
        {
            // Проверяем, что это игрок и переход еще не начался
            if (_isTransitioning) return;

            bool isPlayer = !_requirePlayerTag || other.CompareTag(_playerTag);
            
            if (isPlayer)
            {
                Debug.Log($"Игрок достиг триггера перехода. Загрузка сцены: {_targetSceneName}");
                StartTransition();
            }
        }

        /// <summary>
        /// Начать переход на другую сцену
        /// </summary>
        private void StartTransition()
        {
            _isTransitioning = true;

            // Восстанавливаем time scale перед переходом
            Time.timeScale = 1f;

            // Активируем визуальный эффект если он есть
            if (_visualEffect != null)
            {
                _visualEffect.SetActive(true);
            }

            // Загружаем целевую сцену
            LoadTargetScene();
        }

        /// <summary>
        /// Загрузить целевую сцену
        /// </summary>
        private void LoadTargetScene()
        {
            if (!string.IsNullOrEmpty(_targetSceneName))
            {
                SceneManager.LoadScene(_targetSceneName);
            }
            else
            {
                Debug.LogError("LevelTransitionTrigger: Имя целевой сцены не указано!");
            }
        }

        /// <summary>
        /// Публичный метод для ручного запуска перехода (можно вызвать из других скриптов)
        /// </summary>
        public void TriggerTransition()
        {
            if (!_isTransitioning)
            {
                StartTransition();
            }
        }

        /// <summary>
        /// Установить целевую сцену
        /// </summary>
        public void SetTargetScene(string sceneName)
        {
            _targetSceneName = sceneName;
        }

        private void OnDrawGizmos()
        {
            // Визуализация триггера в редакторе
            Gizmos.color = _gizmoColor;
            
            Collider triggerCollider = GetComponent<Collider>();
            if (triggerCollider != null)
            {
                if (triggerCollider is BoxCollider box)
                {
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawWireCube(box.center, box.size);
                }
                else if (triggerCollider is SphereCollider sphere)
                {
                    Gizmos.DrawWireSphere(transform.position + sphere.center, sphere.radius);
                }
                else if (triggerCollider is CapsuleCollider capsule)
                {
                    // Упрощенная визуализация капсулы
                    Gizmos.DrawWireSphere(transform.position + capsule.center, capsule.radius);
                }
            }
            else
            {
                // Если нет коллайдера, показываем сферу по умолчанию
                Gizmos.DrawWireSphere(transform.position, 1f);
            }
        }
    }
}
