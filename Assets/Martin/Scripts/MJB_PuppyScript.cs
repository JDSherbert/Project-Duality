using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_PuppyScript : MonoBehaviour
{

    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private GameObject puppy = null;

    private Vector3 direction;
    private GameObject player;
    private GameObject currentDistraction;
    private bool distracted = false;
    private bool chasing = false;

    void Start()
    {
        direction = new Vector3(1, 0, 0);
        direction.Normalize();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (Sherbert.GameplayStatics.JDH_World.world == Sherbert.GameplayStatics.JDH_World.WorldState.Evil)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !chasing && !distracted)
        {
            direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2));
            direction.Normalize();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Distraction"))
        {
            distracted = false;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInteraction(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bird"))
        {
            BirdInteraction(collision.gameObject);
        }
    }

    private void CheckForDistractions()
    {
        GameObject[] distractions = GameObject.FindGameObjectsWithTag("Distraction");
        if (distractions.Length != 0)
        {
            List<float> distractionDistances = new List<float>();
            for (int i = 0; i < distractions.Length; i++)
            {
                distractionDistances.Add(Vector3.Distance(transform.position, distractions[i].transform.position));
            }
            distractions = SortDistances(distractionDistances, distractions);
            if (Vector3.Distance(transform.position, distractions[0].transform.position) <= 20)
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
        if (!chasing)
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
        transform.Translate(direction * Time.deltaTime * patrolSpeed);
    }

    private void CheckPlayerDistance()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 15)
        {
            chasing = true;
            direction = player.transform.position - transform.position;
            direction.Normalize();
        }
    }

    private void Chase()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * chaseSpeed);
    }

    private void ChaseDistraction()
    {
        direction = currentDistraction.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * Time.deltaTime * chaseSpeed);
    }

    private void PlayerInteraction(GameObject player)
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
        Instantiate(puppy, spawnPosition, transform.rotation);
    }
}
