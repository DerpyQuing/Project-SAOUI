using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System;

public class generateObjects : MonoBehaviour {
	
	void Start () {

		for(int layerOne = 0; layerOne < 5; layerOne++) {
			GameObject layerOneObj = new GameObject();
			layerOneObj.transform.SetParent(this.transform);
			layerOneObj.name = "layerOneItem" + layerOne;
			layerOneObj.AddComponent<SpriteRenderer>();
			//layerOneItem.AddComponent<MenuObject>();

			GameObject hoverObj = new GameObject();
			hoverObj.transform.SetParent(layerOneObj.transform);
			hoverObj.name = "hoverArea";
			//hoverObj.AddComponent<HoverArea>();

			GameObject pressObj = new GameObject();
			pressObj.transform.SetParent(layerOneObj.transform);
			pressObj.name = "pressArea";
			//pressObj.AddComponent<PressArea>();

		}

		for(int boxItem = 0; boxItem < 20; boxItem++) {
			// Set up the base object
			GameObject boxItemObj = new GameObject();
			boxItemObj.transform.SetParent(this.transform);
			boxItemObj.name = "boxItem" + boxItem;
			boxItemObj.AddComponent<SpriteRenderer>();
			//boxItemObj.AddComponent<MenuObject>();
		
			// Setup the hover child
			GameObject hoverObj = new GameObject();
			hoverObj.transform.SetParent(boxItemObj.transform);
			hoverObj.name = "hoverArea";
			//hoverObj.AddComponent<HoverArea>();

			// Setup the press child
			GameObject pressObj = new GameObject();
			pressObj.transform.SetParent(boxItemObj.transform);
			pressObj.name = "pressArea";
			//pressObj.AddComponent<PressArea>();
		
			// Setup the text child
			GameObject textHolder = new GameObject();
			textHolder.transform.SetParent(boxItemObj.transform);
			textHolder.name = "textHolder";
			textHolder.AddComponent<TextMesh>();

			// Setup the icon child
			GameObject iconHolder = new GameObject();
			iconHolder.transform.SetParent(boxItemObj.transform);
			iconHolder.name = "iconHolder";
			iconHolder.AddComponent<SpriteRenderer>();	

		}

		for(int smallCircle = 0; smallCircle < 20; smallCircle++) {
			GameObject smallCircleObj = new GameObject();
			smallCircleObj.transform.SetParent(this.transform);
			smallCircleObj.name = "smallCircle" + smallCircle;
			smallCircleObj.AddComponent<SpriteRenderer>();
			//layerOneItem.AddComponent<MenuObject>();
			
			GameObject hoverObj = new GameObject();
			hoverObj.transform.SetParent(smallCircleObj.transform);
			hoverObj.name = "hoverArea";
			//hoverObj.AddComponent<HoverArea>();
			
			GameObject pressObj = new GameObject();
			pressObj.transform.SetParent(smallCircleObj.transform);
			pressObj.name = "pressArea";
			//pressObj.AddComponent<PressArea>();
		}

	}


}
