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

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Class template that defines items.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Sherbert Tools/Item")]
    public class JDH_Item : ScriptableObject
    {
        [Tooltip("The item's ID.")]
        public string ID;

        public enum ItemType
        {
            Consumable, KeyItem
        }
        [Tooltip("The item's type.")]
        public ItemType type = new ItemType();

        [Tooltip("The item's image to use.")]
        public Sprite sprite;
        [Tooltip("The item's physical object.")]
        public GameObject itemObject;

        [Tooltip("The item's name.")]
        public string itemName;
        [TextArea] [Tooltip("Short description for the item.")]
        public string description;
        [Tooltip("Current amount of this item held by the player.")]
        public int currentAmount;
        [Tooltip("Maximum amount of this item that can be held.")]
        public int maximumCapacity;

        public enum ItemWeight
        {
            None, Light, Heavy
        }
        [Tooltip("The item's weight for noise purposes.")]
        public ItemWeight weight = new ItemWeight();
    }
}
