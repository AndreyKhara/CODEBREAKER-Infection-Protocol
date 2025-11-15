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
        [SerializeField] private TextMeshProUGUI _tutorialText;
        [SerializeField] private Button _closeButton;

        [Header("Tutorial Content")]
        [TextArea(3, 10)]
        [SerializeField] private string _tutorialMessage = 
            "Добро пожаловать в CODEBREAKER: Infection Protocol!\n\n" +
            "Управление:\n" +
            "WASD - Движение\n" +
            "Мышь - Осмотр\n" +
            "ЛКМ - Стрельба\n" +
            "Пробел - Прыжок\n\n" +
            "Пройдите к порталу, чтобы начать игру!";

        [Header("Settings")]
        [SerializeField] private bool _showOnStart = true;

        private bool _isVisible = false;
        private InputAction _closeAction;

        private void Awake()
        {
            // Инициализация Input Action для закрытия
            _closeAction = new InputAction("CloseUI", binding: "<Keyboard>/enter");
            _closeAction.Enable();
            
            // Проверка наличия всех необходимых компонентов
            if (_tutorialPanel == null)
            {
                Debug.LogError("TutorialManager: Tutorial Panel не назначен!");
                return;
            }

            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(CloseTutorial);
            }

            if (_tutorialText != null && !string.IsNullOrEmpty(_tutorialMessage))
            {
                _tutorialText.text = _tutorialMessage;
            }
        }

        private void Start()
        {
            if (_showOnStart)
            {
                ShowTutorial();
            }
            else
            {
                HideTutorialImmediate();
            }
        }

        private void Update()
        {
            // Закрытие по нажатию клавиши Escape
            if (_isVisible && _closeAction != null && _closeAction.triggered)
            {
                CloseTutorial();
            }
        }

        /// <summary>
        /// Показать панель туториала
        /// </summary>
        public void ShowTutorial()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(true);
                _isVisible = true;
                Time.timeScale = 0f; // Пауза игры во время туториала
            }
        }

        /// <summary>
        /// Закрыть панель туториала
        /// </summary>
        public void CloseTutorial()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(false);
                _isVisible = false;
                Time.timeScale = 1f; // Возобновление игры
            }
        }

        /// <summary>
        /// Скрыть панель без анимации (для старта)
        /// </summary>
        private void HideTutorialImmediate()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(false);
                _isVisible = false;
            }
        }

        /// <summary>
        /// Обновить текст туториала
        /// </summary>
        public void SetTutorialText(string newText)
        {
            if (_tutorialText != null)
            {
                _tutorialText.text = newText;
            }
        }

        private void OnDestroy()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(CloseTutorial);
            }
            
            // Освобождаем Input Action
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
