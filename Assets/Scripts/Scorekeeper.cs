using UnityEngine;
using System.Collections;

public class Scorekeeper : MonoBehaviour {

	public int ScoreLimit = 10;
	public Transform[] Spawns;
	public GameObject playerPrefab;
	public GameObject otherPlayerPrefab;


	void Start () {
		if (Network.isServer) {
			Network.Instantiate (playerPrefab, Spawns[0].position, Quaternion.identity,0);
//			NetworkViewID viewID = Network.AllocateViewID(); 
//			GameObject me = GameObject.Instantiate(playerPrefab,Spawns[0].position,Quaternion.identity) as GameObject;
//			me.GetComponent<NetworkView>().viewID = viewID;
//			networkView.RPC ("net_SpawnOthers", RPCMode.AllBuffered, viewID, Spawns[0].position);
		}
	}

	void OnPlayerConnected (NetworkPlayer player) {
//		NetworkViewID viewID = Network.AllocateViewID(); 
		int playercount = Network.connections.Length;
		networkView.RPC ("net_SpawnMe", player, Spawns[playercount].position);
//		networkView.RPC ("net_SpawnOthers", RPCMode.AllBuffered, viewID, Spawns[playercount].position);
	}

	void OnPlayerDisconnected (NetworkPlayer player) {
		Network.DestroyPlayerObjects(player);
	}

	void OnDisconnectedFromServer (NetworkDisconnection cause) {
		Application.LoadLevel ("TestMenu");
	}
	[RPC]
	void net_SpawnMe (Vector3 position) {
		Network.Instantiate (playerPrefab, position, Quaternion.identity,0);
//		GameObject me;
//		me = Instantiate (playerPrefab, position, Quaternion.identity) as GameObject;
//		NetworkView nView;
//		nView = me.GetComponent<NetworkView>();
//		nView.viewID = viewID;

	}

	[RPC]
	void net_SpawnOthers (NetworkViewID viewID, Vector3 position) {
		GameObject other;
		other = Instantiate (otherPlayerPrefab, position, Quaternion.identity) as GameObject;
		NetworkView nView;
		nView = other.GetComponent<NetworkView>();
		nView.viewID = viewID;
	}

//	public void AddScore (int player) {
//		networkView.RPC ("net_AddScore", RPCMode.Others, player);
//	}
//
//	[RPC]
//	public void net_AddScore (int player) {
//		if (player == 1) {
//			p1Score ++;
//		}
//		else if (player == 2) {
//			p2Score ++;
//		}
//
//		if (p1Score >= ScoreLimit || p2Score >= ScoreLimit) {
//			if (p1Score > p2Score)
//				Debug.Log ("Player 1 Wins");
//			else if (p2Score > p1Score)
//				Debug.Log ("Player 2 Wins");
//			else
//				Debug.Log ("Player are tied");
//
//			p1Score = 0;
//			p2Score = 0;
//		}
//
//		Player1ScoreDisplay.text = p1Score.ToString ();
//		Player2ScoreDisplay.text = p2Score.ToString ();
//	}
}
