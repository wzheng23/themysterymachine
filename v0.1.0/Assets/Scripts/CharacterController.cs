using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

	private float x;
	private float y;
	private float z;

	private bool isGrounded;
    private bool isCollide;

	private float togr;

	public float speed = 10.0F;
	public float jumpf = 300.0F;
	public float gravity = -1.0F; 

	public Rigidbody rb;


	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;

		rb = GetComponent<Rigidbody> ();

		Physics.gravity = new Vector3 (0, gravity, 0); 


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space") && (isGrounded && isCollide))
        {
            rb.AddForce(transform.up * jumpf);
        }
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}

	void FixedUpdate (){
		float translation = Input.GetAxis ("Vertical") * speed;
		float straffe = Input.GetAxis ("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate (straffe, 0, translation);

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

    void OnCollisionStay(Collision coll){
		isCollide = true;
	}
	void OnCollisionExit(Collision coll){
        isCollide = false;
	}
		
}
