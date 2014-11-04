using UnityEngine;
using System.Collections;

public class Scorekeeper : MonoBehaviour {

	public int ScoreLimit = 10;
	public Transform[] Spawns;
	public GameObject paddlePrefab;


	void Start () {
		if (Network.isServer) {
			Network.Instantiate (paddlePrefab, Spawns[0].position, Quaternion.identity, 0);
		}
	}

	void OnPlayerConnected (NetworkPlayer player) {
		int playercount = Network.connections.Length;
		networkView.RPC ("net_DoSpawn", player, Spawns[playercount-1].position);
	}

	void OnPlayerDisconnected (NetworkPlayer player) {
	
	}

	void OnDisconnectedFromServer (NetworkDisconnection cause) {
		Application.LoadLevel ("TestMenu");
	}
	[RPC]
	void net_DoSpawn (Vector3 position) {
		Network.Instantiate (paddlePrefab, position, Quaternion.identity, 0);
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
