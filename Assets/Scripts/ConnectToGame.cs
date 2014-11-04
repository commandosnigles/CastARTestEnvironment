using UnityEngine;
using System.Collections;

public class ConnectToGame : MonoBehaviour {
	private string ip = "127.0.0.1";
	private int port = 25005;

	void OnGUI() {
		GUILayout.Label ("IP Address");
		ip = GUILayout.TextField (ip, GUILayout.Width (200f));

		GUILayout.Label ("Port");
		string port_str = GUILayout.TextField (port.ToString(), GUILayout.Width (100f));
		int port_num = port;
		if (int.TryParse (port_str, out port_num)) 
			port = port_num;
		if (GUILayout.Button ( "Connect",GUILayout.Width (100f) ) ) {
			Debug.Log ("Attempting to connect...");
			Network.Connect (ip, port);
		}
		if (GUILayout.Button ("Host", GUILayout.Width (100f)) ) {
			Debug.Log ("Setting up server");
			Network.InitializeServer (1, port, true);
		}
	}

	void OnConnectedToServer () {
		Debug.Log ("Connected to Server");
		NetworkLevelLoader.Instance.LoadLevel(1);
	}

	void OnServerInitialized() {
		Debug.Log ("Server initialized");
		NetworkLevelLoader.Instance.LoadLevel(1);
	}
}
