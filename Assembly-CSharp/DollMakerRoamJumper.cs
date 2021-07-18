﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DollMakerRoamJumper : MonoBehaviour
{
	public void TriggerSpeechJump()
	{
		this.myRoamController.SetMasterLock(true);
		GameManager.TimeSlinger.FireTimer(0.1f, delegate()
		{
			this.myCamera.transform.SetParent(DollMakerBehaviour.Ins.HelperBone);
			DollMakerBehaviour.Ins.TriggerSpeech();
			DepthOfField depthOfField = ScriptableObject.CreateInstance<DepthOfField>();
			depthOfField.enabled.Override(true);
			depthOfField.focusDistance.Override(0.13f);
			depthOfField.aperture.Override(8.9f);
			depthOfField.focalLength.Override(13f);
			this.ppVol = PostProcessManager.instance.QuickVolume(this.PPLayerObject.layer, 100f, new PostProcessEffectSettings[]
			{
				depthOfField
			});
			this.ppVol.weight = 0f;
			Sequence sequence = DOTween.Sequence();
			TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
			{
				this.myCamera.transform.localPosition = x;
			}, Vector3.zero, 0.5f), 21));
			TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
			{
				this.myCamera.transform.localRotation = x;
			}, Vector3.zero, 0.5f), 1), true));
			TweenSettingsExtensions.Insert(sequence, 0.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ppVol.weight, delegate(float x)
			{
				this.ppVol.weight = x;
			}, 1f, 0.5f), 1));
			TweenExtensions.Play<Sequence>(sequence);
		}, 0);
	}

	public void ClearSpeechJump()
	{
		MainCameraHook.Ins.BlackOut(1f, 3f);
		MainCameraHook.Ins.AddHeadHit(3f);
		RuntimeUtilities.DestroyVolume(this.ppVol, false, false);
		GameManager.TimeSlinger.FireTimer(1.5f, delegate()
		{
			MainCameraHook.Ins.ClearARF(2.75f);
			MainCameraHook.Ins.ClearDoubleVis(2.5f);
		}, 0);
		this.myRoamController.GlobalTakeOver(2f, 1f, 1f);
	}

	public void TriggerUniJump()
	{
		this.myRoamController.SetMasterLock(true);
		this.myCamera.transform.SetParent(DollMakerBehaviour.Ins.HelperBone);
		TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, Vector3.zero, 0.5f), 1), true);
	}

	private void Awake()
	{
		DollMakerRoamJumper.Ins = this;
		this.myRoamController = base.GetComponent<roamController>();
		CameraManager.Get(CAMERA_ID.MAIN, out this.myCamera);
	}

	public static DollMakerRoamJumper Ins;

	[SerializeField]
	private GameObject PPLayerObject;

	private roamController myRoamController;

	private Camera myCamera;

	private PostProcessVolume ppVol;
}
