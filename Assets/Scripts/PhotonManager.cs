using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhotonManager : Photon.MonoBehaviour {
	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("v7.0");
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnJoinedLobby ()
	{
		Debug.Log ("PhotonManager OnJoinedLobby");
		CreateRoom ();
	}
	public void CreateRoom(){
		string userName = "ユーザ4";
		string userId = "user4";
		string roomId = "room1";
		// PhotonNetwork.autoCleanUpPlayerObjects = false;
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName);
		customProp.Add ("userId", userId);
		PhotonNetwork.SetPlayerCustomProperties(customProp);
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.customRoomProperties = customProp;
		roomOptions.customRoomPropertiesForLobby = new string[]{ "userName","userId"};
		roomOptions.maxPlayers = 10; // 部屋の最大人数
		roomOptions.isOpen = true; // 入室許可する
		roomOptions.isVisible = true; // ロビーから見えるようにする
		PhotonNetwork.JoinOrCreateRoom (roomId, roomOptions, null);
	}
	void OnPhotonJoinFailed() {
		Debug.Log ("PhotonManager OnPhotonJoinFailed");
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ("PhotonManager OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom(null);
	}
	// ルーム入室した時
	void OnJoinedRoom() {
		Debug.Log ("PhotonManager OnJoinedRoom");
		GameObject cube = PhotonNetwork.Instantiate ("Cube", new Vector3 (0.0f, 0.0f, 0.0f),
			Quaternion.Euler(Vector3.zero),0);
		cube.name = "Cube"+cube.GetComponent<PhotonView>().ownerId;


		GameObject member = PhotonNetwork.Instantiate ("Member", new Vector3 (0.0f, 0.0f, 0.0f),
			Quaternion.Euler(Vector3.zero),0);
		member.name = "Member"+cube.GetComponent<PhotonView>().ownerId;

	}
	// ルーム一覧が取れた場合
	void OnReceivedRoomListUpdate(){
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();
		if (rooms.Length == 0) {
			Debug.Log ("ルームが一つもありません");
		} else {
			foreach (RoomInfo room in rooms) {
				Debug.Log ("RoomName:"   + room.name);
				Debug.Log ("userName:" + room.customProperties["userName"]);
				Debug.Log ("userId:"   + room.customProperties["userId"]);
				// GameObject.Find("StatusText").GetComponent<Text>().text = rooms [i].name;
			}
		}
	}
	private Vector3 GetRandomPosition()
	{
		var rand = Random.insideUnitCircle * 4.0f;
		return rand;
	}
}
