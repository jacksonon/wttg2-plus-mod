﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

public class HackerManager : MonoBehaviour
{
	public HackingTimerBehaviour HackingTimer
	{
		get
		{
			return this.hackingTimer;
		}
	}

	public HackingTerminalBehaviour HackingTerminal
	{
		get
		{
			return this.hackingTerminal;
		}
	}

	public bool OpenForHacks
	{
		get
		{
			return this.openForHacks;
		}
		set
		{
			this.openForHacks = value;
		}
	}

	public bool InTestMode
	{
		get
		{
			return this.isInTestMode;
		}
	}

	public void PresentHackGame()
	{
		this.myComputerCameraManager.ClearPostFXs();
		this.sweeperHack.ActivateMe();
		this.sweeperHack.PrepSweepAttack(false);
	}

	public void ProcessSweepAttack(HACK_SWEEPER_SKILL_TIER SetTier)
	{
		this.sweeperHack.DeActivateMe();
		if (this.isInTestMode)
		{
			this.clearTestHack();
			return;
		}
		if (SetTier == HACK_SWEEPER_SKILL_TIER.INSTABLOCK && !this.twitchGodHack)
		{
			SteamSlinger.Ins.PlayerBeatZone();
			this.presentInstaHackBlocked();
			return;
		}
		SteamSlinger.Ins.PlayerLostZone();
		this.pickHack(SetTier);
	}

	public void PlayerWon(int LevelIndex = 0)
	{
		if (this.isInTestMode)
		{
			this.clearTestHack();
			return;
		}
		this.presentHackBlocked(LevelIndex);
	}

	public void PlayerLost()
	{
		if (this.isInTestMode)
		{
			this.clearTestHack();
			return;
		}
		this.presentHacked();
	}

