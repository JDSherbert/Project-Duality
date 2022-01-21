using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_BirdScript : MonoBehaviour
{

    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private GameObject bird = null;

    private bool chasing = false;
    private GameObject player;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            player = null;
        }
    }

    private void FixedUpdate()
    {
        if (Sherbert.GameplayStatics.JDH_World.world == Sherbert.GameplayStatics.JDH_World.WorldState.Evil)
        {
            if (chasing)
            {
                ChasePlayer();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInteraction(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bunny"))
        {
            BunnyInteraction(collision.gameObject);
        }
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            transform.Translate((player.transform.position - transform.position) * Time.deltaTime * chaseSpeed);
        }
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        // Kill the player here
    }

    private void BunnyInteraction(GameObject bunny)
    {
        StartCoroutine(SpawnKind(bunny.transform.position));
        Destroy(bunny);
    }

    public void TriggerBird()
    {
        if (!chasing)
        {
            StartCoroutine(ChaseCooldown());
        }
        chasing = true;
    }

    private IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(3);
        chasing = false;
    }

    private IEnumerator SpawnKind(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(3);
        Instantiate(bird, spawnPosition, transform.rotation);
    }
}
