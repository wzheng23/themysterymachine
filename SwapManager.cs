using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    //Script handles possession of shot enemies
public class SwapManager : MonoBehaviour {

    public GameObject initial;
    private bool reorient;

    private GameObject c_enemy;     //currently possessed enemy
    private Rigidbody c_rb;
    private GameObject player;
    private Camera fpsCam;

    private GameObject n_weapon;

    private IDamageable dmgScript;
    //private EnemyShoot shotScript;

	// Use this for initialization
	void Start () {
        c_enemy = initial;
        dmgScript = c_enemy.GetComponent<IDamageable>();
        player = this.gameObject;
        fpsCam = player.GetComponentInChildren<Camera>();
        reorient = false;
	}
        
        //Called by W_Makarov when an enemy health is below set threshold
        //Player becomes n_enemy.
    public void Swap(GameObject n_enemy)
    {

            //enable current enemy, unparent from player
        c_enemy.transform.parent = null;
        c_enemy.GetComponent<Collider>().enabled = true;

        c_rb = c_enemy.GetComponent<Rigidbody>();
            //enable physics related to enemy
        c_rb.isKinematic = false;
        c_rb.useGravity = true;

        c_enemy.GetComponent<MeshRenderer>().enabled = true;

            //disable new enemy

            //disables collision
        n_enemy.GetComponent<Collider>().enabled = false;

            //disables visual
        n_enemy.GetComponent<MeshRenderer>().enabled = false;

        c_rb = n_enemy.GetComponent<Rigidbody>();
        c_rb.isKinematic = true;
        c_rb.useGravity = false;

            //moves player to where enemy was
        var destination = n_enemy.transform.position;
        var originalPos = player.transform.position;
        player.transform.position = destination;
;
            //experiment used to make player face direction n_enemy was facing when shot, currently unused
        if (reorient == true)
        {
            print(n_enemy.transform.rotation.eulerAngles.y);
            player.transform.localEulerAngles = new Vector3(0, n_enemy.transform.rotation.eulerAngles.y, 0);
        }
            //sets player as parent for new enemy
        n_enemy.transform.parent = player.transform;

            //kills unpossessed enemy if threshold is <=0
        if(dmgScript.Threshold() <= 0)
        {
            dmgScript.Dead();
        }

        c_enemy = n_enemy;
        dmgScript = c_enemy.GetComponent<IDamageable>();

        //gethealth


    }
}
