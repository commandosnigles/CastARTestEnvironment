using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RotateToCursor(Vector3 cursorDir){
		transform.rotation = Quaternion.FromToRotation(Vector3.up, cursorDir);
	}
}
