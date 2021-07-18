using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

[RequireComponent(typeof(braceController))]
public class BreatherBraceJumper : MonoBehaviour
{
	public void TriggerDoorJump()
	{
		this.myBraceController.SetMasterLock(true);
		MainCameraHook.Ins.AddHeadHit(0f);
		this.myCamera.transform.SetParent(BreatherBehaviour.Ins.HelperBone);
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.myCamera.transform.localPosition = x;
		}, Vector3.zero, 0.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<Tweener>(TweenSettingsExtensions.SetOptions(DOTween.To(() => this.myCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.myCamera.transform.localRotation = x;
		}, new Vector3(0f, 90f, 0f), 0.5f), true), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void Awake()
	{
		BreatherBraceJumper.Ins = this;
		this.myBraceController = base.GetComponent<braceController>();
		CameraManager.Get(CAMERA_ID.MAIN, out this.myCamera);
	}

	public static BreatherBraceJumper Ins;

	[SerializeField]
	private GameObject PPLayerObject;

	private braceController myBraceController;

	private Camera myCamera;
}
