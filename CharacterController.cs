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

    public float peak;

    public PlayerHealth ph;

    public float height;


	// Use this for initialization
	void Start () {

        ph = GetComponent<PlayerHealth>();

		Cursor.lockState = CursorLockMode.Locked;

		rb = GetComponent<Rigidbody> ();

		Physics.gravity = new Vector3 (0, gravity, 0);

        height = transform.localScale.y;


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space") && (isGrounded))
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

        Grounded();
        FallDamage();
        
    }

    void OnCollisionStay(Collision coll){
		isCollide = true;
	}
	void OnCollisionExit(Collision coll){
        isCollide = false;
	}

    private void Grounded()
    {
        Vector3 jumpPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 jmp_fd = transform.position + transform.forward * .5f;
        Vector3 jmp_bk = transform.position - transform.forward * .5f;
        Vector3 jmp_lf = transform.position - transform.right * .5f;
        Vector3 jmp_rt = transform.position + transform.right * .5f;
        RaycastHit hit;
        RaycastHit hf;
        RaycastHit hb;
        RaycastHit hl;
        RaycastHit hr;
        Ray grounder = new Ray(jumpPoint, Vector3.down);
        Ray gr_fd = new Ray(jmp_fd, Vector3.down);
        Ray gr_bk = new Ray(jmp_bk, Vector3.down);
        Ray gr_lt = new Ray(jmp_lf, Vector3.down);
        Ray gr_rt = new Ray(jmp_rt, Vector3.down);
        if (Physics.Raycast(grounder, out hit, 500))
        {
         //   print("ctr");
            if (hit.distance > (height+.1f)) { isGrounded = false; }
            else { isGrounded = true; }
        }
        
        if (Physics.Raycast(gr_fd, out hf, 500))
        {
         //   print("fwd");
            if (hf.distance > (height + .1f)) { isGrounded = false; }
            else { isGrounded = true; }
        }
        if (Physics.Raycast(gr_bk, out hb, 500))
        {
        //    print("bck");
            if (hb.distance > (height + .1f)) { isGrounded = false; }
            else { isGrounded = true; }
        }
        if (Physics.Raycast(gr_lt, out hl, 500))
        {
         //   print("lft");
            if (hl.distance > (height + .1f)) { isGrounded = false; }
            else { isGrounded = true; }
        }
        if (Physics.Raycast(gr_rt, out hr, 500))
        {
         //   print("rgt");
            if (hr.distance > (height + .1f)) { isGrounded = false; }
            else { isGrounded = true; }
        }
    //    print(isGrounded);
       // print(hit.distance);
    }

    void FallDamage()
    {
        if (!isGrounded)
        {
          //  print("airborne");
            if (peak < transform.position.y)
            {
                peak = transform.position.y;
            }
        }
        else
        {
          //  print("grounded");
            if ((peak - transform.position.y) > 30)
            {
                int dmg = Mathf.RoundToInt((peak - transform.position.y) * 2);
                ph.Damage(dmg, transform.position);
                peak = transform.position.y;
            }
            else if ((peak - transform.position.y) > 10)
            {
                int dmg = Mathf.RoundToInt((peak - transform.position.y)*.5f);
                ph.Damage(dmg, transform.position);
                peak = transform.position.y;
            }
            else
            {
                peak = transform.position.y;
            }
        }
    }

}
