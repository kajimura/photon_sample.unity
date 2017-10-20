using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MemberScript : Photon.MonoBehaviour {

	public string str = "guest";
	public float x = 0f;
	public float y = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake() {
		this.name = "Member"+GetComponent<PhotonView> ().ownerId;
		if (GetComponent<PhotonView> ().isMine) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
		SetNameText ();
	}
	public void SetPlayerName(string name) {
		str = name;
		SetNameText ();
	}
	void OnMouseDrag()
	{
		Debug.Log ("member ownerId=" + GetComponent<PhotonView> ().ownerId);
		Debug.Log ("member isMine=" + GetComponent<PhotonView> ().isMine);
		if (GetComponent<PhotonView>().isMine) {
			GetComponent<Renderer> ().material.color = Color.red;
			Vector3 objectPointInScreen = Camera.main.WorldToScreenPoint (this.transform.position);
			Vector3 mousePointInScreen = new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y,
				objectPointInScreen.z);
			Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint (mousePointInScreen);
			mousePointInWorld.z = this.transform.position.z;
			this.transform.position = mousePointInWorld;
		}
	}
	void SetNameText()
	{
		Debug.Log("name="+this.name);
		GameObject objstr = GameObject.Find(this.name + "/Canvas/Text");
		try {
			objstr.transform.GetComponent<Text> ().text = str;
		} catch (NullReferenceException ex) {
		}
	}

	void OnPhotonSerializeView( PhotonStream stream, PhotonMessageInfo info )
	{
		if (stream.isWriting) {
			// Debug.Log ("member isWriting="+str);
			//データの送信
			stream.SendNext(str);
			stream.SendNext(this.transform.position.x);
			stream.SendNext(this.transform.position.y);

		} else {
			Debug.Log ("member  isread="+str);
			//データの受信
			this.str = (string)stream.ReceiveNext();
			this.x = (float)stream.ReceiveNext();
			this.y = (float)stream.ReceiveNext();
			this.transform.position = new Vector3 (x, y, transform.position.z);
			SetNameText ();
		}
	}
}
