using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChaseSceneCameraScript : MonoBehaviour
{

    private float moveSpeed, moveSpeedMultiplier;

    void Start()
    {
        moveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<MJB_ChaseScenePlayerScript>().GetChaseSpeed();
        moveSpeedMultiplier = GameObject.FindGameObjectWithTag("Player").GetComponent<MJB_ChaseScenePlayerScript>().GetChaseSpeedMultiplier();
    }
    private void Update()
    {
        moveSpeed *= moveSpeedMultiplier;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }
}
