using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;

public class MJB_TrapTileBehaviour : MonoBehaviour
{

    private SpriteRenderer sRender;
    private bool uncovered = false;

    private void Awake()
    {
        sRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        sRender.enabled = false;
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
