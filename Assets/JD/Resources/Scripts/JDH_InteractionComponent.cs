/// <summary>
///____________________________________________________________________________________________________________________________________________
/// License:
/// Copyrighted to Joshua "JDSherbert" Herbert ©2022 for GGJ 2022.
/// Do not copy, modify, or redistribute this code without prior consent.
///____________________________________________________________________________________________________________________________________________
/// </summary>

namespace Sherbert.Framework
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    using Sherbert.Inventory;
    using Sherbert.Lexicon;

    /// <summary>
    ///____________________________________________________________________________________________________________________________________________________
    /// Interaction component for allowing the player to interact with an interactable. Should be attached to the root gameobject.
    ///____________________________________________________________________________________________________________________________________________________
    /// </summary>
    public class JDH_InteractionComponent : MonoBehaviour
    {
        [System.Serializable]
        public class InteractionSettings
        {
            public JDH_InteractableObject target;

            public JDH_InventorySystem inventory;

            [System.Serializable]
            public class InputSettings
            {
                public string InteractionAxis = "Interact";
                public float AXIS_INTERACT;
            }
            public InputSettings input = new InputSettings();

            public float range;
            public float size;
        }
        public InteractionSettings interaction = new InteractionSettings();

        [System.Serializable]
        public class Events
        {
            public UnityEvent<JDH_InteractableObject> OnTargetObjectAcquired;
            public UnityEvent<JDH_InteractableObject> OnInteractWithObject;
        }
        public Events events = new Events();

        //____________________________________________________________________________________________________________________________________________
        // Monobehaviour methods
        //____________________________________________________________________________________________________________________________________________

        void Awake()
        {
            Init();
        }
        void Update()
        {
            InputHandler();
        }

        //____________________________________________________________________________________________________________________________________________
        // Class methods
        //____________________________________________________________________________________________________________________________________________

        void InputHandler()
        {
            interaction.input.AXIS_INTERACT = Convert.ToSingle(Input.GetButtonDown(interaction.input.InteractionAxis));

            if (interaction.input.AXIS_INTERACT > 0)
            {
                if (interaction.target)
                {
                    interaction.target.Interact(this);
                }
            }
        }

        public void SetInteractionObject(JDH_InteractableObject NewObject)
        {
            interaction.target = NewObject;
        }
        public void ClearInteractionObject()
        {
            interaction.target = null;
        }

        //? Called by Interactable object
        public void PerformInteraction(JDH_InteractableObject.InteractableSettings.Type TypeOfInteractable)
        {
            switch (TypeOfInteractable)
            {
                case (JDH_InteractableObject.InteractableSettings.Type.Pickup):
                    if (interaction.target.interactable.itemPickup) GetComponentInChildren<JDH_InventorySystem>().AddItem(interaction.target.interactable.itemPickup);
                    if (interaction.target.interactable.runePickup) GetComponentInChildren<JDH_RuneSystem>().UnlockRune(interaction.target.interactable.runePickup);
                    interaction.target.events.OnInteractionWithInstigator.Invoke(this);
                    break;

                case (JDH_InteractableObject.InteractableSettings.Type.Event):
                    interaction.target.events.OnInteractionWithInstigator.Invoke(this);
                    break;
            }
            interaction.target.FinishInteraction();
        }

        void Init()
        {
            if (!interaction.inventory && GetComponentInChildren<JDH_InventorySystem>()) interaction.inventory = GetComponentInChildren<JDH_InventorySystem>();
            else if (!interaction.inventory && !GetComponentInChildren<JDH_InventorySystem>()) Debug.LogWarning("No inventory found as a child of this gameobject.");
        }
    }
}
