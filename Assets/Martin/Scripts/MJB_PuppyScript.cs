using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.AI;
using Sherbert.GameplayStatics;
using Sherbert.Framework;

public class MJB_PuppyScript : JDH_AIBaseFramework
{
    [SerializeField] private float maxDistractionDistance = 20.0f;
    [SerializeField] private float maxDetectionDistance = 15.0f;
    [SerializeField] private float spawnDelay = 3.0f;

    private Vector3 direction;
    private GameObject currentDistraction;
    private bool distracted = false, checkedTraps = false;
    private List<GameObject> dodgeTheseTraps;

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
        if (collision.gameObject.CompareTag(EntityTypes.WALL) && !baseProperties.chasing && !distracted)
        {
            direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
            direction.Normalize();
            GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().ReceiveSound(transform.position, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(EntityTypes.DISTRACTION))
        {
            distracted = false;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(EntityTypes.PLAYER) || collision.gameObject.CompareTag(EntityTypes.BIRD))
        {
            RunInteraction(collision.gameObject.tag, collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<MJB_TrapTileBehaviour>())
        {
            TrapInteraction(collision.gameObject);
        }
    }

    private void RunInteraction(string interactionType, GameObject interactor)
    {
        if (interactionType == EntityTypes.PLAYER)
        {
            PlayerInteraction(interactor);
        }
        else if (interactionType == EntityTypes.BIRD)
        {
            BirdInteraction(interactor);
        }
    }

    public override void InitializeAI()
    {
        base.InitializeAI();
        baseProperties.patrolSpeed = 1.0f;
        baseProperties.chaseSpeed = 3.0f;
        direction = new Vector3(1, 0, 0);
        direction.Normalize();
        baseProperties.target = base.AcquireTarget();
        dodgeTheseTraps = new List<GameObject>();
    }

    private void GetTrapsToDodge()
    {
        FindTraps("FloorSpikes");
        FindTraps("Explosives");
        FindTraps("StickyGoo");
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
            CheckForDistractions();
            if (!distracted)
            {
                NotDistracted();
            }
            else
            {
                ChaseDistraction();
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

    private void CheckForDistractions()
    {
        GameObject[] distractions = GameObject.FindGameObjectsWithTag(EntityTypes.DISTRACTION);
        if (distractions.Length != 0)
        {
            List<float> distractionDistances = new List<float>();
            for (int i = 0; i < distractions.Length; i++)
            {
                distractionDistances.Add(Vector3.Distance(transform.position, distractions[i].transform.position));
            }
            distractions = SortDistances(distractionDistances, distractions);
            if (Vector3.Distance(transform.position, distractions[0].transform.position) <= maxDistractionDistance)
            {
                SetDistractedObject(distractions[0]);
            }
        }
    }

    private GameObject[] SortDistances(List<float> distances, GameObject[] distractionObjects)
    {
        for (int j = 0; j < distances.Count; j++)
        {
            for (int i = 0; i < distances.Count - 1; i++)
            {
                if (distances[i] > distances[i + 1])
                {
                    distances = SwapDistances(distances, i);
                    distractionObjects = SwapObjects(distractionObjects, i);
                }
            }
        }

        return distractionObjects;
    }

    private List<float> SwapDistances(List<float> distances, int i)
    {
        float temp = distances[i + 1];
        distances[i + 1] = distances[i];
        distances[i] = temp;

        return distances;
    }

    private GameObject[] SwapObjects(GameObject[] objects, int i)
    {
        GameObject tempObj = objects[i + 1];
        objects[i + 1] = objects[i];
        objects[i] = tempObj;

        return objects;
    }

    private void SetDistractedObject(GameObject distraction)
    {
        direction = distraction.transform.position - transform.position;
        direction.Normalize();
        currentDistraction = distraction;
        distracted = true;
    }

    private void NotDistracted()
    {
        if (!baseProperties.chasing)
        {
            Patrol();
            CheckPlayerDistance();
        }
        else
        {
            Chase();
        }
    }

    private void Patrol()
    {
        DodgeTheTraps();
        transform.Translate(direction * Time.deltaTime * baseProperties.patrolSpeed);
    }

    private void CheckPlayerDistance()
    {
        if (baseProperties.target != null)
        {
            if (Vector3.Distance(baseProperties.target.transform.position, transform.position) <= maxDetectionDistance)
            {
                baseProperties.chasing = true;
                direction = baseProperties.target.transform.position - transform.position;
                direction.Normalize();
            }
        }
    }

    private void Chase()
    {
        if (baseProperties.target != null)
        {
            direction = baseProperties.target.transform.position - transform.position;
            direction.Normalize();
            DodgeTheTraps();
            transform.Translate(direction * Time.deltaTime * baseProperties.chaseSpeed);
        }
    }

    private void ChaseDistraction()
    {
        direction = currentDistraction.transform.position - transform.position;
        direction.Normalize();
        DodgeTheTraps();
        transform.Translate(direction * Time.deltaTime * baseProperties.chaseSpeed);
    }

    private void DodgeTheTraps()
    {
        foreach (GameObject trap in dodgeTheseTraps)
        {
            if (Vector3.Distance(trap.transform.position, transform.position) <= 2)
            {
                direction *= -1;
            }
        }
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        // Dim lights here 
        playerObject.GetComponent<MJB_PlayerEnemyCountScript>().AddtoCount("puppy");
        gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
    }

    private void BirdInteraction(GameObject bird)
    {
        StartCoroutine(SpawnKind(bird.transform.position));
        bird.GetComponent<JDH_HealthSystem>().DealDamage();
    }

    private void TrapInteraction(GameObject trap)
    {
        trap.GetComponent<MJB_TrapTileBehaviour>().UncoverTrap();
        if (trap.CompareTag("Alarm"))
        {
            GameObject.Find("BunnySoundManager").GetComponent<MJB_BunnySoundManager>().ReceiveSound(transform.position, 100f);
        }
        else
        {
            gameObject.GetComponent<JDH_HealthSystem>().DealDamage();
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
        if (baseProperties.chasing || distracted)
        {
            return baseProperties.chaseSpeed;
        }
        else
        {
            return baseProperties.patrolSpeed;
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }
}
