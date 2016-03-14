using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;



public class RectangleMenuItemController : MenuItemController {

	public GameObject textObject;
	public TextController textController;

	public GameObject subIconObject;
	public IconController subIconController;

	public string rectangleIconBasePath = "SAO/Icons/listObj";

	public Vector3 menuItemScale = new Vector3(.06f, .06f, .06f);
	

	public Vector3 hoverBoxCenter = new Vector3(0.09f, 0, -.1f);
	public Vector3 hoverBoxSize = new Vector3(1.6f, .46f, .35f);

	public Vector3 pressBoxCenter = new Vector3(0.09f, 0, 0);
	public Vector3 pressBoxSize = new Vector3(1.6f, .45f, .1f);

	public Vector3 boxScale = new Vector3(1f, 1f, 1f);

	public Vector3 subIconPosition = new Vector3(-.477f, 0, 0);
	public Vector3 subIconScale = new Vector3(.015f, .015f, .015f);


	// Apparently

	
	public void giveData(JSONNode jsonNode, GameObject menuItem) {
		this.jsonNode = jsonNode;
		this.menuItem = menuItem;
		betterMethodName(jsonNode, menuItem);

		subIconObject = transform.FindChild("icon").gameObject;
		textObject = transform.FindChild("text").gameObject;

	}

	public void handleCreation() {
		handleHover();
		handlePress();
		handleIcon();
		handleSubIcon();
		handleText();

		transform.localScale = menuItemScale;

		setItemParent();

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
		iconController.Image = Resources.Load<Sprite>(rectangleIconBasePath);
	}

	public void handleSubIcon() {
		subIconController = subIconObject.GetComponent<IconController>();
		subIconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
		subIconController.SpritePosition = subIconPosition;
		subIconController.SpriteScale = subIconScale;
	}

	public void handleText() {

	}

	void Start() {

	}
	
}
