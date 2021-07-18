using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class NodeHexStartArrowObject : MonoBehaviour
{
	public void Clear()
	{
		TweenExtensions.Restart(this.hideMeTween, true, -1f);
	}

	public void Present(RectTransform NewParent)
	{
		this.myRT.SetParent(NewParent);
		this.myRT.anchoredPosition = Vector2.zero;
		TweenExtensions.Restart(this.presentMeTween1, true, -1f);
		TweenExtensions.Restart(this.presentMeTween2, true, -1f);
	}

	private void Awake()
	{
		this.myCG = base.GetComponent<CanvasGroup>();
		this.myRT = base.GetComponent<RectTransform>();
		this.presentMeTween1 = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.45f), 1);
		TweenExtensions.Pause<Tweener>(this.presentMeTween1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.presentMeTween1, false);
		this.presentMeTween2 = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.myRT.anchoredPosition, delegate(Vector2 x)
		{
			this.myRT.anchoredPosition = x;
		}, new Vector2(-35f, 0f), 0.15f), 3);
		TweenExtensions.Pause<Tweener>(this.presentMeTween2);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.presentMeTween2, false);
		this.hideMeTween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.25f), 1), delegate()
		{
			this.myRT.SetParent(this.DefaultParent);
			this.myRT.anchoredPosition = Vector2.zero;
		});
		TweenExtensions.Pause<Tweener>(this.hideMeTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideMeTween, false);
	}

	public RectTransform DefaultParent;

	private CanvasGroup myCG;

	private RectTransform myRT;

	private Tweener presentMeTween1;

	private Tweener presentMeTween2;

	private Tweener hideMeTween;
}
