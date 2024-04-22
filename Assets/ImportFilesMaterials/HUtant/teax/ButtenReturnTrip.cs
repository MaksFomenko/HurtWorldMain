using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtenReturnTrip : MonoBehaviour
{
    public GameObject _gameObject;
    public GameObject _gameObject2;
    //public bool _oN = false;


    public void OpenTest()
    {
        _gameObject.SetActive(true);
        _gameObject2.SetActive(false);
        /*if (_oN == false)
        {
            _gameObject.SetActive(true);
            _oN = true;
            _gameObject2.SetActive(false);
        }
        else if(_oN == true)
        {
            _gameObject.SetActive(false);
            _oN = false;
            _gameObject2.SetActive(true);
        }
        //_gameObject.SetActive(true);*/
    }
}
