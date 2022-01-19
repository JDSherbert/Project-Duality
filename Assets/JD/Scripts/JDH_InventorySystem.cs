/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Inventory
{
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
        public int currentlySelectedIndex = 0;

        [System.Serializable]
        public class InventorySettings
        {
            public const int SIZE = 3;

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
            public UnityEvent<JDH_Item> OnItemCycle;
        }

        public Events events = new Events();

        void Update()
        {
            InputHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void InputHandler()
        {
            inventory.input.AXIS_ITEMUSE = Input.GetAxisRaw(inventory.input.ItemUseAxis);
            inventory.input.AXIS_ITEMDROP = Input.GetAxisRaw(inventory.input.ItemDropAxis);
            inventory.input.AXIS_ITEMCYCLE = Input.GetAxisRaw(inventory.input.ItemCycleAxis);

            if(inventory.input.bAlsoCycleWithNumPadKeys)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1)) CycleItem(0);
                if(Input.GetKeyDown(KeyCode.Alpha2)) CycleItem(1);
                if(Input.GetKeyDown(KeyCode.Alpha3)) CycleItem(2);
            }

            if (inventory.input.AXIS_ITEMUSE > 0) UseItem();
            else if (inventory.input.AXIS_ITEMDROP > 0) DropItem();
            else if (inventory.input.AXIS_ITEMCYCLE != 0) CycleItem();
        }

        public void UseItem()
        {
            if (EquippedItems[currentlySelectedIndex].currentAmount > 0)
            {
                events.OnItemUsed.Invoke(EquippedItems[currentlySelectedIndex]);
                if(EquippedItems[currentlySelectedIndex].type == JDH_Item.ItemType.Consumable) EquippedItems[currentlySelectedIndex].currentAmount--;
                if(EquippedItems[currentlySelectedIndex].currentAmount == 0) EquippedItems[currentlySelectedIndex] = null;
            }
            else Debug.Log("No Item.");
        }

        public void DropItem()
        {
            if (EquippedItems[currentlySelectedIndex].currentAmount > 0)
            {
                events.OnItemDropped.Invoke(EquippedItems[currentlySelectedIndex]);
                EquippedItems[currentlySelectedIndex].currentAmount--;
                if(EquippedItems[currentlySelectedIndex].currentAmount == 0) EquippedItems[currentlySelectedIndex] = null;
            }
            else Debug.Log("No Item.");
        }

        public void AddItem(JDH_Item NewItem)
        {
            //? Check for matching entries first
            for(int i = 0; i < EquippedItems.Length; i++)
            {
                if(NewItem.ID == EquippedItems[i].ID)
                {
                    if(EquippedItems[i].currentAmount < EquippedItems[i].maximumCapacity)
                    {
                        EquippedItems[i].currentAmount++;
                        events.OnItemPickedUp.Invoke(NewItem);
                    }
                    return;
                }
            }

            //? If no match, check for null
            for(int i = 0; i < EquippedItems.Length; i++)
            {
                if(EquippedItems[i] == null)
                {
                    EquippedItems[i] = NewItem;
                    events.OnItemPickedUp.Invoke(NewItem);
                    return;
                }
            }

            //? If no null, we are here. There is no room for item, so one needs to be dropped.
            Debug.Log("No space for item.");
        }

        public void CycleItem()
        {
            int NewIndex = currentlySelectedIndex;
            NewIndex += (int)inventory.input.AXIS_ITEMCYCLE;

            if (NewIndex > EquippedItems.Length) NewIndex = 0;
            if (NewIndex < 0) NewIndex = EquippedItems.Length;

            currentlySelectedIndex = NewIndex;
            events.OnItemCycle.Invoke(EquippedItems[currentlySelectedIndex]);
        }
        public void CycleItem(int NewIndex)
        {
            if (NewIndex > EquippedItems.Length) NewIndex = EquippedItems.Length;
            if (NewIndex < 0) NewIndex = 0;

            currentlySelectedIndex = NewIndex;
            events.OnItemCycle.Invoke(EquippedItems[currentlySelectedIndex]);
        }

    }
}
