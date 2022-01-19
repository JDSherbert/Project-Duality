using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_InteractionTexts : MonoBehaviour
{

    [SerializeField] private List<string> interactionTexts = null;

    public string GetInteractionText()
    {
        if (Sherbert.GameplayStatics.JDH_World.world == Sherbert.GameplayStatics.JDH_World.WorldState.Cute)
        {
            return interactionTexts[Random.Range(0, 3)];
        }
        else
        {
            return interactionTexts[Random.Range(3, 6)];
        }
    }

}
