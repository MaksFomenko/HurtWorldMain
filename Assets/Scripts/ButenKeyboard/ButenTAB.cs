using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButenTAB : MonoBehaviour
{
    public GameObject _gameObject;
    public bool _oN = false;

    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        if (_oN == false)
        {
            _gameObject.SetActive(true);
            _oN = true;
        }
        else if(_oN == true)
        {
            _gameObject.SetActive(false);
            _oN = false;
        }
        //_gameObject.SetActive(true);
    }
}
