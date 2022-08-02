using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSet_scr : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    private GameObject obj;
    [SerializeField] GameObject[] ghostBox;

    private List<int> num = new List<int>();

    private int index;
    private int ran;
    
    private float setTime;
    [SerializeField] float minSetTime = 40f;
    [SerializeField] float maxSetTime = 100f;
    private float destroyTime;
    private bool ghostSet;
    private float repeatTime = 0.1f;
    public float lineWidth;
    public float bleedingIntensity;

    private GameObject UICamera;

    
    void Start()
    {
        lineWidth = 0f;
        bleedingIntensity = 0f;
        setTime = Random.Range(minSetTime, maxSetTime);
        ghostBox = GameObject.FindGameObjectsWithTag("GhostWayPoiints"); //keyBoxタグを全てリストに格納

        UICamera = GameObject.FindWithTag("UICamera");
        UICamera.GetComponent<ShaderEffect_BleedingColors>().enabled = false;
        UICamera.GetComponent<ShaderEffect_CRT>().enabled = false;
    }

    void Update()
    {
        setTime -= Time.deltaTime;

        if(setTime <= 3){
            UICamera.GetComponent<ShaderEffect_CRT>().enabled = true;
            repeatTime -= Time.deltaTime;

            if(setTime <= 0 && !ghostSet){
                UICamera.GetComponent<ShaderEffect_BleedingColors>().enabled = true;
                ran = Random.Range(0, ghostBox.Length);
                obj = Instantiate(ghost, ghostBox[ran].transform.position, Quaternion.identity);
                LightControl_scr.mainLight_ctl = false;
                destroyTime = Random.Range(10f, 30f);
                ghostSet = true;
            }

            if(repeatTime <= 0){
                lineWidth = Random.Range(1f, 100f);
                if(setTime <= 0){
                    bleedingIntensity = Random.Range(-10f, 0f);
                }
                repeatTime = 0.01f;
            }
        }

        if(ghostSet){
            destroyTime -= Time.deltaTime;
            if(destroyTime <= 0){
                UICamera.GetComponent<ShaderEffect_BleedingColors>().enabled = false;
                UICamera.GetComponent<ShaderEffect_CRT>().enabled = false;
                Destroy(obj);
                setTime = Random.Range(minSetTime, maxSetTime);
                ghostSet = false;
            }
        }
    }
}
