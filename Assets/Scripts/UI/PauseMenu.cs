using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuUI;
    private bool _isPaused = false;

    [Inject]
    private PlayerInput _playerInput;
    private InputAction _pauseAction;

    void Awake()
    {
        _pauseAction = _playerInput.actions["Pause"];
        _pauseAction.performed += OnPause;
    }


    private void OnEnable()
    {
        _pauseAction.Enable();
    }

    private void OnDisable()
    {
        _pauseAction.performed -= OnPause;
        _pauseAction.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (_isPaused) Resume();
        else Pause();
    }

    private void Pause()
    {
        _pauseMenuUI.SetActive(true);
        _playerInput.SwitchCurrentActionMap("UI");
        Time.timeScale = 0f;
        _isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        _playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1f;
        _isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        // ОБЯЗАТЕЛЬНО сбрасываем паузу, чтобы не висели подписки
        //OnDestroy();

        SceneManager.LoadScene("MainMenu");
    }
}
