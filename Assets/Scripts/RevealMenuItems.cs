using UnityEngine;
using System.Collections;
using SimpleJSON;

public class RevealMenuItems : MonoBehaviour {

	public GameObject menuHolder;

	public GameObject[] allMenuItems;

	public bool isMenuRevealed = false;

	public bool isTrue = false;

	// Use this for initialization
	void Start () {
		menuHolder = GameObject.Find("MenuHolder");
	}

	void Update() {
		if(isTrue)
			hideMenu();
	}

	public void updateMenuItemsReference() {
		allMenuItems = GameObject.FindGameObjectsWithTag("MenuItem");
	}

	public void revealMenu() {
		if(!isMenuRevealed) {
			foreach(Transform childTransform in menuHolder.transform) {
				childTransform.gameObject.SetActive(true);
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
				if(jsonNode[i]["_text"] != null) {
					GameObject.Find(jsonNode[i]["_text"]).SetActive(false);
				} else if(jsonNode[i]["_name"] != null) {
					GameObject.Find(jsonNode[i]["_name"]).SetActive(false);
				} 
			}
		}
	}

}
