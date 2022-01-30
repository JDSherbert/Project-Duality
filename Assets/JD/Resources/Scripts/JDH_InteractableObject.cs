/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.GameplayStatics;
    using Sherbert.Inventory;
    using Sherbert.Lexicon;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Interaction component for an object.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_InteractableObject : MonoBehaviour
    {
        [System.Serializable]
        public class InteractableSettings
        {
            public const float DELAY = 0.2f;
            public bool bDestroyAfterUse = true;
            public enum Type
            {
                Pickup, Event
            }
            public Type type = new Type();

            public JDH_Item itemPickup;
            public JDH_Rune runePickup;
        }
        public InteractableSettings interactable = new InteractableSettings();

        [System.Serializable]
        public class Requirements
        {
            [Tooltip("Checks for this item.")]
            public JDH_Item item;
            public const string PAYLOAD = "Needs a "; 
        }
        public Requirements require = new Requirements();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<JDH_InteractionComponent> OnInteractionWithInstigator;
            public UnityEvent<string> OnRequiresItem;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void OnTriggerEnter(Collider Instigator)
        {
            Target(Instigator);
        }
        void OnTriggerEnter2D(Collider2D Instigator)
        {
            Target(Instigator);
        }

        //____________________________________________________________________________________________________________________________________________
        // Class Methods
        //____________________________________________________________________________________________________________________________________________

        void Target(Collider Instigator)
        {
            if (Instigator.gameObject.GetComponent<JDH_InteractionComponent>())
            {
                JDH_InteractionComponent temp = Instigator.gameObject.GetComponent<JDH_InteractionComponent>();
                temp.SetInteractionObject(this);
                temp.events.OnTargetObjectAcquired.Invoke(this);
            }
        }
        void Target(Collider2D Instigator)
        {
            if (Instigator.gameObject.GetComponent<JDH_InteractionComponent>())
            {
                JDH_InteractionComponent temp = Instigator.gameObject.GetComponent<JDH_InteractionComponent>();
                temp.SetInteractionObject(this);
                temp.events.OnTargetObjectAcquired.Invoke(this);
            }
        }

        public void Interact(JDH_InteractionComponent Instigator)
        {
            Debug.Log(this.gameObject.name + " was interacted with by " + Instigator.gameObject.name);
            if(!JDH_GameplayStatics.IsTrueNull(require.item)) //Required item is null
            {
                if(JDH_GameplayStatics.IsTrueNull(Instigator.interaction.inventory.EquippedItems[Instigator.interaction.inventory.currentSelection]))
                {
                    events.OnRequiresItem.Invoke(Requirements.PAYLOAD + require.item.itemName);
                    return;
                }
                else if(Instigator.interaction.inventory.EquippedItems[Instigator.interaction.inventory.currentSelection].ID == require.item.ID)
                {
                    Instigator.PerformInteraction(interactable.type);
                    StartCoroutine(FinishInteraction());
                }
                else 
                {
                    events.OnRequiresItem.Invoke(Requirements.PAYLOAD + require.item.itemName);
                    return;
                }
            }

            Instigator.PerformInteraction(interactable.type);
            StartCoroutine(FinishInteraction());
        }

        public IEnumerator FinishInteraction()
        {
            yield return new WaitForSeconds(InteractableSettings.DELAY);
            if (interactable.bDestroyAfterUse)
            {
                yield return new WaitForEndOfFrame();
                Destroy(this.gameObject);
            }
        }
    }
}
