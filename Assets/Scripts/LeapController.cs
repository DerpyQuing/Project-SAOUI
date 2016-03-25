using UnityEngine;
using System.Collections;
using Leap;

public class LeapController : MonoBehaviour {

    private Controller controller;

    // Variable for locking 
    private bool canSetPrevious = true;

    // Why aren't these Vector 3's?
    // Prevoius finger tip positions
    private Vector previousIndexFingerTipPosition;
    private Vector previousMiddleFingerTipPosition;
    
    // Current finger tip positions
    private Vector currentIndexFingerTipPosition;
    private Vector currentMiddleFingerTipPosition;

    // Use this for initialization
    void Start() {
        // Setup the controller
        controller = new Controller();
    }

    // Update is called once per frame
    void Update() {
        // Get the frame of the leap controller
        Frame frame = controller.Frame();

        // If a hand is in the frame, we'll look for a swipe
        if (frame.Hands.Count >= 1) {
            // Get the hand
            Hand hand = frame.Hands[0];
            // Iterate through its fingers
            foreach (Finger finger in hand.Fingers) {
                // If the finger is the index, get it's tip position value
                if (finger.Type == Finger.FingerType.TYPE_INDEX)
                    currentIndexFingerTipPosition = finger.TipPosition;
                // If the finger is the middle finger, get its tip position value
                else if (finger.Type == Finger.FingerType.TYPE_MIDDLE)
                    currentMiddleFingerTipPosition = finger.TipPosition;
            }

            // Check to see if we are able to set previous values
            if (canSetPrevious)
                StartCoroutine(SetPrevious(currentMiddleFingerTipPosition, currentIndexFingerTipPosition));

            // Do some black magic here to figure out if we swipe down. All whilst we wait for gestures to be supported in orion
            if (Mathf.Abs(currentIndexFingerTipPosition.z - previousIndexFingerTipPosition.z) > 150f && Mathf.Abs(currentIndexFingerTipPosition.y - previousIndexFingerTipPosition.y) < 60f && Mathf.Abs(currentIndexFingerTipPosition.x - previousIndexFingerTipPosition.x) < 60f)
                // Create Menu becase we swiped down
                GetComponent<RevealMenuItems>().revealMenu();

            // Again, we do some sorcery to tell if we swipe left
            if (Mathf.Abs(currentIndexFingerTipPosition.z - previousIndexFingerTipPosition.z) > 50f && Mathf.Abs(currentIndexFingerTipPosition.y - previousIndexFingerTipPosition.y) < 60f && Mathf.Abs(currentIndexFingerTipPosition.x - previousIndexFingerTipPosition.x) < 140f)
                // Hide menu becase we swiped left
                GetComponent<RevealMenuItems>().revealMenu();
        }
    }

    // This sets the previous values for comparison and locks for us
    IEnumerator SetPrevious(Vector middle, Vector index) {
        // Locks here
        canSetPrevious = false;

        // Sets previous
        previousIndexFingerTipPosition = index;
        previousMiddleFingerTipPosition = middle;

        // Waits .5 seconds
        yield return new WaitForSeconds(.5f);

        // Unlocks
        canSetPrevious = true;
    }
}