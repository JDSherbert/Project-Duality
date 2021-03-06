using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChaseSceneAIScript : MonoBehaviour
{

    [SerializeField] private float jumpHeight = 5, jumpSpeed = 10, fallSpeed = 5, playerPushBack = 1;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumping)
        {
            StartCoroutine(EnemyJump());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.Translate(Vector3.left * Time.deltaTime * playerPushBack);
            Destroy(gameObject);
        }
    }

    private IEnumerator EnemyJump()
    {
        jumping = true;
        while (transform.position.y < jumpHeight)
        {
            transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed);
            yield return null;
        }
        while (transform.position.y > 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
            yield return null;
        }
        jumping = false;
    }
}
