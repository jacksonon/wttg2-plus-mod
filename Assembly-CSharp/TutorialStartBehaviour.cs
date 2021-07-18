﻿using System;
using UnityEngine;

public class TutorialStartBehaviour : TutorialStepper
{
	public void Begin()
	{
		TutorialVoiceCallBehaviour.Ins.WasPresentedEvents.Event -= this.Begin;
		TutorialVoiceCallBehaviour.Ins.HangUpEvents.Event += this.hardEnd;
		base.StartTutorial();
	}

	public void ForceNextStep()
	{
		base.step();
	}

	public void ShowExampleHash()
	{
		this.exampleHashObject.SetActive(true);
	}

	public void HideExampleHash()
	{
		this.exampleHashObject.SetActive(false);
	}

	public void ShowKeyDiscovery()
	{
		this.exampleKeyDiscovery.SetActive(true);
	}

	public void HideKeyDiscovery()
	{
		this.exampleKeyDiscovery.SetActive(false);
	}

	public void ShowNoirIcon()
	{
		this.noirIconCG.alpha = 1f;
		if (!DataManager.LeetMode)
		{
			this.ShowDocIcons();
		}
		WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
		WindowManager.Get(SOFTWARE_PRODUCTS.ZERODAY).Launch();
	}

	public void ShowDocIcons()
	{
		for (int i = 0; i < this.docIconCGs.Length; i++)
		{
			this.docIconCGs[i].alpha = 1f;
		}
	}

	private void hardEnd()
	{
		TutorialVoiceCallBehaviour.Ins.HangUpEvents.Event -= this.hardEnd;
		base.HardClear();
		TutorialAnnHook.Ins.HardClear();
		DeadOrNotBehaviour.Ins.ClearOut();
		TutorialManager.Ins.ClearOut();
		this.HideExampleHash();
		this.HideKeyDiscovery();
		this.ShowNoirIcon();
		this.ShowDocIcons();
	}

	private void Awake()
	{
		TutorialStartBehaviour.Ins = this;
	}

	public static TutorialStartBehaviour Ins;

	[SerializeField]
	private GameObject exampleHashObject;

	[SerializeField]
	private GameObject exampleKeyDiscovery;

	[SerializeField]
	private CanvasGroup noirIconCG;

	[SerializeField]
	private CanvasGroup[] docIconCGs = new CanvasGroup[0];
}
