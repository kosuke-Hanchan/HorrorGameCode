using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneCam_scr : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    Vector3 _camAng;

    public bool menu = false;
    public bool cursorLock = true;
    
    [SerializeField] Text nameText;
    [SerializeField] LayerMask mask;

    void Start()
    {
        cursorLock = true;
        menu = false;
        _camAng = this.transform.localEulerAngles;
    }

    void Update()
    {
        UpdateCursorLock();//マウスカーソルのロック処理

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * -Input.GetAxis("Mouse Y");

        var camAng_x = _camAng.x + v;
        if(!menu){
            if(camAng_x >= -45 && camAng_x <= 45){
                _camAng += new Vector3(v, 0, 0);
                this.transform.localEulerAngles = _camAng;
            }

            var camAng_y = _camAng.y + h;
            if(camAng_y >= -60 && camAng_y <= 60){
                _camAng += new Vector3(0, h, 0);
                this.transform.localEulerAngles = _camAng;
            }
        }

        Ray ray = new Ray(this.transform.position,this.transform.forward);
        Debug.DrawRay(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            if(hit.collider.tag == "Menu"){
                nameText.text = hit.collider.gameObject.name;
            }
            else{
                nameText.text = null;
            }
        }
    }


//----------------------------マウスカーソルロック処理-----------------------
    public void UpdateCursorLock(){
        if(!menu){
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLock = false;
                Cursor.visible = true;
            }
            else if(Input.GetMouseButton(0))
            {
                cursorLock = true;
                Cursor.visible = false;
            }
        }
        else{
            cursorLock = false;
            Cursor.visible = true;
        }

        if (cursorLock)
        {
            if(Mathf.Abs(Screen.width/2-Input.mousePosition.x) > 10 || Mathf.Abs(Screen.height/2-Input.mousePosition.y) > 10)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        else if(!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        }
}
