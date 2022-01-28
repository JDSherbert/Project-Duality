using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MJB_ChaseSceneScript : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemies = null;
    [SerializeField] private float spawnCooldown = 3, distanceToOffScreen = 5;

    private GameObject player;
    private float maxCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxCooldown = spawnCooldown + 1;
        transform.position = new Vector3(transform.position.x, GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, transform.position.z);
    }

    void Update()
    {
        spawnCooldown -= Time.deltaTime;
        if (spawnCooldown <= 0)
        {
            spawnCooldown = Random.Range(2, (int)maxCooldown + 1);
            SpawnEnemy(enemies[Random.Range(0, enemies.Count)]);
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, new Vector3(player.transform.position.x, 0, 0) + (Vector3.right * (distanceToOffScreen + Random.Range(0, 6))), Quaternion.identity);
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
