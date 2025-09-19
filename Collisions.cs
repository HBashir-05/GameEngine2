using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public GameObject player;
    public float playerHeight = 2f;
    private GameObject hitObj;
    public LayerMask ground;
    RaycastHit hit;

    private void Update()
    {
        if(Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground)) //creates a raycast based on the players hight
        {
            if (hit.collider != null) //checks if the player has made contact with the raycast
            {
                player.transform.SetParent(hit.transform);
                Debug.Log("Collision");
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
    }


    private void OnCollisionExit(Collision collision)
    {
        player.transform.SetParent(null);
    }

   
}
