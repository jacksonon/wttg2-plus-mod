using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class StackPusherGridPoperObject : MonoBehaviour
{
	public void Clear()
	{
		TweenExtensions.Restart(this.clearTween, true, -1f);
	}

	public void SetMyParent(RectTransform ParentRectTrans)
	{
		this.myRT.SetParent(ParentRectTrans);
		this.myRT.anchoredPosition = this.centerPOS;
	}

	public void PresentActive()
	{
		TweenExtensions.Restart(this.presentSelf, true, -1f);
		this.SetActive();
	}

	public void PresentInactive()
	{
		TweenExtensions.Restart(this.presentSelf, true, -1f);
		this.SetInactive();
	}

	public void SetActive()
	{
		if (this.arrowCG.alpha != 1f)
		{
			TweenExtensions.Restart(this.setActiveTween, true, -1f);
		}
	}

	public void SetInactive()
	{
		TweenExtensions.Restart(this.setInActiveTween, true, -1f);
	}

	public void PopMouseEnter()
	{
		TweenExtensions.Restart(this.mouseEnterTween, true, -1f);
	}

	public void PopMouseExit()
	{
		TweenExtensions.Restart(this.mouseExitTween, true, -1f);
	}

	private void Awake()
	{
		this.myRT = base.GetComponent<RectTransform>();
		this.myCG = base.GetComponent<CanvasGroup>();
		this.arrowCG = this.PopArrowIMG.GetComponent<CanvasGroup>();
		this.hoverCG = this.PopHoverIMG.GetComponent<CanvasGroup>();
		this.defaultParent = (RectTransform)this.myRT.parent;
		this.presentSelf = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.presentSelf);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.presentSelf, false);
		this.setActiveTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.arrowCG.alpha, delegate(float x)
		{
			this.arrowCG.alpha = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.setActiveTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.setActiveTween, false);
		this.setInActiveTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.arrowCG.alpha, delegate(float x)
		{
			this.arrowCG.alpha = x;
		}, 0.1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.setInActiveTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.setInActiveTween, false);
		this.mouseEnterTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hoverCG.alpha, delegate(float x)
		{
			this.hoverCG.alpha = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.mouseEnterTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.mouseEnterTween, false);
		this.mouseExitTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.hoverCG.alpha, delegate(float x)
		{
			this.hoverCG.alpha = x;
		}, 0f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.mouseExitTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.mouseExitTween, false);
		this.clearTween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.15f), 1), delegate()
		{
			this.arrowCG.alpha = 0f;
			this.hoverCG.alpha = 0f;
			this.SetMyParent(this.defaultParent);
		});
		TweenExtensions.Pause<Tweener>(this.clearTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.clearTween, false);
	}

	public GameObject DefaultBGIMG;

	public GameObject PopArrowIMG;

	public GameObject PopHoverIMG;

	private RectTransform myRT;

	private RectTransform defaultParent;

	private CanvasGroup myCG;

	private CanvasGroup arrowCG;

	private CanvasGroup hoverCG;

	private Vector2 centerPOS = Vector2.zero;

	private Tweener presentSelf;

	private Tweener setActiveTween;

	private Tweener setInActiveTween;

	private Tweener mouseEnterTween;

	private Tweener mouseExitTween;

	private Tweener clearTween;
}
