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
	private bool gamePaused = false;
	private float ticks = 0;

	private int levelScore = 0;
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

		if (this.gameOver || this.gamePaused) {
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
				resultScreen.SetResult (GameMechanicHandler.Instance.GetCurrentLevel(), this.score, this.correctMatches, this.wrongMatches);
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
		this.levelScore++;
		this.scoreText.text = "SCORE: " +this.score.ToString();

		int requiredMatches = GameMechanicHandler.Instance.GetRequiredMatches ();
		if (this.levelScore == requiredMatches) {
			this.levelScore = 0;
			//delay increase of level to allow the user to see their last mactch briefly
			this.StartCoroutine(this.DelayIncreaseLevel(0.25f));
		}
	}

	private IEnumerator DelayIncreaseLevel(float seconds) {
		yield return new WaitForSeconds (seconds);
		GameMechanicHandler.Instance.IncreaseLevel ();
	}

	private void OnCorrectMatch() {
		this.correctMatches++;
	}

	private void OnWrongMatch() {
		this.wrongMatches++;
	}

	public void PauseTimer() {
		this.gamePaused = true;
	}

	public void ResumeTimer() {
		this.gamePaused = false;
	}
}
