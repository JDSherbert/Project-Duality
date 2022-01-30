using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_TeleportPointScript : MonoBehaviour
{

    [SerializeField] private int pairedIndex = 0;
    private GameObject teleportManager;
    public Vector3 spawnPlayerDirection;

    private void Start()
    {
        teleportManager = GameObject.Find("TeleportationManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject otherPoint = teleportManager.GetComponent<MJB_TeleportationManager>().teleportPoints[pairedIndex];
            collision.gameObject.transform.position = otherPoint.transform.position + otherPoint.GetComponent<MJB_TeleportPointScript>().spawnPlayerDirection;
        }
    }
}
