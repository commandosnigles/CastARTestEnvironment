using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	public float MoveSpeed = 15f;
	public float MoveRange = 15f;
	public bool AcceptsInput = true;
	
	private Vector3 readNetworkPos;

	void Start () {
		AcceptsInput = networkView.isMine;
	}

	void Update () {
		if (!AcceptsInput) {
			transform.position = Vector3.Lerp (transform.position, readNetworkPos, MoveSpeed * Time.deltaTime);
			return;
		}

		float input = Input.GetAxis ("Vertical");
		Vector3 pos = transform.position;
		pos.z += input * MoveSpeed * Time.deltaTime;
		pos.z = Mathf.Clamp (pos.z, -MoveRange, MoveRange);
		transform.position = pos;
	}

	void OnSerializeNetworkView (BitStream stream) {
		if (stream.isWriting) {
			Vector3 pos = transform.position;
			stream.Serialize (ref pos);
		}
		else {
			Vector3 pos = Vector3.zero;
			stream.Serialize (ref pos);
			readNetworkPos = pos;
		}
	}
}
