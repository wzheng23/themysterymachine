using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwapManager : MonoBehaviour
{

    public GameObject initial;
    private bool reorient;

    private GameObject c_enemy;
    private Rigidbody c_rb;
    private GameObject player;
    private Camera fpsCam;

    private GameObject n_weapon;

    private IDamageable dmgScript;
    private PlayerHealth ph;
    private CharacterController ch;
    //private EnemyShoot shotScript;

    // Use this for initialization
    void Start()
    {
        c_enemy = initial;
        dmgScript = c_enemy.GetComponent<IDamageable>();
        player = this.gameObject;
        fpsCam = player.GetComponentInChildren<Camera>();
        reorient = false;
        ph = GetComponent<PlayerHealth>();
        ch = GetComponent<CharacterController>();
    }

    public void Swap(GameObject n_enemy)
    {

        //enable current enemy
        c_enemy.transform.parent = null;

        CapsuleCollider col = c_enemy.GetComponent<CapsuleCollider>();
        col.enabled = true;

        c_rb = c_enemy.GetComponent<Rigidbody>();
        c_rb.isKinematic = false;
        c_rb.useGravity = true;

        //        c_enemy.GetComponent<MeshRenderer>().enabled = true;

        for (int i = 0; i < c_enemy.transform.childCount - 1; i++)
        {
            c_enemy.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = true; //
        }

        if (dmgScript.Threshold() > 0)
        {

            c_enemy.GetComponent<Animator>().enabled = true;                                      //
            c_enemy.GetComponent<AgentScript>().enabled = true;
            c_enemy.GetComponent<EnemyPOV>().enabled = true;
            c_enemy.GetComponent<NavMeshAgent>().enabled = true;

        }
        else
        {
            col.radius = .2f;
            c_enemy.transform.Rotate(-2, 0, 0);
            c_enemy.tag = "Corpse";
        }

        //disable new enemy

        n_enemy.GetComponent<Collider>().enabled = false;



        //     n_enemy.GetComponent<MeshRenderer>().enabled = false;

        for (int i = 0; i < c_enemy.transform.childCount - 1; i++)
        {
            n_enemy.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = false; 
        }

        c_rb = n_enemy.GetComponent<Rigidbody>();
        c_rb.isKinematic = true;
        c_rb.useGravity = false;

        var destination = n_enemy.transform.position;
        var originalPos = player.transform.position;
        player.transform.position = destination;

        n_enemy.GetComponent<Animator>().enabled = false;                                 
        n_enemy.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;                
        n_enemy.GetComponent<NavMeshAgent>().enabled = false;
        n_enemy.GetComponent<EnemyPOV>().enabled = false;
        n_enemy.GetComponent<AgentScript>().enabled = false;                                
        ch.peak = 0;

        if (reorient == true)
        {
            print(n_enemy.transform.rotation.eulerAngles.y);
            player.transform.localEulerAngles = new Vector3(0, n_enemy.transform.rotation.eulerAngles.y, 0);
        }

        n_enemy.transform.parent = player.transform;

        if (dmgScript.Threshold() <= 0)
        {

        //    dmgScript.Dead();
        }

        c_enemy = n_enemy;
        dmgScript = c_enemy.GetComponent<IDamageable>();

        //gethealth

        ph.setHealth(n_enemy.GetComponent<EnemyHealth>().currentHealth);            //
   
    }
}
