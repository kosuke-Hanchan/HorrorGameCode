using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopMenu_scr : MonoBehaviour
{

    [SerializeField] GameObject shopMenu;
    [SerializeField] Button closeButton;
    [SerializeField] Text moneyText;

    private GameObject mainCam;

    [SerializeField] Button buyButton1;
    [SerializeField] Text hasCount1;
    [SerializeField] Text costText1;
    [SerializeField] int itemCost1;

    [SerializeField] Button buyButton2;
    [SerializeField] Text hasCount2;
    [SerializeField] Text costText2;
    [SerializeField] int itemCost2;

    


    void Start()
    {
        shopMenu.SetActive(false);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger1 = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerClick;
        entry1.callback.AddListener((eventDate) => {OnClickShopMenu();});
        trigger1.triggers.Add(entry1);

        closeButton.onClick.AddListener(OnClickCloseButton);

        costText1.text = itemCost1 + " $";
        costText2.text = itemCost2 + " $";
        buyButton1.onClick.AddListener(OnClickButton1);
        buyButton2.onClick.AddListener(OnClickButton2);
    }


    void Update()
    {
        moneyText.text = "所持金 : " + PlayerPrefs.GetInt("TOTAL_MONEY") + " $";
        hasCount1.text = "所持数 : " + PlayerPrefs.GetInt("HAS_KEYSEARCHER");
        hasCount2.text = "所持数 : " + PlayerPrefs.GetInt("HAS_BATTERY");
    }

    private void OnClickShopMenu(){
        if(!mainCam.GetComponent<MainSceneCam_scr>().menu){
            mainCam.GetComponent<MainSceneCam_scr>().menu = true;
            shopMenu.SetActive(true);
        }
    }

    private void OnClickCloseButton(){
        mainCam.GetComponent<MainSceneCam_scr>().menu = false;
        mainCam.GetComponent<MainSceneCam_scr>().cursorLock = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        shopMenu.SetActive(false);
    }


    private void OnClickButton1(){
        if(PlayerPrefs.GetInt("TOTAL_MONEY") >= itemCost1){
            int i = PlayerPrefs.GetInt("HAS_KEYSEARCHER")+1;
            int j = PlayerPrefs.GetInt("TOTAL_MONEY") - itemCost1;

            PlayerPrefs.SetInt("HAS_KEYSEARCHER",i);
            PlayerPrefs.SetInt("TOTAL_MONEY", j);
            PlayerPrefs.Save();
        }
    }

    private void OnClickButton2(){
        if(PlayerPrefs.GetInt("TOTAL_MONEY") >= itemCost2){
            int i = PlayerPrefs.GetInt("HAS_BATTERY")+1;
            int j = PlayerPrefs.GetInt("TOTAL_MONEY") - itemCost2;

            PlayerPrefs.SetInt("HAS_BATTERY",i);
            PlayerPrefs.SetInt("TOTAL_MONEY",j);
            PlayerPrefs.Save();
        }
    }
}
