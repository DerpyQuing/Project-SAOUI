using UnityEngine;
using System.Collections;

public interface IHover {
	void handleHover();
	void handleHoverLoss();
}

public interface IPress {
	void handlePress();
	void handlePressLoss();
}