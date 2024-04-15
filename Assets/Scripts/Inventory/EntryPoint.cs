using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntryPoint: MonoBehaviour

    {
        public InventoryGridView _view;
        private InventoryService _inventoryService;

        private void Start ()
        {
            _inventoryService = new InventoryService();

            var ownerId = "Toma1";
            var inventoryData = createTestInventory(ownerId);
            var inventory = _inventoryService.RegisterInventory(inventoryData);
            
            _view.Setup(inventory);

            var addedResult = _inventoryService.AddItemsToInventory(ownerId, "Apple", 30);
            Debug.Log($"Items added. ItemId: apple, amount to add: 30, amount added:{addedResult.ItemsAddedAmount}");
            
             addedResult = _inventoryService.AddItemsToInventory(ownerId, "Кірпіч", 260);
            Debug.Log($"Items added. ItemId: apple, amount to add: 30, amount added:{addedResult.ItemsAddedAmount}");
            
             addedResult = _inventoryService.AddItemsToInventory(ownerId, "Letter", 10);
            Debug.Log($"Items added. ItemId: apple, amount to add: 30, amount added:{addedResult.ItemsAddedAmount}");

            _view.Print();

            var removedResult = _inventoryService.RemoveItems(ownerId, "apple", 13);
            Debug.Log($"Items removed. ItemId: apple, amount remove: 13, Success:{removedResult.Success}");
            removedResult = _inventoryService.RemoveItems(ownerId, "apple", 18);
            Debug.Log($"Items removed. ItemId: apple, amount remove: 18, Success:{removedResult.Success}");
            
            _view.Print();
        }

        private InventoryGridData createTestInventory(string ownerId)
        {
            var size = new Vector2Int(3, 3);
            var createdIventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y; // потрібно витасковати з конфігов
            for (int i = 0; i < length; i++)
            {
                createdIventorySlots.Add(new InventorySlotData());
            }

            var createdIventoryData = new InventoryGridData
            {
                OwnerId = ownerId,
                Size = size,
                Slots = createdIventorySlots
            };

            return createdIventoryData;
        }
    }
}