using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLightSwitch_scr : MonoBehaviour
{
    [SerializeField]GameObject swObj;
    private Animator anim;

    [SerializeField] int _lightLimit = 6;
    public int lightLimit = 0;
    [SerializeField] float timeLimit = 10f;
    private float beginTime;

    private bool _mainLight_ctl;
    private bool player_hand = false;
    private bool oneShot = true; 
    
    [SerializeField] AudioSource audioSouce;
    [SerializeField] AudioClip switchClip;

    void Start()
    {
        lightLimit = 0;
        beginTime = timeLimit;
        anim = swObj.GetComponent<Animator>();
    }

    void Update(){
        if(!(_mainLight_ctl == LightControl_scr.mainLight_ctl)){
            oneShot = true;
        }

        if(oneShot){
            if(!LightControl_scr.mainLight_ctl){
                anim.SetBool("switch", false);
            }

            else{
                anim.SetBool("switch", true);
            }
            oneShot = false;
        }
        
        _mainLight_ctl = LightControl_scr.mainLight_ctl;
        if(lightLimit >= _lightLimit && LightControl_scr.mainLight_ctl){
            beginTime -= Time.deltaTime;
            if(beginTime < 0){
                LightControl_scr.mainLight_ctl = false;
                beginTime = timeLimit;
            }
        }
    }

    //-----------------ブレーカー------------------------------------------------
    public void LightMainSwitchClicked(){
        if(player_hand){
            if(LightControl_scr.mainLight_ctl){
                LightControl_scr.mainLight_ctl = false;
            }
            else{
                LightControl_scr.mainLight_ctl = true;
            }
            audioSouce.PlayOneShot(switchClip,0.3f);
        }
    }


    //----------------------------手が届くか判定---------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //接触したオブジェクトのタグが"Ghost_Hand"または"Human_Hand"のとき
        if(other.gameObject.tag == "PlayerHandReach"){
            player_hand = true;
        }   
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "PlayerHandReach"){
            player_hand = false;
        }   
    }
}
