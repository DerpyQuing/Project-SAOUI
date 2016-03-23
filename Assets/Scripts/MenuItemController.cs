using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MenuItemController : MonoBehaviour, IHover, IPress {

	public JSONNode jsonNode;

	public GameObject menuItem;

	public ChildController childController;

	public GameObject hoverObject;
	public CollisionController hoverController;

	public GameObject pressObject;
	public CollisionController pressController;

	public IconController iconController;

	public float unfocusedAlpha = .688f;
	public float baseAlpha = 1f;

	public int iconSortingOrder = 100;

	public bool hoverStatus = false;
	public bool pressStatus = false;
	public bool selectedStatus = false;


	public bool Hover {
		get { return hoverStatus; }
		set { hoverStatus = value; }
	}

	public bool Press {
		get { return pressStatus; }
		set { pressStatus = value; }
	}

	public bool Selected {
		get { return selectedStatus; }
		set { selectedStatus = value; }
	}
	
	public MenuItemController() {
		//Debug.Log("Don't think I need this");
	}

	public MenuItemController(JSONNode jsonNode, GameObject menuItem) {
		Debug.Log("Does this get called now");
	}

	// Come up with a better method name
	public void betterMethodName(JSONNode jsonNode, GameObject menuItem) {
		this.jsonNode = jsonNode;
		this.menuItem = menuItem;


		hoverObject = menuItem.transform.FindChild("hover").gameObject;
		hoverController = hoverObject.GetComponent<CollisionController>();
		
		pressObject = menuItem.transform.FindChild("press").gameObject;
		pressController = pressObject.GetComponent<CollisionController>();
		
		iconController = menuItem.GetComponent<IconController>();

	}

	public void setItemParent() {
		if(jsonNode["_parent"] != null) {
			Debug.Log(GameObject.Find(jsonNode["_parent"]));
			if(GameObject.Find(jsonNode["_parent"]).transform.Find("childContainer") == null) {
				GameObject childContainer = new GameObject("childContainer");
				childController = childContainer.AddComponent<ChildController>();
				childContainer.transform.SetParent(GameObject.Find(jsonNode["_parent"]).transform);
				childContainer.transform.localPosition = Vector3.zero;
			}
			menuItem.transform.SetParent(GameObject.Find(jsonNode["_parent"]).transform.FindChild("childContainer").transform);
		} else if(jsonNode["_parent"] == null) {
			menuItem.transform.SetParent(GameObject.Find("MenuHolder").transform);
		} else
			Debug.Log("Parent Error");
	}

	public void changeAlpha(float newAlpha) {
		iconController.SpriteColor = new Color(255f, 255f, 255f, newAlpha);
	}

	public float getMyGroupIndex(string parentName, string myName) {
		Transform parentTransform = null;
		if(parentName != null) {
			parentTransform = GameObject.Find(parentName).transform.Find("childContainer");
		} else {
			parentTransform = GameObject.Find("MenuHolder").transform;
		}
		for(int childIndex = 0; childIndex < parentTransform.childCount; childIndex++) {
			if(parentTransform.GetChild(childIndex).name == myName)
				return childIndex;
		}
		return -1;
	}

    public Transform getParentTransform(string parent) {
        return GameObject.Find(parent).transform.Find("childContainer").transform;
    }

	// We'll override these in the children
	public virtual void handleHover () {}
	public virtual void handleHoverLoss() {}
	public virtual void handlePress() {}
	public virtual void handlePressLoss() {}


}
