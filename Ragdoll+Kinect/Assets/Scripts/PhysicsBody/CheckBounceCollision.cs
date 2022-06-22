using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBounceCollision : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Terrain")
        {
            playerController.isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        playerController.isGrounded = false;
    }
}
