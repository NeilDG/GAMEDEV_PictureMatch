using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Picture component attachable to a button
/// Created By: NeilDG
/// </summary>
public class PictureComponent : MonoBehaviour {

	[SerializeField] private Button assignedButton;
	[SerializeField] private GameObject hiddenImage;
	[SerializeField] private GameObject shownImage;
	[SerializeField] private PictureModel.PictureType pictureType;

	private const float TWEEN_SPEED = 0.5f;

	private PictureModel pictureModel;

	// Use this for initialization
	void Start () {
		this.Reset ();
	}

	public void OnClicked() {
		this.assignedButton.interactable = false;
		this.ShowStep1 ();
		this.ShowStep2 ();
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

		rectTransform.DOScaleY (1.0f, TWEEN_SPEED).OnComplete(this.OnImageShownComplete);
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
		this.assignedButton.interactable = true;
	}

	private void OnImageShownComplete() {
		this.StartCoroutine (this.DelayReset ());
	}

	private IEnumerator DelayReset() {
		yield return new WaitForSeconds (1.0f);
		this.HideStep1 ();
		this.HideStep2 ();
		this.assignedButton.interactable = true;
	}
	
}
