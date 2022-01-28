using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChaseSceneBossScript : MonoBehaviour
{

    private float bossMoveSpeed, moveSpeedMultiplier;

    void Start()
    {
        bossMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<MJB_ChaseScenePlayerScript>().GetChaseSpeed();
        moveSpeedMultiplier = GameObject.FindGameObjectWithTag("Player").GetComponent<MJB_ChaseScenePlayerScript>().GetChaseSpeedMultiplier();
    }

    void Update()
    {
        bossMoveSpeed *= moveSpeedMultiplier;
    }

    private void FixedUpdate()
    {
        MoveBoss();
    }

    private void MoveBoss()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bossMoveSpeed);
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
