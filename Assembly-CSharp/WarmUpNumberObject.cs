using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class WarmUpNumberObject : MonoBehaviour
{
	public void FireMe(string setNumber)
	{
		GameManager.AudioSlinger.PlaySound(this.MySFX);
		this.myRT.anchoredPosition = this.myStartPOS;
		this.myText.text = setNumber;
		this.myCG.alpha = 1f;
		TweenExtensions.Restart(this.scaleDownTween, true, -1f);
		TweenExtensions.Restart(this.fadeOutTween, true, -1f);
	}

	private void Awake()
	{
		this.myCG = base.GetComponent<CanvasGroup>();
		this.myRT = base.GetComponent<RectTransform>();
		this.myText = base.GetComponent<Text>();
		this.myCG.alpha = 0f;
		this.myStartPOS.x = 0f;
		this.myStartPOS.y = -(this.myRT.sizeDelta.y / 2f + 5f);
		this.scaleDownTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.myRT.localScale, delegate(Vector3 x)
		{
			this.myRT.localScale = x;
		}, WarmUpNumberObject.myDownScale, 0.6f), 14);
		TweenExtensions.Pause<Tweener>(this.scaleDownTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.scaleDownTween, false);
		this.fadeOutTween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.myCG.alpha, delegate(float x)
		{
			this.myCG.alpha = x;
		}, 0f, 0.6f), 1), delegate()
		{
			this.myCG.alpha = 0f;
			this.myRT.localScale = this.myDefaultScale;
		});
		TweenExtensions.Pause<Tweener>(this.fadeOutTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.fadeOutTween, false);
	}

	public AudioFileDefinition MySFX;

	private static Vector3 myDownScale = new Vector3(0.1f, 0.1f, 1f);

	private CanvasGroup myCG;

	private RectTransform myRT;

	private Text myText;

	private Vector2 myStartPOS = Vector2.zero;

	private Vector3 myDefaultScale = Vector3.one;

	private Tweener scaleDownTween;

	private Tweener fadeOutTween;
}
