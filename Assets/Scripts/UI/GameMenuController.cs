using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private SceanControler _sceanControler;

    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private GameObject pauseWindow;

    private void Awake()
    {
        Player.onDeath += ActiveMenu;
    }

    private void Update()
    {
        if (!gameOverWindow.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 0)
                    ContinueGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        pauseWindow.SetActive(false);
        Time.timeScale = 1;
    }

    private void ActiveMenu()
    {
        gameOverWindow.SetActive(true);
    }
}
