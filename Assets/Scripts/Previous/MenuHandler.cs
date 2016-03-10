using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class MenuHandler : MonoBehaviour {

	//  Party -> Create (plus icon), Invite (3 dots icon), Dissolve (x icon)
	//	Items -> list (indicator size is dependent on list size, between 1-5
	//  Friends -> list all friends (ditto) ----> Message Box (quest icon), Position Check (floor map icon), Profile (party icon)
	// 	Skills -> list
	//  Equipment -> Weapons (sword icon), Armor (armor icon), Accesories (pendant icon)
	//  Floor Map -> number spinner, shows blue map on leftside menu area (top half)
	//
	//	Befriend sends a friend request
	//  Trade opens a trade window for both parties, showing what you are giving (at the start)
	//  Duel sends a duel request
	//  Marriage sends a marriage request
	//
	//  Help calls a gm on the left side menu
	//
	//  Guild, Quest, Option, Field Map --> ?????? go no go for launch captain
	//
	
	public Vector3[] senpai = new Vector3[]{new Vector3(-0.1906f, 0.102f, 0.4727f) , new Vector3(0.0963f, 0, 0.0274f), new Vector3(0.1184f, 0, 0.0084f), new Vector3(0.1187f, 0, -0.0159f)};
	public Vector3[] senpaiNoticer = new Vector3[]{};

	public float y1Diff = .075f;
	public float y2kDiff = 0.0261f;
	
	public bool isObjHovered  = false;
	public bool isObjPressed  = false;
	public bool isObjSelected = false;
	
	public int objHovered = -1;
	public int objPressed = -1;
	public int objSelected = -1;

	public int myGroupTier;
	public int myGroupType; // 0 = Regular, 1 = List
	public int numberInMyGroup;
	public string myParentName = "";
	public int myShape;
	public float menuListObjLocationMaxYOffset 	= 0.0261f * 3;
	public Vector3 nonT1Scale 	= new Vector3(.06f, .06f, .06f);
	public Vector3 t1Scale 		= new Vector3(.003f, .003f, .003f);
	public Vector3 firstIconLocation;
	
	public Dictionary<string, string[]> namesDictionary = new Dictionary<string, string[]>() {
		{"Items", 	  new string[] {"Space Rifle", 		"M4-A1", 			"Kagemitsu", 		"Portal Gun",	 	"Elucidator", 	"MP7",				"Dark Repulser"}},
		{"Skills", 	  new string[] {"Battle Healing", 	"Blade Throwing", 	"Dual Blades", 		"One-Handed", 		"Parrying", 	"Searching", 		"Tracking"}},
		{"Friend",    new string[] {"Kirito", 			"Asuna", 			"Sugu", 			"Klien", 			"Lizbeth"}},
		{"Weapons",   new string[] {"Simple Sword", 	"Simple Sheild"}},
		{"Armor",	  new string[] {"Cloak of Midnight"}},
		{"Accessory", new string[] {"Ring of Strength"}},

		{"CL4P-TP",   new string[] {"Player",	 		"Party_",			"Message",			"Location", 		"Settings"}},
		{"Player",    new string[] {"Equipment", 		"Items", 			"Skills"}},
		{"Party_",	  new string[] {"Party",     		"Friend", 			"Guild"}},
		{"Message",   new string[] {"Befriend",  		"Trade",       		"Duel",				"Marriage"}},
		{"Location",  new string[] {"Field Map",		"Dungeon Map", 		"Quests"}},
		{"Settings",  new string[] {"Options",   		"Help", 			"Logout"}},
		{"Equipment", new string[] {"Weapons",   		"Armor", 			"Accessory"}},
		{"Party", 	  new string[] {"Create",    		"Invite",			"Dissolve"}}
	};

	public Dictionary<string, int> typesDictionary = new Dictionary<string, int>() {
		{"CL4P-TP", 0}, {"Player", 0}, {"Party", 0}, {"Message",   0}, {"Location",  0}, {"Settings",  0}, {"Equipment", 1},
		{"Party_",  0}, {"Items", 1}, {"Skills", 1}, {"Friend", 1}, {"Weapons", 1}, {"Armor", 1}, {"Accessory", 1}
	};

	public Dictionary<string, int> tierDict = new Dictionary<string, int>() {
		{"Player", 1}, {"Party", 2}, {"Message", 1}, {"Location",  1}, {"Settings",  1}, {"Equipment", 2},
		{"Party_", 1}, {"Items", 2}, {"Skills",  2}, {"Friend", 2}, {"Weapons", 3}, {"Armor", 3}, {"Accessory", 3}
	};
	
	public string[] exceptions = new string[]{"Items", "Weapons", "Armor", "Accessory"};
	public string myApGpName = "";

	public void createGroup(string myParentName) {
		this.myParentName = myParentName;
		bool nonFriendlyDifferent = false;

		try {
			nonFriendlyDifferent = this.exceptions.Contains(this.transform.parent.transform.parent.GetComponent<MenuHandler>().myParentName);
		} catch (Exception e){
			Debug.Log(e.ToString());
		}

		try {
			this.myApGpName = this.transform.parent.GetComponent<MenuObject>().parent.myParentName;
		} catch(Exception e) {
			Debug.Log(e.ToString());
		}

		if(this.myParentName != "CL4P-TP") {
			try {
				this.myGroupTier = tierDict[this.myParentName];
			} catch(Exception e) {
				try {
					this.myGroupTier = tierDict[this.myApGpName] + 1;
				} catch(Exception e2) {
					Debug.Log("shits on fire yo: " + e2.ToString());
				}
			}
		}

		if(nonFriendlyDifferent) {
			createListIcons();
		} else if(this.myApGpName == "Friend") {
			createFriendZone();
		} else {
			createNormalGroup();
		}
	}

	public void createListIcons() {
		this.myGroupType = 2;
		this.myShape = 0;
		this.numberInMyGroup = 3;

		string[] namez = new string[]{"Start", "Minus", "Dissolve"};
		Vector3[] locs = new Vector3[]{new Vector3(-0.0224f, 0, 0), new Vector3( 0.006f, 0, 0), new Vector3( 0.0327f, 0, 0)};

		for(int icon = 0; icon < 3; icon++) {
			initObj(namez[icon], this.myShape, icon, this.myGroupType, locs[icon], new Vector3(0.0008f, 0.0008f, 0.0008f));
		}
	}

	public void createNormalGroup() {
		this.myGroupType  = this.typesDictionary[this.myParentName];
		this.myShape  = 1;

		if(this.myParentName == "CL4P-TP")
			this.myShape  = 0;

		this.numberInMyGroup = this.namesDictionary[this.myParentName].Length;

		if(this.numberInMyGroup % 2 == 0) {
			this.y2kDiff /= 2;
		}

		for(int icon = 0; icon < this.namesDictionary[this.myParentName].Length; icon++) {
			if(this.myParentName == "CL4P-TP")
				initObj(this.namesDictionary[this.myParentName][icon], this.myShape, icon, this.myGroupType, new Vector3(this.firstIconLocation.x, this.firstIconLocation.y - this.y1Diff * icon, this.firstIconLocation.z), this.t1Scale);
			else if(this.numberInMyGroup % 2 == 0)
				initObj(this.namesDictionary[this.myParentName][icon], this.myShape, icon, this.myGroupType, new Vector3(this.senpai[this.myGroupTier].x, this.numberInMyGroup / 2 * this.y2kDiff - this.y2kDiff * icon * 2, this.senpai[this.myGroupTier].z), this.nonT1Scale);
			else
				initObj(this.namesDictionary[this.myParentName][icon], this.myShape, icon, this.myGroupType, new Vector3(this.senpai[this.myGroupTier].x, this.numberInMyGroup / 2 * this.y2kDiff - this.y2kDiff * icon, this.senpai[this.myGroupTier].z), this.nonT1Scale);
		}
	}

	public void createFriendZone() {
		this.myGroupType = 0;
		this.myShape = 1;
		this.numberInMyGroup = 3;

		string[] namez = new string[]{"Message Box", "Position Check", "Profile"};

		for(int icon = 0; icon < 3; icon++) {
			initObj(namez[icon], this.myShape, icon, this.myGroupType, new Vector3(this.senpai[this.myGroupTier].x, y2kDiff - y2kDiff * icon, 0), this.nonT1Scale);
		}
	}
	
	void initObj(string name, int shape, int index, int type, Vector3 location, Vector3 scale) {
		MenuObject obj = new GameObject("MenuObject").AddComponent<MenuObject> ();
		obj.transform.SetParent(this.transform);
		obj.defineMe(name, shape, index, type, location, scale);
		obj.createMe ();
	}

}
