using UnityEngine;
using System.Collections;
using System;
using Leap;

public class DeafListener : MonoBehaviour {//Listener {

	/*
	 * 
	public bool createIt = false;

	public Kickstarter hitmonlee;

	public void punch(Kickstarter toe) {
		this.hitmonlee = toe;
	}
	
	public override void OnConnect (Controller controller) {
		Debug.Log ("I'm in");
		controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
	}
	
	public override void OnFrame (Controller controller) {
		Frame frame = controller.Frame();
		GestureList gestures = frame.Gestures();
		SwipeGesture swiperNoSwiping;
		for(int i = 0; i < gestures.Count; i++) {
			Gesture gesture = gestures[i];
			switch (gesture.Type) {
				case Gesture.GestureType.TYPESWIPE:
					swiperNoSwiping = new SwipeGesture(gesture);
		
					if(swiperNoSwiping.Direction.z > .5 && Math.Abs(swiperNoSwiping.Direction.x) < .35 && Math.Abs(swiperNoSwiping.Direction.y) < .35 && this.createIt == false) {
						Loom.QueueOnMainThread(()=>{
							this.hitmonlee.createInterface(GameObject.Find("GlowRobotFullRightHand(Clone)").transform.FindChild("index").transform.FindChild("bone3").transform.position);//swiperNoSwiping.Position);
						});

						this.createIt = true;
				} else if(swiperNoSwiping.Direction.x > .9 && Math.Abs(swiperNoSwiping.Direction.y) < .2 && Math.Abs(swiperNoSwiping.Direction.z) < .2 && this.createIt == true) {
						Loom.QueueOnMainThread(()=>{
							this.hitmonlee.killInterface();
						});
						this.createIt = false;
					}
					break;
					
				default:
					break;
			}
		}	
	}

	*/
}
