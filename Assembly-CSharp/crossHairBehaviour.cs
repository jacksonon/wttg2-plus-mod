using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class crossHairBehaviour : MonoBehaviour
{
	public void ShowCrossHairGroup()
	{
		this.CrossHairCanvas.enabled = true;
		this.hideCrossHairGroup = false;
	}

	public void HideCrossHairGroup()
	{
		this.CrossHairCanvas.enabled = false;
		this.hideCrossHairGroup = true;
	}

	public void ShowActiveCrossHair()
	{
		TweenExtensions.Pause<Tweener>(this.deActivateCrossHairTrans);
		TweenExtensions.Pause<Tweener>(this.deActivateCrossHairCG);
		TweenExtensions.Restart(this.activateCrossHairTrans, true, -1f);
		TweenExtensions.Restart(this.activateCrossHairCG, true, -1f);
	}

	public void HideActiveCrossHair()
	{
		TweenExtensions.Pause<Tweener>(this.activateCrossHairTrans);
		TweenExtensions.Pause<Tweener>(this.activateCrossHairCG);
		TweenExtensions.Restart(this.deActivateCrossHairTrans, true, -1f);
		TweenExtensions.Restart(this.deActivateCrossHairCG, true, -1f);
	}

	private void PlayerHitPause()
	{
		if (!this.hideCrossHairGroup)
		{
			this.CrossHairCanvas.enabled = false;
		}
	}

	private void PlayerHitUnPause()
	{
		if (!this.hideCrossHairGroup)
		{
			this.CrossHairCanvas.enabled = true;
		}
	}

	private void Awake()
	{
		GameManager.BehaviourManager.CrossHairBehaviour = this;
		GameManager.PauseManager.GamePaused += this.PlayerHitPause;
		GameManager.PauseManager.GameUnPaused += this.PlayerHitUnPause;
		this.activateCrossHairTrans = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => new Vector3(0.5f, 0.5f, 1f), delegate(Vector3 x)
		{
			this.CrossHairTrans.localScale = x;
		}, Vector3.one, 0.15f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.activateCrossHairTrans, false);
		TweenExtensions.Pause<Tweener>(this.activateCrossHairTrans);
		if (crossHairBehaviour.<>f__mg$cache0 == null)
		{
			crossHairBehaviour.<>f__mg$cache0 = new DOGetter<Vector3>(Vector3.get_one);
		}
		this.deActivateCrossHairTrans = TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(crossHairBehaviour.<>f__mg$cache0, delegate(Vector3 x)
		{
			this.CrossHairTrans.localScale = x;
		}, new Vector3(0.5f, 0.5f, 1f), 0.15f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.deActivateCrossHairTrans, false);
		TweenExtensions.Pause<Tweener>(this.deActivateCrossHairTrans);
		this.activateCrossHairCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => 0.25f, delegate(float x)
		{
			this.CrossHairCG.alpha = x;
		}, 0.9f, 0.15f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.activateCrossHairCG, false);
		TweenExtensions.Pause<Tweener>(this.activateCrossHairCG);
		this.deActivateCrossHairCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => 0.9f, delegate(float x)
		{
			this.CrossHairCG.alpha = x;
		}, 0.25f, 0.15f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.deActivateCrossHairCG, false);
		TweenExtensions.Pause<Tweener>(this.deActivateCrossHairCG);
	}

	private void OnDestroy()
	{
		GameManager.PauseManager.GamePaused -= this.PlayerHitPause;
		GameManager.PauseManager.GameUnPaused -= this.PlayerHitUnPause;
	}

	public Canvas CrossHairCanvas;

	public Transform CrossHairTrans;

	public CanvasGroup CrossHairCG;

	private bool hideCrossHairGroup;

	private Tweener activateCrossHairTrans;

	private Tweener deActivateCrossHairTrans;

	private Tweener activateCrossHairCG;

	private Tweener deActivateCrossHairCG;

	[CompilerGenerated]
	private static DOGetter<Vector3> <>f__mg$cache0;
}
