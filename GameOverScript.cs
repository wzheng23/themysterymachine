using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    public GameObject goScreen;
    public IDamageable playHealth;
    public GameObject[] Enemies;
    public GameObject bigbad;

    private AudioSource[] source;

    private void Start()
    {
        playHealth = GameObject.Find("Player").GetComponent<IDamageable>();
        bigbad = GameObject.FindGameObjectWithTag("Boss");

        source = GetComponents<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        print(Enemies.Length);

        if (playHealth.Health() <= 0 /*replace with when player dies or enemy count is reached*/)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            goScreen.SetActive(true);
            //    Time.timeScale = 0f;
            source[0].Play(); //fall

        }
        else if (Enemies.Length == 1)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            goScreen.SetActive(true);
            source[1].Play();
            //    Time.timeScale = 0f;
        }
        else if (bigbad == null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            goScreen.SetActive(true);
            source[1].Play();
        }
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    //    Time.timeScale = 1f;

    }
}
