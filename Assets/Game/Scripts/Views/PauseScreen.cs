using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : View {

	// Use this for initialization
	void Start () {
		
	}

	public void OnResumeClicked() {
		this.Hide ();
	}

	public override void OnShowStarted ()
	{
		base.OnShowStarted ();
		PlayerScoreHandler.Instance.PauseTimer ();
	}

	public override void OnHideCompleted ()
	{
		base.OnHideCompleted ();
		PlayerScoreHandler.Instance.ResumeTimer ();
	}

	public void OnMainMenuClicked() {
		DialogInterface choiceDialog = DialogBuilder.Create (DialogBuilder.DialogType.CHOICE_DIALOG);
		choiceDialog.SetMessage ("Are you sure? All progress wil be lost.");
		choiceDialog.SetOnConfirmListener (() => {
			SceneManager.LoadScene(SceneNames.MAIN_MENU_SCENE);
		});

	}

}
