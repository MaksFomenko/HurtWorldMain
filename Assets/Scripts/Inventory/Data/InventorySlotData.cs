using System;

namespace Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public string ItemId; //ID придмет
        public int Amount; //кулькість об'єктів в комірки (одного типу наприклад 5шт. з 255 )
    }
}
