using UnityEngine;
using System.Collections;

public class ArmMenuController : MonoBehaviour {

    public GameObject[] children;

    public LeapController leapController;

	// Use this for initialization
	void Start () {
        children = new GameObject[transform.childCount];
        for (int childIndex = 0; childIndex < transform.childCount; childIndex++) {
            children[childIndex] = transform.GetChild(childIndex).gameObject;
        }
        
        hideItems();
    }
	

    public void hideItems() {
        foreach (GameObject child in children)
            hideItem(child);
    }

    public void revealItems() {
        foreach (GameObject child in children)
            revealItem(child);
    }

    public void revealItem(GameObject gm) {
        for (int i = 0; i < 2; i++)
            gm.GetComponentsInChildren<CapsuleCollider>()[i].enabled = true;
        gm.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void hideItem(GameObject gm) {
        for (int i = 0; i < 2; i++) {
            gm.GetComponentsInChildren<CapsuleCollider>()[i].enabled = false;
        }
        gm.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void lockMenu(bool state) {
        leapController.canCreateMenu = state ? false : true;
    }
   



}
