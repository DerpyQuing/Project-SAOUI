using UnityEngine;

using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

public class CircleMenuItemController : MenuItemController {

	// The Capsule Objects' Capsule Collider's Direction Variable. 2 is Z. [x,y,z]. This makes it face the way we want
	public int direction = 2; // Why does this turn into 0?

	// The Capsule Objects' localScale Variable
	public Vector3 scale = new Vector3(30f, 30f, 30f);

	// The Capsule Press Object's Capsule Collider's Height Variable
	public float pressHeight = 0;

	// This allows us to clense ourselves of unnecessary if statements
	// Since we have 2 size of circular menu items, we need 2 variables for their height, radius, and center
	public Dictionary<string, float> hoverHeightContainer;/* = new Dictionary<string, float>();/* {
		{"large", 1.5f},
		{"small", 1f}
	};
*/

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

	// Have to put things in here, because apparently Unity isn't liking the above.
	// Also, my direction variable prints out as 0..... it is 2.
	// Seems that the rapid instantiation of all of these.... im just kidding. 
	// I can't figure out why it pretends things are 0 and/or don't exist
	void Awake (){ 
		this.hoverHeightContainer = new Dictionary<string, float>();
		this.hoverHeightContainer.Add("large", 1.5f);
		this.hoverHeightContainer.Add("small", 1f);
		
		Debug.Log(this.hoverHeightContainer["large"]);
	}

	// Calling this gets things started
	public CircleMenuItemController(JSONNode jsonNode, GameObject menuItem) {
		this.jsonNode = jsonNode;
		this.menuItem = menuItem;
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
	}
	
	// Since there are overlaps for the Press and Hover inits (below), we take those duplicate lines out and put them in here
	public void handleCircleColliderSimilarities(CollisionController colController) {
		hoverController.initCapsuleCollider();
		colController.ColliderDirection = direction;
		Debug.Log(direction);
		colController.ColliderIsTrigger = true;
		colController.ColliderScale = scale;
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





}





