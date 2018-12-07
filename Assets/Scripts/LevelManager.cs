using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;

    //the name of the script you have attached to the player
    //private PlayerController player;


    // Use this for initialization
    void Start () {
        //player = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer ()
    {
       // player.transform.position = currentCheckpoint.transform.position;
    }

}
