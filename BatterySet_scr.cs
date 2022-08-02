using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//バッテリーをマップ上にランダムに設置

public class BatterySet_scr : MonoBehaviour
{
    [SerializeField] int count = 2; //batteryの生成数
    [SerializeField] GameObject batteryObj;
    [SerializeField] GameObject[] batteryBox; //batteryの設置場所リスト
    [SerializeField] List<GameObject> batteryList = new List<GameObject>();

    private List<int> num = new List<int>();
    private int index;
    private int ran;

    void Start()
    {
        batteryBox = GameObject.FindGameObjectsWithTag("BatteryBox");

        for(int i = 0; i < batteryBox.Length; i++){
            num.Add(i);
        }

        int j = 0;
        while(count > j){
            index = Random.Range(0, num.Count);
            ran = num[index];   
            num.RemoveAt(index);

            GameObject battery = GameObject.Instantiate(batteryObj) as GameObject;
            battery.name = "電池";
            batteryList.Add(battery);
            batteryList[j].transform.parent = batteryBox[ran].transform;
            batteryList[j].transform.localPosition = new Vector3(-0.3f,0f,0f); //位置調整
            j++;
        }
    }
}
