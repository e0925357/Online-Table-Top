using UnityEngine;
using System.Collections;

public class FigureScript : MonoBehaviour {

	public float forceModifier = 30;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		Drawable.drawingLocked = true;
	}

	void OnMouseUp() {
		Drawable.drawingLocked = false;
	}

	void OnMouseDrag() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);

		foreach (RaycastHit hit in hits) {
			if (hit.collider.tag.Equals("Table")) {
				Vector3 hitPoint = hit.point;
				hitPoint.y = transform.position.y;
				Vector3 force = hitPoint - transform.position;
				force = force * forceModifier;
				rigidbody.AddForce (force);
				return;
			}
		}
	}

	public void setColor(Color c) {
		setColor(c, transform);
	}
	
	private void setColor(Color c, Transform trans) {
		MeshRenderer renderer = trans.GetComponent<MeshRenderer>();
		
		if(renderer != null) {
			renderer.material.color = c;
		}

		for(int i = 0; i < trans.childCount; i++) {
			setColor(c, trans.GetChild(i));
		}
	}
}
