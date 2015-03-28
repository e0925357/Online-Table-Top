using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FigureSpawnerScript : MonoBehaviour {

	public Image colorDisplay;
	public GameObject figurePrefab;
	public GameObject mouseParticles;

	private Color figureColor = Color.red;

	// Use this for initialization
	void Start () {
	
	}

	public void spawnFigure() {
		GameObject go = (GameObject)GameObject.Instantiate (figurePrefab);
		FigureScript fs = go.GetComponent<FigureScript> ();
		fs.setColor (figureColor);

		MoveToCurser mtc = go.GetComponent<MoveToCurser> ();
		mtc.objectToMove = mouseParticles;

		go.transform.position = transform.position;
	}
	
	public void setColorHue(float hue) {
		figureColor = ColorUtil.HSVToRGB (hue, 1, 1);
		colorDisplay.color = figureColor;
	}
}
