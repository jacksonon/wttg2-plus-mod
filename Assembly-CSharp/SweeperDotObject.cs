using System;
using System.Diagnostics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class SweeperDotObject : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event SweeperDotObject.UpdateHotSpotActions ActivateHotSpot;

	public void BuildMe(int setIndex, int maxIndex)
	{
		this.myIndex = setIndex;
		TweenExtensions.Restart(this.showMeSeq, true, (float)setIndex * 0.015f);
		if (setIndex == maxIndex)
		{
			this.fireDelay = (float)setIndex * 0.015f;
			this.fireTimeStamp = Time.time;
			this.fireHotSpot = true;
		}
	}

	public void MakeMeHot()
	{
		this.iAmHot = true;
	}

	public void ActivateMyHotSpot(int setIndex)
	{
		if (this.iAmHot)
		{
			TweenExtensions.Restart(this.showHotSpotSeq, true, (float)setIndex * 0.1f);
		}
	}

	public bool GetAmHot()
	{
		return this.iAmHot;
	}

	public void Destroy()
	{
		this.myCG.alpha = 0f;
		this.myHotSpotCG.alpha = 0f;
		this.iAmHot = false;
		this.myRT.anchoredPosition = Vector2.zero;
		this.fireHotSpot = false;
	}

	private void Awake()
	{
		this.myCG = base.GetComponent<CanvasGroup>();
		this.myHotSpotCG = this.MyHotSpot.GetComponent<CanvasGroup>();
		this.myRT = base.GetComponent<RectTransform>();
		this.showMeSeq = TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 1f, 0.2f), 1), 0.015f);
		TweenExtensions.Pause<Tweener>(this.showMeSeq);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showMeSeq, false);
		this.showHotSpotSeq = TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myHotSpotCG.alpha, delegate(float x)
		{
			this.myHotSpotCG.alpha = x;
		}, 1f, 0.1f), 1), 0.1f);
		TweenExtensions.Pause<Tweener>(this.showHotSpotSeq);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showHotSpotSeq, false);
	}

	private void Update()
	{
		if (this.fireHotSpot && Time.time - this.fireTimeStamp >= this.fireDelay)
		{
			this.fireHotSpot = false;
			if (this.ActivateHotSpot != null)
			{
				this.ActivateHotSpot();
			}
		}
	}

	public GameObject MyHotSpot;

	private const float DELAY_TIME = 0.015f;

	private const float FADE_TIME = 0.2f;

	private const float HOT_SPOT_DELAY_TIME = 0.1f;

	private const float HOT_SPOT_FADE_TIME = 0.1f;

	private bool iAmHot;

	private bool fireHotSpot;

	private int myIndex;

	private float fireTimeStamp;

	private float fireDelay;

	private CanvasGroup myCG;

	private CanvasGroup myHotSpotCG;

	private RectTransform myRT;

	private Tweener showMeSeq;

	private Tweener showHotSpotSeq;

	public delegate void UpdateHotSpotActions();
}
