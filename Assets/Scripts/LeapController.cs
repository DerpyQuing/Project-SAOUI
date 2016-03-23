using UnityEngine;
using System.Collections;
using Leap;

public class LeapController: MonoBehaviour {

	private Controller controller;

	private bool canSetPrevious = true;

	private Vector previousIndexFingerTipPosition;
	private Vector previousMiddleFingerTipPosition;

	private Vector currentIndexFingerTipPosition;
	private Vector currentMiddleFingerTipPosition;

	// Use this for initialization
	void Start () {
		controller = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();

		if(frame.Hands.Count >= 1) {
			Hand hand = frame.Hands[0];
			foreach(Finger finger in hand.Fingers) {
				if(finger.Type == Finger.FingerType.TYPE_INDEX) {
					currentIndexFingerTipPosition = finger.TipPosition;
				} else if(finger.Type == Finger.FingerType.TYPE_MIDDLE) {
					currentMiddleFingerTipPosition = finger.TipPosition;
				}
			}

			if(canSetPrevious)
				StartCoroutine(SetPrevious(currentMiddleFingerTipPosition, currentIndexFingerTipPosition));

            if(Mathf.Abs(currentIndexFingerTipPosition.z - previousIndexFingerTipPosition.z) > 150f && Mathf.Abs(currentIndexFingerTipPosition.y - previousIndexFingerTipPosition.y) < 50f  && Mathf.Abs(currentIndexFingerTipPosition.x - previousIndexFingerTipPosition.x) < 40f)  
				GetComponent<RevealMenuItems>().revealMenu();
			///Debug.Log(currentIndexFingerTipPosition.x - previousIndexFingerTipPosition.x);

		}
	}

	IEnumerator SetPrevious(Vector middle, Vector index) {
		canSetPrevious = false;
		previousIndexFingerTipPosition = index;
		previousMiddleFingerTipPosition = middle;
		yield return new WaitForSeconds(.5f);
		canSetPrevious = true;
	}





}
//Debug.Log(GameObject.FindGameObjectsWithTag("MenuItem"));
