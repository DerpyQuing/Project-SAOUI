using UnityEngine;
using System;
using System.Collections;

public class PressArea : MonoBehaviour {

	public MenuObject parent;
	public bool allowTouch = false;

	void Start () {
		parent = this.transform.parent.GetComponent<MenuObject> ();
		StartCoroutine(takeFive());
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "bone3" && allowTouch) 
			parent.onPress();
	}
	
	void OnTriggerExit(Collider other) {
		if (other.name == "bone3" && allowTouch)
			parent.onPressLoss();
	}

	public IEnumerator takeFive() {
		yield return new WaitForSeconds (2);
		allowTouch = true;
	}
}
