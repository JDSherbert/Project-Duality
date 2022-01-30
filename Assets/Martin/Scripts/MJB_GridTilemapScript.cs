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
        evilMap.GetComponent<TilemapRenderer>().enabled = false;
        cuteMap.GetComponent<TilemapRenderer>().enabled = true;
    }

    void Update()
    {
        if (JDH_World.GetWorldIsCute())
        {
            evilMap.GetComponent<TilemapRenderer>().enabled = false;
            cuteMap.GetComponent<TilemapRenderer>().enabled = true;
        }
        else
        {
            evilMap.GetComponent<TilemapRenderer>().enabled = true;
            cuteMap.GetComponent<TilemapRenderer>().enabled = false;
        }
    }
}
