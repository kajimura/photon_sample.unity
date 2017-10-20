using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleScript : MonoBehaviour {

	PhotonManager manager;
	// Use this for initialization
	void Start () {
		manager = PhotonManager.Instance;
		manager.Start ();
		GameObject obj = GameObject.Find ("/Canvas/Button");
		obj.GetComponent<Button> ().enabled = false;
	}
	void Awake() {
	}
	// Update is called once per frame
	void Update () {
		GameObject obj = GameObject.Find ("/Canvas/Button");
		if (manager.lobbyFlag && !obj.GetComponent<Button> ().enabled) {
			obj.GetComponent<Button> ().onClick.AddListener (OnClick);
			obj.GetComponent<Button> ().enabled = true;
		}
	}
	void OnClick()
	{
		Debug.Log ("OnClick");
		GameObject objtext = GameObject.Find ("/Canvas/InputField/Text");
		string str = objtext.transform.GetComponent<Text> ().text;
		if (str.Equals("")) {
			str = "guest" + Random.Range (1000, 9999);
		}
		manager.SetPlayerName (str);
		SceneManager.LoadScene ("LobbyScene");
	}
}
