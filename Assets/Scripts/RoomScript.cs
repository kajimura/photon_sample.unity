using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {
	PhotonManager manager;
	// Use this for initialization
	void Start () {
		manager = PhotonManager.Instance;
		manager.CreateRoom ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
