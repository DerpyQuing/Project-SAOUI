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
		{"large", new Vector3(.003f, .003f, .003f)},
		{"small", new Vector3(.003f, .003f, .003f)},

		{"maybeSmall", new Vector3(30f, 30f, 30f)}
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

	// Calling this gets things started
	public void giveData(JSONNode jsonNode, GameObject menuItem) {
		base.jsonNode = jsonNode;
		base.menuItem = menuItem;
		base.betterMethodName(jsonNode, menuItem);
	}

	public void handleCreation() {
		handleHover();
		handlePress();
		handleIcon();

		transform.localScale = scaleContainer[jsonNode["_type"]];

		setItemParent();

	}
	
	// Since there are overlaps for the Press and Hover inits (below), we take those duplicate lines out and put them in here
	public void handleCircleColliderSimilarities(CollisionController colController) {
		colController.initCapsuleCollider();
		colController.ColliderDirection = direction;
		colController.ColliderIsTrigger = true;
		colController.ColliderScale = colScale;
	}

	// Handles hover 
	public void handleHover() {
		handleCircleColliderSimilarities(hoverController);
		hoverController.IsHover = true;

		hoverController.ColliderHeight = hoverHeightContainer[jsonNode["_type"]];
		hoverController.ColliderRadius = hoverRadiusContainer[jsonNode["_type"]];
		hoverController.ColliderCenter = hoverCenterContainer[jsonNode["_type"]];
	}

	// Handles press
	public void handlePress() {
		handleCircleColliderSimilarities(pressController);
		pressController.IsHover = false;


		pressController.ColliderHeight = pressHeight;
		pressController.ColliderRadius = pressRadiusContainer[jsonNode["_type"]];
	}

	public void handleIcon() {
		iconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
	}

	void Start() {

	}





}





