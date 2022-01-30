using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_BirdTrigger : MonoBehaviour
{

    private GameObject player;
    private List<GameObject> allBirds;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        allBirds = new List<GameObject>();
    }

    public void AddBird(GameObject bird)
    {
        allBirds.Add(bird);
    }

    public void RemoveBird(GameObject bird)
    {
        allBirds.Remove(bird);
    }

    public void TriggerAllBirds()
    {
        foreach(GameObject bird in allBirds)
        {
            bird.GetComponent<MJB_BirdScript>().TriggerBird();
        }
    }

    private void Update()
    {
        foreach (GameObject bird in allBirds)
        {
            if (Vector3.Distance(player.transform.position, bird.transform.position) <= 1)
            {
                bird.GetComponent<MJB_BirdScript>().TriggerBird();
            }
        }
    }
}
