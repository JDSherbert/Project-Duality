using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;

public class MJB_FloorTileScript : MonoBehaviour
{

    [SerializeField] private List<Sprite> allSprites = null;

    public bool isTrap = false;
    public bool uncovered = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = allSprites[0];
    }

    public void AddTrapSprite(Sprite trap)
    {
        allSprites.Add(trap);
        gameObject.tag = trap.name;
        BoxCollider2D newCol = gameObject.AddComponent<BoxCollider2D>();
        newCol.isTrigger = true;
    }

    public void SetSpriteType(JDH_World.WorldState type)
    {
        if (type == JDH_World.WorldState.Cute)
        {
            spriteRenderer.sprite = allSprites[0];
        }
        else
        {
            EvilSprite();
        }
    }

    private void EvilSprite()
    {
        if (!isTrap)
        {
            spriteRenderer.sprite = allSprites[1];
        }
        else
        {
            CoveredTrap();
        }
    }

    private void CoveredTrap()
    {
        if (!uncovered)
        {
            spriteRenderer.sprite = allSprites[1];
        }
        else
        {
            spriteRenderer.sprite = allSprites[2];
        }
    }

    public void UncoverTrap()
    {
        uncovered = true;
        spriteRenderer.sprite = allSprites[2];
    }
}
