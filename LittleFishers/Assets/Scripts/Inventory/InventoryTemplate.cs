using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleFishers.LFInventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System v2/Inventory")]
    public class InventoryTemplate : ScriptableObject
    {
        [SerializeField] private string _inventoryName;
        [SerializeField] private int _inventoryCapacity;
        [SerializeField] private List<InventoryItemStack> _defaultItems;
        [SerializeField] private int _defaultGold;


        public string InventoryName { get => this._inventoryName; set => this._inventoryName = value; }
        public int InventoryCapacity { get => this._inventoryCapacity; set => this._inventoryCapacity = value; }
        public int Gold { get => this._defaultGold; set => this._defaultGold = value; }

        // TODO - Default Items on inventory
    }

}
