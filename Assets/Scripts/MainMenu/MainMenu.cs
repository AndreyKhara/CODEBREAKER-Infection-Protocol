using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // замените на вашу игровую сцену
    }

    public void OpenSettings()
    {
        // пока можно оставить пустым или загрузить сцену настроек
        Debug.Log("Открыты настройки");
    }

    public void ExitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }
}
