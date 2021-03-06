using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChaseScenePlayerScript : MonoBehaviour
{

    [SerializeField] private float playerMoveSpeed = 7, damageDistanceMultiplier = 2, jumpHeight = 5, jumpSpeed = 2, fallSpeed = 5, moveSpeedMultiplier = 1.0001f;

    private bool jumping = false;
    private float yPos;

    void Start()
    {
        yPos = transform.position.y;
    }

    void Update()
    {
        DodgeEnemy();

        playerMoveSpeed *= moveSpeedMultiplier;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.Translate(Vector3.right * Time.deltaTime * playerMoveSpeed);
    }

    private void DodgeEnemy()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(PlayerIsJumping());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bird(Clone)" || collision.gameObject.name == "Puppy(Clone)" || collision.gameObject.name == "Bunny(Clone)")
        {
            transform.Translate(Vector3.left * Time.deltaTime * playerMoveSpeed * damageDistanceMultiplier);
            Destroy(collision.gameObject);
        }
    }

    public float GetChaseSpeed()
    {
        return playerMoveSpeed;
    }

    public float GetChaseSpeedMultiplier()
    {
        return moveSpeedMultiplier;
    }

    private IEnumerator PlayerIsJumping()
    {
        jumping = true;
        while (transform.position.y < jumpHeight)
        {
            transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed);
            yield return null;
        }
        while (transform.position.y > yPos)
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        jumping = false;
    }
}
