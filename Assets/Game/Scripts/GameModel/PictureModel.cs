using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the model of the picture to be matched.
/// By: Neil DG
/// </summary>
public class PictureModel {
	
	public enum PictureType {
		BLUE_FISH,
		GREEN_FISH,
		VIOLET_FISH,
		RED_FISH,
		ORANGE_FISH,
		YELLOW_STAR,
		SPECIAL_BOOT,
		SPECIAL_FLOATABLE,
		SPECIAL_SLIPPER
	}

	private PictureType pictureType;

	public PictureModel(PictureType pictureType) {
		this.pictureType = pictureType;
	}

	public PictureType GetPictureType() {
		return this.pictureType;
	}

	public static string ConvertTypeToString(PictureType pictureType) {
		if (pictureType == PictureType.BLUE_FISH) {
			return "Blue Fish";
		} else if (pictureType == PictureType.GREEN_FISH) {
			return "Green Fish";
		} else if (pictureType == PictureType.VIOLET_FISH) {
			return "Violet Fish";
		} else if (pictureType == PictureType.RED_FISH) {
			return "Red Fish";
		} else if (pictureType == PictureType.ORANGE_FISH) {
			return "Orange Fish";
		} else if (pictureType == PictureType.YELLOW_STAR) {
			return "Yellow Star";
		} else if (pictureType == PictureType.SPECIAL_BOOT) {
			return "Old Boots";
		} else if (pictureType == PictureType.SPECIAL_FLOATABLE) {
			return "Floatable";
		} else if (pictureType == PictureType.SPECIAL_SLIPPER) {
			return "Slippers";
		} else {
			return "N/A";
		}
	}
}
