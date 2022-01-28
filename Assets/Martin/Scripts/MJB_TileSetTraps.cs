using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MJB_TileSetTraps : MonoBehaviour
{

    [SerializeField] private Tilemap evilMap = null;
    [SerializeField] private List<Sprite> floorTiles = null;
    [SerializeField] private List<GameObject> trapTypes = null;
    [SerializeField] private int trapNumberDivisor = 20;

    private List<Vector3> possibleTrapPositions;
    private List<Vector3> trapsSet;
    private int numberOfTraps;

    void Start()
    {
        GetPossiblePositions();
        numberOfTraps = possibleTrapPositions.Count / trapNumberDivisor;
        SetFirstTrap();
        SetTraps();
        Destroy(gameObject);
    }

    private void GetPossiblePositions()
    {
        possibleTrapPositions = new List<Vector3>();
        trapsSet = new List<Vector3>();
        foreach (Vector3Int tilePosition in evilMap.cellBounds.allPositionsWithin)
        {
            if (evilMap.HasTile(tilePosition))
            {
                Sprite tile = evilMap.GetTile<Tile>(tilePosition).sprite;
                if (floorTiles.Contains(tile))
                {
                    possibleTrapPositions.Add(evilMap.CellToWorld(tilePosition));
                }
            }
        }
    }

    private void SetFirstTrap()
    {
        Vector3 trapPosition = possibleTrapPositions[Random.Range(0, possibleTrapPositions.Count)];
        GameObject trapType = trapTypes[Random.Range(0, trapTypes.Count)];
        CreateTrap(trapType, trapPosition);
    }

    private void CreateTrap(GameObject trap, Vector3 trapPos)
    {
        Instantiate(trap, trapPos, Quaternion.identity);
        trapsSet.Add(trapPos);
    }

    private void SetTraps()
    {
        for (int i = 0; i < numberOfTraps - 1; i++)
        {
            Vector3 targetTile = possibleTrapPositions[Random.Range(0, possibleTrapPositions.Count)];
            if (!CheckAdjacency(targetTile))
            {
                GameObject trapType = trapTypes[Random.Range(0, trapTypes.Count)];
                CreateTrap(trapType, targetTile);
            }
            else
            {
                i--;
            }
        }
    }

    private bool CheckAdjacency(Vector3 tilePosition)
    {
        foreach (Vector3 trap in trapsSet)
        {
            if (Vector3.Distance(trap, tilePosition) <= 2)
            {
                return true;
            }
        }
        return false;
    }
}
