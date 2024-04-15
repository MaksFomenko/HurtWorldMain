using System;
using Unity.VisualScripting;

namespace Inventory
{
    public interface IReadOnlyInventory
    {
        event Action<string, int> ItemsAdded; // івент на додавання придмета
        event Action<string, int> ItemsRemoved; // на виделення
        
        string OwnerId { get; }


        int GetAmount(string itemId);
        bool Has(string itemId, int amount);
    }
}