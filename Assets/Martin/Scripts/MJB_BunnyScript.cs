using System.Collections;
using UnityEngine;

using Sherbert.AI;
using Sherbert.GameplayStatics;

public class MJB_BunnyScript : JDH_AIBaseFramework
{
    public float cooldown = 3.0f;
    public float maxListenDistance = 10.0f;

    private Vector3 patrolLocation;
    private Vector3 lastSoundLocation;

    void Start()
    {
        InitializeAI();
    }

    private void FixedUpdate()
    {
        BehaviourHandler();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(EntityTypes.PLAYER))
        {
            PlayerInteraction(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(EntityTypes.PUPPY))
        {
            PuppyInteraction(collision.gameObject);
        }
    }

    private void Patrol()
    {
        baseProperties.patrolWaitTime -= Time.deltaTime;
        if (baseProperties.patrolWaitTime <= 0)
        {
            baseProperties.patrolWaitTime = cooldown;
            patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
        }
        if (transform.position != patrolLocation)
        {
            transform.Translate((patrolLocation - transform.position) * Time.deltaTime * baseProperties.patrolSpeed);
        }
    }

    private void ChaseSound()
    {
        transform.Translate((lastSoundLocation - transform.position) * Time.deltaTime * baseProperties.chaseSpeed);
        if (transform.position == lastSoundLocation)
        {
            baseProperties.chasing = false;
        }
    }

    public void SetSoundLocation(Vector3 location)
    {
        if (Vector3.Distance(transform.position, location) <= maxListenDistance)
        {
            lastSoundLocation = location;
            baseProperties.chasing = true;
        }
    }

    private void PlayerInteraction(GameObject player)
    {
        // Add heavy onto player weight here
        Destroy(gameObject);
    }

    private void PuppyInteraction(GameObject puppy)
    {
        StartCoroutine(SpawnKind(puppy.transform.position));
        Destroy(puppy);
    }

    private IEnumerator SpawnKind(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(3);
        Instantiate(baseProperties.self, spawnPosition, transform.rotation);
    }

    //!------------- VIRTUAL METHODS ----------------!//

    //! Override - Sets defaults
    public override void InitializeAI()
    {
        base.InitializeAI();
        patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
        baseProperties.patrolWaitTime = 3.0f;
        baseProperties.patrolSpeed = 0.5f;
        baseProperties.chaseSpeed = 3.0f;
    }

    //! Override - update behaviour
    public override void BehaviourHandler()
    {
        base.BehaviourHandler();
        if (JDH_World.GetWorldIsEvil())
        {
            if (!baseProperties.chasing)
            {
                Patrol();
            }
            else
            {
                ChaseSound();
            }
        }
    }

    //! Override - world transform state
    public override void Transformation()
    {
        base.Transformation();
    }
}
