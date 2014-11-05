using UnityEngine;
using System.Collections;

public class NetworkCharacter : MonoBehaviour {
	public GameObject[] ChildrenToDestroy;
	public MonoBehaviour[] BehaviorsToDeactivate;
	public Camera cameratokill;
	public GameObject[] toShow;


	void Awake () {
		if (networkView.isMine){
			return;
		}
		foreach (GameObject child in ChildrenToDestroy)
			GameObject.Destroy (child);
		foreach (MonoBehaviour behavior in BehaviorsToDeactivate)
			behavior.enabled = false;
		cameratokill.enabled = false;
		foreach (GameObject show in toShow)
			show.layer = 0;
	}
}
