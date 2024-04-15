using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable] // помiткa для збереження класа
    public class InventoryGridData
    {
        public string OwnerId; //власник цього інвентаря
        public List<InventorySlotData> Slots;
        public Vector2Int Size; //розмір (0,0)
    }
}