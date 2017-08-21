using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : View {

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		
	}

	public void OnPlayClicked() {
		//TEMP:
		ViewHandler.Instance.Show(ViewNames.TEST_SCREEN_NAME);

	}

	public void OnQuitClicked() {
		Application.Quit();
	}
}
