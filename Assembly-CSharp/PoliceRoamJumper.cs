﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PoliceRoamJumper : MonoBehaviour
{
	public void TriggerFloor8DoorJump()
	{
		this.myRoamController.SetMasterLock(true);
		PauseManager.LockPause();
		GameManager.InteractionManager.LockInteraction();
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, Vector3.zero, 0.45f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(0f, -135f, 9.9f), 0.45f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void TriggerRoomRaid(Vector3 LookAtObject)
	{
		this.myRoamController.SetMasterLock(true);
		PauseManager.LockPause();
		GameManager.InteractionManager.LockInteraction();
		Vector3 eulerAngles = Quaternion.LookRotation(LookAtObject - base.transform.position, Vector3.up).eulerAngles;
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, this.myRoamController.DefaultCameraPOS, 0.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, this.myRoamController.DefaultCameraROT, 0.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => base.transform.rotation, delegate(Quaternion x)
		{
			base.transform.rotation = x;
		}, eulerAngles, 0.5f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void TriggerConstantLookAt(Vector3 LookAtObject)
	{
		Vector3 eulerAngles = Quaternion.LookRotation(LookAtObject - this.myCamera.transform.position).eulerAngles;
		eulerAngles.x = 0f;
		eulerAngles.z = 0f;
		base.transform.rotation = Quaternion.Euler(eulerAngles);
	}

	public void TriggerCameraConstantLookAt(Vector3 LookAtObject)
	{
		this.myCamera.transform.rotation = Quaternion.LookRotation(LookAtObject - this.myCamera.transform.position);
	}

	public void TriggerStairWayJump()
	{
		PauseManager.LockPause();
		this.myRoamController.SetMasterLock(true);
		GameManager.InteractionManager.LockInteraction();
		DepthOfField depthOfField = ScriptableObject.CreateInstance<DepthOfField>();
		depthOfField.enabled.Override(true);
		depthOfField.focusDistance.Override(0.1f);
		depthOfField.aperture.Override(5.5f);
		depthOfField.focalLength.Override(9f);
		PostProcessVolume ppVol = PostProcessManager.instance.QuickVolume(this.PPLayerObject.layer, 100f, new PostProcessEffectSettings[]
		{
			depthOfField
		});
		ppVol.weight = 0f;
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.transform.position, delegate(Vector3 x)
		{
			this.transform.position = x;
		}, new Vector3(24.51742f, 40.51829f, -6.294f), 0.6f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.transform.rotation, delegate(Quaternion x)
		{
			this.transform.rotation = x;
		}, new Vector3(0f, 90f, 0f), 0.6f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(0.6f, 0f, 0f), 0.6f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0.68f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, new Vector3(0.023f, 0.246f, -0.15f), 0.2f), 1));
		TweenSettingsExtensions.Insert(sequence, 0.68f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(-56.68f, 0f, 0f), 0.2f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0.88f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, new Vector3(0.023f, -0.228f, -1.056f), 0.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 0.88f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(-57.83f, 0f, 0f), 0.5f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 1.38f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, new Vector3(0.023f, -1f, -1.354f), 0.3f), 1));
		TweenSettingsExtensions.Insert(sequence, 1.38f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(-71.237f, 0f, 0f), 0.3f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 2.73f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => ppVol.weight, delegate(float x)
		{
			ppVol.weight = x;
		}, 1f, 0.75f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void Awake()
	{
		PoliceRoamJumper.Ins = this;
		this.myRoamController = base.GetComponent<roamController>();
		CameraManager.Get(CAMERA_ID.MAIN, out this.myCamera);
	}

	public static PoliceRoamJumper Ins;

	[SerializeField]
	private GameObject PPLayerObject;

	private roamController myRoamController;

	private Camera myCamera;
}
