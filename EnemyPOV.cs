using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPOV : MonoBehaviour {

    public Transform player;
    public bool found = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (Vector3.Distance(player.position, this.transform.position) < 100 && angle < 45)
        {
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.79f);

            if(direction.magnitude > 5)
            {
                found = true;
                this.transform.Translate(0, 0, .025f);
            }
            else
            {
                found = false;
            }
        }
	}
}
