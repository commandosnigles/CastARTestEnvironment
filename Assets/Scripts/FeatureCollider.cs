using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshCollider))]
public class FeatureCollider : MonoBehaviour {
//	public class Response {
//		public string Answer = "";
//		public bool Correct = false;
//		public Response (string answer, bool correct) {
//			Answer = answer;
//			Correct = correct;
//		}
//	}
//	public List<Response> Responses = new List<Response>();
	public bool CorrectResponse = false;
	public float HighlightIntensity = 0.8f;
	public float HighlightSpeed = 10f;

	private bool highlighted = false;

	void OnValidate () {
		HighlightIntensity = Mathf.Clamp01(HighlightIntensity);
		HighlightSpeed = Mathf.Clamp(HighlightSpeed, 0, 100);
	}

	public void SelectAsAnswer() {
		TestLog.Instance.Answer (CorrectResponse);
	}

	public void StartHover () {
		highlighted = true;
		StartCoroutine(Highlight(true));
		Debug.Log ("Hover: On");
	}

	public void EndHover () {
		highlighted = false;
		StartCoroutine(Highlight(false));
		Debug.Log ("Hover: Off");
	}

	IEnumerator Highlight(bool start) {
		if (start) {
			while (renderer.material.color.a < HighlightIntensity) {
				if (!highlighted)
					return false;
				Color color = renderer.material.color;
				color.a += HighlightSpeed/100 * HighlightIntensity;
				renderer.material.color = color;
				yield return new WaitForFixedUpdate();
			}
		}
		else {
			while (renderer.material.color.a > 0) {
				if (highlighted)
					return false;
				Color color = renderer.material.color;
				color.a -= HighlightSpeed/100 * HighlightIntensity;
				renderer.material.color = color;
				yield return new WaitForFixedUpdate();
			}
		}
	}

	void Select () {

	}
}
