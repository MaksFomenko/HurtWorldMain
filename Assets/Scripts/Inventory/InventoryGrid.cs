﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        public string OwnerId => _data.OwnerId;

        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            var size = data.Size;
            for (var i = 0; i < size.x ; i++)
            {
                for (var j = 0;  j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public AddItemsToInventoryGridResult AddItems(string itemId, int amount = 1)
        {
            var remainingAmount = amount;
            var itemAddedToSlotsWithSameItemsAmount =
                AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                return new AddItemsToInventoryGridResult(OwnerId, amount, itemAddedToSlotsWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotsAmount =
                AddToFirstAvailableSlots(itemId, remainingAmount, out remainingAmount);
            var totalAddedItemsAmount = itemAddedToSlotsWithSameItemsAmount + itemsAddedToAvailableSlotsAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount);
        }
        
        
        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1) //додавання об'єкта в ячейку
        {
            var slot = _slotsMap[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.ItemId = itemId;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            } 
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }
        

        public RemoveItemsFromInventoryGridResult RemoveItems(string iteamId, int amount = 1)
        {
            if (!Has(iteamId, amount))
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            var amontToRemove = amount;
            
            for (var i = 0; i < Size.x ; i++)
            {
                for (var j = 0;  j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if (slot.ItemId != iteamId)
                    {
                        continue;
                    }

                    if (amontToRemove > slot.Amount)
                    {
                        amontToRemove -= slot.Amount;

                        RemoveItems(slotCoords, iteamId, slot.Amount);

                    }
                    else
                    {
                        RemoveItems(slotCoords, iteamId, amontToRemove);

                        return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }
        
        
        public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords,string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);
            }

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.ItemId = null;
            }

            return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
        }

        public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB) //метод зміни місцями обєктів в ячейках
        {
            var slotA = _slotsMap[slotCoordsA];
            var slotB = _slotsMap[slotCoordsB];
            var tempSlotItemId = slotA.ItemId;
            var tempSlotItemAmount = slotA.Amount;
            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;
            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotItemAmount;
        }
        
        public int GetAmount(string itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.ItemId == itemId)
                {
                    amount += slot.Amount;
                }
                
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }
        
        public IReadOnlyInventorySlot[,] GetSlote()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];
            
            for (var i = 0; i < Size.x ; i++)
            {
                for (var j = 0;  j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }

        /*public bool AddItems(string itemId, int amount)
        {
            throw new NotImplementedException();
        }*/

        private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount) //принімаємо те що хочемо покласти і виводім те що не влізло
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; i < Size.y; i++)
            {
                for (var j = 0; i < Size.y;j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);
                    if (slot.Amount >= slotItemCapacity)
                    {
                        continue;
                    }

                    if (slot.ItemId != itemId)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;
                    }
                }
            }

            return itemsAddedAmount;
        }
        
        private int GetItemSlotCapacity(string itemId)
        {
            return 255;
        }

        private int AddToFirstAvailableSlots(string itemId, int amount, out int remainingAmount) //перевірка пустих слотів
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.ItemId = itemId;
                    var newValue = remainingAmount;
                    var slotItemCapacity = GetItemSlotCapacity(slot.ItemId);

                    if (newValue > slotItemCapacity) // якщо не влазить 
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else//якщо влазить в слотів 
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }
    }
}