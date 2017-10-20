using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CubeScript : Photon.MonoBehaviour {
	public string str = "guest";
	void Awake() {
		this.name = "Cube"+GetComponent<PhotonView> ().ownerId;
		if (GetComponent<PhotonView> ().isMine) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
		SetCubeText ();
	}
	public void SetPlayerName(string name) {
		str = name;
		SetCubeText ();
	}
	void OnMouseDrag()
	{
		Debug.Log ("ownerId=" + GetComponent<PhotonView> ().ownerId);
		Debug.Log ("isMine=" + GetComponent<PhotonView> ().isMine);
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
	void OnClick()
	{
		if (GetComponent<PhotonView> ().isMine) {
			GameObject objstr = GameObject.Find ("/Canvas/InputField/Text");
			str = objstr.transform.GetComponent<Text> ().text;
		}
		SetCubeText ();
	}
	void SetCubeText()
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
			Debug.Log ("cube isWriting="+str);
			//データの送信
			stream.SendNext(str);
			//stream.SendNext(hensu1);
			//stream.SendNext(hensu2);

		} else {
			Debug.Log ("cube isread="+str);
			//データの受信
			this.str = (string)stream.ReceiveNext();
			SetCubeText ();

		}
	}
}