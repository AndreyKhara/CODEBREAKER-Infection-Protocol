using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    private PlayerInput playerInput;
    private InputAction pauseAction;

    void Awake()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        if (playerInput != null)
            pauseAction = playerInput.actions["Pause"];
    }

    void Start()
    {
        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInput>();
            if (playerInput != null)
                pauseAction = playerInput.actions["Pause"];
        }
    }

    void OnEnable()
    {
        if (pauseAction != null)
            pauseAction.performed += OnPause;
    }

    void OnDisable()
    {
        if (pauseAction != null)
            pauseAction.performed -= OnPause;
    }

    void OnDestroy()
    {
        if (pauseAction != null)
            pauseAction.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused) Resume();
        else Pause();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerInput.SwitchCurrentActionMap("UI");
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        // ОБЯЗАТЕЛЬНО сбрасываем паузу, чтобы не висели подписки
        OnDestroy();

        SceneManager.LoadScene("MainMenu");
    }
}
