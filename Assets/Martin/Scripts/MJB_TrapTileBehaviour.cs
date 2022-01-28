using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;

public class MJB_TrapTileBehaviour : MonoBehaviour
{

    private SpriteRenderer sRender;
    private bool uncovered = false;

    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        sRender.enabled = false;
        transform.SetParent(GameObject.Find("Traps").transform, true);
    }

    void Update()
    {
        if (JDH_World.GetWorldIsCute())
        {
            sRender.enabled = false;
        }
        else
        {
            sRender.enabled = uncovered;
        }
    }

    public void UncoverTrap()
    {
        uncovered = true;
    }
}
