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
		Debug.Log(gm.name);
		Debug.Log(shape);
		if(shape.Equals("rectangle")) {
			foreach(BoxCollider boxCollider in gm.GetComponentsInChildren<BoxCollider>())
				boxCollider.enabled = true;
			
			foreach(SpriteRenderer spriteRenderer in gm.GetComponentsInChildren<SpriteRenderer>())
				spriteRenderer.enabled = true;
			
			gm.GetComponentInChildren<MeshRenderer>().enabled = true;
			
		} else if(shape.Equals("circle")) {
			foreach(CapsuleCollider capsuleCollider in gm.GetComponentsInChildren<CapsuleCollider>())
				capsuleCollider.enabled = true;
			gm.GetComponentInChildren<SpriteRenderer>().enabled = true;
		}
	}

	public void hideItem(GameObject gm, string shape) {
		if(shape.Equals("rectangle")) {
			foreach(BoxCollider boxCollider in gm.GetComponentsInChildren<BoxCollider>())
				boxCollider.enabled = false;
			
			foreach(SpriteRenderer spriteRenderer in gm.GetComponentsInChildren<SpriteRenderer>())
				spriteRenderer.enabled = false;
			
			gm.GetComponentInChildren<MeshRenderer>().enabled = false;
			
		} else if(shape.Equals("circle")) {
			foreach(CapsuleCollider capsuleCollider in gm.GetComponentsInChildren<CapsuleCollider>())
				capsuleCollider.enabled = false;
			gm.GetComponentInChildren<SpriteRenderer>().enabled = false;
		}
	}



	IEnumerator waitForSetup() {
		yield return new WaitForSeconds(3);
		handleChildrenSetup();
	}


}
