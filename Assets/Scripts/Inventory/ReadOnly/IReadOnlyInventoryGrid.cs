using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Inventory
{
    public interface IReadOnlyInventoryGrid: IReadOnlyInventory

    {
        event Action<Vector2Int> SizeChanged;

        Vector2Int Size { get; }

        IReadOnlyInventorySlot[,] GetSlote();
    }
}