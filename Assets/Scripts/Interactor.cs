using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable 
{
    public void Interact(GameObject Character);
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public GameObject Character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = Character.GetComponent(typeof(Player)) as Player;
        player.position = Character.transform.position;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {                     
                    interactObj.Interact(Character);
                }
            }
        }
    }
}
