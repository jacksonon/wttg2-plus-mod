using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.CrossPlatformInput;

public class braceController : mouseableController
{
	public bool BracingDoor
	{
		get
		{
			return this.playerIsBracing;
		}
	}

	public void LoseControl()
	{
		this.Active = false;
		base.SetMasterLock(true);
		this.MyState = GAME_CONTROLLER_STATE.LOCKED;
		StateManager.PlayerState = PLAYER_STATE.BUSY;
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ppVol.weight, delegate(float x)
		{
			this.ppVol.weight = x;
		}, 0f, 0.75f), 1), delegate()
		{
			base.transform.position = this.defaultPOS;
			base.transform.rotation = Quaternion.Euler(Vector3.zero);
		});
	}

	public void TakeControl()
	{
		if (!this.MouseCaptureInit)
		{
			base.Init();
		}
		this.Active = true;
		base.SetMasterLock(false);
		this.MyState = GAME_CONTROLLER_STATE.IDLE;
		StateManager.PlayerState = PLAYER_STATE.BRACE;
	}

	public void TakeOverFromRoam()
	{
		this.PlayerEnteredEvent.Execute();
		if (this.MouseCaptureInit)
		{
			this.MyMouseCapture.setCameraTargetRot(0f);
			this.MyMouseCapture.setRotatingObjectTargetRot(this.defaultCameraHolderRotation);
			this.MyMouseCapture.setRotatingObjectRotation(this.defaultCameraHolderRotation);
		}
		CameraManager.GetCameraHook(this.CameraIControl).SetMyParent(this.MouseRotatingObject.transform);
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), new TweenCallback(this.TakeControl));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.MyCamera.transform.localPosition, delegate(Vector3 x)
		{
			this.MyCamera.transform.localPosition = x;
		}, Vector3.zero, 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetOptions(TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => this.MyCamera.transform.localRotation, delegate(Quaternion x)
		{
			this.MyCamera.transform.localRotation = x;
		}, Vector3.zero, 0.5f), 1), true));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ppVol.weight, delegate(float x)
		{
			this.ppVol.weight = x;
		}, 1f, 0.75f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void getInput()
	{
		if (!this.lockControl)
		{
			float axis = CrossPlatformInputManager.GetAxis("Right");
			Vector3 vector;
			vector..ctor(0f, axis * this.maxRotationRight, 0f);
			Vector3 position;
			position..ctor(this.defaultPOS.x, this.defaultPOS.y + axis * this.maxTopPeak, this.defaultPOS.z + axis * this.maxRightPeak);
			base.transform.position = position;
			base.transform.rotation = Quaternion.Euler(vector);
			if (axis <= 0.4f && CrossPlatformInputManager.GetButtonDown("RightClick"))
			{
				this.switchToRoamController();
			}
		}
	}

	private void switchToRoamController()
	{
		this.PlayerLeftEvent.Execute();
		this.LoseControl();
		ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).GlobalTakeOver();
	}

	private void playerIsBracingDoor(float SetValue)
	{
		if (SetValue >= this.lastBraceValue && SetValue != 0f && !this.playerRanOutOfEnergy)
		{
			this.playerIsBracing = true;
		}
		else
		{
			this.playerIsBracing = false;
		}
		this.lastBraceValue = SetValue;
	}

	private void postBaseStage()
	{
		this.PostStage.Event -= this.postBaseStage;
	}

	private void postBaseLive()
	{
		this.PostLive.Event -= this.postBaseLive;
	}

	protected new void Awake()
	{
		braceController.Ins = this;
		base.Awake();
		ControllerManager.Add(this);
		this.defaultPOS = base.transform.position;
		this.currentEnergy = this.maxEnergyTime;
		this.currentDoorHandleValue = 0f;
		this.playerIsBracing = false;
		this.ppDOF = ScriptableObject.CreateInstance<DepthOfField>();
		this.ppDOF.focusDistance.Override(0.34f);
		this.ppDOF.aperture.Override(20.4f);
		this.ppDOF.focalLength.Override(30f);
		this.ppVol = PostProcessManager.instance.QuickVolume(this.PPLayerObject.layer, 100f, new PostProcessEffectSettings[]
		{
			this.ppDOF
		});
		this.ppVol.weight = 0f;
		this.PostStage.Event += this.postBaseStage;
		this.PostLive.Event += this.postBaseLive;
		this.braceInteractionTrigger.LeftAxisAction += this.playerIsBracingDoor;
	}

	protected new void Start()
	{
		base.Start();
	}

	protected new void Update()
	{
		base.Update();
		this.getInput();
		if (this.Active)
		{
			if (this.playerIsBracing)
			{
				this.currentEnergy = Mathf.MoveTowards(this.currentEnergy, 0f, Time.deltaTime);
				this.currentDoorHandleValue = Mathf.MoveTowards(this.currentDoorHandleValue, this.maxDoorHandleTime, Time.deltaTime);
			}
			else
			{
				this.currentEnergy = Mathf.MoveTowards(this.currentEnergy, this.maxEnergyTime, Time.deltaTime);
				this.currentDoorHandleValue = Mathf.MoveTowards(this.currentDoorHandleValue, 0f, Time.deltaTime);
			}
			if (this.currentEnergy <= 0f && !ModsManager.UnlimitedStamina)
			{
				this.playerRanOutOfEnergy = true;
				LookUp.PlayerUI.EBarBorder.color = Color.red;
				GameManager.TimeSlinger.FireTimer(this.maxEnergyTime, delegate()
				{
					this.playerRanOutOfEnergy = false;
					LookUp.PlayerUI.EBarBorder.color = Color.white;
				}, 0);
			}
			LookUp.PlayerUI.EBarFill.fillAmount = this.currentEnergy / this.maxEnergyTime;
			this.doorHandle.transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0f, 0f, 20f), this.currentDoorHandleValue / this.maxDoorHandleTime));
		}
	}

	protected new void OnDestroy()
	{
		this.braceInteractionTrigger.LeftAxisAction -= this.playerIsBracingDoor;
		ControllerManager.Remove(this.Controller);
		base.OnDestroy();
	}

	public static braceController Ins;

	public CustomEvent PlayerEnteredEvent = new CustomEvent(2);

	public CustomEvent PlayerLeftEvent = new CustomEvent(2);

	[SerializeField]
	private float maxRotationRight = 90f;

	[SerializeField]
	private float maxTopPeak = 0.5f;

	[SerializeField]
	private float maxRightPeak = 0.1f;

	[SerializeField]
	private float maxEnergyTime = 4f;

	[SerializeField]
	private float maxDoorHandleTime = 1f;

	[SerializeField]
	private Vector3 defaultCameraHolderRotation = Vector3.zero;

	[SerializeField]
	private GameObject PPLayerObject;

	[SerializeField]
	private InteractionHook braceInteractionTrigger;

	[SerializeField]
	private Transform doorHandle;

	private PostProcessVolume ppVol;

	private DepthOfField ppDOF;

	private Vector3 defaultPOS;

	private bool playerIsBracing;

	private bool playerRanOutOfEnergy;

	private float currentEnergy;

	private float lastBraceValue;

	private float currentDoorHandleValue;
}
