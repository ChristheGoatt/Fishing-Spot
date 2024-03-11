using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishingSpot : MonoBehaviour, IInteractable
{
    private Dictionary<Player, float> occupants = new Dictionary<Player, float>();
    private Dictionary<Player, Vector3> starting_points = new Dictionary<Player, Vector3>();
    private float targetTime = 6.0f;
    private float allowedDistance = 4.0f;

    public void Interact(GameObject Character)
    {
        Player player = Character.GetComponent(typeof(Player)) as Player;

        if (Vector3.Distance(transform.position, Character.transform.position) < allowedDistance)
        {
            if (occupants.ContainsKey(player))
            {
                Debug.Log("Stopping Fishing.");
                occupants.Remove(player);
                starting_points.Remove(player);
                player.occupied = false;
            }
            else
            {
                if (!player.occupied)
                {
                    Debug.Log("Started fishing.");
                    player.occupied = true;
                    occupants.Add(player, targetTime);
                    starting_points.Add(player, player.position);
                }
                else
                {
                    Debug.Log("Player is already occupied.");
                }
            }  
        }
        else 
        {
            Debug.Log("Player is too far away.");
        }
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
        foreach (Player key in occupants.Keys.ToList())
        {
            occupants[key] -= Time.deltaTime;

            if (occupants[key] <= 0.0f)
            {
                Fish();
                occupants[key] = targetTime;
            }

            if (Vector3.Distance(transform.position, key.position) > allowedDistance || Vector3.Distance(starting_points[key], key.position) > 0.3f)
            {
                Debug.Log("Player is too far away or moved too much, cancelling action.");                
                occupants.Remove(key);
                starting_points.Remove(key);
                key.occupied = false;
            }
        }
    }
}
