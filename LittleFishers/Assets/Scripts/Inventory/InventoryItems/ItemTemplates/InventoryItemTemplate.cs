using System.Collections.Generic;
using UnityEngine;
namespace LittleFishers.LFInventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System v2/Inventory Items/Default Item")]
    public class InventoryItemTemplate : ScriptableObject
    {
        public string ItemName;
        public string Description;
        public int GoldValue;
        public Sprite InventoryIcon;
        public int MaxStackSize;

        public virtual InventoryItemType GetItemType()
        {
            return InventoryItemType.Default;
        }
    }
}