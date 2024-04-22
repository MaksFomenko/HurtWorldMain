using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button exitButton;
    public GameObject _gameObject;
    public void Start()
    {
        exitButton = GetComponent<Button>();
    }

    private void Update()
    {
        exitButton.onClick.AddListener(QuitGame);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
