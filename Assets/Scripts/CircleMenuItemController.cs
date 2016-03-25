using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


public class CircleMenuItemController : MenuItemController {

	// The Capsule Objects' Capsule Collider's Direction Variable. 2 is Z. [x,y,z]. This makes it face the way we want
	public int direction = 2; // Why does this turn into 0?

	// The Colliders' scales
	public Vector3 colScale = new Vector3(30f, 30f, 30f);

	// The Capsule Press Object's Capsule Collider's Height Variable
	public float pressHeight = 0;

	// Menu Item's scale
	public Dictionary<string, Vector3> scaleContainer = new Dictionary<string, Vector3>() {
		{"large", new Vector3(.0025f, .0025f, .0025f)},
		{"small", new Vector3(0.0008f, 0.0008f, 0.0008f)},
	};

	// This allows us to clense ourselves of unnecessary if statements
	// Since we have 2 size of circular menu items, we need 2 variables for their height, radius, and center
	public Dictionary<string, float> hoverHeightContainer = new Dictionary<string, float>() {
		{"large", 1.5f},
		{"small", 1f}
	};
	
	public Dictionary<string, float> hoverRadiusContainer = new Dictionary<string, float>() {
		{"large", .31f},
		{"small", .5f}
	};


	public Dictionary<string, Vector3> hoverCenterContainer = new Dictionary<string, Vector3>() {
		{"large", new Vector3 (0, 0, -.45f)},
		{"small", new Vector3 (0, 0, -.15f)}
	};

	public Dictionary<string, float> pressRadiusContainer = new Dictionary<string, float>() {
		{"large", .25f},
		{"small", .35f}
	};

	public Dictionary<string, float> yPositionContainer = new Dictionary<string, float>() {
		{"large", .075f},
		{"small", 0f}
	};

	// Small icons positions (Start, Minus, Dissolve)
	Vector3[] smallIconLocations = new Vector3[]{new Vector3(-0.0224f, 0, 0), new Vector3( 0.006f, 0, 0), new Vector3( 0.0327f, 0, 0)};


	// Calling this gets things started
	public override void giveData(JSONNode jsonNode, GameObject menuItem) {
		this.jsonNode = jsonNode;
		this.menuItem = menuItem;
	    betterMethodName(jsonNode, menuItem);
	}

	public override void handleCreation() {
		handleHoverInit();
		handlePressInit();
		handleIcon();
		handleTransform();
	}
	
	// Since there are overlaps for the Press and Hover inits (below), we take those duplicate lines out and put them in here
	public void handleCircleColliderSimilarities(CollisionController colController) {
		colController.initCapsuleCollider();
		colController.ColliderDirection = direction;
		colController.ColliderIsTrigger = true;
		colController.ColliderScale = colScale;
		colController.setupInterfaces(this, this);
	}

	// Handles hover 
	public void handleHoverInit() {
		handleCircleColliderSimilarities(hoverController);
		hoverController.IsHover = true;
		hoverController.ColliderHeight = hoverHeightContainer[jsonNode["_type"]];
		hoverController.ColliderRadius = hoverRadiusContainer[jsonNode["_type"]];
		hoverController.ColliderCenter = hoverCenterContainer[jsonNode["_type"]];
	}

	// Handles press
	public void handlePressInit() {
		handleCircleColliderSimilarities(pressController);
		pressController.IsHover = false;
		pressController.ColliderHeight = pressHeight;
		pressController.ColliderRadius = pressRadiusContainer[jsonNode["_type"]];
	}

	public void handleIcon() {
		iconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
	}

	public void handleTransform() {
		setItemParent();
		transform.localScale = scaleContainer[jsonNode["_type"]];
		//		transform.localRotation = rotation;

	}

	public override void handlePosition() {
		if(jsonNode["_type"].ToString().Equals(@"""small""")) {
			transform.localPosition = smallIconLocations[(int)getMyGroupIndex(jsonNode["_parent"], gameObject.name)];
		} else {
			transform.localPosition = new Vector3(-0.178f, transform.localPosition.y - yPositionContainer[jsonNode["_type"]] * getMyGroupIndex(jsonNode["_parent"] , gameObject.name) - .17f, 0.41f);
		}
	}

	void Start() {
	}





}





