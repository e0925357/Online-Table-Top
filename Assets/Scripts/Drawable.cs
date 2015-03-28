using UnityEngine;
using System.Collections;

public class Drawable : MonoBehaviour {

	public static bool drawingLocked = false;

	private Texture2D textureToDrawOn;
	private Vector2i[] brushShape = {new Vector2i(1, 0), new Vector2i(0, 1), new Vector2i(-1, 0), new Vector2i(0, -1), new Vector2i(0, 0)};
	private Vector2i? lastPoint = null;
	private LineDrawer lineDrawer;

	public int textureWidth;
	public int textureHeight;

	public Color brushColor = Color.black;

	// Use this for initialization
	void Start () {
		textureToDrawOn = new Texture2D (textureWidth, textureHeight, TextureFormat.RGB24, false, false);

		for (int x = 0; x < textureWidth; x++) {
			for(int y = 0; y < textureHeight; y++) {
				textureToDrawOn.SetPixel (x, y, Color.white);
			}
		}

		textureToDrawOn.filterMode = FilterMode.Point;


		textureToDrawOn.Apply();

		renderer.material.mainTexture = textureToDrawOn;
		lineDrawer = new RoundToNearestLineDrawer(drawPoint);
	}

	void OnMouseUp() {
		lastPoint = null;
	}
	
	// Update is called once per frame
	void OnMouseDrag () {
		if (drawingLocked || !Input.GetMouseButton (0)) {
			lastPoint = null;
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		
		foreach (RaycastHit hit in hits) {
			if (hit.collider.tag.Equals ("Table")) {
				Vector3 hitPoint = hit.point;
				Vector2 texCoords = new Vector2 (hitPoint.x - (transform.position.x + transform.localScale.x / 2), hitPoint.z - (transform.position.z + transform.localScale.z / 2));
				texCoords = new Vector2 (texCoords.x / transform.localScale.x, texCoords.y / transform.localScale.z);
				texCoords = texCoords * -1;

				Vector2i currentPoint = new Vector2i ((int)(texCoords.x * (textureWidth - 1)), (int)(texCoords.y * (textureHeight - 1)));

				if (lastPoint.HasValue) {
					lineDrawer.drawLine (lastPoint.Value, currentPoint);
				} else {
					drawPoint (currentPoint);
				}

				lastPoint = currentPoint;

				textureToDrawOn.Apply ();
			}
		}
	}

	private void drawPoint(Vector2i point) {

		for (int i = 0; i < brushShape.Length; i++) {
			Vector2i offsetPoint = point + brushShape[i];

			if(offsetPoint.x >= 0 && offsetPoint.x < textureWidth && offsetPoint.y >= 0 && offsetPoint.y < textureHeight) {
				textureToDrawOn.SetPixel (offsetPoint.x, offsetPoint.y, brushColor);
			}
		}
	}

	void OnDestroy() {
		renderer.material.mainTexture = null;
		Texture2D.Destroy (textureToDrawOn);
	}

	public void clearTexture() {
		for (int x = 0; x < textureWidth; x++) {
			for(int y = 0; y < textureHeight; y++) {
				textureToDrawOn.SetPixel (x, y, Color.white);
			}
		}
		
		textureToDrawOn.Apply();
	}
}
