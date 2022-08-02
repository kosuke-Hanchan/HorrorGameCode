using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureCounter_scr : MonoBehaviour
{
    [SerializeField] int score = 0; 
    [SerializeField] int treasur1 = 10;
    [SerializeField] int treasur2 = 30;
    [SerializeField] int treasur3 = 50;

    [SerializeField] Text scoreText;

    [SerializeField] AudioSource audioSouce;
    [SerializeField] AudioClip switchClip;

    void Start()
    {
        scoreText.text =  score + " $";
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Treasur1"){
            score += treasur1;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Treasur2"){
            score += treasur2;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Treasur3"){
            score += treasur3;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Treasur1" || other.gameObject.tag == "Treasur2" || other.gameObject.tag == "Treasur3"){
           audioSouce.PlayOneShot(switchClip,0.5f); 
        }
        
        PlayerPrefs.SetInt ("GET_MONEY", score);
        PlayerPrefs.Save ();
        scoreText.text =  score + " $";
    }
}
