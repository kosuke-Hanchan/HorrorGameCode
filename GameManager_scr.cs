using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_scr : MonoBehaviour
{
    [SerializeField] GameObject keyText;
    private Text text;
    [SerializeField] GameObject keyDoor;
    [SerializeField] int reqKeys = 1;
    public int keyCount = 0;

    void Start()
    {
        text = keyText.GetComponent<Text>(); 
    }

    void Update()
    {
        text.text = "鍵 " + keyCount + "/" + reqKeys;
        if(keyCount == reqKeys){
            Destroy(keyDoor);
        }
    }

}
