using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController_scr : MonoBehaviour
{
    [SerializeField] GameObject playerHand;
    [SerializeField] GameObject target;
    [SerializeField] GameObject handObj;

    [SerializeField] GameObject[] itemList = new GameObject[3];
    private int itemNum = 4;

    private int layerId_1;
    private int layerId_2;

    [SerializeField] GameObject cam;
    [SerializeField] LayerMask mask;
    [SerializeField] float rayGetDis = 2.5f;
    [SerializeField] float rayDropDis = 1.5f;
    
    private GameObject gameManager;
    [SerializeField] Text nameText;
    [SerializeField] Image[] icon;
    [SerializeField] Sprite[] itemImage = new Sprite[6];
    [SerializeField] Image[] itemSlot = new Image[3];
    [SerializeField] Image slotFrame;


    [SerializeField]AudioSource source;
    [SerializeField]AudioClip buttonClip;
    [SerializeField]AudioClip keyClip;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        layerId_1 = LayerMask.NameToLayer("hasItems");
        layerId_2 = LayerMask.NameToLayer("Items");
        nameText.text = null;

        for(int i = 0; i < itemList.Length; i++){
            icon[i].GetComponent<Image>().enabled = false;
        }

        slotFrame.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position,cam.transform.forward);
        Debug.DrawRay(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.E)){
            if (Physics.Raycast(ray, out hit, rayGetDis, mask)){
                if(hit.collider.gameObject.layer == layerId_2){              
                    //バッテリーだった場合
                    if(hit.collider.tag == "Battery"){
                        if(CameraBattery_scr.subBattery < 2){
                            CameraBattery_scr.subBattery++;
                            Destroy(hit.collider.gameObject);
                            source.PlayOneShot(buttonClip,0.5f);
                        }
                    }

                    //手に持つアイテムだった場合
                    else if(hit.collider.tag != "NotRaycast"){
                        for(int i = 0; i <= itemList.Length-1; i++){
                            if(itemList[i] == null){
                                itemList[i] = hit.collider.gameObject;
                                itemList[i].transform.parent = playerHand.transform;
                                itemList[i].transform.localPosition = new Vector3(0,0,0);
                                itemList[i].transform.localEulerAngles = new Vector3(0,0,0);
                                itemList[i].GetComponent<Rigidbody>().isKinematic = true;
                                itemList[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                                
                                if(i == itemNum){
                                    itemList[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                                }
                                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                                hit.collider.gameObject.SetLayerRecursively(layerId_1);
                                break;
                            }
                        }
                        source.PlayOneShot(buttonClip,0.5f);
                    }
                }

                //鍵の掛かったドアの場合
                else if(hit.collider.tag == "KeyDoor" && itemNum <= itemList.Length-1){
                    if(!(itemList[itemNum] == null)){
                        if(itemList[itemNum].tag =="Key"){
                            Destroy(itemList[itemNum]);
                            itemList[itemNum] = null;
                            gameManager.GetComponent<GameManager_scr>().keyCount += 1;
                            source.PlayOneShot(keyClip,0.5f);
                        }
                    }
                }
            } 
        }

        //---------------------アイテム投げ処理------------------------------
        else if(Input.GetKeyDown(KeyCode.Q) && itemNum <= itemList.Length-1){
            if(!(itemList[itemNum] == null)){
                Vector3 pos;
                pos = target.transform.position;
                if (Physics.Raycast(ray, out hit, rayDropDis, mask)){
                    pos = hit.point;
                    pos.y += 0.1f;
                }
                itemList[itemNum].SetLayerRecursively( layerId_2 );
                itemList[itemNum].GetComponent<BoxCollider>().enabled = true;
                itemList[itemNum].GetComponent<Rigidbody>().isKinematic = false;
                itemList[itemNum].transform.position = pos;
                itemList[itemNum].transform.parent = null;
                itemList[itemNum] = null;
            }
        }

        nameText.text = null;
        if(Physics.Raycast(ray, out hit, rayGetDis, mask)){
            if(hit.collider.gameObject.layer == layerId_2){
                nameText.text = hit.collider.gameObject.name;
            }
        }
        
//---------------------マウスホイールで変更する処理-------------------
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        int _itemNum = itemNum;
        if (wheel > 0){
            itemNum -=1; 
            if(itemNum <= -1){
                itemNum = itemList.Length-1;
            }
        } 

        else if (wheel < 0){
            itemNum +=1; 
            if(itemNum >= itemList.Length){
                itemNum = 0;
            }
        }

//---------------------ナンバーキーで変更する処理-------------------
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            itemNum = 0;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            itemNum = 1;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            itemNum = 2;
        }

        else if(Input.GetKeyDown(KeyCode.Alpha4)){
            itemNum = 3;
        }

//---------------------表示切替処理-----------------------------
        if(!(_itemNum == itemNum)){ //OneShot
            if(itemNum < itemList.Length){
                slotFrame.GetComponent<Image>().enabled = true;
                slotFrame.transform.position = itemSlot[itemNum].transform.position;
                for(int i = 0; i < itemList.Length; i++){
                    if(!(itemList[i] == null)){
                        itemList[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                        if(i == itemNum){
                            itemList[itemNum].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
            else{
                for(int i = 0; i < itemList.Length; i++){
                    if(!(itemList[i] == null)){
                        itemList[i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    slotFrame.GetComponent<Image>().enabled = false;
                }
            }
        }
        for(int i = 0; i < itemList.Length; i++){
            if(!(itemList[i] == null)){
                icon[i].GetComponent<Image>().enabled = true;
                if(itemList[i].tag == "Key"){
                    icon[i].sprite = itemImage[0];
                }
                else if(itemList[i].tag == "FlashLight"){
                    icon[i].sprite = itemImage[1];
                }
                else if(itemList[i].tag == "Treasur1"){
                    icon[i].sprite = itemImage[2];
                }
                else if(itemList[i].tag == "Treasur2"){
                    icon[i].sprite = itemImage[3];
                }
                else if(itemList[i].tag == "Treasur3"){
                    icon[i].sprite = itemImage[4];
                }
                else if(itemList[i].tag == "KeySearcher"){
                    icon[i].sprite = itemImage[5];
                }
            }
            else{
                icon[i].GetComponent<Image>().enabled = false;
            }
        }
    }
}
