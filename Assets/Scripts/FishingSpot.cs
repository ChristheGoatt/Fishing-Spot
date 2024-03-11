using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpot : MonoBehaviour, IInteractable
{
    private bool occupied = false;
    private float targetTime = 6.0f;
    private float remainingTime = 6.0f;

    public void Interact()
    {
        if (occupied)
        {
            Debug.Log("Stopped fishing.");
            remainingTime = targetTime;
        }
        else
        {
            Debug.Log("Started fishing.");
        }
        
        occupied = !occupied;
    }

    private void Fish()
    {
        Debug.Log(Random.Range(0, 5) + " Fish caught.");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (occupied)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0.0f)
            {
                Fish();
                remainingTime = targetTime;
            }
        }
    }
}
