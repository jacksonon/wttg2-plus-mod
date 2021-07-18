using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretLoader : MonoBehaviour
{
	private IEnumerator loadWorld(int worldID)
	{
		AsyncOperation result = SceneManager.LoadSceneAsync(worldID, 1);
		while (!result.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		this.ClearLoading();
		yield break;
	}

	private void ClearLoading()
	{
		TweenExtensions.Kill(this.skullTween, false);
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.skullCanvasGroup.alpha, delegate(float x)
		{
			this.skullCanvasGroup.alpha = x;
		}, 0f, 0.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 0.5f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.loadingScreenCG.alpha, delegate(float x)
		{
			this.loadingScreenCG.alpha = x;
		}, 0f, 1f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void Awake()
	{
		this.skullTween = TweenSettingsExtensions.SetLoops<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.skullCanvasGroup.alpha, delegate(float x)
		{
			this.skullCanvasGroup.alpha = x;
		}, 1f, 0.75f), 1), -1, 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.skullTween, false);
		TweenExtensions.Pause<Tweener>(this.skullTween);
	}

	private void Start()
	{
		if (!this.debugMode)
		{
			TweenExtensions.Restart(this.skullTween, true, -1f);
			GameManager.TimeSlinger.FireTimer(4f, delegate()
			{
				base.StartCoroutine(this.loadWorld(this.secretWorldID));
			}, 0);
			if (DevTools.Ins != null)
			{
				Object.Destroy(DevTools.Ins.gameObject);
				DevTools.Ins = null;
				return;
			}
		}
		else
		{
			this.loadingScreenCG.alpha = 0f;
		}
	}

	[SerializeField]
	private CanvasGroup loadingScreenCG;

	[SerializeField]
	private CanvasGroup skullCanvasGroup;

	[SerializeField]
	private int secretWorldID;

	[SerializeField]
	private bool debugMode;

	private Tweener skullTween;
}
