using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class RectangleMenuItemController : MenuItemController
{

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
    public int sortingOrder = -1;

    // Sub Icon
    public Vector3 subIconPosition = new Vector3(-.477f, 0, 0);
    public Vector3 subIconScale = new Vector3(.015f, .015f, .015f);
    public int subIconSortingOrder = 105;

    // Text
    public Vector3 textPosition = new Vector3(-.2433072f, 0.0769f, 0);
    public Vector3 textScale = new Vector3(.01f, .01f, .01f);
    public int textSize = 150;
    public Color textColorNormal = Color.gray;
    public Color textColorRaised = Color.white;
    public Color textColorFaded = new Color(Color.gray.r, Color.gray.g, Color.gray.b, .588f);
    public int textSortingOrder = 102;
    public Font textFont;

    // Misc
    public static float yOffset = 0.0261f;
    public bool childrenAreList;
    public static float xOffset = 0.1187f;

    public override void giveData(JSONNode jsonNode, GameObject menuItem) {
        this.jsonNode = jsonNode;
        this.menuItem = menuItem;
        betterMethodName(jsonNode, menuItem);

        subIconObject = transform.FindChild("icon").gameObject;
        textObject = transform.FindChild("text").gameObject;
        textFont = Resources.Load<Font>("SAO/Fonts/SAOUI-Regular");
    }

    public override void handleCreation() {
        handleHoverInit();
        handlePressInit();
        handleBox();
        handleSubIcon();
        handleText();
        handleTransform();
        handleMisc();
    }

    public void handleBoxColliderSimilarities(CollisionController colController) {
        colController.initBoxCollider();
        colController.ColliderScale = boxScale;
        colController.ColliderIsTrigger = true;
        colController.setupInterfaces(this, this);
    }

    public void handleHoverInit() {
        handleBoxColliderSimilarities(hoverController);
        hoverController.IsHover = true;
        hoverController.ColliderCenter = hoverBoxCenter;
        hoverController.ColliderSize = hoverBoxSize;
    }

    public void handlePressInit() {
        handleBoxColliderSimilarities(pressController);
        pressController.IsHover = false;
        pressController.ColliderCenter = pressBoxCenter;
        pressController.ColliderSize = pressBoxSize;
    }

    public void handleBox() {
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
        textController.TextColor = textColorNormal;
        textController.TextSize = textSize;
        textController.TextFont = textFont;
        textController.TextScale = textScale;
        textController.TextPosition = textPosition;
    }

    public void handleTransform() {
        setItemParent();
        transform.localScale = menuItemScale;
        //transform.localRotation = rotation; // will handle this when I get to curvature
    }

    public override void handlePosition() {
        if (getParentTransform(jsonNode["_parent"]).childCount % 2 == 0)
            transform.localPosition = new Vector3(xOffset, (-yOffset / 2) + yOffset * getParentTransform(jsonNode["_parent"]).childCount / 2 - yOffset * getMyGroupIndex(jsonNode["_parent"], gameObject.name), 0f);
        else
            transform.localPosition = new Vector3(xOffset, yOffset * (getParentTransform(jsonNode["_parent"]).childCount / 2) - (yOffset * getMyGroupIndex(jsonNode["_parent"], gameObject.name)), 0f);
    }

    public void handleMisc() {
        iconController.SortingOrder = sortingOrder;
        if (jsonNode["_childrenAreList"] != null)
            childrenAreList = jsonNode["_childrenAreList"].AsBool;
    }

	public override void handleHover() {
		// If any other child is hovered, skip this. Same for pressed. That way there are no duplicates
		if (!childController.isAChildHovered() && !childController.isAChildPressed()) {
			Hover = true;
			subIconController.Image = Resources.Load<Sprite>(jsonNode["_activeIconPath"]);
			iconController.Image = Resources.Load<Sprite>(rectangleIconBasePath + "_hover");
		}
	}
	
	public override void handlePress() {
		if (Hover && !childController.isAChildPressed()) {
			Press = true;
			// I'm not going to have a pressed ver of the icons. Could add it here if wanted.
		}
	}
	
	public override void handleHoverLoss() {
		Hover = false;
		if (!Selected) {
			subIconController.Image = Resources.Load<Sprite>(jsonNode["_baseIconPath"]);
			iconController.Image = Resources.Load<Sprite>(rectangleIconBasePath);
		}
	}
	
	public override void handlePressLoss() {
		Press = false;
		if (Hover && !childController.isAChildPressed()) {
			Selected = true;
			revealChildren();
		}
	}


    void Start() {  }

}
