using UnityEngine;
using System;
using System.Collections;

public class onCollision : MonoBehaviour {
	
	public bool allowTouch = false;
	public bool isHover;

	void Start () {
		StartCoroutine(takeFive());
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.name == "bone3" && allowTouch)
			if(isHover)
				Debug.Log("Hover");
			else 
				Debug.Log("Press");
		
	}
	
	void OnTriggerExit(Collider other) {
		if (other.name == "bone3" && allowTouch)
			if(isHover)
				Debug.Log("Hover Loss");
			else 
				Debug.Log("Press Loss");
	}
	
	public IEnumerator takeFive() {
		yield return new WaitForSeconds (2);
		allowTouch = true;
	}
}
