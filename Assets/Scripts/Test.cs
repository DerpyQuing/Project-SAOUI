using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {

	// This region contains all of the different types of menu items we want to create
	#region Types of Menu Items  
	public GameObject rectMenuItem;
	public GameObject circleMenuItem;
	#endregion

	// Container for the prefabs above
	public Dictionary<string, GameObject> menuItemPrefabContainer = new Dictionary<string, GameObject>();

	// Container for the methods that create the menu items
	public Dictionary<string, Delegate> menuItemCreationMethodContainer = new Dictionary<string, Delegate>();
	
	// Use this for initialization
	void Start () {

		// Add prefabs to container
		// This allows us to skip the ugly if/else
		menuItemPrefabContainer.Add("rectangle", rectMenuItem);
		menuItemPrefabContainer.Add("circle", circleMenuItem);

		// Adds methods to the container
		// This also allows us to skip the if/else when it comes to deciding which shape of a menu item to create
		menuItemCreationMethodContainer["circle"] = new Func<JSONNode, GameObject, bool>(createCircleTestItem);
		menuItemCreationMethodContainer["rectangle"] = new Func<JSONNode, GameObject, bool>(createRectangleTestItem);

		// Create a new instance of the ReadFromFile class
		// Allows us to access the method that turns a .json file into a string
		ReadFromFile readFromFile = new ReadFromFile();

		// Parse the .json file into a string
		string menu = readFromFile.LoadJSONResourceFile("ReferenceFiles/test.json");

		// Turn that string into a SimpleJSON JSONNode
		JSONNode jsonNode = JSONNode.Parse(menu);

		// Use a recursive method to generate all of the Menu Items at start up
		testRecursiveness(jsonNode["Menu"]);
	}

	// Recursion is "neat"
	public void testRecursiveness(JSONNode jsonNode) {
		for(int i = 0; i < jsonNode.Count; i++) {
			if(jsonNode[i]["_hasChildren"] != null) {
				createTestItems (jsonNode[i]);
				if(jsonNode[i]["_hasChildren"].AsBool) {
					testRecursiveness(jsonNode[i]); 
				}
			}
		}
	}

	// Handles creating the GameObject, then passes it over to the proper method for giving it it's correct values and what not
	public void createTestItems (JSONNode jsonNode) {

		// Instantiate an instace of the Menu Item Prefab we want. (Based of the "_itemShape" value)
		// Set the position of this Menu Item at (0, -10f, 0) so its out of site (This can be adjusted)
		GameObject menuItem = (GameObject) Instantiate(menuItemPrefabContainer[jsonNode["_itemShape"]], new Vector3(0f, -10f, 0f), Quaternion.identity);

		// If there are a lot of similarities when assigning values and stuff to the prefab, put them here. No need for code duplication

		// Sets the name of the GameObject, just for visual help
		if(jsonNode["_name"] != null) {
			menuItem.name = jsonNode["_name"];
		} else if(jsonNode["_text"] != null) {
			menuItem.name = jsonNode["_text"];
		}

		// Create the Menu Item (ie. Give it all its unique attributes)
		menuItemCreationMethodContainer[jsonNode["_itemShape"]].DynamicInvoke(jsonNode, menuItem);

	}

	// This region contains all of the methods for creating the Menu Item (Not the GameObject)
	#region Menu Item Creation Methods

	// This could be done much better, no need for this duplication. Fix it later i guess


	// The return for this method has no meaning to us. 
	// Not an expert on C# but it looks like we can't have our Dictionary of Delegates without the methods returning something
	public bool createCircleTestItem(JSONNode jsonNode, GameObject menuItem) {
		CircleMenuItemController cmic = menuItem.GetComponent<CircleMenuItemController>();
		cmic.giveData(jsonNode, menuItem);
		cmic.handleCreation();
		return true;
	}

	// The return for this method has no meaning to us. See above if curious
	public bool createRectangleTestItem(JSONNode jsonNode, GameObject menuItem) {
		RectangleMenuItemController cmic = menuItem.GetComponent<RectangleMenuItemController>();
		cmic.giveData(jsonNode, menuItem);
		return true;
	}

	#endregion









}
