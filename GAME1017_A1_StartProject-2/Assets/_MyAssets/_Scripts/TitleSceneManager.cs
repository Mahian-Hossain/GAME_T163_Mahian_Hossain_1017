using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
     
        Application.Quit();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
    }
}
