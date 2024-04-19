using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButenAnimation : MonoBehaviour
{
    private Animator _anim;

    private bool isAnimating = false;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayAnim();

        }
        else if (Input.GetMouseButton(0))
        {
            if (!isAnimating)
            {
                PlayAnim();
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isAnimating = false;
        }
    }

    public void PlayAnim()
    {
        _anim.SetTrigger("PlayTest1");
        isAnimating = true;
    }
}
