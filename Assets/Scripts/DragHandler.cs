using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject draggedObject;
	public Vector3 startPosition;
	public MouseOrbit mouseOrbitScript;

	void Awake () {
		mouseOrbitScript = GameObject.Find ("Main Camera").GetComponent<MouseOrbit> ();
	}

	public void OnBeginDrag (PointerEventData eventData) {
		mouseOrbitScript.lockCamera(true);
		draggedObject = gameObject;
		startPosition = transform.position;
	}

	public void OnDrag (PointerEventData eventData) {
		// This needs to be here to maintain the drag, otherwise it
		// ends immediately.
	}

	public void OnEndDrag (PointerEventData eventData) {
		mouseOrbitScript.lockCamera(false);
		draggedObject = null;
		transform.position = startPosition;
	}
}
