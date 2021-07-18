using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CameraTilterHook : MonoBehaviour
{
	private void triggerPresentAni()
	{
		this.presentAni.DOPlay();
	}

	private void presentToGame()
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Quaternion, Vector3, QuaternionOptions>>(DOTween.To(() => base.transform.rotation, delegate(Quaternion x)
		{
			base.transform.rotation = x;
		}, new Vector3(90f, 0f, 0f), 1.5f), 1);
		TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(-0.0009f, 2.63f, -0.00081f), 2.4f), 14);
	}

	private void presentToOptions()
	{
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
		{
			TitleManager.Ins.OptionsPresented.Execute();
		});
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(-0.0009f, 2.117f, this.defaultPOS.z), 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 0.6f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(-0.0009f, 2.117f, 0.64f), 0.75f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void dismissFromOptions()
	{
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
		{
			TitleManager.Ins.OptionsDismissed.Execute();
		});
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, new Vector3(this.defaultPOS.z, 2.117f, this.defaultPOS.z), 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 0.6f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => base.transform.position, delegate(Vector3 x)
		{
			base.transform.position = x;
		}, this.defaultPOS, 0.75f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void Awake()
	{
		CameraTilterHook.Ins = this;
		this.presentAni = base.GetComponent<DOTweenAnimation>();
		this.defaultPOS = base.transform.position;
		TitleManager.Ins.TitlePresent.Event += this.triggerPresentAni;
		TitleManager.Ins.TitleDismissing.Event += this.presentToGame;
		TitleManager.Ins.OptionsPresent.Event += this.presentToOptions;
		TitleManager.Ins.OptionsDismissing.Event += this.dismissFromOptions;
	}

	private void OnDestroy()
	{
		TitleManager.Ins.TitlePresent.Event -= this.triggerPresentAni;
		TitleManager.Ins.TitleDismissing.Event -= this.presentToGame;
		TitleManager.Ins.OptionsPresent.Event -= this.presentToOptions;
		TitleManager.Ins.OptionsDismissing.Event -= this.dismissFromOptions;
	}

	public static CameraTilterHook Ins;

	private DOTweenAnimation presentAni;

	private Vector3 defaultPOS;
}
