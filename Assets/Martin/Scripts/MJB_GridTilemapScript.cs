using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sherbert.GameplayStatics;

public class MJB_GridTilemapScript : MonoBehaviour
{

    [SerializeField] private Tilemap evilMap = null, cuteMap = null;

    void Start()
    {
        evilMap.enabled = false;
        cuteMap.enabled = true;
    }

    void Update()
    {
        if (JDH_World.GetWorldIsCute())
        {
            evilMap.enabled = false;
            cuteMap.enabled = true;
        }
        else
        {
            evilMap.enabled = true;
            cuteMap.enabled = false;
        }
    }
}
