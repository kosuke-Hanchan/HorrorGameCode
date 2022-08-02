using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverMenu_scr : MonoBehaviour
{
    [SerializeField] Button closeButton;
    private GameObject mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        mainCam.GetComponent<MainSceneCam_scr>().menu = true;
        closeButton.onClick.AddListener(OnClickCloseButton);
        PlayerPrefs.SetInt ("GET_MONEY", 0);
        PlayerPrefs.Save ();
    }

    private void OnClickCloseButton(){
        mainCam.GetComponent<MainSceneCam_scr>().menu = false;
        mainCam.GetComponent<MainSceneCam_scr>().cursorLock = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.SetInt("GAME",0);
        PlayerPrefs.Save ();
        this.gameObject.SetActive(false);
    }
}
