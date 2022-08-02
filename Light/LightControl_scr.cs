using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightControl_scr : MonoBehaviour
{   

    [SerializeField] GameObject[] Lights = new GameObject[1];

    //電気のスイッチの変数
    public bool Light_ctl = false;
    private bool _mainLight_ctl;
    static public bool mainLight_ctl = true; 
    private bool player_hand = false;
    private bool oneShot = true;

    private Animator anim;
    [SerializeField]GameObject sw;

    private GameObject mainSwitch;

    [SerializeField] AudioSource audioSouce;
    [SerializeField] AudioClip switchClip;
    
    void Start()
    {   
        mainSwitch = GameObject.FindGameObjectWithTag("MainSwitch");
        anim = sw.GetComponent<Animator>();
        mainLight_ctl = true;
    }

    void Update()
    {
        if(_mainLight_ctl != mainLight_ctl){
            oneShot = true;
        }

        if(oneShot){
            if(mainLight_ctl){             //ブレーカーONの時
                if(Light_ctl){          //電気スイッチONの時
                    for(int i = 0; i < Lights.Length; i++){
                        Lights[i].GetComponent<Light>().enabled = true;
                        foreach(Transform child in Lights[i].transform.GetComponentsInChildren<Transform>().Skip(1))
                        {
                            child.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");                    
                        }
                    }
                }
                else{                          //電気スイッチOFFの時
                    for(int i = 0; i < Lights.Length; i++){
                        Lights[i].GetComponent<Light>().enabled = false;
                        foreach(Transform child in Lights[i].transform.GetComponentsInChildren<Transform>().Skip(1))
                        {
                            child.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");                    
                        }
                    }                      
                }    
            }
            else{                           //ブレーカーOFFの時
                for(int i = 0; i < Lights.Length; i++){
                    Lights[i].GetComponent<Light>().enabled = false;
                    foreach(Transform child in Lights[i].transform.GetComponentsInChildren<Transform>().Skip(1))
                    {   
                        child.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");                    
                    }
                }
                if(!(_mainLight_ctl == mainLight_ctl)){
                    Light_ctl = false;
                    mainSwitch.GetComponent<MainLightSwitch_scr>().lightLimit = 0;
                    anim.SetBool("switch", true);
                }  
            }
            oneShot = false;
        }
        _mainLight_ctl = mainLight_ctl;
    }

//----------------------------電気のスイッチ処理-------------------------------------------------------
    public void LightSubSwitchClicked()
    {
        if(player_hand){//プレイヤーが届くとき
            if(Light_ctl){
                anim.SetBool("switch", true);
                mainSwitch.GetComponent<MainLightSwitch_scr>().lightLimit -= 1;
                Light_ctl = false;
            }
            else{
                mainSwitch.GetComponent<MainLightSwitch_scr>().lightLimit += 1;
                anim.SetBool("switch", false);
                Light_ctl = true;
            }
            oneShot = true;
            audioSouce.PlayOneShot(switchClip,0.3f);
        }
    }
//---------------------------------プレイヤーの手が届くか判定---------------------------------------------------------------

    void OnTriggerEnter(Collider other)
    {
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
