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

    private Vector3 patrolLocation;
    private Vector3 lastSoundLocation;
    private List<GameObject> dodgeTheseTraps;
    private bool checkedTraps = false;

    void Start()
    {
        InitializeAI();
    }

    private void FixedUpdate()
    {
        BehaviourHandler();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (JDH_World.GetWorldIsEvil())
        {
            if (collision.gameObject.CompareTag(EntityTypes.PLAYER))
            {
                PlayerInteraction(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag(EntityTypes.PUPPY))
            {
                PuppyInteraction(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag(EntityTypes.WALL))
            {
                transform.Translate(GetDirection() * -1 * Time.deltaTime * baseProperties.chaseSpeed);
                lastSoundLocation = transform.position;
                patrolLocation = transform.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<MJB_TrapTileBehaviour>() && JDH_World.GetWorldIsEvil())
        {
            TrapInteraction(collision.gameObject);
        }
    }

    public override void InitializeAI()
    {
        base.InitializeAI();
        GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().AddBunny(gameObject);
        lastSoundLocation = Vector3.zero;
        patrolLocation = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2));
        baseProperties.patrolWaitTime = 3.0f;
        baseProperties.patrolSpeed = 0.5f;
        baseProperties.chaseSpeed = 1.5f;
        dodgeTheseTraps = new List<GameObject>();
    }

    private void GetTrapsToDodge()
    {
        FindTraps("Water");
        FindTraps("Pit");
        FindTraps("BearTrap");
        checkedTraps = true;
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
        base.UpdateAnimator(GetDirection());
        TrapCheck();
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

    private void TrapCheck()
    {
        if (!checkedTraps)
        {
            GetTrapsToDodge();
        }
    }

    private void Patrol()
    {
        baseProperties.patrolWaitTime -= Time.deltaTime;
        if (baseProperties.patrolWaitTime <= 0)
        {
            baseProperties.patrolWaitTime = cooldown;
            patrolLocation = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0);
            CheckPatrol();
        }
        if (transform.position != patrolLocation)
        {
            DodgeTheTraps();
            transform.Translate((patrolLocation - transform.position) * Time.deltaTime * baseProperties.patrolSpeed);
        }
    }

    private void CheckPatrol()
    {
        Vector3 direction = patrolLocation - transform.position;
        direction.Normalize();
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position + direction, direction, Vector3.Distance(patrolLocation, transform.position));
        if (wallHit.collider)
        {
            patrolLocation = transform.position;
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
                if (baseProperties.chasing)
                {
                    baseProperties.chasing = false;
                }
                baseProperties.patrolWaitTime = cooldown;
                patrolLocation = transform.position - trap.transform.position;
            }
        }
    }

    public void SetSoundLocation(Vector3 location, float soundValue)
    {
        float doesBunnyChase = Random.Range(0f, 100f);
        if (doesBunnyChase <= soundValue)
        {
            lastSoundLocation = location;
            baseProperties.chasing = true;
        }
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        // Add heavy onto player weight here
        playerObject.GetComponent<JDH_HealthSystem>().DealDamage();
        gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
        GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().RemoveBunny(gameObject);
        Destroy(gameObject);
    }

    private void PuppyInteraction(GameObject puppy)
    {
        StartCoroutine(SpawnKind(puppy.transform.position));
        puppy.GetComponent<JDH_HealthSystem>().DealDamage();
        Destroy(puppy);
    }

    private void TrapInteraction(GameObject trap)
    {
        trap.GetComponent<MJB_TrapTileBehaviour>().UncoverTrap();
        if (trap.CompareTag("Alarm"))
        {
            GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().ReceiveSound(transform.position, 100f);
            GameObject.Find("BirdTrigger").GetComponent<MJB_BirdTrigger>().TriggerAllBirds();
            Destroy(trap);
        }
        else
        {
            GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().RemoveBunny(gameObject);
            gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
            Destroy(gameObject);
        }
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
