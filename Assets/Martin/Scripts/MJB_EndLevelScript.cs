using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sherbert.Application;

public class MJB_EndLevelScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int index = SceneManager.GetActiveScene().buildIndex + 1;
            JDH_ApplicationManager.LoadSceneAsync(index);
        }
    }
}
