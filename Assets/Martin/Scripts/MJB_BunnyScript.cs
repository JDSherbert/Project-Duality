using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.AI;
using Sherbert.GameplayStatics;
using Sherbert.Framework;

public class MJB_BunnyScript : JDH_AIBaseFramework
{
    [SerializeField] private float cooldown = 3.0f;
    [SerializeField] private float spawnDelay = 3.0f;
    [SerializeField] private float maxListenDistance = 10.0f;

    private Vector3 patrolLocation;
    private Vector3 lastSoundLocation;
    private List<GameObject> dodgeTheseTraps;

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
        else if (collision.gameObject.GetComponent<MJB_FloorTileScript>())
        {
            TrapInteraction();
        }
    }

    public override void InitializeAI()
    {
        base.InitializeAI();
        patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
        baseProperties.patrolWaitTime = 3.0f;
        baseProperties.patrolSpeed = 0.5f;
        baseProperties.chaseSpeed = 3.0f;
        GetTrapsToDodge();
    }

    private void GetTrapsToDodge()
    {
        dodgeTheseTraps = new List<GameObject>();
        FindTraps("Water");
        FindTraps("Pit");
        //FindTraps("BearTrap");
    }

    private void FindTraps(string trapTag)
    {
        GameObject[] traps = GameObject.FindGameObjectsWithTag(trapTag);
        foreach (GameObject trap in traps)
        {
            dodgeTheseTraps.Add(trap);
        }
    }

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
            DodgeTheTraps();
            transform.Translate((patrolLocation - transform.position) * Time.deltaTime * baseProperties.patrolSpeed);
        }
    }

    private void ChaseSound()
    {
        DodgeTheTraps();
        transform.Translate((lastSoundLocation - transform.position) * Time.deltaTime * baseProperties.chaseSpeed);
        if (transform.position == lastSoundLocation)
        {
            baseProperties.chasing = false;
            baseProperties.patrolWaitTime = cooldown;
        }
    }

    private void DodgeTheTraps()
    {
        foreach (GameObject trap in dodgeTheseTraps)
        {
            if (Vector3.Distance(trap.transform.position, transform.position) <= 2)
            {
                if (!baseProperties.chasing)
                {
                    patrolLocation = transform.position;
                }
                else
                {
                    lastSoundLocation = transform.position;
                }
            }
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

    private void PlayerInteraction(GameObject playerObject)
    {
        // Add heavy onto player weight here
        playerObject.GetComponent<MJB_PlayerEnemyCountScript>().AddtoCount("bunny");
        gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
    }

    private void PuppyInteraction(GameObject puppy)
    {
        StartCoroutine(SpawnKind(puppy.transform.position));
        puppy.GetComponent<JDH_HealthSystem>().DealDamage();
    }

    private void TrapInteraction()
    {
        gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
    }

    private IEnumerator SpawnKind(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(baseProperties.self, spawnPosition, transform.rotation);
    }

    public override void Transformation()
    {
        base.Transformation();
    }

    public float GetSpeed()
    {
        if (baseProperties.chasing)
        {
            return baseProperties.chaseSpeed;
        }
        else if (!baseProperties.chasing && transform.position != patrolLocation)
        {
            return baseProperties.patrolSpeed;
        }
        return 0;
    }

    public Vector3 GetDirection()
    {
        if (!baseProperties.chasing && transform.position == patrolLocation)
        {
            return Vector3.zero;
        }
        else if (!baseProperties.chasing && transform.position != patrolLocation)
        {
            return patrolLocation - transform.position;
        }
        return lastSoundLocation - transform.position;
    }
}
