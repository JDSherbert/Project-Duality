using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;
using Sherbert.Framework;

public class MJB_TrapTileBehaviour : MonoBehaviour
{

    private SpriteRenderer sRender;
    private bool uncovered = false;

    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        sRender.enabled = false;
        transform.Translate(new Vector3(0.25f, 0.25f, 0), Space.World);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UncoverTrap();
            collision.gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
        }
    }
}
