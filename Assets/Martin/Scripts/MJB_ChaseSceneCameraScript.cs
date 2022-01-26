using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChaseSceneCameraScript : MonoBehaviour
{

    private float moveSpeed;

    void Start()
    {
        moveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<MJB_ChaseScenePlayerScript>().GetChaseSpeed();
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
