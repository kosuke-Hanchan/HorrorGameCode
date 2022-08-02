using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu_scr : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] Button closeButton;
    [SerializeField] Button stageSelect_1;

    private GameObject mainCam;

    void Start()
    {
        startMenu.SetActive(false);

        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger1 = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventDate) => {OnClickStartMenu();});
        trigger1.triggers.Add(entry1);

        closeButton.onClick.AddListener(OnClickCloseButton);
        stageSelect_1.onClick.AddListener(OnStage1Button);
    }


    void Update()
    {
        
    }

    private void OnClickStartMenu(){
        if(!mainCam.GetComponent<MainSceneCam_scr>().menu){
            mainCam.GetComponent<MainSceneCam_scr>().menu = true;
            startMenu.SetActive(true);
        }
    }

    private void OnClickCloseButton(){
        mainCam.GetComponent<MainSceneCam_scr>().menu = false;
        mainCam.GetComponent<MainSceneCam_scr>().cursorLock = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        startMenu.SetActive(false);
    }

    private void OnStage1Button(){
        SceneManager.LoadScene("DemoScene");
        PlayerPrefs.SetInt("GAME",0);
        PlayerPrefs.SetInt("GET_MONEY",0);
        PlayerPrefs.Save ();
    }
}
