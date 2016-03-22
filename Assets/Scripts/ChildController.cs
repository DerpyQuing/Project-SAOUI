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


	public bool test = false;
	void Update () {
		if(test) {
			test = false;
			children[0].GetComponent<MenuItemController>().Hover = true;
		}
	}




	IEnumerator waitForSetup() {
		yield return new WaitForSeconds(3);
		handleChildrenSetup();
	}


}
