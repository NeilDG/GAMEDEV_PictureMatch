using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game mechanic handler handles the overall mechanic of the game such as scoring and tracking of matches.
/// By: Neil DG
/// </summary>
public class GameMechanicHandler : MonoBehaviour {

	private static GameMechanicHandler sharedInstance = null;
	public static GameMechanicHandler Instance {
		get {
			return sharedInstance;
		}
	}

	public const string PICTURE_MODEL_KEY = "PICTURE_MODEL_KEY";
	public const string PICTURE_MATCH_LISTENER_KEY = "PICTURE_MATCH_LISTENER_KEY";

	private PictureModel firstPicture;
	private PictureModel secondPicture;

	private IPictureMatchListener firstListener;
	private IPictureMatchListener secondListener;

	private List<PictureModel> generatedPictureModels = new List<PictureModel>(); //holds picture models to be referenced by several picture components
	private int currentLevel = 1;

	void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		EventBroadcaster.Instance.AddObserver (EventNames.ON_PICTURE_CLICKED, this.OnReceivedPictureClickedEvent);
	}

	void OnDestroy() {
		sharedInstance = null;
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_PICTURE_CLICKED);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Returns the number of pictures to be spawned in the game board based on game level
	/// </summary>
	/// <returns>The number pictures based on level.</returns>
	private int GetNumPicturesBasedOnLevel() {
		return 5 + (this.currentLevel * 5);
	}


	/// <summary>
	/// Generates a set of picture models.
	/// </summary>
	/// <returns>The picture models.</returns>
	public PictureModel[] GeneratePictureModels() {
		int numPicturesNeeded = this.GetNumPicturesBasedOnLevel () / 2;

		this.generatedPictureModels.Clear (); //clear models first
		for (int i = 0; i < numPicturesNeeded; i++) {
			this.generatedPictureModels.Add (new PictureModel (PictureModel.GenerateRandomType ()));
		}

		return this.generatedPictureModels.ToArray ();
	}

	private void OnReceivedPictureClickedEvent(Parameters parameters) {
		PictureModel pictureModel = (PictureModel) parameters.GetObjectExtra (GameMechanicHandler.PICTURE_MODEL_KEY);
		IPictureMatchListener pictureMatchListener = (IPictureMatchListener)parameters.GetObjectExtra (GameMechanicHandler.PICTURE_MATCH_LISTENER_KEY);

		if (this.firstPicture == null) {
			this.firstPicture = pictureModel;
			this.firstListener = pictureMatchListener;
		} else if (this.secondPicture == null) {
			this.secondPicture = pictureModel;
			this.secondListener = pictureMatchListener;

			//verify if both are matched
			if (this.firstPicture.GetPictureType () == this.secondPicture.GetPictureType ()) {
				this.firstListener.OnMatchValid ();
				this.secondListener.OnMatchValid ();

				EventBroadcaster.Instance.PostEvent (EventNames.ON_UPDATE_SCORE);
				EventBroadcaster.Instance.PostEvent (EventNames.ON_CORRECT_MATCH);
			} else {
				this.firstListener.OnMatchInvalid ();
				this.secondListener.OnMatchInvalid ();

				EventBroadcaster.Instance.PostEvent (EventNames.ON_WRONG_MATCH);
			}

			//set to null after checking
			this.firstPicture = null;
			this.secondPicture = null;

			this.firstListener = null;
			this.secondListener = null;

		} else {
			Debug.LogWarning ("[GameMechanicHandler] Error. Both picture models are stored already!");
		}
	}


}
