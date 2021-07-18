using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class trailerController : baseController
{
	private void takeInput()
	{
		if (Input.GetKeyDown(108))
		{
			this.lobbyPan();
		}
		if (Input.GetKeyDown(104))
		{
			this.hallwayPan();
		}
	}

	private void lobbyPan()
	{
		base.transform.position = new Vector3(-1.5f, 1.386f, -9.27f);
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 3f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(-1.5f, 1.386f, -26.569f), 6f), 1));
		TweenSettingsExtensions.Insert(sequence, 6f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => base.transform.rotation, delegate(Quaternion x)
		{
			base.transform.rotation = x;
		}, new Vector3(0f, 100f, 0f), 3f), 1), true));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void hallwayPan()
	{
		base.transform.position = new Vector3(-25.2f, 41.1f, -6.273f);
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 3f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(-3.9f, 41.1f, -6.273f), 5f), 1));
		TweenSettingsExtensions.Insert(sequence, 3f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => base.transform.rotation, delegate(Quaternion x)
		{
			base.transform.rotation = x;
		}, new Vector3(0f, -90f, -13f), 5f), 1), true));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void postBaseStage()
	{
		this.PostStage.Event -= this.postBaseStage;
		if (this.Active)
		{
			CameraManager.GetCameraHook(this.CameraIControl).SetMyParent(base.transform);
			this.MyCamera.transform.localPosition = this.startingCameraPOS;
		}
	}

	private void postBaseLive()
	{
		this.PostLive.Event -= this.postBaseLive;
		if (this.Active)
		{
			this.MyCamera.transform.localPosition = this.startingCameraPOS;
			this.MyCamera.transform.localRotation = Quaternion.Euler(this.startingCameraROT);
			GameManager.BehaviourManager.CrossHairBehaviour.HideCrossHairGroup();
		}
	}

	protected new void Awake()
	{
		base.Awake();
		ControllerManager.Add(this);
		this.PostStage.Event += this.postBaseStage;
		this.PostLive.Event += this.postBaseLive;
	}

	protected new void Start()
	{
		base.Start();
	}

	protected new void Update()
	{
		base.Update();
	}

	protected new void OnDestroy()
	{
		ControllerManager.Remove(this.Controller);
		base.OnDestroy();
	}

	[SerializeField]
	private Vector3 startingCameraPOS = Vector3.zero;

	[SerializeField]
	private Vector3 startingCameraROT = Vector3.zero;
}
