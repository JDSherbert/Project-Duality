/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Inventory
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// System Component that manipulates how items are stored and used.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_InventorySystem : MonoBehaviour
    {
        public JDH_Item[] EquippedItems = new JDH_Item[InventorySettings.SIZE];
        public int currentSelection = 0;

        [System.Serializable]
        public class InventorySettings
        {
            public const int SIZE = 3;
            public const string NOITEM = "Nothing";

            [System.Serializable]
            public class InputSettings
            {
                public string ItemUseAxis = "UseItem";
                public float AXIS_ITEMUSE;
                public string ItemDropAxis = "DropItem";
                public float AXIS_ITEMDROP;
                public string ItemCycleAxis = "CycleItem";
                public float AXIS_ITEMCYCLE;
                public bool bAlsoCycleWithNumPadKeys = true;
            }
            public InputSettings input = new InputSettings();
        }
        public InventorySettings inventory = new InventorySettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<JDH_Item> OnItemUsed;
            public UnityEvent<JDH_Item> OnItemPickedUp;
            public UnityEvent<JDH_Item> OnItemDropped;
            public UnityEvent<string> OnItemCycle;
        }

        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Update()
        {
            InputHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void InputHandler()
        {
            inventory.input.AXIS_ITEMUSE = Convert.ToSingle(Input.GetButtonDown(inventory.input.ItemUseAxis));
            inventory.input.AXIS_ITEMDROP = Convert.ToSingle(Input.GetButtonDown(inventory.input.ItemDropAxis));
            inventory.input.AXIS_ITEMCYCLE = Input.GetAxisRaw(inventory.input.ItemCycleAxis);

            if (inventory.input.bAlsoCycleWithNumPadKeys)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) CycleItem(0);
                if (Input.GetKeyDown(KeyCode.Alpha2)) CycleItem(1);
                if (Input.GetKeyDown(KeyCode.Alpha3)) CycleItem(2);
            }

            if (inventory.input.AXIS_ITEMUSE > 0) UseItem();
            else if (inventory.input.AXIS_ITEMDROP > 0) DropItem();
            else if (inventory.input.AXIS_ITEMCYCLE != 0) CycleItem();
        }

        public void UseItem()
        {
            if (EquippedItems[currentSelection] != null)
            {
                if (EquippedItems[currentSelection].currentAmount > 0)
                {
                    events.OnItemUsed.Invoke(EquippedItems[currentSelection]);
                    if (EquippedItems[currentSelection].type == JDH_Item.ItemType.Consumable) EquippedItems[currentSelection].currentAmount--;
                    if (EquippedItems[currentSelection].currentAmount == 0) EquippedItems[currentSelection] = null;
                    return;
                }
            }

            Debug.Log("No Item.");
        }

        public void DropItem()
        {
            if (EquippedItems[currentSelection] != null)
            {
                if (EquippedItems[currentSelection].currentAmount > 0)
                {
                    events.OnItemDropped.Invoke(EquippedItems[currentSelection]);
                    EquippedItems[currentSelection].currentAmount--;
                    if (EquippedItems[currentSelection].currentAmount == 0) EquippedItems[currentSelection] = null;
                    return;
                }
            }

            Debug.Log("No Item.");
        }

        public void AddItem(JDH_Item NewItem)
        {
            if (Array.IndexOf(EquippedItems, NewItem) != -1)
            {
                int insert = Array.IndexOf(EquippedItems, NewItem);
                if (EquippedItems[insert].currentAmount < EquippedItems[insert].maximumCapacity)
                {
                    EquippedItems[insert].currentAmount++;
                    events.OnItemPickedUp.Invoke(NewItem);
                    return;
                }
            }
            else
            {
                if (Array.IndexOf(EquippedItems, null) != -1)
                {
                    int insert = Array.IndexOf(EquippedItems, null);
                    EquippedItems[insert] = NewItem;
                    EquippedItems[insert].currentAmount++;
                    events.OnItemPickedUp.Invoke(NewItem);
                    return;
                }
            }

            //? If no null, we are here. There is no room for item, so one needs to be dropped.
            Debug.Log("No space for item.");
        }

        public void CycleItem()
        {
            int NewIndex = currentSelection;
            NewIndex += (int)inventory.input.AXIS_ITEMCYCLE;

            if (NewIndex > EquippedItems.Length) NewIndex = 0;
            if (NewIndex < 0) NewIndex = EquippedItems.Length;

            currentSelection = NewIndex;
            if (EquippedItems[currentSelection] == null) events.OnItemCycle.Invoke(InventorySettings.NOITEM);
            else events.OnItemCycle.Invoke(EquippedItems[currentSelection].itemName);

        }
        public void CycleItem(int NewIndex)
        {
            if (NewIndex > EquippedItems.Length) NewIndex = EquippedItems.Length;
            if (NewIndex < 0) NewIndex = 0;

            currentSelection = NewIndex;
            if (EquippedItems[currentSelection] == null) events.OnItemCycle.Invoke(InventorySettings.NOITEM);
            else events.OnItemCycle.Invoke(EquippedItems[currentSelection].itemName);
        }

        public List<JDH_Item> GetAllItems()
        {
            List<JDH_Item> itemList = new List<JDH_Item>(EquippedItems);
            itemList.TrimExcess();
            return itemList;
        }

        public void RemoveAllItems()
        {
            Array.Clear(EquippedItems, 0, EquippedItems.Length);
        }

    }
}
