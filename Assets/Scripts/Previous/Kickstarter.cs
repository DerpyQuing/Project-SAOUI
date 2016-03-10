using UnityEngine;
using System.Collections;
using Leap;

public class Kickstarter : MonoBehaviour {
	
	public MenuHandler hand;
	public GameObject kicker;
	public AudioSource cortana;

	public Controller controller;
	public DeafListener deafListener;

	public bool menuCreated = false;

	public Loom loom;

	public Vector3 createMenuHere;
	
	void Start () {
		this.loom = Loom.Current;
		this.deafListener = new DeafListener();
		//this.deafListener.punch(this);
		this.controller = new Controller();
		//this.controller.AddListener(this.deafListener);
		createInterface(Vector3.zero);
	}

	public void playPressSound() {
		cortana.PlayOneShot((AudioClip)Resources.Load("Sounds/press"));
	}
	
	public void playHoverSound() {
		cortana.PlayOneShot((AudioClip)Resources.Load("Sounds/menu"));
	}

	public void createInterface(Vector3 pos) {
		this.createMenuHere = new Vector3(-0.1906f, 0.152f, 0.4727f); //new Vector3(0, 0.317f, 0.498f);		// -0.212, 0.317, 0.498
			//pos + new Vector3(0, .02f, 0); //new Vector3(pos.x, pos.y, pos.z);
		this.kicker = new GameObject("MenuHolder");
		this.kicker.transform.SetParent(GameObject.Find("TrackingSpace").transform);
		this.kicker.transform.localPosition = Vector3.zero;
		this.hand = this.kicker.AddComponent<MenuHandler>();
		this.hand.firstIconLocation = this.createMenuHere;
		this.hand.createGroup("CL4P-TP");
		this.cortana = this.kicker.AddComponent<AudioSource>();
		this.cortana.PlayOneShot((AudioClip)Resources.Load("Sounds/Popup.SAO.Launcher"));
	}

	public void killInterface() {
		Destroy(this.kicker);
		this.hand = null;
		this.kicker = null;
	}
	
}
