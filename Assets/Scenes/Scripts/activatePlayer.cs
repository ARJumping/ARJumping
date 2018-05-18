using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlayer : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start () {
        Debug.Log("activate cube!!");
        player.SetActive(true);
	}
}
