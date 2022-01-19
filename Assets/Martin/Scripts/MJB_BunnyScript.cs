using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_BunnyScript : MonoBehaviour
{

    [SerializeField] private float patrolWaitTime = 3f;
    [SerializeField] private float patrolSpeed = 0.5f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private GameObject bunny = null;

    private bool chasing = false;
    private Vector3 patrolLocation;
    private Vector3 lastSoundLocation;

    void Start()
    {
        patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
    }

    private void FixedUpdate()
    {
        if (Sherbert.GameplayStatics.JDH_World.world == Sherbert.GameplayStatics.JDH_World.WorldState.Evil)
        {
            if (!chasing)
            {
                Patrol();
            }
            else
            {
                ChaseSound();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Run bunny-player interaction here
        }
        else if (collision.gameObject.CompareTag("Puppy"))
        {
            PuppyInteraction(collision.gameObject);
        }
    }

    private void Patrol()
    {
        patrolWaitTime -= Time.deltaTime;
        if (patrolWaitTime <= 0)
        {
            patrolWaitTime = 3f;
            patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
        }
        if (transform.position != patrolLocation)
        {
            transform.Translate((patrolLocation - transform.position) * Time.deltaTime * patrolSpeed);
        }
    }

    private void ChaseSound()
    {
        transform.Translate((lastSoundLocation - transform.position) * Time.deltaTime * chaseSpeed);
        if (transform.position == lastSoundLocation)
        {
            chasing = false;
        }
    }

    public void SetSoundLocation(Vector3 location)
    {
        if (Vector3.Distance(transform.position, location) <= 10)
        {
            lastSoundLocation = location;
            chasing = true;
        }
    }

    private void PuppyInteraction(GameObject puppy)
    {
        StartCoroutine(SpawnKind(puppy.transform.position));
        Destroy(puppy);
    }

    private IEnumerator SpawnKind(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(3);
        Instantiate(bunny, spawnPosition, transform.rotation);
    }
}
