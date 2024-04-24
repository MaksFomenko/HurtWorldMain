using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPressTheKeyBoard : MonoBehaviour
{
    public List<GameObject> objectsToManage;
    public List<GameObject> objectsUi;
    private int selectedObjectIndex = -1;

    void Update()
    {
        // Перевірка натискання клавіш 1, 2, 3 і т.д. для вибору об'єкта
        for (int i = 0; i < objectsToManage.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectObject(i);
                break;
            }
        }
    }

    void SelectObject(int index)
    {
        // Перевіряємо, чи індекс є в межах списку об'єктів
        if (index >= 0 && index < objectsToManage.Count)
        {
            // Деактивуємо всі об'єкти, крім обраного
            for (int i = 0; i < objectsToManage.Count; i++)
            {
                objectsToManage[i].SetActive(i == index);
                objectsUi[i].SetActive(i == index);
            }
            selectedObjectIndex = index;
        }
    }
}
