using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueStory : MonoBehaviour
{
    [SerializeField] private Story[] _stories;
    private Dictionary<string, Story> _storiesDictionary;
    public Action<Story> ChangedStory;

    [Serializable]
    public struct Story
    {
        [field: SerializeField] public string Teg { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public Answer[] Answers { get; private set; }

    }
    
    [Serializable]
    public struct Answer
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string ReposeText { get; private set; }
        
    }

    private void Start()
    {
        _storiesDictionary = _stories.ToDictionary(key => key.Teg, element => element);
        ChangeStory(_stories[0].Teg);
    }

    public void ChangeStory(string tag) => ChangedStory?.Invoke(_storiesDictionary[tag]);
}
