using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize_scr : MonoBehaviour
{
public Camera camera;
public float baseWidth = 9.0f;
public float baseHeight = 16.0f;

void Update()
{
    // アスペクト比固定
    var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
    var width = (this.baseWidth * scale) / Screen.width;
    var height = (this.baseHeight * scale) / Screen.height;
    this.camera.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);
}
}
