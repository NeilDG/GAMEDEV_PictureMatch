using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : View {

	// Use this for initialization
	void Start () {
		
	}

	public void OnPauseClicked() {
		ViewHandler.Instance.Show (ViewNames.PAUSE_SCREEN_NAME);
	}

}
