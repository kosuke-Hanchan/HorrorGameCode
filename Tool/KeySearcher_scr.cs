using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySearcher_scr : MonoBehaviour
{
    private int layerId;
    private bool power = false;
    [SerializeField] AudioSource beepSource;
    [SerializeField] AudioClip beepSound;
    private float repeatTime = 0f;

    [SerializeField] AudioSource switchSource;
    [SerializeField]AudioClip switchClip;

    Material[] tmp;

    void Start()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        layerId = LayerMask.NameToLayer("hasItems");
        tmp = gameObject.GetComponent<Renderer>().materials;
        tmp[1].DisableKeyword("_EMISSION");
    }

    void Update()
    {
        if(this.gameObject.layer == layerId){
            if(Input.GetMouseButtonDown(1)){
                power = !power;
                this.gameObject.GetComponent<SphereCollider>().enabled = !this.gameObject.GetComponent<SphereCollider>().enabled;
                switchSource.PlayOneShot(switchClip,0.5f);
            }
        }
        else{
            power = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        if(!this.gameObject.GetComponent<MeshRenderer>().enabled){
            power = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }  

        if(power){
            tmp[1].EnableKeyword("_EMISSION");
        }
        else{
            tmp[1].DisableKeyword("_EMISSION");
        }
    }

    private void OnTriggerStay(Collider other){
        if(other.gameObject.tag == "Key"){
            if(power){
                repeatTime -= Time.deltaTime;
                if(repeatTime <= 0){
                    beepSource.PlayOneShot(beepSound);
                    repeatTime = 0.5f;
                    Debug.Log("KEY");
                }
            }
        }
    }
}
