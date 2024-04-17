using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Player_Moves : MonoBehaviour
{

    public float _HSpeed = 6.0f;
    public float _VSpeed = 4.0f;
    public float _speed_Run;
    
    public float _Hspeed_Corrend;
    public float _Vspeed_Corrend;
    public bool _onGo = true;

    public CharacterController _PlayerObject;

    [SerializeField] private float _force;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_onGo == true)
        {
            TransformPositionControler();
            JumpingPlayer();
        }
    }

    private void TransformPositionControler()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _Hspeed_Corrend = _speed_Run;
            _Vspeed_Corrend = _speed_Run; 
        }
        else
        {
            _Hspeed_Corrend = _HSpeed;
            _Vspeed_Corrend = _VSpeed;
        }
        transform.Translate
        (Input.GetAxis("Horizontal") * _Hspeed_Corrend  * Time.deltaTime, 0f,
            Input.GetAxis("Vertical") * _Vspeed_Corrend * Time.deltaTime);
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    private void JumpingPlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _force);
        }
    }

    void CheckingGround()
    {
       // onGround = Physics.OverlapCapsule(GroundCheck.position, Vector3, Ground);
    }
    
}
