using UnityEngine;
using System;
using System.Collections;

public class HoverArea : MonoBehaviour {

	public MenuObject parent;
	public bool allowTouch = false;

	void Start () {
		parent = this.transform.parent.GetComponent<MenuObject> ();
		StartCoroutine(takeFive());
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "bone3" && allowTouch)
			parent.onHover();
	}
	
	void OnTriggerExit(Collider other) {
		if (other.name == "bone3" && allowTouch)
			parent.onHoverLoss();
	}

	public IEnumerator takeFive() {
		yield return new WaitForSeconds (2);
		allowTouch = true;
	}
}
