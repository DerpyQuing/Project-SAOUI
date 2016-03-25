using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System;

public class GenerateMenuItems : MonoBehaviour {

	// This region contains all of the different types of menu items we want to create
	#region Types of Menu Items  
	public GameObject rectMenuItem;
	public GameObject circleMenuItem;
	#endregion

	public JSONNode _jsonNode;
	
	// Container for the prefabs above
	public Dictionary<string, GameObject> menuItemPrefabContainer = new Dictionary<string, GameObject>();
	
	// Use this for initialization
	void Start () {

		// Add prefabs to container
		// This allows us to skip the ugly if/else
		menuItemPrefabContainer.Add("rectangle", rectMenuItem);
		menuItemPrefabContainer.Add("circle", circleMenuItem);

		// Create a new instance of the ReadFromFile class
		// Allows us to access the method that turns a .json file into a string
		ReadFromFile readFromFile = new ReadFromFile();

		// Parse the .json file into a string
		string menu = readFromFile.LoadJSONResourceFile("ReferenceFiles/test.json");

		// Turn that string into a SimpleJSON JSONNode
		_jsonNode = JSONNode.Parse(menu);

        // Use a recursive method to generate all of the Menu Items at start up
        generateMenuItems(_jsonNode["Menu"]);

        // This method "creates" the menu items
        createMenuItems(_jsonNode["Menu"]);

        // Positions the menu items correctly
        positionMenuItems(_jsonNode["Menu"]);

        // Hides the menu items
        hideAllItems(_jsonNode["Menu"]);
    }

    // Call this to create every item in the menu by using recursion
    public void generateMenuItems(JSONNode jsonNode) {
        // Iterate through every thing in the current layer of the json node, including attributes
		for(int i = 0; i < jsonNode.Count; i++) {
            // This is to weed out the attributes, because every menu item has this as true or false
			if(jsonNode[i]["_hasChildren"] != null) {
                // Creates the menu item for that jsonnode node
				createMenuItem (jsonNode[i]);
                // If that item has children we must create it's children
				if(jsonNode[i]["_hasChildren"].AsBool) {
                    // So we create those children
                    generateMenuItems(jsonNode[i]); 
				}
			}
		}
	}

	// Handles creating the GameObject, then passes it over to the proper method for giving it it's correct values and what not
	public void createMenuItem(JSONNode jsonNode) {

		// Instantiate an instace of the Menu Item Prefab we want. (Based of the "_itemShape" value)
		// Set the position of this Menu Item at (0, -10f, 0) so its out of site (This can be adjusted)
		GameObject menuItem = (GameObject) Instantiate(menuItemPrefabContainer[jsonNode["_itemShape"]], new Vector3(0f, 2f, 0f), Quaternion.identity);
        // Give the item a unique tag for .... reasons
		menuItem.tag = "MenuItem";

		// If there are a lot of similarities when assigning values and stuff to the prefab, put them here. No need for code duplication
		
        // Sets the name of the GameObject
		menuItem.name = jsonNode["_name"];

        // If the menu item has no children, it's name could be repetitive, so we add the parent's name to it so there are no duplicates
		if(!jsonNode["_hasChildren"].AsBool) {
			menuItem.name += "-" + jsonNode["_parent"];
		}
        // Passes data to the class that controls that menu item
        MenuItemController menuItemController = menuItem.GetComponent<MenuItemController>();
        menuItemController.giveData(jsonNode, menuItem);
	}
    
    // This method goes through all of the menu items and handles their creation, not generation (i.e. setting location, icons, text, etc)
    public void createMenuItems(JSONNode jsonNode) {
        // Iterate through every thing in the current layer of the json node, including attributes
        for (int i = 0; i < jsonNode.Count; i++) {
            // This is to weed out the attributes, because every menu item has this as true or false
            if (jsonNode[i]["_hasChildren"] != null) {
             
                // Not sure if I should go bottom up or top down, lets find out
                if (!jsonNode[i]["_hasChildren"].AsBool)
                    GameObject.Find(jsonNode[i]["_name"] + "-" + jsonNode[i]["_parent"]).GetComponent<MenuItemController>().handleCreation();
                else
                    GameObject.Find(jsonNode[i]["_name"]).GetComponent<MenuItemController>().handleCreation();
                // If that item has children we must create it's children
                if (jsonNode[i]["_hasChildren"].AsBool)
                    createMenuItems(jsonNode[i]);
            }
        }
    }

