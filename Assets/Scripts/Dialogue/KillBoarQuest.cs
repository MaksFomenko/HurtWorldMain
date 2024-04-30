using UnityEngine;
using TMPro;

public class KillBoarQuest : MonoBehaviour
{
    public int requiredBoars = 5;
    private int boarsKilled = -1;
    public TextMeshProUGUI questText;

    void Start()
    {
        DialogueManager.OnDialogueEnd += StartQuest;
    }

    void OnDestroy()
    {
        DialogueManager.OnDialogueEnd -= StartQuest;
    }
    
    void StartQuest()
    {
        // Ваш код для початку квесту...
        BoarKilled();
        Debug.Log("Початок квесту: Вбити " + requiredBoars + " кабанів");
    }

    public void BoarKilled()
    {
        boarsKilled++;
        Debug.Log("+ кабан");
        UpdateQuestText();

        if (boarsKilled >= requiredBoars)
        {
            CompleteQuest();
        }
    }

    void UpdateQuestText()
    {
        questText.text = "Вбито " + boarsKilled + " з " + requiredBoars + " кабанів";
    }

    void CompleteQuest()
    {
        questText.text ="Квест вбивства кабанів завершено!";
        Debug.Log("Квест вбивства кабанів завершено!");
        // Додатковий код, що виконується при завершенні квесту...
    }
}