using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueWindow : MonoBehaviour
{
    private TMP_Text _text;
    private DialogueStory _dialogueStory;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _dialogueStory = FindObjectOfType<DialogueStory>();
        _dialogueStory.ChangedStory += ChangeAnswer;
    }

    private void ChangeAnswer(DialogueStory.Story story) => _text.text = story.Text;
}
