using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreparationMenu_scr : MonoBehaviour
{
    private GameObject mainCam;
    [SerializeField] GameObject preparationMenu;
    [SerializeField] Button closeButton;

    [SerializeField] Button upButton1;
    [SerializeField] Button downButton1;
    [SerializeField] Text countText1;
    [SerializeField] Text hasCount1;
    private int itemCount1;

    [SerializeField] Button upButton2;
    [SerializeField] Button downButton2;
    [SerializeField] Text countText2;
    [SerializeField] Text hasCount2;
    private int itemCount2;

    private int hasKeySearcherCount;
    private int hasBatteryCount;

    void Start()
    {
        preparationMenu.SetActive(false);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger1 = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventDate) => {OnClickPreparationMenu();});
        trigger1.triggers.Add(entry1);

        closeButton.onClick.AddListener(OnClickCloseButton);
        upButton1.onClick.AddListener(OnClickUpButton1);
        downButton1.onClick.AddListener(OnClickDownButton1);
        upButton2.onClick.AddListener(OnClickUpButton2);
        downButton2.onClick.AddListener(OnClickDownButton2);

        PlayerPrefs.SetInt("KEYSEARCHER", 0);
        PlayerPrefs.GetInt("BATTERY", 0);
        PlayerPrefs.Save();
    }

    void Update()
    {
        hasKeySearcherCount = PlayerPrefs.GetInt("HAS_KEYSEARCHER");
        hasBatteryCount = PlayerPrefs.GetInt("HAS_BATTERY");

        countText1.text = "× " + itemCount1;
        countText2.text = "× " + itemCount2;
        hasCount1.text = "所持数 : " + PlayerPrefs.GetInt("HAS_KEYSEARCHER");
        hasCount2.text = "所持数 : " + PlayerPrefs.GetInt("HAS_BATTERY");

        PlayerPrefs.SetInt("KEYSEARCHER", itemCount1);
        PlayerPrefs.SetInt("BATTERY", itemCount2);
        PlayerPrefs.Save();
    }


    private void OnClickPreparationMenu(){
        if(!mainCam.GetComponent<MainSceneCam_scr>().menu){
            mainCam.GetComponent<MainSceneCam_scr>().menu = true;
            preparationMenu.SetActive(true);
        }
    }

    private void OnClickCloseButton(){
        mainCam.GetComponent<MainSceneCam_scr>().menu = false;
        mainCam.GetComponent<MainSceneCam_scr>().cursorLock = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        preparationMenu.SetActive(false);
    }


    private void OnClickUpButton1(){
        if(PlayerPrefs.GetInt("HAS_KEYSEARCHER") > 0){
            if(itemCount1 < 1){
                itemCount1 += 1;
            }
        }
    }
    private void OnClickDownButton1(){
        if(itemCount1 > 0){
            itemCount1 -= 1;
        }
    }


    private void OnClickUpButton2(){
        if(PlayerPrefs.GetInt("HAS_BATTERY") > itemCount2){
            if(itemCount2 < 4){
                itemCount2 += 1;
            }
        }
    }
    private void OnClickDownButton2(){
        if(itemCount2 > 0){
            itemCount2 -= 1;
        }
    }
}
