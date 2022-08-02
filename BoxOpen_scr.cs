using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxOpen_scr : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float OpenSpeed = 5f;
    [SerializeField] float maxDis = 0.5f;
    [SerializeField] float minDis = 0f;

    [SerializeField] bool handReach;
    [SerializeField] bool handAct;

    private Vector3 prevPos;
    private int itemsLayer;

    [SerializeField]AudioSource source;
    [SerializeField]AudioClip openClip;
    [SerializeField]AudioClip closeClip;
    
    void Start()
    {
        prevPos = this.transform.localPosition;
        target = GameObject.FindGameObjectWithTag("LongTarget");

        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerDown;
        entry1.callback.AddListener((eventDate) => { DownPointerBox(); });
        entry2.eventID = EventTriggerType.PointerUp;
        entry2.callback.AddListener((eventDate) => { UpPointerBox(); });
        trigger.triggers.Add(entry1);
        trigger.triggers.Add(entry2);

        itemsLayer = LayerMask.NameToLayer("Items");
    }


    void Update()
    {
        var boxPos = this.transform.localPosition.x - prevPos.x;
        if(handAct){  
            var targetPos = this.transform.parent.transform.InverseTransformPoint(target.transform.position);
            var direction = targetPos - prevPos;

            if(boxPos <= maxDis && boxPos >= minDis){
                this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(targetPos.x,transform.localPosition.y, transform.localPosition.z), OpenSpeed * Time.deltaTime); 
            }
            else if(direction.x <= maxDis && direction.x >= minDis){
                this.transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(targetPos.x,transform.localPosition.y, transform.localPosition.z), OpenSpeed * Time.deltaTime);
            }
        }
    }

    void OnCollisionEnter(Collision Items){
        if(Items.gameObject.layer == itemsLayer){
            Items.transform.SetParent(this.gameObject.transform);
        }
    }
    
    void OnCollisionExit(Collision Items){
        if(Items.gameObject.layer == itemsLayer){
            Items.transform.SetParent(null);
        }
    }

    //---------------------扉をクリックしているか判定-------------------------------------------------------------
    public void DownPointerBox(){
        if(handReach){
            handAct = true;
        }
        else{}
    }

    public void UpPointerBox(){
        handAct = false;
    }


//----------------------------プレイヤーの手が届くか判定---------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Ghost_Hand"のとき
        if(other.gameObject.tag == "PlayerHandReach"){
            handReach = true;
        }
        if(other.gameObject.tag == "BoxSETrigger"){
            source.PlayOneShot(closeClip,0.1f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerHandReach"){
            handReach = false;
            handAct = false;
        }   
        if(other.gameObject.tag == "BoxSETrigger"){
            source.PlayOneShot(openClip,0.1f);
        }
    }
}
