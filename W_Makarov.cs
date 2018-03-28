using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //Weapon script, currently all weapons use same W_Makarov script with altered public variables
    //Weapons are saved as prefabs after creation.
public class W_Makarov : MonoBehaviour, IShootable {

    public bool equipped = false;
    public bool reloading = false;

    public bool automatic = false;

    public bool swap = false;

    public float fireRate = .25f;
    public float range = 50;
    public float spread = .05f;     //inaccuracy
    public float off_x;             //shot deviation x
    public float off_y;             //shot deviation x
    public int damage = 1;
    public Transform gunEnd;        
    public float perfectFireTime = .35f;    //unused
    public float equipDelay = .35f;         //unused

        //coordinates coresponding to where weapon should appear on screen in relation to camera when equipped.
    public static float x = .325f;      
    public static float y = -.264f;
    public static float z = .735f; 

    public Vector3 cam_pos = new Vector3(x, y, z);

    public int max_reserve = 80;    
    public int cur_reserve = 80;
    public int cur_clip = 8;

    public int clip = 8;            //weapon clip size
    public float t_reload = 1.5f;   //reload time

    private Camera fpsCam = null;
    private GameObject player = null;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(.07f);
    private float nextFireTime;

    private SwapManager sm;         //handles possession
    private WeaponManager wm;       //handles weapon swapping

        //Called when script is enabled
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
        player = GameObject.Find("Player");
        sm = player.GetComponent<SwapManager>();
        wm = player.GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
                //draws line from gunEnd to center of screen
            RaycastHit hit;
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
  
                //shoot function, automatic weapons, nearly identical to semi-auto, see below
            if (automatic && Input.GetButton("Fire1") && (Time.time > nextFireTime))
            {
                if (cur_clip > 0)
                {
                    nextFireTime = Time.time + fireRate;
                    cur_clip = cur_clip - 1;
                    Vector2 off_spr = Random.insideUnitCircle.normalized * spread;
                    off_x = off_spr.x;
                    off_y = off_spr.y;
                    Vector3 spr = Quaternion.AngleAxis(off_x, Vector3.right) * Quaternion.AngleAxis(off_y, Vector3.up) * fpsCam.transform.forward;

                    if (Physics.Raycast(rayOrigin, spr, out hit, range))
                    {
                        GameObject n_enemy = hit.collider.gameObject;
                        IDamageable dmgScript = hit.collider.gameObject.GetComponent<EnemyHealth>();

                        if (dmgScript != null)
                        {
                            dmgScript.Damage(damage, hit.point);
                            if (dmgScript.Health() <= (dmgScript.Threshold())) {
                                dmgScript.SetNextThreshold(dmgScript.Health() - dmgScript.Threshold());
                                sm.Swap(n_enemy);
                            //    wm.SwapEquip(n_enemy);
                            }
                        }

                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * 100f);
                        }
                            //debug, draws line from gun end to center of screen
                        lineRenderer.SetPosition(0, gunEnd.position); 
                        lineRenderer.SetPosition(1, hit.point);
                    }
                    StartCoroutine(ShotEffect());
                }
                else if(Time.time > nextFireTime)
                {
                    //empty gun sound
                }
            }
                //shoot function semi-auto
            else if (Input.GetButtonDown("Fire1") && (Time.time > nextFireTime))
            {
                if (cur_clip > 0)
                {
                        //sets next time on clock when player can fire again
                    nextFireTime = Time.time + fireRate;
                    cur_clip = cur_clip - 1;

                        //calculates spread on weapon, randomly picks point within circle with a radius of 'spread'
                    Vector2 off_spr = Random.insideUnitCircle.normalized * spread;
                    off_x = off_spr.x;
                    off_y = off_spr.y;
                    Vector3 spr = Quaternion.AngleAxis(off_x, Vector3.right) * Quaternion.AngleAxis(off_y, Vector3.up) * fpsCam.transform.forward;

                    if (Physics.Raycast(rayOrigin, spr, out hit, range))
                    {
                        GameObject n_enemy = hit.collider.gameObject;
                        IDamageable dmgScript = hit.collider.gameObject.GetComponent<EnemyHealth>();

                            //checks if shot object is damageable
                        if (dmgScript != null)
                        {   
                                //hurt enemy for 'damage'
                            dmgScript.Damage(damage, hit.point);
                                //If enemy's health is <= threshold, possess, set new threshold for possessed enemy for later
                            if (dmgScript.Health() <= (dmgScript.Threshold()))
                            { 
                                dmgScript.SetNextThreshold(dmgScript.Health() - dmgScript.Threshold());
                                sm.Swap(n_enemy);
                                //    wm.SwapEquip(n_enemy);
                            }
                        }
                            //shot effects physics based object
                        if (hit.rigidbody != null)
                        {
                            hit.rigidbody.AddForce(-hit.normal * 100f);
                        }

                        lineRenderer.SetPosition(0, gunEnd.position);
                        lineRenderer.SetPosition(1, hit.point);
                    }
                    StartCoroutine(ShotEffect());
                }
                else if (Time.time > nextFireTime)
                {
                    //empty gun sound
                }
            }
                //reloads if 'r' is pressed
            else if (Input.GetKeyDown("r") && cur_clip != clip)
            {
                Reload();
            }
                //sets next available firetime after reload is called
            else if (reloading && Time.time > nextFireTime)
            {
                transform.Translate(0, 0, -2);
                reloading = !reloading;
            }
        }
    }

    public bool Swap()  //unused
    {
        return swap;
    }

        //Semi-automatic and full automatic functions are near identical, method will be created to save space later.
    public void Fire()                          
    {

    }

    public void Reload()    //reload function
    {
        nextFireTime = Time.time + t_reload;
        if (cur_reserve > 0)
        {
            reloading = true;
            transform.Translate(0, 0, 2);   //temporary, hides weapon while reloading
            if (clip > cur_reserve)
            {                   
                cur_clip = cur_reserve;
                cur_reserve = 0;
            }
            else
            {
                cur_clip = clip;
                cur_reserve = cur_reserve - clip;
            }
        }
    }


    private IEnumerator ShotEffect()    //coroutine to handle lineRenderer, audio, visual effects
    {
        lineRenderer.enabled = true;
        yield return shotLength;
        lineRenderer.enabled = false;
    }


}
