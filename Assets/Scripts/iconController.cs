using UnityEngine;
using System.Collections;

public class IconController : MonoBehaviour {
	
	private SpriteRenderer spriteRenderer;

	private Sprite sprite;
	private string spritePath;
	private int sortingOrder;

	private Vector3 spritePosition;
	private Vector3 spriteScale;
	

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	#region Setters and Getters  

	// Using a Sprite directly
	public Sprite Image {
		get { return sprite; }
		set { 
			sprite = value; 
			spriteRenderer.sprite = value;
		}
	}

	// Using the Sprite path
	public string SpritePath {
		get { return spritePath; }
		set { 
			spritePath = value; 
			spriteRenderer.sprite = Resources.Load<Sprite>(value);
		}
	}
	
	public Vector3 SpritePosition {
		get { return spritePosition; }
		set { 
			spritePosition = value; 
			transform.localPosition = value;
		}
	}
	
	public Vector3 SpriteScale {
		get { return spriteScale; }
		set {
			spriteScale = value;
			transform.localScale = value;
		}
	}
	
	public int SortingOrder {
		get { return sortingOrder; }
		set {
			sortingOrder = value;
			spriteRenderer.sortingOrder = value;
		}
	}
	#endregion
	
	void Start() {
		
	}
	
}
