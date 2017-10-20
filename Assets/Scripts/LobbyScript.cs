using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LobbyScript : MonoBehaviour {
	PhotonManager manager;
	// Use this for initialization
	void Start () {
		manager = PhotonManager.Instance;
		GameObject obj = GameObject.Find ("/Canvas/Button");
		obj.GetComponent<Button> ().onClick.AddListener (OnClick);

	}
	// Update is called once per frame
	void Update () {
	}
	void OnClick()
	{
		SceneManager.LoadScene("RoomScene");
	}
}
