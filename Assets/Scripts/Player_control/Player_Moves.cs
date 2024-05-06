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
    public bool _onGround = true;

    public CharacterController _PlayerObject;
    public string groundTag = "Grounds";

    [SerializeField] private float _force;
    private Rigidbody _rigidbody;

    private void Start()
    {
        // Приховуємо курсор
        //Cursor.visible = false;
        // Блокуємо курсор, щоб він не виходив за межі вікна гри
       // Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        _PlayerObject = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_onGo == true)
        {
            TransformPositionControlers();
            CheckingGround();
            if (_onGround == true)
            {
                JumpingPlayer();
            }
        }
    }

    private void TransformPositionControlers()
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

    /*public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;*/
    private void JumpingPlayer()
    {
        if (Input.GetKey(KeyCode.Space) && _onGround)
        {
            _rigidbody.AddForce(Vector3.up * _force * Time.deltaTime);
        }
    }

    void CheckingGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
        {
            if (hit.collider.CompareTag(groundTag))
            {
                _onGround = true;
            }
            else
            {
                _onGround = false;
            }
        }
        else
        {
            _onGround = false;
        }
    }
    
}
