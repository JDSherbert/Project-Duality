using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sherbert.GameplayStatics;
using Sherbert.Framework;
using Sherbert.Application;

public class MJB_EndGameLoop : MonoBehaviour
{

    private GameObject player;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
        JDH_GameplayStatics.ToggleUI(false);
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<JDH_PlayerController2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !loading)
        {
            JDH_ApplicationManager.LoadSceneAsync(0);
            loading = true;
        }
    }
}
