using UnityEngine;
using System.Collections;
using SimpleJSON;

public class ChildController : MonoBehaviour {

	public GameObject[] children;

	// Use this for initialization
	void Start () {
		StartCoroutine(waitForSetup());
	}
	
	void handleChildrenSetup() {
		children = new GameObject[transform.childCount];
		for(int childIndex = 0; childIndex < transform.childCount; childIndex++) {
			children[childIndex] = transform.GetChild(childIndex).gameObject;
		}
	}

	public void handleChildHover(string childName) {
		foreach(GameObject child in children) {
			if(child.name != childName)
				child.GetComponent<MenuItemController>().Hover = false;
		}
	}

	public void handleChildPress(string childName) {
		foreach(GameObject child in children) {
			if(child.name != childName)
				child.GetComponent<MenuItemController>().Press = false;
		}
	}

	public bool isAChildHovered() {
		foreach(GameObject child in children) {
			if(child.GetComponent<MenuItemController>().Hover)
				return true;
		}
		return false;
	}

	public bool isAChildPressed() {
		foreach(GameObject child in children) {
			if(child.GetComponent<MenuItemController>().Press)
				return true;
		}
		return false;
	}

	public bool isAChildSelected() {
		foreach(GameObject child in children) {
			if(child.GetComponent<MenuItemController>().Selected)
				return true;
		}
		return false;
	}

	
	public void revealItem(GameObject gm, string shape) {
		if(shape.Equals("rectangle")) {
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<SpriteRenderer>()[i].enabled = true;
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<BoxCollider>()[i].enabled = true;
			gm.GetComponentInChildren<MeshRenderer>().enabled = true;
			
		} else if(shape.Equals("circle")) {
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<CapsuleCollider>()[i].enabled = true;
			gm.GetComponentInChildren<SpriteRenderer>().enabled = true;
		}
	}

	public void hideItem(GameObject gm, string shape) {
		if(shape.Equals("rectangle")) {
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<BoxCollider>()[i].enabled = false;
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
			gm.GetComponentInChildren<MeshRenderer>().enabled = false;
			
		} else if(shape.Equals("circle")) {
			for(int i = 0; i < 2; i++) 
				gm.GetComponentsInChildren<CapsuleCollider>()[i].enabled = false;
			gm.GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}


	// .....
	IEnumerator waitForSetup() {
		yield return new WaitForSeconds(2);
		handleChildrenSetup();
	}


}
