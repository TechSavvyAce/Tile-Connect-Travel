using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera _camera;
	public float currentTime;
	public bool isZoom;
	public float time;
	public float defaultOrthographicSize;
	public float amount;
	public Vector3 defaultPosition;
	public Vector3 zoomTowards;

	private float minZoom = 1f;
	private float maxZoom = 200;


	// Use this for initialization
	void Start () {
		defaultOrthographicSize = this._camera.orthographicSize;
		defaultPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isZoom) {
			currentTime += Time.deltaTime;
			if (currentTime < time) {
				// Calculate how much we will have to move towards the zoomTowards position
				float multiplier = (1.0f / this._camera.orthographicSize * amount);

				// Move camera
				transform.position += (zoomTowards - transform.position) * multiplier * Time.deltaTime / time;

				// Zoom camera
				this._camera.orthographicSize -= amount * Time.deltaTime / time;

				// Limit zoom
				this._camera.orthographicSize = Mathf.Clamp (this._camera.orthographicSize, minZoom, maxZoom);
			} else {
				currentTime = 0;
				isZoom = false;
			}
		}
	}

	// Ortographic camera zoom towards a point (in world coordinates). Negative amount zooms in, positive zooms out
	// TODO: when reaching zoom limits, stop camera movement as well
	public void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
	{
		// Calculate how much we will have to move towards the zoomTowards position
		float multiplier = (1.0f / this._camera.orthographicSize * amount);

		// Move camera
		transform.position += (zoomTowards - transform.position) * multiplier;

		// Zoom camera
		this._camera.orthographicSize -= amount;

		// Limit zoom
		this._camera.orthographicSize = Mathf.Clamp(this._camera.orthographicSize, minZoom, maxZoom);
	}

	public void ZoomOrthoCamera(Vector3 zoomTowards, float amount, float time)
	{
		this.zoomTowards = zoomTowards;
		this.amount = amount;
		this.time = time;
		isZoom = true;
	}

	public void resetCamera() {
		this._camera.orthographicSize = defaultOrthographicSize;
		transform.position = defaultPosition;
	}
}
