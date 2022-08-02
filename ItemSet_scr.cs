using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet_scr : MonoBehaviour
{
    [SerializeField] GameObject flashLightBox;
    [SerializeField] GameObject keySearcherBox;
    [SerializeField] GameObject[] batteryBox = new GameObject[3];

    [SerializeField] GameObject flashLight;
    [SerializeField] GameObject keySercher;
    [SerializeField] GameObject battery;

    void Start()
    {
        if(PlayerPrefs.GetInt("FLASHLIGHT") == 1){
            var obj = Instantiate(flashLight, flashLightBox.transform.position, Quaternion.identity);
            obj.name = "強力な懐中電灯";
        }  
        if(PlayerPrefs.GetInt("KEYSEARCHER") == 1){
            var obj = Instantiate(keySercher, keySearcherBox.transform.position, Quaternion.identity);
            obj.name = "鍵探知機";
        }

        if(PlayerPrefs.GetInt("BATTERY") > 0){
            for(int i = 0; i < PlayerPrefs.GetInt("BATTERY"); i++){
                var obj = Instantiate(battery, batteryBox[i].transform.position, Quaternion.identity);
                obj.name = "電池";
            }
        } 
    }
}
