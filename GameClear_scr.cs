using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameClear_scr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            PlayerPrefs.SetInt ("GAME", 1);//1がゲームクリア2がゲームオーバー
            PlayerPrefs.Save ();
            SceneManager.LoadScene("MainScene");
        } 
    }
}
