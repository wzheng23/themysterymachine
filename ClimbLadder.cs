using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    /*Script allows player to climb ladders*/
public class ClimbLadder : MonoBehaviour {

    private CharacterController chController;
    private Rigidbody playerBody;
    public Transform chPos;
    public bool canClimb = false;
    private float speed = .25f;

        //called upon initialization
    void Start()
    {
        chController = gameObject.GetComponent<CharacterController>();
        playerBody = gameObject.GetComponent<Rigidbody>();
    }
        
        //When player enters predefined area
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            playerBody.useGravity = false;
            canClimb = true;
        }
    }
        //When player leaves predefined area
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            playerBody.useGravity = true;
            canClimb = false;
        }
    }

    void Update()
    {
        if (canClimb)
        {
                //allows players to move vertically up or down when on ladder, removes ability to move forward or backward
            if (Input.GetKey("w"))
            {
                chPos.transform.position += Vector3.up * speed;
            }
            if (Input.GetKey("s"))
            {
                chPos.transform.position += Vector3.down * speed;
            }
        }
    }
}
