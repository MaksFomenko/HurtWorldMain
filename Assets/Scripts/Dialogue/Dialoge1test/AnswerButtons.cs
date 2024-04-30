using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButtons : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private TMP_Text[] _buttonsText;
    private string[] _currentReplyTags;
    private DialogueStory _dialogueStory;

    private void Start()
    {
        _dialogueStory = GetComponent<DialogueStory>();
        _dialogueStory.ChangedStory += ChangeAnswers;

        _buttonsText = new TMP_Text[_buttons.Length];
        _currentReplyTags = new string[_buttons.Length];

        for (int i = 0; i < _buttons.Length; i++)
        {
            int button = i;
            _buttons[i].onClick.AddListener(() => SendAnswer(button));
            _buttonsText[i] = _buttons[i].gameObject.GetComponentInChildren<TMP_Text>();
        }
    }

    private void ChangeAnswers(DialogueStory.Story story)
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttonsText[i].text = null;
            _buttons[i].interactable = false;
            continue;
        }
    }

    private void SendAnswer(int button) => _dialogueStory.ChangeStory(_currentReplyTags[button]);
}
