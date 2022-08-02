using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoorRotation_scr : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float openSpeed = 5f;
    private bool handReach;
    private bool handAct;

    [SerializeField] float maxAng = 90f;
    [SerializeField] float minAng = 0f;

    [SerializeField]AudioSource source;
    [SerializeField]AudioClip openClip;
    [SerializeField]AudioClip closeClip;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("LongTarget");
        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry1.callback.AddListener((eventDate) => { DownPointerDoor(); });
        entry2.eventID = EventTriggerType.PointerUp;
        entry2.callback.AddListener((eventDate) => { UpPointerDoor(); });
        trigger.triggers.Add(entry1);
        trigger.triggers.Add(entry2);
    }



    void Update()
    {
        //0～360度から-180～180度へ
        float doorAngle_y = transform.localEulerAngles.y;
        if(doorAngle_y > 180){
            doorAngle_y = doorAngle_y - 360;
        }

        if(handAct){   
            var direction = this.transform.parent.transform.InverseTransformPoint(target.transform.position) - transform.localPosition;
            direction.y = 0;
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            //0～360度から-180～180度へ
            float lookRotation_y = lookRotation.eulerAngles.y;
            if(lookRotation_y > 180){ 
                lookRotation_y = lookRotation_y - 360;
            }

            //扉開閉処理
            if(doorAngle_y <= maxAng && doorAngle_y >= minAng){
                transform.localRotation = Quaternion.Lerp(transform.localRotation, lookRotation, openSpeed * Time.deltaTime);
            }

            else if(lookRotation_y <= maxAng && lookRotation_y >= minAng){
                transform.localRotation = Quaternion.Lerp(transform.localRotation, lookRotation, openSpeed * Time.deltaTime);
            }
        }

        //制限の角度を超えてしまった時の処理
        else{
            if(doorAngle_y < minAng){
                transform.localEulerAngles = new Vector3(0f, minAng, 0f);
            }

            else if(doorAngle_y > maxAng){
                transform.localEulerAngles = new Vector3(0f, maxAng, 0f);
            }
        }
    }


//---------------------扉をクリックしているか判定-------------------------------------------------------------
    public void DownPointerDoor(){
        if(handReach){
            handAct = true;
        }
        else{}
    }

    public void UpPointerDoor(){
        handAct = false;
    }


//----------------------------プレイヤーの手が届くか判定と効果音---------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Ghost_Hand"のとき
        if(other.gameObject.tag == "PlayerHandReach"){
            handReach = true;
        }

        if(other.gameObject.tag == "DoorSETrigger"){
            source.PlayOneShot(closeClip);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerHandReach"){
            handReach = false;
            handAct = false;
        }
        
        if(other.gameObject.tag == "DoorSETrigger"){
            source.PlayOneShot(openClip);
        }
    }

}
