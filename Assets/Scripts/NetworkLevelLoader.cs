using UnityEngine;
using System.Collections;

public class NetworkLevelLoader : MonoBehaviour {

	public static NetworkLevelLoader Instance {
		get {
			if (instance == null){
				GameObject go = new GameObject ("_networkLevelLoader");
				go.hideFlags = HideFlags.HideInHierarchy;
				instance = go.AddComponent<NetworkLevelLoader>();
				GameObject.DontDestroyOnLoad (go);
			}
			return instance;
		}
	}

	private static NetworkLevelLoader instance;

	public void LoadLevel (int levelIndex, int prefix = 0) {
		StopAllCoroutines();
		StartCoroutine (doLoadLevel (levelIndex, prefix));
	}

	IEnumerator doLoadLevel (int index, int prefix) {
		Network.SetSendingEnabled (0, false);
		Network.isMessageQueueRunning = false;

		Network.SetLevelPrefix (prefix);
		Application.LoadLevel (index);
		yield return null;
		yield return null;

		Network.isMessageQueueRunning = true;
		Network.SetSendingEnabled (0, true);
	}
}
