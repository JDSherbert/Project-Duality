using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MJB_BunnySoundManager : MonoBehaviour
{

    [SerializeField] private float maxListenDistance = 5f;

    private List<GameObject> allBunnies;

    private void Awake()
    {
        allBunnies = new List<GameObject>();
    }

    public void AddBunny(GameObject bunny)
    {
        allBunnies.Add(bunny);
    }

    public void RemoveBunny(GameObject bunny)
    {
        allBunnies.Remove(bunny);
    }

    public void ReceiveSound(Vector3 position, float intensity)
    {
        foreach (GameObject bunny in allBunnies)
        {
            float dist = Vector3.Distance(position, bunny.transform.position);
            if (dist <= maxListenDistance)
            {
                float soundValue = (intensity / dist) * 100;
                if (soundValue > 100)
                {
                    soundValue = 100;
                }
                bunny.GetComponent<MJB_BunnyScript>().SetSoundLocation(position, soundValue);
            }
        }
    }
}
