using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject terrain;
	public Transform prefab;

	public static GameObject draggedObject;
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
		if (!placed)
		{
			Destroy(coral.gameObject);
		}
		coral = null;
		Camera.main.GetComponent<MouseOrbit>().lockCamera(false);
	}
}
