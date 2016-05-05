using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// Only one object can be dragged at a time,
	// so this attribute is static for that reason.
	public static GameObject draggedObject;

	public GameObject terrain;
	public Transform prefab;

	public Vector3 startPosition;

	private Transform coral;

	void Awake () {
	}

	public void OnBeginDrag (PointerEventData eventData) {
		draggedObject = gameObject;
		startPosition = transform.position;

		coral = (Transform)Instantiate(prefab, Vector3.zero, Quaternion.identity);
		coral.GetComponent<SnapToTerrain>().terrain = terrain;

		Camera.main.GetComponent<MouseOrbit>().lockCamera(true);
	}

	public void OnDrag (PointerEventData eventData) {
		// This needs to be here to maintain the drag, otherwise it
		// ends immediately.
	}

	public void OnEndDrag (PointerEventData eventData) {
		draggedObject = null;
		transform.position = startPosition;

		bool placed = coral.GetComponent<SnapToTerrain>().SetLocked(true);

		// If the component was placed, then remove currency equal to the objects
		// cost. If it wasn't, destroy the created object.
		if (placed) {
			GameObject.Find ("GameController").GetComponent<GameScript> ().removePolyps (100);
		} else {
			Destroy (coral.gameObject);
		}

		coral = null;
		Camera.main.GetComponent<MouseOrbit>().lockCamera(false);
	}
}