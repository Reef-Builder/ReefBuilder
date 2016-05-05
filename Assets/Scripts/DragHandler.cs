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
		mouseOrbitScript.dragging = true;
		draggedObject = gameObject;
		startPosition = transform.position;
	}

	public void OnDrag (PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag (PointerEventData eventData) {
		mouseOrbitScript.dragging = false;
		draggedObject = null;
		transform.position = startPosition;
	}
}
