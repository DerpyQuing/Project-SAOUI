using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RectangleMenuItemController : MenuItemController {

	public GameObject textObject;
	public TextController textController;

	public GameObject subIconObject;
	public IconController subIconController;
	

	public Vector3 hoverBoxCenter = new Vector3(0.09f, 0, -.1f);
	public Vector3 hoverBoxSize = new Vector3(1.6f, .46f, .35f);

	public Vector3 pressBoxCenter = new Vector3(0.09f, 0, 0);
	public Vector3 pressBoxSize = new Vector3(1.6f, .45f, .1f);

	public Vector3 boxScale = new Vector3(1f, 1f, 1f);

	public RectangleMenuItemController(JSONNode jsonNode, GameObject menuItem) {
		betterMethodName(jsonNode, menuItem);
	}

	// Or this
	public void giveData(JSONNode jsonNode, GameObject menuItem) {
		this.jsonNode = jsonNode;
		this.menuItem = menuItem;
		betterMethodName(jsonNode, menuItem);
	}

	public void handleCreation() {
		handleHover();
		handlePress();
		handleIcon();
		handleSubIcon();
		handleText();
	}

	public void handleBoxColliderSimilarities (CollisionController colController) {
		colController.initBoxCollider();
		colController.ColliderScale = boxScale;
		colController.ColliderIsTrigger = true;
	}


	public void handleHover() {
		handleBoxColliderSimilarities(hoverController);
		hoverController.IsHover = true;
		hoverController.ColliderCenter = hoverBoxCenter;
		hoverController.ColliderSize = hoverBoxSize;
	}

	public void handlePress() {
		handleBoxColliderSimilarities(pressController);
		pressController.IsHover = false;
		pressController.ColliderCenter = pressBoxCenter;
		pressController.ColliderSize = pressBoxSize;
	}

	public void handleIcon() {
		iconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
	}
	
}
