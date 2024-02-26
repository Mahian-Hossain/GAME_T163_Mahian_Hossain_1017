using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanu : MonoBehaviour
{
   public GameObject Continue;
   public GameObject Pause;
   public GameObject Main_manu;

void Start(){
Continue.SetActive(false);
Main_manu.SetActive(false);
Pause.SetActive(true);

}
    public void PauseGame()
    {
        Time.timeScale = 0f; 
        Continue.SetActive(true);
        Main_manu.SetActive(true);
        Pause.SetActive(false);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        Pause.SetActive(true);
        Continue.SetActive(false);
        Main_manu.SetActive(false);
    }

    public void MainManu(){

     SceneManager.LoadScene(0); 

    }
}
