using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager_scr : MonoBehaviour
{

    private int situation;

    [SerializeField] GameObject clearObject;
    [SerializeField] GameObject overObject;

    void Start()
    {
        situation = PlayerPrefs.GetInt("GAME");

        if(situation == 1){
            clearObject.SetActive(true);
        }
        else if(situation == 2){
            overObject.SetActive(true);
        }
        else{
            clearObject.SetActive(false);
            overObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
