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

	// Item
	public Vector3 menuItemScale = new Vector3(.06f, .06f, .06f);
	public Vector3 hoverBoxCenter = new Vector3(0.09f, 0, -.1f);
	public Vector3 hoverBoxSize = new Vector3(1.6f, .46f, .35f);
	public Vector3 pressBoxCenter = new Vector3(0.09f, 0, 0);
	public Vector3 pressBoxSize = new Vector3(1.6f, .45f, .1f);
	public Vector3 boxScale = new Vector3(1f, 1f, 1f);

	// Sub Icon
	public Vector3 subIconPosition = new Vector3(-.477f, 0, 0);
	public Vector3 subIconScale = new Vector3(.015f, .015f, .015f);
	public int     subIconSortingOrder = 105;

	// Text
	public Vector3 textPosition = new Vector3(-.2433072f, 0.0769f, 0);
	public Vector3 textScale = new Vector3(.01f, .01f, .01f);
	public int     textSize = 150;
	public Color   textColorNormal = Color.gray;
	public Color   textColorRaised = Color.white;
	public Color   textColorFaded  = new Color(Color.gray.r, Color.gray.g, Color.gray.b, .588f);
	public int     textSortingOrder = 102;
	public Font    textFont = Resources.Load<Font>("SAO/Fonts/TTF/SAOUITTF-Regular");

	// Misc
	public float yOffset = 0.0261f;
	public bool  childrenAreList;

	
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
		handleTransform();
		handleMisc();
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
		textController = textObject.GetComponent<TextController>();
		textController.Text = jsonNode["_text"];
		textController.TextFont = textFont;
		textController.TextColor = textColorNormal;
		textController.TextSize = textSize;
		textController.TextScale = textScale;
		textController.TextPosition = textPosition;
	}

	public void handleTransform() {
		setItemParent();
		transform.localScale = menuItemScale;
		transform.position = GameObject.Find(jsonNode["_parent"]).transform.position;
		//transform.localRotation = rotation;
	}

	public void handleMisc() {
		if(jsonNode["_childrenAreList"] != null)
			childrenAreList = jsonNode["_childrenAreList"].AsBool;
	}

	void Start() {

	}
	
}
