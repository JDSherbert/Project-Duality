using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sherbert.GameplayStatics;
using Sherbert.Framework;

public class MJB_FinalLevelBookScript : MonoBehaviour
{

    [SerializeField] private Canvas puzzleCanvas = null;

    private void Start()
    {
        puzzleCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JDH_GameplayStatics.ToggleUI(false);
            puzzleCanvas.enabled = true;
            collision.gameObject.GetComponent<JDH_PlayerController2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
