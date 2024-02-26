using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soundmanager : MonoBehaviour
{

    public GameObject menu_button;
    public GameObject Sound_button;
  
    void Start()
    {
        Sound_button.SetActive(false);
        menu_button.SetActive(true);
    }
    
    public void sound_button(){
      
        Sound_button.SetActive(true);
        menu_button.SetActive(false);

    }

    public void Ok_button(){
      
        Sound_button.SetActive(false);
        menu_button.SetActive(true);

    }


}
