using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayPlayerInventory : DisplayInventory1
{

    void Start()
    {
        CreateSlots();
        UpdateSlots();
    }

    private void OnEnable()
    {
        UpdateSlots();
        inventory.OnChange += UpdateSlots;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        inventory.OnChange -= UpdateSlots;
        Time.timeScale = 1;
    }

    private new void CreateSlots()
    {
        DisplayedItems = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position = slotsToPlaceObjects[i].transform.position;
            //  AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            //  AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            //  AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            //  AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            //  AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
            DisplayedItems.Add(obj, inventory.slots[i]);
        }
        
        {
            var obj = Instantiate(slotObject, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<Transform>().position =
                slotsToPlaceObjects[inventory.slots.Length].transform.position;
            //  AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            //  AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            //  AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            //  AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            //  AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerDown, delegate { OnPointerDown(obj); });
           
        }
    }
}