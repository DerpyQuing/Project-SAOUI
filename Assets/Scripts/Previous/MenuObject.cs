using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class MenuObject : MonoBehaviour {
	
	public MenuHandler parent;
	public GameObject hoverArea;
	public GameObject pressArea;
	public GameObject reference;	
	public GameObject childGroup;
	public MenuHandler childGroupHandler;

	public GameObject siri;

	// // // // // Personal
	public string myName = "";
	public int myIndex;
	public int myShape; // 0 for circle, 1 for box
	public float myBaseAlpha 		 = 1f;
	public static float myFadedAlpha = .688f;
	public float myEdgeAlpha 		 = .235f;
	public int myType;	// 0 for Regular, 1 List Object
	public Vector3 myLocation;
	public Vector3 myScale;
	public SpriteRenderer mySpriteRenderer;
	public bool isHovered = false;
	public bool isPressed = false;
	public bool isSelected = false;
	public Color myNormalColor = new Color(255f, 255f, 255f, 1f);
	public Color myFadedColor  = new Color(255f, 255f, 255f, myFadedAlpha);

	public Quaternion myLocRot;

	// // // // // Text
	public GameObject myText;
	public TextMesh textTextMesh;
	public Vector3 textLocation;
	public Vector3 textScale = new Vector3(.01f, .01f, .01f);
	public Color textColorNormal = Color.gray;
	public Color textColorRaised = Color.white;
	public Color textColorFaded  = new Color(Color.gray.r, Color.gray.g, Color.gray.b, .588f);
	public int textSortingOrder = 102;
	
	// // // // // Body
	public string mySpriteBasePath = "SAO/Icons/";

	// // // // // Box Icon
	public GameObject boxIcon;
	public SpriteRenderer boxIconSpriteRenderer;
	public string boxIconSpritePath;
	public Vector3 boxIconLocation = new Vector3(-.477f, 0, 0);
	public Vector3 boxIconScale = new Vector3(.015f, .015f, .015f);
	public int iconSortingOrder = 105;

	public string boxBaseName 	= "listObj";
	public string _normal 		= "_normal";
	public string _hover  		= "_hover";
	public string _press  		= "_press";
	public string _select 		= "_select";
	public string _on 	  		= "_on";
	//																												Moe
	public string[] requestMakers = new string[]{"Befriend", "Marriage", "Duel", "Trade", "Create", "Invite", "Dissolve", "Options"};

	public Vector3[] indicatorLocations = new Vector3[]{new Vector3(0.0454f, 0, 0.0154f), new Vector3(0.06785f, 0, 0.00891f), new Vector3(0.0701f, 0, -0.0042f)};

	public void defineMe(string myName, int myShape, int myIndex, int myType, Vector3 myLoc, Vector3 myScale) {
		this.myName		 		= myName;
		this.myShape	 		= myShape;
		this.myIndex	 		= myIndex;
		this.myType		 		= myType;
		this.siri 				= GameObject.Find("Player");
		this.myScale			= myScale;    
		this.reference 			= GameObject.Find("menuReference");
		this.textLocation 		= this.myLocation + new Vector3(-0.01459842f, 0.006769f, 0);
		this.boxIconSpritePath 	= "SAO/Icons/" + myName;
		this.myLocation			= myLoc;
	}
	
	public void createMe() {
		this.parent = this.transform.parent.gameObject.GetComponent<MenuHandler>();
		this.name = myName;
		this.gameObject.AddComponent<SpriteRenderer>();
		this.mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

		this.transform.localPosition = this.myLocation;
		this.transform.localScale = this.myScale;

		if(this.parent.myGroupType == 2) {
			this.transform.rotation = Quaternion.identity;
			this.parent.gameObject.transform.localRotation = Quaternion.identity;
		} else
			rotatoePotatoe(this.transform);

		if(this.myShape == 0) {
			this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this.myName);
			this.mySpriteRenderer.sortingOrder = 110;
		} else if(this.myShape == 1) {
			boxifyMe();
			createText();
			createConcentricIcon();
		} 

		if(this.myType == 1) {
			listifyMe();
		}

		createHoverArea();
		createPressArea();
	}

	public void rotatoePotatoe(Transform transFat) {
		Vector3 relPos = transFat.position - GameObject.Find("TrackingSpace").transform.position;
		relPos = new Vector3(relPos.x, 0, relPos.z);
		Quaternion rotatoe = Quaternion.LookRotation(relPos);
		transFat.localRotation = new Quaternion(0, rotatoe.y, 0, rotatoe.w);
	}

	public void listifyMe() {
		;
	}

	public void boxifyMe() {
		this.mySpriteBasePath = this.mySpriteBasePath + this.boxBaseName;
		this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath);
		this.mySpriteRenderer.sortingOrder = 100;
	}

	public void createText() {
		this.myText = (GameObject)Instantiate(this.reference, this.textLocation, Quaternion.identity);
		this.myText.transform.SetParent(this.transform);
		this.myText.transform.localScale = this.textScale;
		this.myText.transform.localPosition = new Vector3(-.2433072f, 0.0769f, 0);

		this.myText.transform.localRotation = Quaternion.identity;

		this.myText.name = this.myName + "_text";
		this.textTextMesh = this.myText.AddComponent<TextMesh>();
		if(this.myName != "Logout")
			this.textTextMesh.text = this.myName;
		else
			this.textTextMesh.text = "";
		this.textTextMesh.font = Resources.Load<Font>("SAO/Fonts/TTF/SAOUITTF-Regular");
		this.textTextMesh.fontSize = 150;
		this.textTextMesh.color = this.textColorNormal;
		this.myText.GetComponent<MeshRenderer>().sortingOrder = this.textSortingOrder;
	}

	public void createConcentricIcon() {
		this.boxIcon = (GameObject)Instantiate(this.reference, this.boxIconLocation, Quaternion.identity);
		this.boxIcon.transform.SetParent(this.transform);
		this.boxIconSpriteRenderer = this.boxIcon.AddComponent<SpriteRenderer>();
		if(this.parent.myParentName == "Friend") 
			this.boxIconSpritePath = "SAO/Icons/Player__";
		else if(Resources.Load<Sprite>(this.boxIconSpritePath) == null) 
			this.boxIconSpritePath = "SAO/Icons/Help";

		this.boxIconSpriteRenderer.sprite = Resources.Load<Sprite>(this.boxIconSpritePath);
		this.boxIconSpriteRenderer.sortingOrder = this.iconSortingOrder;
		this.boxIcon.transform.localScale = this.boxIconScale;
		this.boxIcon.transform.localPosition = this.boxIconLocation;

		this.boxIcon.transform.localRotation = Quaternion.identity;

		this.boxIcon.name = this.myName + "_icon";
	}

	public void createHoverArea() {
		this.hoverArea = (GameObject)Instantiate(this.reference, this.myLocation, Quaternion.identity);
		this.hoverArea.name = "Hover Area";
		this.hoverArea.transform.SetParent(this.transform);
		this.hoverArea.transform.localPosition = Vector3.zero;
		this.hoverArea.transform.localRotation = Quaternion.identity;
		if(this.myShape == 0) 
			createHoverCapsule(this.myType);
		else
			createHoverBox();

		this.hoverArea.AddComponent<HoverArea> ();
	}
	
	public void createPressArea() {
		this.pressArea = (GameObject)Instantiate(this.reference, this.myLocation, Quaternion.identity);
		this.pressArea.name = "Press Area";
		this.pressArea.transform.SetParent(this.transform);
		this.pressArea.transform.localPosition = Vector3.zero;
		this.pressArea.transform.localRotation = Quaternion.identity;
		if(this.myShape == 0) 
			createPressCapsule(this.myType);
		else
			createPressBox();
		this.pressArea.AddComponent<PressArea> ();
	}

	public void onHover() {
		if(!this.isHovered && !this.parent.isObjHovered && !this.isPressed && !this.isSelected) {
			this.isHovered = true;
			this.parent.isObjHovered = true;
			this.parent.objHovered = myIndex;
			playHoverSound();
		}
	}
	
	public void onHoverLoss() {
		if(this.isHovered && this.parent.isObjHovered && this.parent.objHovered == this.myIndex) {
			this.isHovered = false;
			this.parent.isObjHovered = false;
			this.parent.objHovered = -1;
		}
	}

	public void onPress() {
		if(!this.isPressed && this.isHovered && !this.isSelected && !this.parent.isObjPressed) {
			this.isPressed = true;
			this.parent.isObjPressed = true;
			this.parent.objPressed = myIndex;
			playPressSound();
			onSelect ();
		}
	}
	
	public void onPressLoss() {
		if(this.isPressed && this.isHovered && this.parent.isObjPressed && this.parent.objPressed == this.myIndex) {
			this.isPressed = false;
			this.parent.isObjPressed = false;
			this.parent.objPressed = -1;
		}
	}
	
	public new void onSelect() {
		this.isSelected = true;
		this.parent.isObjSelected = true;
		this.parent.objSelected = myIndex;

		if(this.parent.myParentName == "CL4P-TP") {
			destroyOtherLeftSides();
			createLeftSide();
		}

		if(this.requestMakers.Contains(this.myName)) {
			doSpecial();
		} else {
			destroyOtherChildMenus();
			createChildMenu();
			fadeOthers();
			colorMeCyan();
		}
	}
	
	public void onSelectLoss() {
		this.isSelected = false;
		destroyChildMenu();
	}

	public void destroyOtherChildMenus() {
		foreach(Transform child in transform.parent.transform) {
			if(child.name != this.myName && !child.name.Contains("Indicator_") && !child.name.Contains("Left Side")) 
				child.GetComponent<MenuObject>().onSelectLoss();
		}
	}
	
	public void createChildMenu() {
		this.childGroup = new GameObject(this.myName + "_child");
		this.childGroup.transform.SetParent(this.transform);
		this.childGroup.transform.localPosition = Vector3.zero;
		this.childGroupHandler = this.childGroup.AddComponent<MenuHandler>();
		this.childGroupHandler.createGroup(this.myName);
		if(!this.parent.exceptions.Contains(this.parent.myParentName)) {
			createIndicator();
			destroyPreviousIndicator();
		}
	}
	
	public void destroyChildMenu() {
		this.childGroup = null;
		this.childGroupHandler = null;
		try {
			Destroy(GameObject.Find(this.myName + "_child"));
		} catch(Exception e) {
			Debug.Log(e.ToString());
		}
	}

	public void createIndicator() {
		int size = GameObject.Find(this.myName + "_child").GetComponent<MenuHandler>().numberInMyGroup;
		if(size == 2) 
			size = 1;
		else if(size == 4) 
			size = 3;
		else if(size >= 5)
			size = 5;
		GameObject indicator = new GameObject("Indicator_" + size);
		indicator.AddComponent<SpriteRenderer>();
		indicator.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SAO/Misc/Indicator_" + size);
		indicator.transform.SetParent(GameObject.Find(this.myName + "_child").transform);
		indicator.transform.localScale = new Vector3(.06f, .06f, .06f);
		indicator.transform.localPosition = this.indicatorLocations[this.parent.myGroupTier];//new Vector3(-0.0076f, 0, 0.0369f);
		rotatoePotatoe(indicator.transform);
	}

	public void destroyPreviousIndicator() {
		try {
			foreach(Transform johnCena in this.transform.parent.transform) {
				if(johnCena.name.Contains("Indicator_"))
				   Destroy(johnCena.gameObject);
			}
		} catch(Exception e) {
			Debug.Log(e.ToString());
		}
	}

	public void createLeftSide() {
		GameObject leftMenuTop = new GameObject("Left Side Menu Top");
		leftMenuTop.AddComponent<SpriteRenderer>();
		leftMenuTop.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SAO/Misc/panelNoBottom");
		leftMenuTop.transform.SetParent(GameObject.Find("MenuHolder").transform);
		leftMenuTop.transform.localPosition = new Vector3(-0.2927f, this.myLocation.y, 0.4179f);
		leftMenuTop.transform.localScale = new Vector3(.06f, .06f, .06f);
		rotatoePotatoe(leftMenuTop.transform);
		// // // // // // // // // // // // // // // // // // // // // // // // // 
		GameObject thatGuyYouKnow_Name = new GameObject("That Guy You Know's Name");
		thatGuyYouKnow_Name.transform.SetParent(leftMenuTop.transform);
		thatGuyYouKnow_Name.transform.localPosition = new Vector3(-0.1f, 1.897f, 0);
		thatGuyYouKnow_Name.transform.localScale = new Vector3(.01f, .01f, .01f);
		thatGuyYouKnow_Name.transform.localRotation = Quaternion.identity;
		TextMesh thatTextMeshYouHaveHeardOf = thatGuyYouKnow_Name.AddComponent<TextMesh>();
		thatTextMeshYouHaveHeardOf.font = Resources.Load<Font>("SAO/Fonts/TTF/SAOUITTF-Regular");
		thatTextMeshYouHaveHeardOf.fontSize = 150;
		thatTextMeshYouHaveHeardOf.color = this.textColorNormal;
		thatTextMeshYouHaveHeardOf.anchor = TextAnchor.MiddleCenter;
		thatTextMeshYouHaveHeardOf.alignment = TextAlignment.Center;
		thatTextMeshYouHaveHeardOf.text = "Sandwich-kun";
		thatGuyYouKnow_Name.GetComponent<MeshRenderer>().sortingOrder = this.textSortingOrder;
		// // // // // // // // // // // // // // // // // // // // // // // // // 

		
		// // // // // // // // // // // // // // // // // // // // // // // // // 
		GameObject leftWingedPelican = new GameObject("Left Leaning Pelican");
		leftWingedPelican.AddComponent<SpriteRenderer>();
		leftWingedPelican.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SAO/Misc/info2");
		leftWingedPelican.transform.SetParent(leftMenuTop.transform);
		leftWingedPelican.transform.localPosition = new Vector3(-0.1f, 0.5f, 0);
		leftWingedPelican.transform.localScale = new Vector3(1f, 1f, 1f);
		leftWingedPelican.transform.localRotation = Quaternion.identity;
		leftWingedPelican.GetComponent<SpriteRenderer>().sortingOrder = 101;
		// // // // // // // // // // // // // // // // // // // // // // // // // 

		
		GameObject leftMenuBotPeek = new GameObject("Left Side Menu Bot Peek");
		leftMenuBotPeek.AddComponent<SpriteRenderer>();
		leftMenuBotPeek.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SAO/Misc/bottomPanel");
		leftMenuBotPeek.transform.SetParent(leftMenuTop.transform);
		leftMenuBotPeek.transform.localPosition = new Vector3(0, -0.697f, 0);
		leftMenuBotPeek.transform.localScale = new Vector3(1f, 0.1f, 1f);
		leftMenuBotPeek.transform.localRotation = Quaternion.identity;
	}

	public void destroyOtherLeftSides() {
		try {
			foreach(Transform westSide in this.transform.parent.transform) {
				if(westSide.name.Contains("Left Side Menu"))
					Destroy(westSide.gameObject);
			}
		} catch(Exception e) {
			Debug.Log(e.ToString());
		}	
	}

	public void fadeOthers() {
		foreach(Transform mrT in this.transform.parent) {
			if(mrT.name != this.myName && !mrT.name.Contains("Indicator_") && !mrT.name.Contains("Left Side")) {
				mrT.GetComponent<MenuObject>().fadeMe();
			}
		}
	}

	public void fadeMe() {
		this.mySpriteRenderer.color = this.myFadedColor;
		try {
			this.boxIconSpriteRenderer.color = this.myFadedColor;
			this.textTextMesh.color = this.textColorFaded;
		} catch(Exception e) {
			Debug.Log(e.ToString()); 
		}		
	}

	public void colorMeCyan() {
		this.mySpriteRenderer.color = this.myNormalColor;
		try {
			this.boxIconSpriteRenderer.color = this.myNormalColor;
			this.textTextMesh.color = this.textColorNormal;
		} catch(Exception e) {
			Debug.Log(e.ToString()); 
		}	
	}
	
	// // // // // // // // // // // // // // // // // // // // // // //
	public void createPressCapsule(int type) {
		CapsuleCollider area = this.pressArea.AddComponent<CapsuleCollider> ();
		area.direction = 2;
        area.height = 0;
        if (type != 2) {
            area.radius = .25f;
        } else {
            area.radius = .35f;
        }
		area.isTrigger = true;
		this.pressArea.transform.localScale = new Vector3(30, 30, 30);
	}

	public void createHoverCapsule(int type) {
		CapsuleCollider area = this.hoverArea.AddComponent<CapsuleCollider> ();
		area.direction = 2;
		if(type != 2) {
			area.radius = .31f;
			area.height = 1.5f;
			area.center = new Vector3 (0, 0, -.45f);
		} else {
			area.radius = .5f;
			area.height = 1f;
			area.center = new Vector3 (0, 0, -.15f);
		}
		area.isTrigger = true;
		this.hoverArea.transform.localScale = new Vector3(30, 30, 30);
	}
	
	public void createHoverBox() {
		BoxCollider area = this.hoverArea.AddComponent<BoxCollider> ();
		area.center = new Vector3(0.09f, 0, -.1f);
		area.size = new Vector3(1.6f, .46f, .35f);
		area.isTrigger = true;
		area.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	
	public void createPressBox() {
		BoxCollider area = this.pressArea.AddComponent<BoxCollider> ();
		area.center = new Vector3(0.09f, 0, 0);
		area.size = new Vector3(1.6f, .45f, .1f);
		area.isTrigger = true;
		area.transform.localScale = new Vector3(1f, 1f, 1f);
	}
	// // // // // // // // // // // // // // // // // // // // // //
	

	public void playPressSound() {
		siri.GetComponent<Kickstarter>().playPressSound();
	}

	public void playHoverSound() {
		siri.GetComponent<Kickstarter>().playHoverSound();
	}

	public void doSpecial() {
		; // Create Popup Message
	}

	void Update() {
		if((isHovered && parent.objHovered == myIndex) || (isPressed && parent.objPressed == myIndex)) {
			if(this.myShape == 0) 
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this.myName + this._on);
			else {
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this._hover);
				this.textTextMesh.color = this.textColorRaised;
				this.boxIconSpriteRenderer.sprite = Resources.Load<Sprite>(this.boxIconSpritePath + this._on);
			}

		} else if(isSelected && parent.objSelected == myIndex) {
			if(this.myShape == 0) 
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this.myName + this._on);
			else if((this.myShape == 1 && this.myType == 0) || (this.myShape == 1 && this.myType == 1 && this.parent.myParentName == "Friend")) {
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this._select);
				this.textTextMesh.color = this.textColorRaised;
				this.boxIconSpriteRenderer.sprite = Resources.Load<Sprite>(this.boxIconSpritePath + this._on);
			} else if((this.myShape == 1 && this.myType == 1) || this.parent.myParentName == "Settings") {
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this._hover);
				this.textTextMesh.color = this.textColorRaised;
				this.boxIconSpriteRenderer.sprite = Resources.Load<Sprite>(this.boxIconSpritePath + this._on);
			}

		} else {
			if(this.myShape == 0) 
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath + this.myName);
			else {
				this.mySpriteRenderer.sprite = Resources.Load<Sprite>(this.mySpriteBasePath);
				this.textTextMesh.color = this.textColorNormal;
				this.boxIconSpriteRenderer.sprite = Resources.Load<Sprite>(this.boxIconSpritePath);
			}
		}
	}	
}
