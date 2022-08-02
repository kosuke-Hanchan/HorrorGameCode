using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySet_scr : MonoBehaviour
{
    [SerializeField] int count = 2; //鍵の生成数
    [SerializeField] GameObject keyObj;
    [SerializeField] GameObject[] keyBox; //鍵の設置場所リスト
    [SerializeField] List<GameObject> keyList = new List<GameObject>();

    private List<int> num = new List<int>();
    private int index;
    private int ran;

    void Start()
    {
        keyBox = GameObject.FindGameObjectsWithTag("keyBox");

        for(int i = 0; i < keyBox.Length; i++){
            num.Add(i);
        }

        int j = 0;
        while(count > j){
            index = Random.Range(0, num.Count);
            ran = num[index];
            num.RemoveAt(index);

            GameObject key = GameObject.Instantiate(keyObj) as GameObject;
            key.name = "鍵";
            keyList.Add(key);
            keyList[j].transform.parent = keyBox[ran].transform;
            keyList[j].transform.localPosition = new Vector3(-0.3f,0f,0f); //位置調整
            j++;
        }
    }
}
