﻿using System;
using UnityEngine;

public class TheSwan
{
	public void ActivateTheSwan()
	{
		if (this.isActivatedBefore)
		{
			return;
		}
		this.SwanSFX.MyAudioHub = AUDIO_HUB.COMPUTER_HUB;
		this.SwanSFX.AudioClip = DownloadTIFiles.SwanFailsafe;
		GameManager.AudioSlinger.PlaySound(this.SwanSFX);
		this.isActivatedBefore = true;
		this._108 = 108f;
		GameManager.TimeSlinger.FireTimer(18f, new Action(this.StartCountdown), 0);
		if (DataManager.LeetMode && !ModsManager.EasyModeActive)
		{
			GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("swan", "THE CODE IS LOST!!!");
			return;
		}
		GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("swan", "THE CODE IS LOST: 4 8 15 16 23 42");
	}

	private void StartCountdown()
	{
		if (this.firstTIME)
		{
			this.SwanSFX.AudioClip = DownloadTIFiles.SwanReset;
			GameManager.AudioSlinger.PlaySound(this.SwanSFX);
			this.firstTIME = false;
			skyBreakBehavior.SwanNumbers[0] = 4;
			skyBreakBehavior.SwanNumbers[1] = 8;
			skyBreakBehavior.SwanNumbers[2] = 15;
			skyBreakBehavior.SwanNumbers[3] = 16;
			skyBreakBehavior.SwanNumbers[4] = 23;
			skyBreakBehavior.SwanNumbers[5] = 42;
		}
		if (this.SwanMalfunction || StateManager.BeingHacked || !ComputerPowerHook.Ins.PowerOn || EnvironmentManager.PowerState == POWER_STATE.OFF)
		{
			GameManager.TimeSlinger.FireTimer(1f, new Action(this.StartCountdown), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(1.25f, new Action(this.StartCountdown), 0);
		if (this._108 > 0f)
		{
			this._108 -= 1f;
			this.MakeSwanSound();
			return;
		}
		this.SwanMalfunction = true;
		this.SystemFailure();
	}

	private void MakeSwanSound()
	{
		if (this._108 < 10f && this._108 > 0f)
		{
			this.SwanSFX.AudioClip = DownloadTIFiles.SwanAlarm;
			GameManager.AudioSlinger.PlaySound(this.SwanSFX);
			GameManager.AudioSlinger.PlaySoundWithCustomDelay(this.SwanSFX, 0.5f);
			return;
		}
		if (this._108 >= 10f && this._108 <= 20f)
		{
			this.SwanSFX.AudioClip = DownloadTIFiles.SwanAlarm;
			GameManager.AudioSlinger.PlaySound(this.SwanSFX);
			return;
		}
		if (this._108 > 20f && this._108 <= 60f)
		{
			this.SwanSFX.AudioClip = DownloadTIFiles.SwanBeep;
			GameManager.AudioSlinger.PlaySound(this.SwanSFX);
		}
	}

	private void SystemFailure()
	{
		this._108 = 108f;
		this.SwanSFX.AudioClip = DownloadTIFiles.SwanFailure;
		if (this.skyBreak != null)
		{
			this.skyBreak.CauseSystemFailure();
		}
		GameManager.AudioSlinger.PlaySound(this.SwanSFX);
		GameManager.TimeSlinger.FireTimer(11f, delegate()
		{
			this.TakeSwanDOS();
			this.SwanMalfunction = false;
		}, 0);
	}

	public void SwanReset()
	{
		if (this._108 > 60f)
		{
			return;
		}
		this.SwanSFX.AudioClip = DownloadTIFiles.SwanReset;
		GameManager.AudioSlinger.PlaySound(this.SwanSFX);
		this._108 = 108f;
	}

	private void TakeSwanDOS()
	{
		GameManager.BehaviourManager.NotesBehaviour.ClearNotes();
		double num = (double)CurrencyManager.CurrentCurrency;
		float num2 = Random.Range(0.5f, 0.9f);
		CurrencyManager.RemoveCurrency((float)Math.Round(num * (double)num2, 3));
		if (ComputerPowerHook.Ins.PowerOn)
		{
			ComputerPowerHook.Ins.ShutDownComputerInsantly();
		}
	}

	public AudioFileDefinition SwanSFX = LookUp.SoundLookUp.LoudDoorBang;

	public bool isActivatedBefore;

	private bool SwanMalfunction;

	public float _108 = 108f;

	public skyBreakBehavior skyBreak;

	private bool firstTIME = true;
}
