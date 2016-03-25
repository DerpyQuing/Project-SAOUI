using UnityEngine;
using System.Collections;

public class TextController : MonoBehaviour {

	private TextMesh textMesh;
	private MeshRenderer meshRenderer;

	private Vector3 textPosition;
	private Vector3 textScale;

	private Font textFont;

	private int sortingOrder;

	private Color textColor;

	private string text;
	private int textSize;

    // We call this to get our textMesh and MeshRenderer componenets
	void Awake () {
		textMesh = GetComponent<TextMesh>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	#region Setters and Getters    
    // Lets us get/set the text itself
	public string Text {
		get { return text; }
		set { 
			text = value; 
			textMesh.text = value;
		}
	}

    // Lets us get/set the Text Position
	public Vector3 TextPosition {
		get { return textPosition; }
		set { 
			textPosition = value; 
			transform.localPosition = value;
		}
	}
    // Lets us get/set the Text Scale
    public Vector3 TextScale {
		get { return textScale; }
		set {
			textScale = value;
			transform.localScale = value;
		}
	}

    // Lets us get/set the Text Size
    public int TextSize {
		get { return textSize; }
		set {
			textSize = value;
			textMesh.fontSize = value;
		}
	}

    // Lets us get/set the Text Color
    public Color TextColor {
		get { return textColor; }
		set {
			textColor = value;
			textMesh.color = value;
		}
	}

    // Lets us get/set the sorting order for the text
    public int SortingOrder {
		get { return sortingOrder; }
		set {
			sortingOrder = value;
			meshRenderer.sortingOrder = value;
		}
	}

    // Lets us get/set the Text font
    public Font TextFont {
		get { return textFont; }
		set {
			textFont = value;
			textMesh.font = value;
		}
	}

    #endregion

    // Try commenting it out now that I have Awake()
    void Start() { }
	
}
