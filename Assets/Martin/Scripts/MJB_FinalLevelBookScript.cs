using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;

public class MJB_FinalLevelBookScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JDH_GameplayStatics.ToggleUI(false);
        }
    }
}
