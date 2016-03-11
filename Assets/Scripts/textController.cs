using UnityEngine;
using System.Collections;

public class textController : MonoBehaviour {

	private TextMesh textMesh;
	private MeshRenderer meshRenderer;

	private Vector3 textPosition;
	private Vector3 textScale;

	private Font textFont;

	private int sortingOrder;

	private Color textColor;

	private string text;
	private int textSize;

	void Awake () {
		textMesh = GetComponent<TextMesh>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	#region Setters and Getters    
	public string Text {
		get { return text; }
		set { 
			text = value; 
			textMesh.text = value;
		}
	}

	public Vector3 TextPosition {
		get { return textPosition; }
		set { 
			textPosition = value; 
			transform.localPosition = value;
		}
	}

	public Vector3 TextScale {
		get { return textScale; }
		set {
			textScale = value;
			transform.localScale = value;
		}
	}

	public int TextSize {
		get { return textSize; }
		set {
			textSize = value;
			textMesh.fontSize = value;
		}
	}

	public Color TextColor {
		get { return textColor; }
		set {
			textColor = value;
			textMesh.color = value;
		}
	}

	public int SortingOrder {
		get { return sortingOrder; }
		set {
			sortingOrder = value;
			meshRenderer.sortingOrder = value;
		}
	}

	public Font TextFont {
		get { return textFont; }
		set {
			textFont = value;
			textMesh.font = value;
		}
	}

	#endregion
	
}
