using UnityEngine;
using System.Collections;
using SimpleJSON;

public class MenuItemController : MonoBehaviour {

	public JSONNode jsonNode;

	public GameObject menuItem;

	public GameObject hoverObject;
	public CollisionController hoverController;

	public GameObject pressObject;
	public CollisionController pressController;

	public IconController iconController;


	public int iconSortingOrder = 100;

	public MenuItemController() {
		//Debug.Log("Don't think I need this");
	}

	public MenuItemController(JSONNode jsonNode, GameObject menuItem) {
		Debug.Log("Does this get called now");
	}

	// Come up with a better method name
	public void betterMethodName(JSONNode jsonNode, GameObject menuItem) {
		jsonNode = jsonNode;
		menuItem = menuItem;


		hoverObject = menuItem.transform.FindChild("hover").gameObject;
		hoverController = hoverObject.GetComponent<CollisionController>();
		
		pressObject = menuItem.transform.FindChild("press").gameObject;
		pressController = pressObject.GetComponent<CollisionController>();
		
		iconController = menuItem.GetComponent<IconController>();

	}

	public void setItemParent() {
		if(jsonNode["_parent"] != null) {
			menuItem.transform.SetParent(GameObject.Find(jsonNode["_parent"]).transform.FindChild("childContainer").transform);
		} else if(jsonNode["_parent"] == null) {
			menuItem.transform.SetParent(GameObject.Find("TestObject").transform);
		} else
			Debug.Log("Parent Error");
	}


}
