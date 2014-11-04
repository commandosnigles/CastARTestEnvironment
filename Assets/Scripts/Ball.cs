using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public float StartSpeed = 10f;
	public float MaxSpeed = 40f;
	public float SpeedIncrease = 0.5f;
	public ParticleSystem collisionParticles;

	private float currentSpeed;
	private Vector2 currentDir;
	private bool resetting = false;


	void Start () {
		currentSpeed = StartSpeed;
		currentDir = Random.insideUnitCircle.normalized;
	}

	void Update () {
		if (resetting) return;
		if (Network.connections.Length == 0) return;
		Vector2 moveDir = currentDir * currentSpeed * Time.deltaTime;
		transform.Translate (new Vector3 (moveDir.x, 0f, moveDir.y));
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Boundary") {
			currentDir.y *= -1;
		}

		else if (col.tag == "Player") {
			currentDir.x *= -1;
			collisionParticles.Emit((int)currentSpeed);
		}

		else if (col.tag == "Goal") {
			StartCoroutine (resetBall());
			col.SendMessage ("GetPoint", SendMessageOptions.DontRequireReceiver);
		}

		currentSpeed += SpeedIncrease;

		currentSpeed = Mathf.Clamp (currentSpeed, StartSpeed, MaxSpeed);
	}

	IEnumerator resetBall() {
		resetting = true;
		transform.position = Vector3.zero;

		currentDir = Vector3.zero;
		currentSpeed = 0f;
		yield return new WaitForSeconds (3f);
		Start ();
		resetting = false;
	}

	void OnSerializeNetworkView (BitStream stream) {
		if (stream.isWriting) {
			Vector3 pos = transform.position;
			Vector3 dir = currentDir;
			float speed = currentSpeed;
			stream.Serialize (ref pos);
			stream.Serialize (ref dir);
			stream.Serialize (ref speed);
		}
		else {
			Vector3 pos = Vector3.zero;
			Vector3 dir = Vector3.zero;
			float speed = 0f;
			stream.Serialize (ref pos);
			stream.Serialize (ref dir);
			stream.Serialize (ref speed);
			transform.position = pos;
			currentDir = dir;
			currentSpeed = speed;
		}
	}
}
