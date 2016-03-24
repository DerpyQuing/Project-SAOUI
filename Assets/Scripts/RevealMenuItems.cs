using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RevealMenuItems : MonoBehaviour {

	public GameObject menuHolder;

	public GameObject[] allMenuItems;

	public bool isMenuRevealed = false;
	
	// Use this for initialization
	void Start () {
		menuHolder = GameObject.Find("MenuHolder");
	}


	public void updateMenuItemsReference() {
		allMenuItems = GameObject.FindGameObjectsWithTag("MenuItem");
	}

	public void revealMenu() {
		if(!isMenuRevealed) {
			foreach(Transform childTransform in menuHolder.transform) {
				revealItem(childTransform.gameObject, "circle");
			}
		}
	}

	public void hideMenu() {
		if(isMenuRevealed) {
			hideAllItems(menuHolder.GetComponent<GenerateMenuItems>()._jsonNode["Menu"]);
		}
	}

	public void hideAllItems(JSONNode jsonNode) {
		for(int i = 0; i < jsonNode.Count; i++) {
			if(jsonNode[i]["_hasChildren"] != null) {
				if(jsonNode[i]["_hasChildren"].AsBool) {
					hideAllItems(jsonNode[i]); 
				}
				
				if(!jsonNode[i]["_hasChildren"].AsBool) 
					hideItem(GameObject.Find(jsonNode[i]["_name"] + "-" + jsonNode[i]["_parent"]), jsonNode[i]["_itemShape"]);
				else
					hideItem(GameObject.Find(jsonNode[i]["_name"]), jsonNode[i]["_itemShape"]);
				
			}
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

	
	public void revealItem(GameObject gm, string shape) {
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


}
