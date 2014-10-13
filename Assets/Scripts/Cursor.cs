using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public KeyCode RotationKey = KeyCode.Mouse0;
	public KeyCode SelectionKey = KeyCode.Mouse1;

	private FeatureCollider highlighted;
	private FeatureCollider selected;
	private bool rotating = false;
	private Canvas UI;

	void Awake () {
		UI = GameObject.FindObjectOfType<Canvas>();
		UI.enabled = false;
	}

	void Update () {

		if (Input.GetKeyDown(SelectionKey)){
			if (highlighted && !selected){
				selected = highlighted;
				UI.enabled = true;
			}
			else {
				UI.enabled = false;
				selected = null;
			}
		}
		if (Input.GetKeyDown(RotationKey)) {
			rotating = true;
		}
		if (Input.GetKeyUp(RotationKey)){
			UI.enabled = false;
			selected = null;
			rotating = false;
		}

		if (!selected && !rotating) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.collider.gameObject.GetComponent<FeatureCollider>() != null) {
					// already a highlighted object
					if (highlighted){
						if (hit.collider.gameObject.GetComponent<FeatureCollider>() != highlighted){
							// replace highlighted
								highlighted.EndHover();
								highlighted = hit.collider.gameObject.GetComponent<FeatureCollider>();
								highlighted.StartHover();
						}
					}
					else {
						highlighted = hit.collider.gameObject.GetComponent<FeatureCollider>();
						highlighted.StartHover();
					}					
				}
				//Ray is not hitting a Feature Collider
				else {
					if (highlighted) {
						highlighted.EndHover();
						highlighted = null;
					}
				}
			}
			else {
				if (highlighted) {
					highlighted.EndHover();
					highlighted = null;
				}
			}
		}
	}


}
