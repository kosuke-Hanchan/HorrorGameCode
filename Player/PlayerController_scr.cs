using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController_scr : MonoBehaviour
{
    [SerializeField] float gravity = 3f;//落ちる速さ、重力
    [SerializeField] float walkSpeed = 3;//通常時のスピード
    [SerializeField] float runSpeed = 5;//左Shift押したときのスピード
    private float speed;
    [SerializeField] float stamina = 3f;
    private float staminaTime;

    Vector3 velocity;
    CharacterController controller;

    //マウスの横縦に動かす感度
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    [SerializeField] GameObject cameraObj;
    [SerializeField] float camHeight = 1f;//カメラの高さ
    [SerializeField] float squatHeight = 0.3f;//しゃがんだ時の高さ
    bool cursorLock = true;
    Vector3 _camAng;//カメラの初期角度を格納

    private Animator animator;
    [SerializeField] GameObject characterObj;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraObj.transform.localPosition = new Vector3(0, camHeight, 0);
        cameraObj.transform.rotation = this.transform.rotation;
        _camAng = cameraObj.transform.localEulerAngles;

        staminaTime = stamina;

        animator = characterObj.GetComponent<Animator>();
    }

    void Update()
    { 
        UpdateCursorLock();//マウスカーソルのロック処理

//----------------------------------------------キャラクター移動----------------------------------------------------//
        //左右どちらかのShift押した場合と離している場合
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            staminaTime -= Time.deltaTime;
            if(staminaTime > 0f){
                speed = runSpeed;
            }
            
            else{
                staminaTime = 0f;
                speed = walkSpeed;
            }
        }
        else
        {
            if(staminaTime <= stamina){
                staminaTime += Time.deltaTime;
            }
            speed = walkSpeed;
        }

        //マウスでカメラの向きとキャラの向きを変える処理
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * -Input.GetAxis("Mouse Y");
        this.transform.Rotate(0, h, 0);

        //カメラのx軸回転処理
        var camAng = _camAng.x + v;
        if(camAng >= -90 && camAng <= 90){
            _camAng = new Vector3(camAng,0,0);
            cameraObj.transform.localEulerAngles = _camAng;
        }

        //キャラの移動処理
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded)//Playerが地面に設置していることを判定
        {
            controller.Move(move.normalized * speed * Time.deltaTime);         
        }

        //落下処理
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if(Input.GetKey(KeyCode.LeftControl)){
            cameraObj.transform.localPosition = Vector3.Lerp(cameraObj.transform.localPosition, new Vector3(0, squatHeight, 0), 10f * Time.deltaTime);
        }
        else{
            cameraObj.transform.localPosition = Vector3.Lerp(cameraObj.transform.localPosition, new Vector3(0, camHeight, 0), 10f * Time.deltaTime);
        }


//------------アニメーション--------------------------------------------------
        if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && speed == walkSpeed){
            animator.SetBool("isWalk", true);
            animator.SetBool("isRun", false);
        }
        else if(speed == runSpeed){
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", true);
        }
        else{
            animator.SetBool("isWalk", false);
            animator.SetBool("isRun", false);
        }
    }

//----------------------------マウスカーソルロック処理-----------------------
    public void UpdateCursorLock(){
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
