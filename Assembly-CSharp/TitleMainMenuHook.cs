﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class TitleMainMenuHook : MonoBehaviour
{
	public void Dismiss()
	{
		this.myCG.interactable = false;
		this.myCG.blocksRaycasts = false;
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.25f), 1), delegate()
		{
			this.DismissActions.ExecuteAndKill();
		});
	}

	public void Present()
	{
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.25f), 1), delegate()
		{
			this.myCG.interactable = true;
			this.myCG.blocksRaycasts = true;
		});
	}

	private void logoWasPresented()
	{
		if (!DataManager.ContinuedGame)
		{
			this.continueBTN.ActiveState(false);
		}
		this.presentMe();
	}

	private void presentMe()
	{
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 2f), 1), delegate()
		{
			this.myCG.interactable = true;
			this.myCG.blocksRaycasts = true;
		});
	}

	private void presentMeQuick()
	{
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.25f), 1), delegate()
		{
			this.myCG.interactable = true;
			this.myCG.blocksRaycasts = true;
		});
	}

	private void newGameAction()
	{
		if (DataManager.ContinuedGame)
		{
			this.presentAreYouSure();
		}
		else
		{
			this.presentDiffSelect();
		}
	}

	private void continueAction()
	{
		this.DismissActions.Event += TitleManager.Ins.DismissTitle;
		this.Dismiss();
	}

	private void optionsAction()
	{
		TitleManager.Ins.PresentOptions();
	}

	private void quitAction()
	{
		Application.Quit();
	}

	private void presentAreYouSure()
	{
		this.DismissActions.Event += TitleAreYouSureMenuHook.Ins.Present;
		this.Dismiss();
	}

	private void presentDiffSelect()
	{
		this.DismissActions.Event += TitleDiffSelectMenuHook.Ins.Present;
		this.Dismiss();
	}

	private void Awake()
	{
		TitleMainMenuHook.Ins = this;
		this.myRT = base.GetComponent<RectTransform>();
		this.myCG = base.GetComponent<CanvasGroup>();
		this.startPOS = this.myRT.anchoredPosition;
		this.myCG.interactable = false;
		this.myCG.blocksRaycasts = false;
		TitleManager.Ins.MainMenu = this;
		TitleManager.Ins.TitlePresented.Event += this.logoWasPresented;
		TitleManager.Ins.OptionsPresent.Event += this.Dismiss;
		TitleManager.Ins.OptionsDismissed.Event += this.presentMeQuick;
		this.newGameBTN.MyAction.Event += this.newGameAction;
		this.continueBTN.MyAction.Event += this.continueAction;
		this.optionsBTN.MyAction.Event += this.optionsAction;
		this.quitBTN.MyAction.Event += this.quitAction;
	}

	private void OnDestroy()
	{
		TitleManager.Ins.TitlePresented.Event -= this.logoWasPresented;
		TitleManager.Ins.OptionsPresent.Event -= this.Dismiss;
		TitleManager.Ins.OptionsDismissed.Event -= this.presentMeQuick;
		this.newGameBTN.MyAction.Event -= this.newGameAction;
		this.continueBTN.MyAction.Event -= this.continueAction;
		this.optionsBTN.MyAction.Event -= this.optionsAction;
		this.quitBTN.MyAction.Event -= this.quitAction;
	}

	public static TitleMainMenuHook Ins;

	[SerializeField]
	private TitleMenuBTN newGameBTN;

	[SerializeField]
	private TitleMenuBTN continueBTN;

	[SerializeField]
	private TitleMenuBTN optionsBTN;

	[SerializeField]
	private TitleMenuBTN quitBTN;

	public CustomEvent DismissActions = new CustomEvent(3);

	private RectTransform myRT;

	private CanvasGroup myCG;

	private Vector2 startPOS = Vector2.zero;
}
