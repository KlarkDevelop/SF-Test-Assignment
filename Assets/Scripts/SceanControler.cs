using UnityEngine;
using UnityEngine.SceneManagement;

public class SceanControler : MonoBehaviour
{
    public void ReloadScean()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
