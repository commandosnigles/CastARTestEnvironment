using UnityEngine;
using System.Collections;

public class ConnectVREnvironment : MonoBehaviour {
	public string GameTypeName = "CastARs 411/450 - f14";
	public GUIText text;
	void Start () {
		MasterServer.RequestHostList(GameTypeName);
	}

	void OnGUI() {
		if (GUILayout.Button("Connect")){
			MasterServer.RequestHostList(GameTypeName);
			if (MasterServer.PollHostList().Length != 0) {
				text.text = "Connecting to host...";
				HostData[] hostData = MasterServer.PollHostList();
				Network.Connect (hostData[0]);
//				MasterServer.ClearHostList();
			}
			else {
				text.text = "No host found. Please wait for host list to update, then try again.";
			}
		}
		if (GUILayout.Button("Host")){
			Network.InitializeServer(4, 25452, !Network.HavePublicAddress());
		}
	}

	void OnConnectedToServer () {
		text.text = "Connected to Server";
		NetworkLevelLoader.Instance.LoadLevel(1);
	}
	
	void OnServerInitialized() {
		text.text = "Server initialized";
		MasterServer.RegisterHost(GameTypeName,"VR Room");
		NetworkLevelLoader.Instance.LoadLevel(1);
	}

	void Update () {
	
	}
}
