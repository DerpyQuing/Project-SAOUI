using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MenuItemController : MonoBehaviour, IHover, IPress {

	public JSONNode jsonNode;

	public GameObject menuItem;

	public ChildController childController;

	public ChildController myChildrenController;

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
		Debug.Log("This shouldn't get called");
	}

	// Come up with a better method name
	public void betterMethodName(JSONNode jsonNode, GameObject menuItem) {
		
		hoverObject = menuItem.transform.FindChild("hover").gameObject;
		hoverController = hoverObject.GetComponent<CollisionController>();
		
		pressObject = menuItem.transform.FindChild("press").gameObject;
		pressController = pressObject.GetComponent<CollisionController>();
		
		iconController = menuItem.GetComponent<IconController>();

	}

    public virtual void giveData(JSONNode jsonNode, GameObject menuItem) { }
    public virtual void handleCreation() { }
    public virtual void handlePosition() { }

    public void setChildrenController() {
		if(jsonNode["_hasChildren"].AsBool) {
			myChildrenController = transform.FindChild("childContainer").GetComponent<ChildController>();
		}
	}

	public void setItemParent() {
		if(jsonNode["_parent"] != null) {
			if(GameObject.Find(jsonNode["_parent"]).transform.Find("childContainer") == null) {
				GameObject childContainer = new GameObject("childContainer");
				childContainer.AddComponent<ChildController>();
				childController = childContainer.GetComponent<ChildController>();
				childContainer.transform.SetParent(GameObject.Find(jsonNode["_parent"]).transform);
				childContainer.transform.localPosition = Vector3.zero;
			}
			menuItem.transform.SetParent(GameObject.Find(jsonNode["_parent"]).transform.FindChild("childContainer").transform);
		} else if(jsonNode["_parent"] == null) {
			menuItem.transform.SetParent(GameObject.Find("MenuHolder").transform);
			childController = GameObject.Find("MenuHolder").GetComponent<ChildController>();

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

	public void revealChildren() {
		for(int childIndex = 0; childIndex < jsonNode.Count; childIndex++) {
			if(jsonNode[childIndex]["_hasChildren"] != null) {
				Debug.Log("in 1");
				if(!jsonNode[childIndex]["_hasChildren"].AsBool) {
					foreach(GameObject gm in myChildrenController.children) {
						if(gm.name == jsonNode[childIndex]["_name"] + "-" + jsonNode[childIndex]["_parent"]) {
							childController.revealItem(gm, jsonNode[childIndex]["_itemShape"]);
						}
						Debug.Log(gm.name);

					}
				} else if(jsonNode[childIndex]["_hasChildren"].AsBool) {
					Debug.Log("hi");
					foreach(GameObject gm in myChildrenController.children) {
						if(gm.name == jsonNode[childIndex]["_name"]) {
							childController.revealItem(gm, jsonNode[childIndex]["_itemShape"]);
						}
						Debug.Log(gm.name);
					}
				}
			}
		}

	}
	
	public void hideChildren() {
		for(int childIndex = 0; childIndex < jsonNode.Count; childIndex++) {
			if(jsonNode[childIndex]["_hasChildren"] != null) {
				childController.hideItem(childController.children[childIndex], jsonNode[childIndex]["_itemShape"]);
			}
		}
	}

   // These seem like there are better off here
    public void handleHover() {
        // If any other child is hovered, skip this. Same for pressed. That way there are no duplicates

        if (!childController.isAChildHovered() && !childController.isAChildPressed()) {
            Hover = true;
            iconController.Image = Resources.Load<Sprite>(jsonNode["_activeIconPath"]);
        }
    }

    public void handlePress() {
        if (Hover && !childController.isAChildPressed()) {
            Press = true;
            // I'm not going to have a pressed ver of the icons. Could add it here if wanted.
        }
    }

    public void handleHoverLoss() {
        Hover = false;
        if (!Selected)
            iconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
    }

    public void handlePressLoss() {
        Press = false;
        if (Hover && !childController.isAChildPressed()) {
            Selected = true;
            revealChildren();
        }
    }

}
