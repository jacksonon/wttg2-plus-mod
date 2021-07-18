using System;
using System.Diagnostics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class MemCellObject : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event MemCellObject.MemCellActions IWasActivated;

	public void BuildMe(int setIndex, int setMaxIndex)
	{
		this.myIndex = setIndex;
		this.maxIndex = setMaxIndex;
		this.MemInactiveCellIMGCG.alpha = 0.25f;
	}

	public void ActivateInactiveCellIMG()
	{
		TweenExtensions.Restart(this.activateInactiveCellTween, true, -1f);
	}

	public void ActivateActiveCellIMG()
	{
		TweenExtensions.Restart(this.hideInactiveCellTween, true, -1f);
		if (this.myIndex != this.maxIndex - 1)
		{
			TweenExtensions.Restart(this.showActiveCellTween1, true, -1f);
		}
		else
		{
			TweenExtensions.Restart(this.showActiveCellTween2, true, -1f);
		}
	}

	public void ResetMe()
	{
		this.MemInactiveCellIMGCG.alpha = 1f;
		this.MemActiveCellIMGCG.alpha = 0f;
		this.MemInactiveLineImage.fillAmount = 0f;
	}

	private void ActivateConLine()
	{
		TweenExtensions.Restart(this.activateConLineTween, true, -1f);
	}

	private void TriggerIWasActivated()
	{
		if (this.IWasActivated != null)
		{
			this.IWasActivated();
		}
	}

	private void Awake()
	{
		this.MemInactiveCellIMGCG = this.MemInactiveCellIMG.GetComponent<CanvasGroup>();
		this.MemActiveCellIMGCG = this.MemActiveCellIMG.GetComponent<CanvasGroup>();
		this.MemInactiveLineImage = this.MemInactiveLineIMG.GetComponent<Image>();
		this.activateInactiveCellTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MemInactiveCellIMGCG.alpha, delegate(float x)
		{
			this.MemInactiveCellIMGCG.alpha = x;
		}, 1f, 0.25f), 1);
		TweenExtensions.Pause<Tweener>(this.activateInactiveCellTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.activateInactiveCellTween, false);
		this.hideInactiveCellTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MemInactiveCellIMGCG.alpha, delegate(float x)
		{
			this.MemInactiveCellIMGCG.alpha = x;
		}, 0f, 0.25f), 1);
		TweenExtensions.Pause<Tweener>(this.hideInactiveCellTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideInactiveCellTween, false);
		this.showActiveCellTween1 = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MemActiveCellIMGCG.alpha, delegate(float x)
		{
			this.MemActiveCellIMGCG.alpha = x;
		}, 1f, 0.3f), 1), new TweenCallback(this.ActivateConLine));
		TweenExtensions.Pause<Tweener>(this.showActiveCellTween1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showActiveCellTween1, false);
		this.showActiveCellTween2 = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MemActiveCellIMGCG.alpha, delegate(float x)
		{
			this.MemActiveCellIMGCG.alpha = x;
		}, 1f, 0.3f), 1), new TweenCallback(this.TriggerIWasActivated));
		TweenExtensions.Pause<Tweener>(this.showActiveCellTween2);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showActiveCellTween2, false);
		this.activateConLineTween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MemInactiveLineImage.fillAmount, delegate(float x)
		{
			this.MemInactiveLineImage.fillAmount = x;
		}, 1f, 0.2f), 1), new TweenCallback(this.TriggerIWasActivated));
		TweenExtensions.Pause<Tweener>(this.activateConLineTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.activateConLineTween, false);
	}

	public GameObject MemInactiveLineIMG;

	public GameObject MemInactiveCellIMG;

	public GameObject MemActiveLineIMG;

	public GameObject MemActiveCellIMG;

	private const float INACTIVE_CELL_IMG_FADE_TIME = 0.25f;

	private const float ACTIVE_CELL_IMG_FADE_TIME = 0.3f;

	private const float ACTIVE_CON_LINE_FILL_TIME = 0.2f;

	private int myIndex;

	private int maxIndex;

	private CanvasGroup MemInactiveCellIMGCG;

	private CanvasGroup MemActiveCellIMGCG;

	private Image MemInactiveLineImage;

	private Tweener activateInactiveCellTween;

	private Tweener hideInactiveCellTween;

	private Tweener showActiveCellTween1;

	private Tweener showActiveCellTween2;

	private Tweener activateConLineTween;

	public delegate void MemCellActions();
}
