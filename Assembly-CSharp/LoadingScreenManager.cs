using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
	private void stageLoading()
	{
		this.myAudioHub.PlaySound(this.loadingMusic);
		TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.contentCanvasGroup.alpha, delegate(float x)
		{
			this.contentCanvasGroup.alpha = x;
		}, 1f, 2f), 3f), 1), delegate()
		{
			TweenExtensions.Restart(this.skullTween, true, -1f);
			GameManager.WorldManager.StageGame();
		});
	}

	private void gameIsLive()
	{
		GameManager.StageManager.TheGameIsLive -= this.gameIsLive;
		if (!this.debugMode)
		{
			this.myAudioHub.MuffleHub(0f, 1.75f);
			TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.contentCanvasGroup.alpha, delegate(float x)
			{
				this.contentCanvasGroup.alpha = x;
			}, 0f, 1f), 1f), 1), delegate()
			{
				TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.loadingScreenCanvasGroup.alpha, delegate(float x)
				{
					this.loadingScreenCanvasGroup.alpha = x;
				}, 0f, 0.75f), 1), delegate()
				{
					TweenExtensions.Kill(this.skullTween, false);
					this.loadingScreenGameObject.SetActive(false);
				});
			});
		}
		else
		{
			this.loadingScreenGameObject.SetActive(false);
		}
	}

	private void Awake()
	{
		LoadingScreenManager.Ins = this;
		this.myAudioHub = base.GetComponent<AudioHubObject>();
		this.skullTween = TweenSettingsExtensions.SetLoops<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.skullCanvasGroup.alpha, delegate(float x)
		{
			this.skullCanvasGroup.alpha = x;
		}, 1f, 0.75f), 1), -1, 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.skullTween, false);
		TweenExtensions.Pause<Tweener>(this.skullTween);
		GameManager.StageManager.TheGameIsLive += this.gameIsLive;
	}

	private void Start()
	{
		if (!this.debugMode)
		{
			this.stageLoading();
		}
	}

	public static LoadingScreenManager Ins;

	[SerializeField]
	private bool debugMode;

	[SerializeField]
	private GameObject loadingScreenGameObject;

	[SerializeField]
	private CanvasGroup loadingScreenCanvasGroup;

	[SerializeField]
	private CanvasGroup contentCanvasGroup;

	[SerializeField]
	private CanvasGroup skullCanvasGroup;

	[SerializeField]
	private CanvasGroup proTipCG;

	[SerializeField]
	private TextMeshProUGUI proTipText;

	[SerializeField]
	private AudioFileDefinition loadingMusic;

	[SerializeField]
	private string[] tips;

	private AudioHubObject myAudioHub;

	private Tweener skullTween;
}
