using UnityEngine;
using System.Collections;
using System;

public class RockScript : MonoBehaviour, Placeable {

    public Sprite icon;
    public int cost;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getCost() {
        return cost;
    }

    public Sprite getIcon()
    {
        return icon;
    }

    public void setPlaced(bool place)
    {
        
    }

	public bool canPlaceOnSand() {
		return true;
	}

	public RockData Serialize() {
		RockData data = new RockData ();

		data.prefab = transform.name.Replace ("(Clone)", "");

		data.localPositionX = transform.localPosition.x;
		data.localPositionY = transform.localPosition.y;
		data.localPositionZ = transform.localPosition.z;

		data.localScaleX = transform.localScale.x;
		data.localScaleY = transform.localScale.y;
		data.localScaleZ = transform.localScale.z;

		data.localEulerAnglesX = transform.localEulerAngles.x;
		data.localEulerAnglesY = transform.localEulerAngles.y;
		data.localEulerAnglesZ = transform.localEulerAngles.z;

		return data;
	}

	public static RockScript Deserialize(RockData data) {
		GameObject rock = (GameObject)Instantiate (Resources.Load("Prefabs/GameObjects/Rocks/" + data.prefab));

		SnapToTerrain snapScript = rock.GetComponent<SnapToTerrain> ();
		snapScript.HardLock (true);

		rock.transform.localPosition = new Vector3(data.localPositionX, data.localPositionY, data.localPositionZ);
		rock.transform.localScale = new Vector3(data.localScaleX, data.localScaleY, data.localScaleZ);
		rock.transform.localEulerAngles = new Vector3(data.localEulerAnglesX, data.localEulerAnglesY, data.localEulerAnglesZ);

		return rock.GetComponent<RockScript> ();
	}
}


[System.Serializable]
public class RockData {
	public String prefab;

	public float localPositionX;
	public float localPositionY;
	public float localPositionZ;

	public float localScaleX;
	public float localScaleY;
	public float localScaleZ;

	public float localEulerAnglesX;
	public float localEulerAnglesY;
	public float localEulerAnglesZ;
}