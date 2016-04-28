using UnityEngine;
using System.Collections;

public class ArmMenuItemController : MonoBehaviour, IHover, IPress {

    public bool Hover = false;
    public bool Press = false;
    public bool Select = false;

    public IconController iconController;

    public string mName = "SAO/ArmMenu/";

    public CollisionController hoverCollisionController;
    public CollisionController pressCollisionControler;

    public ArmMenuController armMenuController;

	// Use this for initialization
	void Start () {
        if (name.Equals("LockItem"))
            mName += "lock";
        else
            mName += "pin";

        hoverCollisionController.setupInterfaces(this, this);
        hoverCollisionController.IsHover = true;
        pressCollisionControler.setupInterfaces(this, this);
        pressCollisionControler.IsHover = false;
	}
	

    public void handleHover()  {
        Hover = true;
        iconController.Image = Resources.Load<Sprite>(mName + "_on");
    }

    public void handlePress() {
        if (Hover) 
            Press = true;
    }

    public void handleHoverLoss() {
        Hover = false;
        if (!Select)
            iconController.Image = Resources.Load<Sprite>(mName);
    }

    public void handlePressLoss() {
        Press = false;
        if (!Select) 
            onSelect();
        else if(Select) 
            onSelectionLoss();
        
    }

    public void onSelect() {
        Select = true;
        handleSelectionChange();

    }

    public void onSelectionLoss() {
        // Do things here
        Select = false;
        handleSelectionChange();
    }

    public void handleSelectionChange()  {
        if (name.Equals("LockItem"))
            armMenuController.lockMenu(Select);

        // Handle pinch later
    }



}
