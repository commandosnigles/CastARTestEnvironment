using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	public KeyCode RotationKey = KeyCode.Mouse0;
	public KeyCode SelectionKey = KeyCode.Mouse1;
	public float xpos = 0.5f;

	private FeatureCollider highlighted;
	private FeatureCollider selected;
	private bool rotating = false;
	private OVRGUI GuiHelper = new OVRGUI();
	private RenderTexture GUIRenderTexture;
	private GameObject GUIRenderObject = null;
//	private Canvas UI;

	void Awake () {
//		GUIRenderObject = GameObject.Instantiate(Resources.Load("OVRGUIObjectMain")) as GameObject;
//		GUIRenderTexture = new RenderTexture (Screen.width, Screen.height, 0);
//		GUIRenderObject.renderer.material.mainTexture = GUIRenderTexture;
//		UI = GameObject.FindObjectOfType<Canvas>();
//		UI.enabled = false;
	}
//	void OnGUI() {
//		string text = "HI!";
//		GuiHelper.StereoBox (xpos,.5f,.1f,.1f,ref text,Color.white);
//	}
	void Update () {

		if (Input.GetKeyDown(SelectionKey)){
			if (highlighted && !selected){
				selected = highlighted;
//				UI.enabled = true;
			}
			else {
//				UI.enabled = false;
				selected = null;
			}
		}
		if (Input.GetKeyDown(RotationKey)) {
			rotating = true;
		}
		if (Input.GetKeyUp(RotationKey)){
//			UI.enabled = false;
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
