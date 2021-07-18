using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class DollMakerDeskJumper : MonoBehaviour
{
	public void TriggerDeskJump()
	{
		this.myDeskController.LockRecovery();
		this.myDeskController.SetMasterLock(true);
		GameManager.TimeSlinger.FireTimer(0.1f, delegate()
		{
			this.myCamera.transform.SetParent(DollMakerBehaviour.Ins.HelperBone);
			Sequence sequence = DOTween.Sequence();
			TweenSettingsExtensions.Insert(sequence, 0.1f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
			{
				this.myCamera.transform.localPosition = x;
			}, Vector3.zero, 0.5f), 1));
			TweenSettingsExtensions.Insert(sequence, 0.1f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
			{
				this.myCamera.transform.localRotation = x;
			}, Vector3.zero, 0.5f), 1), true));
			TweenExtensions.Play<Sequence>(sequence);
		}, 0);
	}

	private void Awake()
	{
		DollMakerDeskJumper.Ins = this;
		this.myDeskController = base.GetComponent<deskController>();
		CameraManager.Get(CAMERA_ID.MAIN, out this.myCamera);
	}

	public static DollMakerDeskJumper Ins;

	private deskController myDeskController;

	private Camera myCamera;
}
