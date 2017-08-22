using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Picture component attachable to a button
/// Created By: NeilDG
/// </summary>
public class PictureComponent : MonoBehaviour, IPictureMatchListener {

	[SerializeField] private Button assignedButton;
	[SerializeField] private GameObject hiddenImage;
	[SerializeField] private GameObject shownImage;
	[SerializeField] private Image pictureImage;

	[SerializeField] private Sprite[] pictureSprites;

	private const float TWEEN_SPEED = 0.25f;

	private PictureModel pictureModel;

	// Use this for initialization
	void Start () {
		
	}

	public void OnClicked() {
		this.assignedButton.interactable = false;
		this.ShowStep1 ();
		this.ShowStep2 ();

		Parameters parameters = new Parameters ();
		parameters.PutObjectExtra (GameMechanicHandler.PICTURE_MODEL_KEY, this.pictureModel);
		parameters.PutObjectExtra (GameMechanicHandler.PICTURE_MATCH_LISTENER_KEY, this);
		EventBroadcaster.Instance.PostEvent (EventNames.ON_PICTURE_CLICKED, parameters);
	}

	private void ShowStep1() {
		RectTransform rectTransform = this.hiddenImage.GetComponent<RectTransform> ();
		rectTransform.DOScaleY (0.0f, TWEEN_SPEED).OnComplete(() =>{
			this.hiddenImage.SetActive (false);
		});
	}

	private void ShowStep2() {
		this.shownImage.SetActive (true);
		RectTransform rectTransform = this.shownImage.GetComponent<RectTransform> ();
		rectTransform.localScale = new Vector3 (1.0f, 0.0f, 1.0f);

		rectTransform.DOScaleY (1.0f, TWEEN_SPEED);
	}

	private void HideStep1() {
		RectTransform rectTransform = this.shownImage.GetComponent<RectTransform> ();
		rectTransform.DOScaleY (0.0f, TWEEN_SPEED).OnComplete(() =>{
			this.shownImage.SetActive (false);
		});
	}

	private void HideStep2() {
		this.hiddenImage.SetActive (true);
		RectTransform rectTransform = this.hiddenImage.GetComponent<RectTransform> ();
		rectTransform.localScale = new Vector3 (1.0f, 0.0f, 1.0f);

		rectTransform.DOScaleY (1.0f, TWEEN_SPEED);
	}

	public void Reset() {
		this.hiddenImage.SetActive (true);
		this.shownImage.SetActive (false);
		this.assignedButton.enabled = true;
		this.assignedButton.interactable = true;
	}

	public void TweenedReset() {
		this.StartCoroutine (this.DelayHide (0.0f));
	}

	public void AssignPictureModel(PictureModel pictureModel) {
		this.pictureModel = pictureModel;
		this.pictureImage.sprite = this.pictureSprites [PictureModel.ConvertTypeToInt (this.pictureModel.GetPictureType ())];
	}

	public bool HasPictureModel() {
		return (this.pictureModel != null);
	}
		
	public void OnMatchInvalid() {
		this.StartCoroutine (this.DelayHide (0.5f));
	}

	private IEnumerator DelayHide(float seconds) {
		yield return new WaitForSeconds (seconds);

		this.HideStep1 ();
		this.HideStep2 ();
		this.assignedButton.interactable = true;
	}

	public void OnMatchValid() {

	}
	
}
