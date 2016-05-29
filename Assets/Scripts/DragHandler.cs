using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	// Only one object can be dragged at a time,
	// so this attribute is static for that reason.
	public static GameObject draggedObject;

	public GameObject terrain;
	public Transform prefab;

	public Vector3 startPosition;

	public Text costText;

	private Image image;
	private Transform placedObject;
	private GameScript gameScript;
	private Placeable placeableScript;
	private bool canAfford = true;

	void Start () {
		gameScript = GameObject.Find ("GameController").GetComponent<GameScript> ();
        if (prefab.tag.Equals("Rock")) {
            placeableScript = prefab.GetComponent<RockScript>();
        } else {
            placeableScript = prefab.GetComponent<CoralScript>();
        }
		image = GetComponent<Image> ();
		image.sprite = placeableScript.getIcon();
		costText.text = "" + placeableScript.getCost();
	}

	public void Update() {
		if (prefab.tag.Equals ("Rock")) {
			canAfford = (gameScript.getFossils () >= placeableScript.getCost ());
		} else {
			canAfford = (gameScript.getPolyps () >= placeableScript.getCost ());
		}

		if (!canAfford) {
			image.color = Color.black;
		} else {
			image.color = Color.white;
		}
	}

	public void OnBeginDrag (PointerEventData eventData) {
		// While in deletion mode, preventing placing objects
		if (gameScript.getDeleteMode () || !canAfford) {
			return;
		}

		draggedObject = gameObject;
		startPosition = transform.position;

		placedObject = (Transform)Instantiate(prefab, Vector3.zero, Quaternion.identity);
		placedObject.GetComponent<SnapToTerrain>().terrain = terrain;

		Camera.main.GetComponent<MouseOrbit>().lockCamera(true);
	}

	public void OnDrag (PointerEventData eventData) {
		// This needs to be here to maintain the drag, otherwise it
		// ends immediately.
	}

	public void OnEndDrag (PointerEventData eventData) {
		// While in deletion mode, preventing placing objects
		if (gameScript.getDeleteMode () || !canAfford) {
			return;
		}

		draggedObject = null;
		transform.position = startPosition;

		bool placed = placedObject.GetComponent<SnapToTerrain>().SetLocked(true);

		// If the component was placed, then remove currency equal to the objects
		// cost. If it wasn't, destroy the created object.
		if (placed) {
			placeableScript.setPlaced (true);

			if (placedObject.tag.Equals ("Rock")) {
				gameScript.removeFossils (placeableScript.getCost ());
			} else {
				gameScript.addCoral (placedObject.GetComponent<CoralScript> ());
				gameScript.removePolyps (placeableScript.getCost ());
			}
		} else {
			Destroy (placedObject.gameObject);
		}

		placedObject = null;
		Camera.main.GetComponent<MouseOrbit>().lockCamera(false);
	}
}