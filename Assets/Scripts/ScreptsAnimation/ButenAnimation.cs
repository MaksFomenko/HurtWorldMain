using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButenAnimation : MonoBehaviour
{
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            PlayAnim();
        }
    }

    public void PlayAnim()
    {
        _anim.SetTrigger("PlayTest1");
    }
}
