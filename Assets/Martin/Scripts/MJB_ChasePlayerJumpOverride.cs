using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_ChasePlayerJumpOverride : MonoBehaviour
{

    [SerializeField] private float jumpHeight = 1, jumpSpeed = 1, fallSpeed = 1;

    private GameObject player;
    private bool jumping = false;
    private float yPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        yPos = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            StartCoroutine(PlayerIsJumping());
        }
    }

    private IEnumerator PlayerIsJumping()
    {
        jumping = true;
        while (player.transform.position.y < jumpHeight)
        {
            player.transform.Translate(Vector3.up * Time.deltaTime * jumpSpeed);
            yield return null;
        }
        while (player.transform.position.y > yPos)
        {
            player.transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
            yield return null;
        }
        player.transform.position = new Vector3(player.transform.position.x, yPos, player.transform.position.z);
        jumping = false;
    }
}
