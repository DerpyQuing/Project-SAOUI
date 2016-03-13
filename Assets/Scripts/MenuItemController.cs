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
		Debug.Log("Don't think I need this");
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


}
