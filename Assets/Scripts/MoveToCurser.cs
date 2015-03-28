using UnityEngine;
using System.Collections;

public class MoveToCurser : MonoBehaviour {

	public GameObject objectToMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnMouseOver () {
		if (objectToMove == null)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit)) {
			objectToMove.transform.position = hit.point;
		}
	}
}
