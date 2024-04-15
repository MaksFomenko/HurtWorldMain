using System;
using Unity.VisualScripting;

namespace Inventory
{
    public interface IReadOnlyInventorySlot
    {
        event Action<string> ItemIdChanged;//івент який дає знати нам чи змінився Id 
        event Action<int> ItemAmountChanged;// чи змінилося кількість 
        
        string ItemId { get; } // доступ тільки на перегляд
        int Amount { get; }
        bool IsEmpty { get; }
    }
}