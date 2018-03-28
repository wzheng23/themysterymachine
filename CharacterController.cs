using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //handes all character movement, takes in input from keyboard
public class CharacterController : MonoBehaviour {

	private float x;
	private float y;
	private float z;

	private bool isGrounded;
    private bool isCollide;

	public float speed = 10.0F;
	public float jumpf = 300.0F;
	public float gravity = -1.0F; 

	public Rigidbody rb;    //handles all physics


	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;

		rb = GetComponent<Rigidbody> ();

		Physics.gravity = new Vector3 (0, gravity, 0); 


	}
	
	// Update is called once per frame
	void Update () {

            //jump
        if (Input.GetKeyDown("space") && (isGrounded && isCollide))
        {
                //jump force is only added when the player is on the ground
            rb.AddForce(transform.up * jumpf);
        }
            //pause
        if (Input.GetKeyDown("escape"))
        {
                //enables cursor while paused
            Cursor.lockState = CursorLockMode.None;
        }
	}

        //Fixed update is called every fixed framerate, typically used for all code related to rigidbody
	void FixedUpdate (){
		float translation = Input.GetAxis ("Vertical") * speed;
		float straffe = Input.GetAxis ("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate (straffe, 0, translation);

            //Raycast object is used to detect whether the player is on the ground
        Vector3 jumpPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RaycastHit hit;
        Ray grounder = new Ray(jumpPoint, Vector3.down);
        if (Physics.Raycast(grounder, out hit, 10))
        {
            if (hit.distance > 1.5)
            {
                isGrounded = false;
                // print("airborne");
            }
            else
            {
                isGrounded = true;
                // print("grounded");
            }
        }

    }
        //sets isCollide to true if player object is in contact with any level geometry
    void OnCollisionStay(Collision coll){
		isCollide = true;
	}
        //sets isCollide to false if player object is not in contact with any level geometry
    void OnCollisionExit(Collision coll){
        isCollide = false;
	}
		
}
