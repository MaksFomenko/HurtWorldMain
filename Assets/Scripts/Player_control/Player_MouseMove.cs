
using System;
using UnityEngine;

public class Player_MouseMove : MonoBehaviour
{
    private float _xRot;
    private float _yRot;
    
    private float _xRotCurrent;
    private float _yRotCurrent;
    private float _correntVelosityX;
    private float _correntVelosityY;
   
    public float _sensivuty = 5f;
    public float _smoothTime = 0.1f;

    public Camera _PlayerCamera;
    public GameObject _PlayerGameObject;
   

    private void Update()
    {
        MouseMove();
    }

    void MouseMove()
    {
        _xRot += Input.GetAxis("Mouse X") * _sensivuty ;
        _yRot += Input.GetAxis("Mouse Y") * _sensivuty ;

        _yRot = Mathf.Clamp(_yRot, -85, 85);

        _xRotCurrent = Mathf.SmoothDamp(_xRotCurrent, _xRot, ref _correntVelosityX, _smoothTime);
        _yRotCurrent = Mathf.SmoothDamp(_yRotCurrent, _yRot, ref _correntVelosityY, _smoothTime);

        _PlayerCamera.transform.rotation = Quaternion.Euler(-_yRotCurrent,_xRotCurrent,0f);
        _PlayerGameObject.transform.rotation = Quaternion.Euler(0f,_xRotCurrent ,0f);
    }
    
}