	public void LaunchTestHack(HACK_TYPE SetType)
	{
		this.isInTestMode = true;
		StateManager.BeingHacked = true;
		GameManager.AudioSlinger.MuteAudioLayer(AUDIO_LAYER.WEBSITE);
		LookUp.DesktopUI.DesktopGraphicRaycaster.enabled = false;
		this.hackerOverlayCG.blocksRaycasts = true;
		this.hackerOverlayCG.ignoreParentGroups = true;
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hackerOverlayCG.alpha, delegate(float x)
		{
			this.hackerOverlayCG.alpha = x;
		}, 1f, 0.3f), 1), delegate()
		{
			switch (SetType)
			{
			case HACK_TYPE.SWEEPER:
				this.sweeperHack.ActivateMe();
				this.sweeperHack.PrepSweepAttack(true);
				return;
			case HACK_TYPE.MEMDEFRAG:
				this.memDeFragHack.PrepAttack(HACK_SWEEPER_SKILL_TIER.TIER1);
				return;
			case HACK_TYPE.STACKPUSHER:
				this.stackPusherHack.PrepStackPusherAttack(HACK_SWEEPER_SKILL_TIER.TIER1);
				return;
			case HACK_TYPE.NODEHEXER:
				this.nodeHexerHack.PrepNodeHexAttack(HACK_SWEEPER_SKILL_TIER.TIER1);
				return;
			default:
				return;
			}
		});
	}

	public void RollHack()
	{
		if (Random.Range(0f, 100f) <= 10f && !this.rollHackFroze)
		{
			if (!DataManager.LeetMode)
			{
				this.rollCount++;
				if (this.rollCount >= 3)
				{
					this.rollCount = 0;
					GameManager.TheCloud.ForceKeyDiscover();
				}
			}
			this.triggerHack();
			if (ModsManager.ForceHackingEnabled)
			{
				return;
			}
			this.rollHackFroze = true;
			GameManager.TimeSlinger.FireTimer(300f, delegate()
			{
				this.rollHackFroze = false;
			}, 0);
		}
	}

	private void clearTestHack()
	{
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hackerOverlayCG.alpha, delegate(float x)
		{
			this.hackerOverlayCG.alpha = x;
		}, 0f, 0.3f), 1), delegate()
		{
			this.isInTestMode = false;
			StateManager.BeingHacked = false;
			GameManager.AudioSlinger.UnMuteAudioLayer(AUDIO_LAYER.WEBSITE);
			LookUp.DesktopUI.DesktopGraphicRaycaster.enabled = true;
			this.hackerOverlayCG.blocksRaycasts = false;
			this.hackerOverlayCG.ignoreParentGroups = false;
		});
	}

	private void presentHackAni()
	{
		CursorManager.Ins.SwitchToHackerCursor();
		GameManager.AudioSlinger.MuteAudioLayer(AUDIO_LAYER.WEBSITE);
		LookUp.DesktopUI.DesktopGraphicRaycaster.enabled = false;
		this.hackerOverlayCG.blocksRaycasts = true;
		this.hackerOverlayCG.ignoreParentGroups = true;
		this.myComputerCameraManager.BeginHackAni();
		GameManager.TimeSlinger.FireTimer(1.65f, new Action(this.triggerTerminalDump), 0);
		GameManager.AudioSlinger.PlaySound(this.HackingIntroBedSFX);
	}

	private void triggerTerminalDump()
	{
		this.myComputerCameraManager.ClearPostFXs();
		this.hackerOverlayCG.alpha = 1f;
		this.hackingTerminal.DoDump();
	}

	private void triggerSkull()
	{
		this.myComputerCameraManager.TriggerHackingTerminalDumpGlitch();
		GameManager.TimeSlinger.FireTimer(0.75f, delegate()
		{
			this.myComputerCameraManager.TriggerHackingTerminalSkullEFXs();
			this.hackingTerminal.TerminalHelper.ClearTerminal();
			this.hackingTerminal.TerminalHelper.UpdateTerminalContentScrollHeight();
			this.evilSkull.PresentMe();
		}, 0);
	}

	private void pickHack(HACK_SWEEPER_SKILL_TIER SetTier)
	{
		int num = Random.Range(0, 100);
		if (num == this.lastHackPicked)
		{
			this.lastHackDupCount++;
		}
		if (this.lastHackDupCount > 2)
		{
			if (num < 33)
			{
				num = Random.Range(33, 100);
			}
			else if (num >= 33 & num < 66)
			{
				if (Random.Range(0, 2) == 0)
				{
					num = 10;
				}
				else
				{
					num = 75;
				}
			}
			else
			{
				num = Random.Range(0, 66);
			}
		}
		this.lastHackPicked = num;
		if (num < 10)
		{
			if (this.godHack)
			{
				this.memDeFragHack.PrepAttack(HACK_SWEEPER_SKILL_TIER.GOD_TIER);
				return;
			}
			this.memDeFragHack.PrepAttack(SetTier);
			return;
		}
		else if (num >= 10 & num < 50)
		{
			if (this.godHack)
			{
				this.stackPusherHack.PrepStackPusherAttack(HACK_SWEEPER_SKILL_TIER.GOD_TIER);
				return;
			}
			this.stackPusherHack.PrepStackPusherAttack(SetTier);
			return;
		}
		else
		{
			if (this.godHack)
			{
				this.nodeHexerHack.PrepNodeHexAttack(HACK_SWEEPER_SKILL_TIER.GOD_TIER);
				return;
			}
			this.nodeHexerHack.PrepNodeHexAttack(SetTier);
			return;
		}
	}

	private void presentHacked()
	{
		if (this.twitchGodHack && DataManager.LeetMode)
		{
			if (EnemyManager.State != ENEMY_STATE.LOCKED)
			{
				EnemyManager.State = ENEMY_STATE.LOCKED;
				GameManager.TimeSlinger.FireTimer(60f, new Action(this.ResetEnemyState), 0);
				this.resetEnemyStateTriggered = true;
			}
			EnvironmentManager.PowerBehaviour.ForcePowerOff();
		}
		GameManager.AudioSlinger.KillSound(this.HackingIntroBedSFX);
		CursorManager.Ins.SwitchToHackerCursor();
		GameManager.AudioSlinger.PlaySound(this.HackedSFX);
		this.myComputerCameraManager.TriggerHackedEFXs();
		this.evilSkull.HackedLaugh();
		TweenExtensions.Restart(this.hacked1Tween, true, -1f);
		TweenExtensions.Restart(this.hacked2Tween, true, -1f);
		GameManager.TimeSlinger.FireTimer(3f, new Action(this.dismissHackMode), 0);
	}

	public void presentHackBlocked(int LevelIndex = 0)
	{
		GameManager.AudioSlinger.KillSound(this.HackingIntroBedSFX);
		GameManager.AudioSlinger.PlaySound(this.HackBlockSFX);
		this.myComputerCameraManager.TriggerHackBlockedEFXs();
		TweenExtensions.Restart(this.hackedBlockedTween, true, -1f);
		this.genereateBlockHackedStats(LevelIndex);
		GameManager.TimeSlinger.FireTimer(2.3f, new Action(this.dismissHackMode), 0);
	}

	private void presentInstaHackBlocked()
	{
		GameManager.AudioSlinger.KillSound(this.HackingIntroBedSFX);
		GameManager.AudioSlinger.PlaySound(this.HackBlockSFX);
		this.myComputerCameraManager.TriggerHackBlockedEFXs();
		TweenExtensions.Restart(this.instaHackBlockedTween, true, -1f);
		GameManager.TimeSlinger.FireTimer(2.3f, new Action(this.dismissHackMode), 0);
	}

	private void dismissHackMode()
	{
		StateManager.BeingHacked = false;
		this.generateFireWindow();
		this.hackedCG.alpha = 0f;
		this.hackedBlockedCG.alpha = 0f;
		this.instaHackedBlockedCG.alpha = 0f;
		this.hackerOverlayCG.alpha = 0f;
		this.hackedRT.localScale = this.hackedDefaultScale;
		this.hackerOverlayCG.alpha = 0f;
		this.hackerOverlayCG.blocksRaycasts = false;
		this.hackerOverlayCG.ignoreParentGroups = false;
		this.DOSCoinLostCG.alpha = 0f;
		this.NotesLostCG.alpha = 0f;
		this.DOSCoinGainedCG.alpha = 0f;
		this.myComputerCameraManager.ClearPostFXs();
		this.godHack = false;
		this.twitchGodHack = false;
		LookUp.DesktopUI.DesktopGraphicRaycaster.enabled = true;
		if (!ComputerMuteBehaviour.Ins.Muted)
		{
			GameManager.AudioSlinger.UnMuteAudioLayer(AUDIO_LAYER.WEBSITE);
		}
		CursorManager.Ins.ClearHackerCursor();
	}

	private void generateFireWindow()
	{
		this.freezeAddTime = 0f;
		this.freezeTimeStamp = 0f;
		this.fireWindow = Random.Range(this.fireWindowMin, this.fireWindowMax);
		this.fireWindowTimeStamp = Time.time;
		this.fireWindowActive = true;
	}

	private void generateSmallFireWindow()
	{
		this.freezeAddTime = 0f;
		this.freezeTimeStamp = 0f;
		this.fireWindow = Random.Range(this.smallFireWindowMin, this.smallFireWindowMax);
		this.fireWindowTimeStamp = Time.time;
		this.fireWindowActive = true;
	}

	private void triggerHack()
	{
		if (!this.openForHacks || EnemyManager.State != ENEMY_STATE.IDLE || StateManager.BeingHacked)
		{
			this.generateSmallFireWindow();
			return;
		}
		GameManager.AudioSlinger.UnMuteAudioHub(AUDIO_HUB.COMPUTER_HUB);
		bool flag = true;
		VPN_LEVELS currentVPN = GameManager.ManagerSlinger.VPNManager.CurrentVPN;
		if (currentVPN != VPN_LEVELS.LEVEL0)
		{
			int num = Random.Range(1, 101);
			switch (currentVPN)
			{
			case VPN_LEVELS.LEVEL1:
				if (num >= 80)
				{
					flag = false;
				}
				break;
			case VPN_LEVELS.LEVEL2:
				if (num >= 60)
				{
					flag = false;
				}
				break;
			case VPN_LEVELS.LEVEL3:
				if (num >= 40)
				{
					flag = false;
				}
				break;
			case VPN_LEVELS.LEVEL4:
				if (num >= 20)
				{
					flag = false;
				}
				break;
			case VPN_LEVELS.LEVEL5:
				if (num >= 2)
				{
					flag = false;
				}
				break;
			}
		}
		if (flag)
		{
			StateManager.BeingHacked = true;
			this.presentHackAni();
			return;
		}
		this.generateFireWindow();
	}

	private void playerPausedGame()
	{
		if (!this.userIsOffLine && !StateManager.BeingHacked)
		{
			this.fireWindowActive = false;
			this.freezeTimeStamp = Time.time;
		}
	}

	private void playerUnPausedGame()
	{
		if (!this.userIsOffLine && !StateManager.BeingHacked)
		{
			this.freezeAddTime += Time.time - this.freezeTimeStamp;
			this.fireWindowActive = true;
		}
	}

	private void networkWentOffLine()
	{
		this.userIsOffLine = true;
		this.fireWindowActive = false;
		this.freezeTimeStamp = Time.time;
	}

	private void networkWentOnline()
	{
		this.userIsOffLine = false;
		this.freezeAddTime += Time.time - this.freezeTimeStamp;
		this.fireWindowActive = true;
	}

	private void genereateBlockHackedStats(int levelIndex)
	{
		if (InventoryManager.GetProductCount(SOFTWARE_PRODUCTS.BACKDOOR) > 0)
		{
			float num = (float)Math.Round((double)(Random.Range(this.backdoorDOSCoinMin, this.backdoorDOSCoinMax) * (float)levelIndex), 3);
			if (this.twitchGodHack)
			{
				if (DataManager.LeetMode)
				{
					num = 33.37f;
				}
				else
				{
					num = 0.5f;
				}
			}
			if (num <= 0f)
			{
				num = 3.5f;
			}
			if (DataManager.LeetMode && num >= 75f)
			{
				num = 78.385f;
			}
			CurrencyManager.AddCurrency(num);
			this.DOSCoinGainedText.text = "GAINED: " + num.ToString();
			TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.DOSCoinGainedCG.alpha, delegate(float x)
			{
				this.DOSCoinGainedCG.alpha = x;
			}, 1f, 0.2f), 1);
		}
		InventoryManager.RemoveProduct(SOFTWARE_PRODUCTS.BACKDOOR);
	}

	private void genereateLostHackStats()
	{
		InventoryManager.RemoveProduct(SOFTWARE_PRODUCTS.BACKDOOR);
		double num = (double)CurrencyManager.CurrentCurrency;
		int num2 = Random.Range(1, 21);
		float num3;
		if (num2 >= 8 && num2 < 12)
		{
			num3 = 0.25f;
		}
		else if (num2 >= 5 && num2 < 8)
		{
			num3 = 0.35f;
		}
		else if (num2 >= 3 && num2 < 5)
		{
			num3 = 0.45f;
		}
		else if (num2 == 2)
		{
			num3 = 0.75f;
		}
		else
		{
			num3 = 0.2f;
		}
		if (DataManager.LeetMode)
		{
			num3 = 0.95f;
		}
		float setAMT = (float)Math.Round(num * (double)num3, 3);
		CurrencyManager.RemoveCurrency(setAMT);
		this.DOSCoinLostText.SetText("LOST: " + setAMT.ToString());
		num2 = Random.Range(1, 101);
		if (num2 >= 0 && num2 <= 20)
		{
			GameManager.BehaviourManager.NotesBehaviour.ClearNotes();
			this.NotesLostText.SetText("NOTES: LOST");
		}
		else
		{
			this.NotesLostText.SetText("NOTES: KEPT");
		}
		if (DataManager.LeetMode)
		{
			this.virusManager.ForceVirus();
		}
		else
		{
			this.virusManager.AddVirus();
		}
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.DOSCoinLostCG.alpha, delegate(float x)
		{
			this.DOSCoinLostCG.alpha = x;
		}, 1f, 0.5f), 1);
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.NotesLostCG.alpha, delegate(float x)
		{
			this.NotesLostCG.alpha = x;
		}, 1f, 0.5f), 1);
	}

	private void stageMe()
	{
		GameManager.PauseManager.GamePaused += this.playerPausedGame;
		GameManager.PauseManager.GameUnPaused += this.playerUnPausedGame;
		GameManager.ManagerSlinger.WifiManager.WentOnline += this.networkWentOnline;
		GameManager.ManagerSlinger.WifiManager.WentOffline += this.networkWentOffLine;
		GameManager.StageManager.Stage -= this.stageMe;
	}

	private void activateThreats()
	{
		this.openForHacks = true;
		this.generateFireWindow();
		GameManager.StageManager.ThreatsNowActivated -= this.activateThreats;
	}

	private void Awake()
	{
		GameManager.HackerManager = this;
		this.theSwan = new TheSwan();
		this.sweeperHack = base.GetComponent<SweeperHack>();
		this.memDeFragHack = base.GetComponent<MemDeFragHack>();
		this.stackPusherHack = base.GetComponent<StackPusherHack>();
		this.nodeHexerHack = base.GetComponent<NodeHexerHack>();
		this.virusManager = base.GetComponent<VirusManager>();
		this.hackingTerminal = base.GetComponent<HackingTerminalBehaviour>();
		this.evilSkull = base.GetComponent<EvilSkullBehavior>();
		this.hackingTimer = base.GetComponent<HackingTimerBehaviour>();
		this.hackerOverlayCG = this.HackerOverlay.GetComponent<CanvasGroup>();
		this.hackedBlockedCG = this.HackedBlocked.GetComponent<CanvasGroup>();
		this.hackedCG = this.Hacked.GetComponent<CanvasGroup>();
		this.instaHackedBlockedCG = this.InstaHackBlocked.GetComponent<CanvasGroup>();
		this.hackedRT = this.Hacked.GetComponent<RectTransform>();
		this.instaHackBlockedTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.instaHackedBlockedCG.alpha, delegate(float x)
		{
			this.instaHackedBlockedCG.alpha = x;
		}, 1f, 0.2f), 1);
		TweenExtensions.Pause<Tweener>(this.instaHackBlockedTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.instaHackBlockedTween, false);
		this.hackedBlockedTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hackedBlockedCG.alpha, delegate(float x)
		{
			this.hackedBlockedCG.alpha = x;
		}, 1f, 0.2f), 1);
		TweenExtensions.Pause<Tweener>(this.hackedBlockedTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hackedBlockedTween, false);
		this.hacked1Tween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hackedCG.alpha, delegate(float x)
		{
			this.hackedCG.alpha = x;
		}, 1f, 0.3f), 1), new TweenCallback(this.genereateLostHackStats));
		TweenExtensions.Pause<Tweener>(this.hacked1Tween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hacked1Tween, false);
		this.hacked2Tween = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.hackedRT.localScale, delegate(Vector3 x)
		{
			this.hackedRT.localScale = x;
		}, this.hackedFullScale, 0.3f), 20);
		TweenExtensions.Pause<Tweener>(this.hacked2Tween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hacked2Tween, false);
		this.hackingTerminal.DumpDone += this.triggerSkull;
		if (DataManager.LeetMode)
		{
			this.fireWindowMin = this.leetHackerData.FireWindowMin;
			this.fireWindowMax = this.leetHackerData.FireWindowMax;
			this.smallFireWindowMin = this.leetHackerData.SmallFireWindowMin;
			this.smallFireWindowMax = this.leetHackerData.SmallFireWindowMax;
			this.backdoorDOSCoinMin = this.leetHackerData.BackDoorDOSCoinMin;
			this.backdoorDOSCoinMax = this.leetHackerData.BackDoorDOSCoinMax;
		}
		else
		{
			this.fireWindowMin = this.normalHackerData.FireWindowMin;
			this.fireWindowMax = this.normalHackerData.FireWindowMax;
			this.smallFireWindowMin = this.normalHackerData.SmallFireWindowMin;
			this.smallFireWindowMax = this.normalHackerData.SmallFireWindowMax;
			this.backdoorDOSCoinMin = this.normalHackerData.BackDoorDOSCoinMin;
			this.backdoorDOSCoinMax = this.normalHackerData.BackDoorDOSCoinMax;
		}
		GameManager.StageManager.Stage += this.stageMe;
		GameManager.StageManager.ThreatsNowActivated += this.activateThreats;
	}

	private void Start()
	{
		this.myComputerCameraManager = ComputerCameraManager.Ins;
		this.HackingIntroBedSFX.AudioClip = DownloadTIFiles.hackermansAudio;
	}

	private void Update()
	{
		if (this.fireWindowActive && Time.time - this.freezeAddTime - this.fireWindowTimeStamp >= this.fireWindow)
		{
			this.fireWindowActive = false;
			this.triggerHack();
		}
	}

	private void OnDestroy()
	{
		this.hackingTerminal.DumpDone -= this.triggerSkull;
	}

	public void ForceTwitchHack()
	{
		if (StateManager.BeingHacked || !ComputerPowerHook.Ins.PowerOn || EnvironmentManager.PowerState == POWER_STATE.OFF || EnemyManager.State == ENEMY_STATE.BREATHER || EnemyManager.State == ENEMY_STATE.CULT || EnemyManager.State == ENEMY_STATE.DOLL_MAKER || EnemyManager.State == ENEMY_STATE.HITMAN || EnemyManager.State == ENEMY_STATE.POILCE)
		{
			GameManager.TimeSlinger.FireTimer(20f, new Action(this.ForceTwitchHack), 0);
			return;
		}
		StateManager.BeingHacked = true;
		this.presentHackAni();
		this.godHack = true;
		this.twitchGodHack = true;
	}

	public void ForceNormalHack()
	{
		if (StateManager.BeingHacked || !ComputerPowerHook.Ins.PowerOn || EnvironmentManager.PowerState == POWER_STATE.OFF || EnemyManager.State == ENEMY_STATE.BREATHER || EnemyManager.State == ENEMY_STATE.CULT || EnemyManager.State == ENEMY_STATE.DOLL_MAKER || EnemyManager.State == ENEMY_STATE.HITMAN || EnemyManager.State == ENEMY_STATE.POILCE)
		{
			GameManager.TimeSlinger.FireTimer(20f, new Action(this.ForceNormalHack), 0);
			return;
		}
		StateManager.BeingHacked = true;
		this.presentHackAni();
	}

	public void WhiteHatSound()
	{
		GameManager.AudioSlinger.PlaySound(this.HackBlockSFX);
	}

	public void BlackHatSound()
	{
		GameManager.AudioSlinger.PlaySound(this.HackedSFX);
	}

	public void ForcePogHack()
	{
		if (StateManager.BeingHacked || !ComputerPowerHook.Ins.PowerOn || EnvironmentManager.PowerState == POWER_STATE.OFF || EnemyManager.State == ENEMY_STATE.BREATHER || EnemyManager.State == ENEMY_STATE.CULT || EnemyManager.State == ENEMY_STATE.DOLL_MAKER || EnemyManager.State == ENEMY_STATE.HITMAN || EnemyManager.State == ENEMY_STATE.POILCE)
		{
			GameManager.TimeSlinger.FireTimer(20f, new Action(this.ForcePogHack), 0);
			return;
		}
		StateManager.BeingHacked = true;
		this.presentHackAni();
		this.godHack = true;
	}

	public bool isInGodHack
	{
		get
		{
			return this.godHack;
		}
	}

	private void ResetEnemyState()
	{
		if (EnvironmentManager.PowerState == POWER_STATE.ON)
		{
			EnemyManager.State = ENEMY_STATE.IDLE;
			this.resetEnemyStateTriggered = false;
			return;
		}
		GameManager.TimeSlinger.FireTimer(30f, new Action(this.ResetEnemyState), 0);
	}

	public void BlackHatSound2S()
	{
		GameManager.AudioSlinger.KillSound(this.HackBlockSFX);
		GameManager.AudioSlinger.PlaySound(this.HackedSFX);
		GameManager.TimeSlinger.FireTimer(1.45f, delegate()
		{
			GameManager.AudioSlinger.KillSound(this.HackedSFX);
		}, 0);
	}

	public Canvas DesktopCanvas;

	public GameObject HackerOverlay;

	public GameObject HackedBlocked;

	public GameObject Hacked;

	public GameObject InstaHackBlocked;

	public CanvasGroup DOSCoinLostCG;

	public TextMeshProUGUI DOSCoinLostText;

	public CanvasGroup NotesLostCG;

	public TextMeshProUGUI NotesLostText;

	public CanvasGroup DOSCoinGainedCG;

	public TextMeshProUGUI DOSCoinGainedText;

	public AudioFileDefinition HackingTypeSFX;

	public AudioFileDefinition HackingIntroBedSFX;

	public AudioFileDefinition HackBlockSFX;

	public AudioFileDefinition HackedSFX;

	[SerializeField]
	private HackerManagerDataDefinition normalHackerData;

	[SerializeField]
	private HackerManagerDataDefinition leetHackerData;

	private Vector3 hackedDefaultScale = new Vector3(0.25f, 0.25f, 0.25f);

	private Vector3 hackedFullScale = Vector3.one;

	private SweeperHack sweeperHack;

	private MemDeFragHack memDeFragHack;

	private StackPusherHack stackPusherHack;

	private NodeHexerHack nodeHexerHack;

	public VirusManager virusManager;

	private HackingTerminalBehaviour hackingTerminal;

	private ComputerCameraManager myComputerCameraManager;

	private EvilSkullBehavior evilSkull;

	private HackingTimerBehaviour hackingTimer;

	private CanvasGroup hackerOverlayCG;

	private CanvasGroup hackedBlockedCG;

	private CanvasGroup hackedCG;

	private CanvasGroup instaHackedBlockedCG;

	private RectTransform hackedRT;

	private Tweener instaHackBlockedTween;

	private Tweener hackedBlockedTween;

	private Tweener hacked1Tween;

	private Tweener hacked2Tween;

	private int lastHackPicked;

	private int lastHackDupCount;

	private int rollCount;

	private float fireWindowMin;

	private float fireWindowMax;

	private float smallFireWindowMin;

	private float smallFireWindowMax;

	private float backdoorDOSCoinMin;

	private float backdoorDOSCoinMax;

	private float fireWindow;

	private float fireWindowTimeStamp;

	private float freezeTimeStamp;

	private float freezeAddTime;

	private bool openForHacks;

	private bool fireWindowActive;

	private bool userIsOffLine;

	private bool isInTestMode;

	private bool rollHackFroze;

	private bool godHack;

	private bool twitchGodHack;

	private bool resetEnemyStateTriggered;

	public TheSwan theSwan;
}