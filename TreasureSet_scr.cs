using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//宝をマップ上にランダムに設置
public class TreasureSet_scr : MonoBehaviour
{
    
    [SerializeField] int count = 2; //Treasurの生成数

    [Header("低級の宝")]
    [SerializeField] GameObject treasur_1; //設置する鍵のオブジェクト
    [Header("中級の宝")]
    [SerializeField] GameObject treasur_2;
    [Header("高級の宝")]
    [SerializeField] GameObject treasur_3;
    [SerializeField] GameObject[] treasurBox; //鍵の設置場所リスト
    [SerializeField] List<GameObject> treasurList = new List<GameObject>();

    [SerializeField] int treasurPro2 = 15;//高く
    [SerializeField] int treasurPro3 = 5; //低く
    [SerializeField] int proMax = 30;
    

    private List<int> num = new List<int>();
    private int index;
    private int ran;

    void Start()
    {
        treasurBox = GameObject.FindGameObjectsWithTag("TreasurBox");

        for(int i = 0; i < treasurBox.Length; i++){
            num.Add(i);
        }

        int j = 0;
        while(count > j){
            index = Random.Range(0, num.Count);
            ran = num[index]; 
            num.RemoveAt(index);

            var selectNum = Random.Range(0, proMax);
            GameObject treasur;
            if(selectNum >= treasurPro2){
                treasur = GameObject.Instantiate(treasur_1) as GameObject;
                treasur.name = "低級品";
            }
            else if(selectNum >= treasurPro3){
                treasur = GameObject.Instantiate(treasur_2) as GameObject;
                treasur.name = "中級品";
            }
            else{
                treasur = GameObject.Instantiate(treasur_3) as GameObject;
                treasur.name = "高級品";
            }


            treasurList.Add(treasur);
            treasurList[j].transform.parent = treasurBox[ran].transform;
            treasurList[j].transform.localPosition = new Vector3(-0.3f,0f,0f); //位置調整
            j++;
        }
    }
}
