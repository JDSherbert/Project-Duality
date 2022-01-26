using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MJB_ChaseSceneScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies = null;
    [SerializeField] private float spawnCooldown = 3, distanceToOffScreen = 5;

    private GameObject player;
    private float cooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cooldown = spawnCooldown;
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0)
        {
            spawnCooldown = cooldown;
            SpawnEnemy(enemies[Random.Range(0, enemies.Count)]);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, new Vector3(player.transform.position.x, 0, 0) + (Vector3.right * distanceToOffScreen), Quaternion.identity);
    }

    public void PlayerGotCaught()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("You win");
            //SceneManager.LoadScene(whatever scene happens next, LoadSceneMode.Single);
        }
    }
}
