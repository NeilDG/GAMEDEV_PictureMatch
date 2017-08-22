using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player score handler binded with timer and score text
/// </summary>
public class PlayerScoreHandler : MonoBehaviour {

	private static PlayerScoreHandler sharedInstance = null;
	public static PlayerScoreHandler Instance {
		get {
			return sharedInstance;
		}
	}

	[SerializeField] private float MAX_TIMER = 120.0f;
	[SerializeField] private Text timerText;
	[SerializeField] private Text scoreText;

	private float timeLeft = 0.0f;
	private bool gameOver = false;
	private float ticks = 0;

	private int score = 0;
	private int correctMatches = 0;
	private int wrongMatches = 0;

	void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		this.Reset ();

		EventBroadcaster.Instance.AddObserver (EventNames.ON_UPDATE_SCORE, this.OnUpdateScore);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_CORRECT_MATCH, this.OnCorrectMatch);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_WRONG_MATCH, this.OnWrongMatch);
	}

	void OnDestroy() {
		sharedInstance = null;

		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_UPDATE_SCORE);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_CORRECT_MATCH);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_WRONG_MATCH);
	}
	
	// Update is called once per frame
	void Update () {

		if (this.gameOver) {
			return;
		}

		this.ticks += Time.deltaTime;
		if (this.ticks >= 1.0f) {
			this.ticks = 0;

			this.timeLeft--;
			this.timerText.text = "TIME LEFT: " + this.timeLeft;

			if (this.timeLeft <= 0.0f) {
				//stop game
				this.gameOver = true;
				ResultScreen resultScreen = (ResultScreen) ViewHandler.Instance.Show(ViewNames.RESULT_SCREEN_NAME);
				resultScreen.SetResult (this.score, this.correctMatches, this.wrongMatches);
			}
		}

	}

	public void Reset() {
		this.gameOver = false;
		this.timeLeft = MAX_TIMER;
		this.timerText.text = "TIME LEFT: " + this.timeLeft;
		this.score = 0;
	}

	private void OnUpdateScore() {
		this.score++;
		this.scoreText.text = "SCORE: " +this.score.ToString();
	}

	private void OnCorrectMatch() {
		this.correctMatches++;
	}

	private void OnWrongMatch() {
		this.wrongMatches++;
	}
}
