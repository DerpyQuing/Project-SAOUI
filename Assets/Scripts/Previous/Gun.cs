using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Change the material when certain poses are made with the Myo armband.
// Vibrate the Myo armband when a fist pose is made.
public class Gun : MonoBehaviour {

	public GameObject myo = null;
	private Pose _lastPose = Pose.Unknown;
	
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			
			if (thalmicMyo.pose == Pose.Fist) {
				thalmicMyo.Vibrate (VibrationType.Medium);
				// blue or shoot
				
				ExtendUnlockAndNotifyUserAction (thalmicMyo);
				
			} else if (thalmicMyo.pose == Pose.FingersSpread) {
				thalmicMyo.Vibrate (VibrationType.Medium);
				// yellow

				ExtendUnlockAndNotifyUserAction (thalmicMyo);
			}
		}
	}
	
	// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
	// recognized.
	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}
		
		myo.NotifyUserAction ();
	}
}
