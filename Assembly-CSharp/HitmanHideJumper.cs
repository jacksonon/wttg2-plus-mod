using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

[RequireComponent(typeof(hideController))]
public class HitmanHideJumper : MonoBehaviour
{
	public void WardrobeJump()
	{
		this.myHideController.SetMasterLock(true);
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, Vector3.zero, 0.25f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, new Vector3(0f, 0f, 0.218f), 0.3f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void ShowerJump()
	{
		this.myHideController.SetMasterLock(true);
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, Vector3.zero, 0.25f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, new Vector3(0f, 0f, 0.218f), 0.3f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void ShowerCaughtJump()
	{
		GameManager.TimeSlinger.FireTimer(0.75f, delegate()
		{
			TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => base.transform.rotation, delegate(Quaternion x)
			{
				base.transform.rotation = x;
			}, new Vector3(0f, -18.88f, 0f), 0.3f), 1), true);
			this.myHideController.SetMasterLock(true);
		}, 0);
	}

	private void stageMe()
	{
		CameraManager.Get(this.myHideController.CameraIControl, out this.myCamera);
		GameManager.StageManager.Stage -= this.stageMe;
	}

	private void Awake()
	{
		HitmanHideJumper.Ins = this;
		this.myHideController = base.GetComponent<hideController>();
		GameManager.StageManager.Stage += this.stageMe;
	}

	public static HitmanHideJumper Ins;

	private hideController myHideController;

	private Camera myCamera;
}
