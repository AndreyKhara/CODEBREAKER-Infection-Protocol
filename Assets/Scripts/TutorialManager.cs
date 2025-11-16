using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

namespace CDB.Tutorial
{
    /// <summary>
    /// Управляет панелью туториала на сцене Game
    /// </summary>
    public class TutorialManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject _tutorialPanel;

        private InputAction _closeAction;

        private void Awake()
        {
            ShowTutorial();
            // Инициализация Input Action для закрытия
            _closeAction = new InputAction("CloseUI", binding: "<Keyboard>/enter");
            _closeAction.Enable();


            _closeAction.performed += CloseTutorial;
        }


        /// <summary>
        /// Показать панель туториала
        /// </summary>
        public void ShowTutorial()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(true);
                Time.timeScale = 0f; // Пауза игры во время туториала
            }
        }

        /// <summary>
        /// Закрыть панель туториала
        /// </summary>
        public void CloseTutorial(InputAction.CallbackContext context)
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(false);
                Time.timeScale = 1f; // Возобновление игры
            }
        }

        private void OnDestroy()
        {
            if (_closeAction != null)
            {
                _closeAction.Disable();
                _closeAction.Dispose();
            }
            // Убедимся, что time scale восстановлена
            Time.timeScale = 1f;
        }
    }
}
