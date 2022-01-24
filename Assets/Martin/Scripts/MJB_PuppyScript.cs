using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sherbert.AI;
using Sherbert.GameplayStatics;

public class MJB_PuppyScript : JDH_AIBaseFramework
{
    public float maxDistractionDistance = 20.0f;
    public float maxDetectionDistance = 15.0f;

    private Vector3 direction;
    private GameObject currentDistraction;
    private bool distracted = false;

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
        if (collision.gameObject.CompareTag(EntityTypes.WALL) && baseProperties.chasing && !distracted)
        {
            direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
            direction.Normalize();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(EntityTypes.DISTRACTION))
        {
            distracted = false;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(EntityTypes.PLAYER))
        {
            PlayerInteraction(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag(EntityTypes.BIRD))
        {
            BirdInteraction(collision.gameObject);
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
            transform.Translate(direction * Time.deltaTime * baseProperties.chaseSpeed);
        }
    }

    private void ChaseDistraction()
    {
        direction = currentDistraction.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * baseProperties.chaseSpeed);
    }

    private void PlayerInteraction(GameObject playerObject)
    {
        // Dim lights here 
        Destroy(gameObject);
    }

    private void BirdInteraction(GameObject bird)
    {
        StartCoroutine(SpawnKind(bird.transform.position));
        Destroy(bird);
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
        baseProperties.patrolSpeed = 1.0f;
        baseProperties.chaseSpeed = 3.0f;
        direction = new Vector3(1, 0, 0);
        direction.Normalize();
        AcquireTarget();
    }

    //! Override - update behaviour
    public override void BehaviourHandler()
    {
        base.BehaviourHandler();
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

    //! Override - world transform state
    public override void Transformation()
    {
        base.Transformation();
    }
}
