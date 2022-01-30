using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;

public class MJB_ChaseSceneBossScript : MonoBehaviour
{

    private Vector3 bossMoveSpeed;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossMoveSpeed = JDH_GameplayStatics.GetPlayerChaseSpeed();
    }

    void Update()
    {
        bossMoveSpeed = JDH_GameplayStatics.GetPlayerChaseSpeed();
        MoveBoss();
    }

    private void FixedUpdate()
    {
        
    }

    private void MoveBoss()
    {
        transform.Translate((Time.fixedDeltaTime * bossMoveSpeed) / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bird(Clone)" || collision.gameObject.name == "Puppy(Clone)" || collision.gameObject.name == "Bunny(Clone)")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("ChaseController").GetComponent<MJB_ChaseSceneScript>().PlayerGotCaught();
        }
    }
}
