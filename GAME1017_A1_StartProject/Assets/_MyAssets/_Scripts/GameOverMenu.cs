using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene(1); 
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(0); 
    }
}
