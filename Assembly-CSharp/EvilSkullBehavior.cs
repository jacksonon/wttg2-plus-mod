using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class EvilSkullBehavior : MonoBehaviour
{
	public void PresentMe()
	{
		GameManager.AudioSlinger.PlaySound(this.SkullInSFX);
		BlueWhisperManager.Ins.ProcessSound(this.SkullInSFX);
		this.skullObjectCG.alpha = 1f;
		TweenExtensions.Restart(this.presentSkullTween, true, -1f);
	}

	public void HackedLaugh()
	{
		this.skullObjectCG.alpha = 1f;
		TweenExtensions.Restart(this.skullLaughSeq, true, -1f);
		TweenExtensions.Restart(this.hackedLaughSeq, true, -1f);
	}

	private void haHaHa()
	{
		GameManager.AudioSlinger.PlaySound(this.EvilLaugh);
		BlueWhisperManager.Ins.ProcessSound(this.EvilLaugh);
		TweenExtensions.Restart(this.haHaHaSeq, true, -1f);
	}

	private void dismissMe()
	{
		GameManager.AudioSlinger.PlaySound(this.SkullOutSFX);
		BlueWhisperManager.Ins.ProcessSound(this.SkullOutSFX);
		TweenExtensions.Restart(this.dismissTween, true, -1f);
	}

	private void killMe()
	{
		this.skullObjectCG.alpha = 0f;
		GameManager.HackerManager.PresentHackGame();
	}

	private void Awake()
	{
		this.skullObjectCG = this.SkullObject.GetComponent<CanvasGroup>();
		this.skullBotRT = this.SkullBot.GetComponent<RectTransform>();
		this.skullObjectRT = this.SkullObject.GetComponent<RectTransform>();
		this.defaultSkullBotPOS = this.SkullBot.GetComponent<RectTransform>().anchoredPosition;
		this.presentSkullTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.skullObjectRT.localScale, delegate(Vector3 x)
		{
			this.skullObjectRT.localScale = x;
		}, this.presentSkullScale, 0.75f), 27), new TweenCallback(this.haHaHa));
		TweenExtensions.Pause<Tweener>(this.presentSkullTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.presentSkullTween, false);
		this.dismissTween = TweenSettingsExtensions.OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.skullObjectRT.localScale, delegate(Vector3 x)
		{
			this.skullObjectRT.localScale = x;
		}, this.defaultSkullScale, 0.4f), 26), new TweenCallback(this.killMe));
		TweenExtensions.Pause<Tweener>(this.dismissTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.dismissTween, false);
		this.haHaHaSeq = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), new TweenCallback(this.dismissMe));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 0f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 0.32f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 0.46f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 0.66f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 0.81f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.02f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.17f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.32f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.44f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.67f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 1.81f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.02f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.12f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.33f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.47f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.67f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 2.82f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 3.06f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 3.18f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.haHaHaSeq, 3.72f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose, 0.1f), 1), true));
		TweenExtensions.Pause<Sequence>(this.haHaHaSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.haHaHaSeq, false);
		this.skullLaughSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.skullLaughSeq, 0f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotOpen2, 0.1f), 1), true));
		TweenSettingsExtensions.Insert(this.skullLaughSeq, 0.1f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.skullBotRT.anchoredPosition, delegate(Vector2 x)
		{
			this.skullBotRT.anchoredPosition = x;
		}, this.laughSkullBotClose2, 0.1f), 1), true));
		TweenSettingsExtensions.SetLoops<Sequence>(this.skullLaughSeq, -1);
		TweenExtensions.Pause<Sequence>(this.skullLaughSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.skullLaughSeq, false);
		this.hackedLaughSeq = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
		{
			TweenExtensions.Pause<Sequence>(this.skullLaughSeq);
			this.skullBotRT.anchoredPosition = this.defaultSkullBotPOS;
			this.skullObjectRT.localScale = this.defaultSkullScale;
		});
		TweenSettingsExtensions.Insert(this.hackedLaughSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.skullObjectRT.localScale, delegate(Vector3 x)
		{
			this.skullObjectRT.localScale = x;
		}, this.hackedSkullSmallScale, 0.75f), 21));
		TweenSettingsExtensions.Insert(this.hackedLaughSeq, 0.56f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.SkullObject.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			this.SkullObject.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.2f), 1));
		TweenExtensions.Pause<Sequence>(this.hackedLaughSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.hackedLaughSeq, false);
	}

	public GameObject SkullObject;

	public GameObject SkullTop;

	public GameObject SkullBot;

	public AudioFileDefinition EvilLaugh;

	public AudioFileDefinition SkullInSFX;

	public AudioFileDefinition SkullOutSFX;

	private Vector2 defaultSkullBotPOS;

	private Vector3 presentSkullScale = new Vector3(0.22f, 0.22f, 1f);

	private Vector3 hackedSkullSmallScale = new Vector3(0.01f, 0.01f, 1f);

	private Vector3 defaultSkullScale = Vector3.one;

	private Vector2 laughSkullBotOpen = new Vector2(0f, -145f);

	private Vector2 laughSkullBotClose = new Vector2(0f, 145f);

	private Vector2 laughSkullBotOpen2 = new Vector2(0f, -250f);

	private Vector2 laughSkullBotClose2 = new Vector2(0f, 250f);

	private CanvasGroup skullObjectCG;

	private RectTransform skullBotRT;

	private RectTransform skullObjectRT;

	private Tweener presentSkullTween;

	private Tweener dismissTween;

	private Sequence skullLaughSeq;

	private Sequence haHaHaSeq;

	private Sequence hackedLaughSeq;
}
