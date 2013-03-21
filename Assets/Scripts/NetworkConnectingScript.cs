using UnityEngine;
using System.Collections;

public class NetworkConnectingScript : MonoBehaviour
{

	public string gameName = "Network_Cookie";
	private bool refreshing;
	private HostData[] hostData;
	private float btnX;
	private float btnY;
	private float btnH;
	private float btnW;
	
	// префаб игрока которого бум постоянно спаунить при конекте нового человека
	public GameObject playerPrefab;
	// пустой геймобжект который выставляется на месте где спаунить
	public Transform spawnObject;
	
	void Start ()
	{
		btnX = Screen.width * 0.05f;
		btnY = Screen.width * 0.05f;
		btnH = Screen.width * 0.05f;
		btnW = Screen.width * 0.1f;
	}
	
	void startServer ()
	{
		
		Network.InitializeServer (32, 25001, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (gameName, "Game Name", "This is the game");
	}
	
	void refreshHosts ()
	{
		MasterServer.RequestHostList (gameName);
		refreshing = true;
	}
	
	void Update ()
	{
		if (refreshing) {
			if (MasterServer.PollHostList ().Length > 0) {
				refreshing = false;
				Debug.Log (MasterServer.PollHostList ().Length);
				hostData = MasterServer.PollHostList ();
			}
		}
	}
	
	void spawnPlayer() {
		Network.Instantiate(playerPrefab, spawnObject.position, Quaternion.identity, 0);
	}
	
	void OnServerInitialized ()
	{
		Debug.Log ("Server Initialized");
		spawnPlayer();
	}
	
	void OnConnectedToServer() {
		spawnPlayer();
	}
	
	void OnMasterServerEvent (MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log ("Registration server");
		}
	}
	
	void OnGUI ()
	{
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button (new Rect (btnX, btnY, btnW, btnH), "Start server")) {
				startServer ();
			}
		
			if (GUI.Button (new Rect (btnX, btnY * 1.2f + btnH, btnW, btnH), "Refresh")) {
				refreshHosts ();
				Debug.Log ("Refreshing");
			}
			if (hostData != null) {
				for (int i =0; i < hostData.Length; i++) {
					if (GUI.Button (new Rect (btnX * 1.5f + btnW, btnY * 1.2f + (btnH * i), btnW * 3, btnH * 0.5f), hostData [i].gameName)) {
						Network.Connect (hostData [i]);
					}
				}
			}
		}
	}
}
