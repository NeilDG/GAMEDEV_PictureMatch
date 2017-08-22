using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents the results screen
/// BY: Neil DG
/// </summary>
public class ResultScreen : View {

	[SerializeField] private Text scoreText;
	[SerializeField] private Text correctMatchText;
	[SerializeField] private Text wrongMatchText;

	// Use this for initialization
	void Start () {
		this.SetCancelable (false); //do not allow this to be cancelled.
	}

	public void OnPlayAgainClicked() {
		SceneManager.LoadScene (SceneNames.GAME_SCENE);
	}

	public void OnMainMenuClicked() {
		SceneManager.LoadScene (SceneNames.MAIN_MENU_SCENE);
	}

	public void SetResult(int score, int correctMatches, int wrongMatches) {
		this.scoreText.text = score.ToString ();
		this.correctMatchText.text = correctMatches.ToString ();
		this.wrongMatchText.text = wrongMatches.ToString ();
	}
}
