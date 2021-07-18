using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class startController : baseController
{
	private void loseControl()
	{
		this.Active = false;
		base.SetMasterLock(true);
		this.MyState = GAME_CONTROLLER_STATE.LOCKED;
		StateManager.PlayerState = PLAYER_STATE.BUSY;
	}

	private void postBaseStage()
	{
		this.PostStage.Event -= this.postBaseStage;
		if (this.Active)
		{
			LookUp.PlayerUI.BlackScreenCG.alpha = 1f;
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
			Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
			{
				this.loseControl();
				deskController.Ins.TakeOverFromStart();
				if (!DataManager.LeetMode)
				{
					TutorialProductBehaviour.Ins.AddRemoteVPNSpawn();
					TutorialStartBehaviour.Ins.ShowNoirIcon();
					return;
				}
				TutorialStartBehaviour.Ins.ShowNoirIcon();
			});
			TweenSettingsExtensions.Insert(sequence, 1.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.BlackScreenCG.alpha, delegate(float x)
			{
				LookUp.PlayerUI.BlackScreenCG.alpha = x;
			}, 0f, 3f), 1));
			TweenSettingsExtensions.Insert(sequence, 4.75f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.MyCamera.transform.localPosition, delegate(Vector3 x)
			{
				this.MyCamera.transform.localPosition = x;
			}, new Vector3(-0.2071f, 0.035f, 0f), 2f), 1));
			TweenSettingsExtensions.Insert(sequence, 5f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.MyCamera.transform.localRotation, delegate(Quaternion x)
			{
				this.MyCamera.transform.localRotation = x;
			}, new Vector3(15f, 90f, 0f), 1f), 1), true));
			TweenExtensions.Play<Sequence>(sequence);
		}
	}

	protected new void Awake()
	{
		base.Awake();
		ControllerManager.Add(this);
		startController.Ins = this;
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

	public static startController Ins;

	public CustomEvent TriggerTutorialEvents = new CustomEvent(2);

	[SerializeField]
	private Vector3 startingCameraPOS = Vector3.zero;

	[SerializeField]
	private Vector3 startingCameraROT = Vector3.zero;
}
