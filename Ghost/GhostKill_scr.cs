using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostKill_scr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            PlayerPrefs.SetInt ("GAME", 2);
            PlayerPrefs.SetInt ("GET_MONEY", 0);

            
            PlayerPrefs.SetInt("HAS_FLASHLIGHT",PlayerPrefs.GetInt("HAS_FLASHLIGHT") - PlayerPrefs.GetInt("FLASHLIGHT"));
            PlayerPrefs.SetInt("HAS_KEYSEARCHER",PlayerPrefs.GetInt("HAS_KEYSEARCHER") - PlayerPrefs.GetInt("KEYSEARCHER"));
            PlayerPrefs.SetInt("HAS_BATTERY",PlayerPrefs.GetInt("HAS_BATTERY") - PlayerPrefs.GetInt("BATTERY"));
            PlayerPrefs.Save ();
            
            SceneManager.LoadScene("MainScene");
        } 
    }
}
