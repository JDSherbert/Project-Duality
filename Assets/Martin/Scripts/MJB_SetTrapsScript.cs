using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_SetTrapsScript : MonoBehaviour
{

    [SerializeField] private List<Sprite> trapTypes = null;
    [SerializeField] private int trapNumberDivisor = 10;

    private List<MJB_FloorTileScript> floorTiles;
    private List<Vector3> trapsSet;
    private int numberOfTraps;

    private void Awake()
    {
        SetTileList();
        numberOfTraps = floorTiles.Count / trapNumberDivisor;
        SetFirstTrap();
        SetTraps();
        Destroy(gameObject);
    }

    private void SetTileList()
    {
        floorTiles = new List<MJB_FloorTileScript>();
        trapsSet = new List<Vector3>();
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Floor");
        foreach (GameObject tile in allTiles)
        {
            floorTiles.Add(tile.GetComponent<MJB_FloorTileScript>());
        }
    }

    private void SetFirstTrap()
    {
        MJB_FloorTileScript targetTile = floorTiles[Random.Range(0, floorTiles.Count)];
        Sprite newTile = trapTypes[Random.Range(0, trapTypes.Count)];
        SwapTiles(targetTile, newTile);
    }

    private void SwapTiles(MJB_FloorTileScript currentTile, Sprite newTile)
    {
        currentTile.isTrap = true;
        currentTile.uncovered = false;
        currentTile.AddTrapSprite(newTile);
        trapsSet.Add(currentTile.gameObject.transform.position);
    }

    private void SetTraps()
    {
        for (int i = 0; i < numberOfTraps - 1; i++)
        {
            MJB_FloorTileScript targetTile = floorTiles[Random.Range(0, floorTiles.Count)];
            if (targetTile != null)
            {
                if (!TileExists(targetTile))
                {
                    i--;
                }
            }
            else
            {
                i--;
            }
        }
    }

    private bool TileExists(MJB_FloorTileScript targetTile)
    {
        if (!CheckAdjacency(targetTile))
        {
            Sprite newTile = trapTypes[Random.Range(0, trapTypes.Count)];
            SwapTiles(targetTile, newTile);
            return true;
        }
        return false;
    }

    private bool CheckAdjacency(MJB_FloorTileScript targetTile)
    {
        Vector3 currentTile = targetTile.gameObject.transform.position;
        foreach (Vector3 trap in trapsSet)
        {
            if (Vector3.Distance(trap, currentTile) <= 2)
            {
                return true;
            }
        }
        return false;
    }
}