    // This method goes through all of the menu items and handles their creation, not generation (i.e. setting location, icons, text, etc)
    public void positionMenuItems(JSONNode jsonNode) {
        // Iterate through every thing in the current layer of the json node, including attributes
        for (int i = 0; i < jsonNode.Count; i++) {
            // This is to weed out the attributes, because every menu item has this as true or false
            if (jsonNode[i]["_hasChildren"] != null) {
                // Not sure if I should go bottom up or top down, lets find out
                GameObject gm = null;
                if (!jsonNode[i]["_hasChildren"].AsBool)
                    gm = GameObject.Find(jsonNode[i]["_name"] + "-" + jsonNode[i]["_parent"]);
                else
                    gm = GameObject.Find(jsonNode[i]["_name"]);
                // This corrects the item's positoin, because having it bundled just didn't work
                gm.GetComponent<MenuItemController>().handlePosition();
                // This sets the child controller's child list
                gm.GetComponent<MenuItemController>().setChildrenController();

                // If that item has children we must create it's children
                if (jsonNode[i]["_hasChildren"].AsBool)
                    positionMenuItems(jsonNode[i]);
            }
        }
    }

    // So we can't set them to inactive, have to deactivate all of their components. Sort of a hassle, not gonna lie
    // Call this to hide every menu item (beneath and including the given jsonnode node)
    // The same concept as generating the menu items, just hiding them now. 
    public void hideAllItems(JSONNode jsonNode) {
        // Iterate through every thing in the current layer of the json node, including attributes
        for (int i = 0; i < jsonNode.Count; i++) {
            // This is to weed out the attributes, because every menu item has this as true or false
            if (jsonNode[i]["_hasChildren"] != null) {
                // If that item has children we must hide it's children
                if (jsonNode[i]["_hasChildren"].AsBool) {
                    // So we hide its children
					hideAllItems(jsonNode[i]); 
				}
                // We put this after the recursion part because that way we hide from the bottom up
                // I could probably create a new var in the json or somewhere and use a dictionary instead of these blaspheomus if/else statements
                // If the item has no children, it's gameobject name will be different, so we have to account for that
                if (!jsonNode[i]["_hasChildren"].AsBool) 
                    // Hide item. I don't like that I'm calling gm.find here, it seems too costly
					hideItem(GameObject.Find(jsonNode[i]["_name"] + "-" + jsonNode[i]["_parent"]), jsonNode[i]["_itemShape"]);
				else
                    hideItem(GameObject.Find(jsonNode[i]["_name"]), jsonNode[i]["_itemShape"]);
			}
		}
	}

    // I cleaned up aisle code, not so bad anymore. Just the setchildren thing to fix, I think
    // This method is what "hides" our menu item. Does different things based on its shape, because the menu items have different.... just look at the prefabs
	public void hideItem(GameObject gm, string shape) {
		if(shape.Equals("rectangle")) {
            // This feels pretty clean, just disables all of the box colliders for the menu item (hover and press)
			foreach(BoxCollider boxCollider in gm.GetComponentsInChildren<BoxCollider>())
				boxCollider.enabled = false;
            // Same here, disables both sprite renderers
			foreach(SpriteRenderer spriteRenderer in gm.GetComponentsInChildren<SpriteRenderer>())
				spriteRenderer.enabled = false;
            // Disable the mesh renderer for the text
			gm.GetComponentInChildren<MeshRenderer>().enabled = false;

		} else if(shape.Equals("circle")) {
            // Disable the 2 capsule colliders (hover and press)
			foreach(CapsuleCollider capsuleCollider in gm.GetComponentsInChildren<CapsuleCollider>())
				capsuleCollider.enabled = false;
            // Disable the sprite renderer
			gm.GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}


}









