using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


class test2 : MonoBehaviour {

	public Dictionary<string, int> d = new Dictionary<string, int>() {
		{"cat", 2},
		{"dog", 1},
		{"llama", 0},
		{"iguana", -1}
	};

	
	void Start () {

		Debug.Log(d["cat"]);


	}
}