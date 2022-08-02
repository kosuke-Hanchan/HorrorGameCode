using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight_scr : MonoBehaviour
{
    private int layerId;
    private bool power;
    public bool fg;
    [SerializeField] GameObject lightObj;

    [SerializeField]AudioSource source;
    [SerializeField]AudioClip buttonClip;

    void Start()
    {
        fg = false;
        layerId = LayerMask.NameToLayer("hasItems");
        power = lightObj.GetComponent<Light>().enabled;
    }

    void Update()
    {
        if(this.gameObject.layer == layerId){
            if(Input.GetKeyDown(KeyCode.T)){
                power = !power;
                lightObj.GetComponent<Light>().enabled = power;
                source.PlayOneShot(buttonClip,0.5f);
            }

            if(this.gameObject.GetComponent<MeshRenderer>().enabled){
                if(Input.GetMouseButtonDown(1)){
                    power = !power;
                    lightObj.GetComponent<Light>().enabled = power;
                    source.PlayOneShot(buttonClip,0.5f); 
                }
            }
            
            if(power){
                fg = true;
            }
            else{
                fg = false;
            }
        }
    

        else{
            fg = false;
        }


    }
}
