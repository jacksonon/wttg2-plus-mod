using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class TitleLogoHook : MonoBehaviour
{
	private void presentLogo()
	{
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 2.5f), 1), 5.5f), delegate()
		{
			TitleManager.Ins.LogoWasPresented();
		});
	}

	private void dismissLogo()
	{
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.75f), 1), 0.75f);
	}

	private void presentLogoQuick()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.25f), 1);
	}

	private void dismissLogoQuick()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.25f), 1);
	}

	private void Awake()
	{
		TitleLogoHook.Ins = this;
		this.myCG = base.GetComponent<CanvasGroup>();
		TitleManager.Ins.TitlePresent.Event += this.presentLogo;
		TitleManager.Ins.TitleDismissing.Event += this.dismissLogo;
		TitleManager.Ins.OptionsDismissed.Event += this.presentLogoQuick;
		TitleManager.Ins.OptionsPresent.Event += this.dismissLogoQuick;
	}

	private void OnDestroy()
	{
		TitleManager.Ins.TitlePresent.Event -= this.presentLogo;
		TitleManager.Ins.TitleDismissing.Event -= this.dismissLogo;
		TitleManager.Ins.OptionsDismissed.Event -= this.presentLogoQuick;
		TitleManager.Ins.OptionsPresent.Event -= this.dismissLogoQuick;
	}

	public static TitleLogoHook Ins;

	private CanvasGroup myCG;
}
