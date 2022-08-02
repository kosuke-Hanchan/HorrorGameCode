using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraBattery_scr : MonoBehaviour
{
    [SerializeField] Slider slider;
    public static int subBattery = 0;

    [SerializeField] Image sub1;
    [SerializeField] Image sub2;
    [SerializeField] Sprite batterySprite1;
    [SerializeField] Sprite batterySprite2;

    [SerializeField] GameObject batteryOffObj;
    
    void Start()
    {
        subBattery = 0;
    }


    void Update()
    {
        
        slider.value -= Time.deltaTime * 0.01f;
        if(slider.value <= 0){
            if(subBattery != 0){
                slider.value = 1;
                subBattery--;
            }
            else{
                batteryOffObj.SetActive(true);
                Invoke("ToGameOverScene",3f);
            }
        }


        if(subBattery == 0){
            sub1.sprite = batterySprite1;
            sub2.sprite = batterySprite1;
        }
        else if(subBattery == 1){
            sub1.sprite = batterySprite2;
            sub2.sprite = batterySprite1;
        }
        else{
            sub1.sprite = batterySprite2;
            sub2.sprite = batterySprite2;
        }
    }

    private void ToGameOverScene(){
        PlayerPrefs.SetInt ("GAME", 2);
        PlayerPrefs.SetInt ("GET_MONEY", 0);
        PlayerPrefs.SetInt("HAS_FLASHLIGHT",PlayerPrefs.GetInt("HAS_FLASHLIGHT") - PlayerPrefs.GetInt("FLASHLIGHT"));
        PlayerPrefs.SetInt("HAS_KEYSEARCHER",PlayerPrefs.GetInt("HAS_KEYSEARCHER") - PlayerPrefs.GetInt("KEYSEARCHER"));
        PlayerPrefs.SetInt("HAS_BATTERY",PlayerPrefs.GetInt("HAS_BATTERY") - PlayerPrefs.GetInt("BATTERY"));
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainScene");
    }
}
