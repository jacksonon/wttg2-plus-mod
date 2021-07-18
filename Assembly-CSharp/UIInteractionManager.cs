using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
	public void ShowLeftMouseButtonAction()
	{
		TweenExtensions.Restart(this.showLeftMouseClick, true, -1f);
	}

	public void HideLeftMouseButtonAction()
	{
		TweenExtensions.Restart(this.hideLeftMouseClick, true, -1f);
	}

	public void ShowRightMouseButtonAction()
	{
		TweenExtensions.Restart(this.showRightMouseClick, true, -1f);
	}

	public void HideRightMouseButtonAction()
	{
		TweenExtensions.Restart(this.hideRightMouseClick, true, -1f);
	}

	public void ShowOpenDoor()
	{
		TweenExtensions.Restart(this.showOpenDoor, true, -1f);
	}

	public void HideOpenDoor()
	{
		TweenExtensions.Restart(this.hideOpenDoor, true, -1f);
	}

	public void ShowLock()
	{
		TweenExtensions.Restart(this.showLock, true, -1f);
	}

	public void HideLock()
	{
		TweenExtensions.Restart(this.hideLock, true, -1f);
	}

	public void ShowUnLock()
	{
		TweenExtensions.Restart(this.showUnLock, true, -1f);
	}

	public void HideUnLock()
	{
		TweenExtensions.Restart(this.hideUnLock, true, -1f);
	}

	public void ShowLightOn()
	{
		TweenExtensions.Restart(this.showLightOn, true, -1f);
	}

	public void HideLightOn()
	{
		TweenExtensions.Restart(this.hideLightOn, true, -1f);
	}

	public void ShowLightOff()
	{
		TweenExtensions.Restart(this.showLightOff, true, -1f);
	}

	public void HideLightOff()
	{
		TweenExtensions.Restart(this.hideLightOff, true, -1f);
	}

	public void ShowPeep()
	{
		TweenExtensions.Restart(this.showPeep, true, -1f);
	}

	public void HidePeep()
	{
		TweenExtensions.Restart(this.hidePeep, true, -1f);
	}

	public void ShowLeap()
	{
		TweenExtensions.Restart(this.showLeap, true, -1f);
	}

	public void HideLeap()
	{
		TweenExtensions.Restart(this.hideLeap, true, -1f);
	}

	public void ShowHand()
	{
		TweenExtensions.Restart(this.showHand, true, -1f);
	}

	public void HideHand()
	{
		TweenExtensions.Restart(this.hideHand, true, -1f);
	}

	public void ShowHide()
	{
		TweenExtensions.Restart(this.showHide, true, -1f);
	}

	public void HideHide()
	{
		TweenExtensions.Restart(this.hideHide, true, -1f);
	}

	public void ShowSit()
	{
		TweenExtensions.Restart(this.showSit, true, -1f);
	}

	public void HideSit()
	{
		TweenExtensions.Restart(this.hideSit, true, -1f);
	}

	public void ShowComputer()
	{
		TweenExtensions.Restart(this.showComputer, true, -1f);
	}

	public void HideComputer()
	{
		TweenExtensions.Restart(this.hideComputer, true, -1f);
	}

	public void ShowPower()
	{
		TweenExtensions.Restart(this.showPower, true, -1f);
	}

	public void HidePower()
	{
		TweenExtensions.Restart(this.hidePower, true, -1f);
	}

	public void ShowComputerOn()
	{
		TweenExtensions.Restart(this.showComputerOn, true, -1f);
	}

	public void HideComputerOn()
	{
		TweenExtensions.Restart(this.hideComputerOn, true, -1f);
	}

	public void ShowKnob()
	{
		TweenExtensions.Restart(this.showKnob, true, -1f);
	}

	public void HideKnob()
	{
		TweenExtensions.Restart(this.hideKnob, true, -1f);
	}

	public void ShowDollMakerMarker()
	{
		TweenExtensions.Restart(this.showDollMakerMarker, true, -1f);
	}

	public void HideDollMakerMarker()
	{
		TweenExtensions.Restart(this.hideDollMakerMarker, true, -1f);
	}

	public void ShowEnterBraceMode()
	{
		TweenExtensions.Restart(this.showEnterBraceMode, true, -1f);
	}

	public void HideEnterBraceMode()
	{
		TweenExtensions.Restart(this.hideEnterBraceMode, true, -1f);
	}

	public void ShowHoldMode()
	{
		TweenExtensions.Restart(this.showHoldMode, true, -1f);
	}

	public void HideHoldMode()
	{
		TweenExtensions.Restart(this.hideHoldMode, true, -1f);
	}

	public void ShowEBar()
	{
		TweenExtensions.Restart(this.showEBar, true, -1f);
	}

	public void HideEBar()
	{
		TweenExtensions.Restart(this.hideEBar, true, -1f);
	}

	private void Awake()
	{
		UIInteractionManager.Ins = this;
		this.showLeftMouseClick = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-40f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LeftMouseClickTransform.anchoredPosition = x;
		}, new Vector2(-40f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showLeftMouseClick, false);
		TweenExtensions.Pause<Tweener>(this.showLeftMouseClick);
		this.hideLeftMouseClick = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-40f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LeftMouseClickTransform.anchoredPosition = x;
		}, new Vector2(-40f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideLeftMouseClick, false);
		TweenExtensions.Pause<Tweener>(this.hideLeftMouseClick);
		this.showRightMouseClick = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(40f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.RightMouseClickTransform.anchoredPosition = x;
		}, new Vector2(40f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showRightMouseClick, false);
		TweenExtensions.Pause<Tweener>(this.showRightMouseClick);
		this.hideRightMouseClick = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(40f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.RightMouseClickTransform.anchoredPosition = x;
		}, new Vector2(40f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideRightMouseClick, false);
		TweenExtensions.Pause<Tweener>(this.hideRightMouseClick);
		this.showOpenDoor = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.OpenDoorTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showOpenDoor, false);
		TweenExtensions.Pause<Tweener>(this.showOpenDoor);
		this.hideOpenDoor = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.OpenDoorTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideOpenDoor, false);
		TweenExtensions.Pause<Tweener>(this.hideOpenDoor);
		this.showLock = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LockTransform.anchoredPosition = x;
		}, new Vector2(80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showLock, false);
		TweenExtensions.Pause<Tweener>(this.showLock);
		this.hideLock = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LockTransform.anchoredPosition = x;
		}, new Vector2(80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideLock, false);
		TweenExtensions.Pause<Tweener>(this.hideLock);
		this.showUnLock = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.UnLockTransform.anchoredPosition = x;
		}, new Vector2(80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showUnLock, false);
		TweenExtensions.Pause<Tweener>(this.showUnLock);
		this.hideUnLock = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.UnLockTransform.anchoredPosition = x;
		}, new Vector2(80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideUnLock, false);
		TweenExtensions.Pause<Tweener>(this.hideUnLock);
		this.showLightOn = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LightOnTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showLightOn, false);
		TweenExtensions.Pause<Tweener>(this.showLightOn);
		this.hideLightOn = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LightOnTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideLightOn, false);
		TweenExtensions.Pause<Tweener>(this.hideLightOn);
		this.showLightOff = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LightOffTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showLightOff, false);
		TweenExtensions.Pause<Tweener>(this.showLightOff);
		this.hideLightOff = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LightOffTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideLightOff, false);
		TweenExtensions.Pause<Tweener>(this.hideLightOff);
		this.showPeep = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.PeepEyeTransform.anchoredPosition = x;
		}, new Vector2(-86f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showPeep, false);
		TweenExtensions.Pause<Tweener>(this.showPeep);
		this.hidePeep = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.PeepEyeTransform.anchoredPosition = x;
		}, new Vector2(-86f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hidePeep, false);
		TweenExtensions.Pause<Tweener>(this.hidePeep);
		this.showLeap = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LeapTransform.anchoredPosition = x;
		}, new Vector2(-86f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showLeap, false);
		TweenExtensions.Pause<Tweener>(this.showLeap);
		this.hideLeap = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.LeapTransform.anchoredPosition = x;
		}, new Vector2(-86f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideLeap, false);
		TweenExtensions.Pause<Tweener>(this.hideLeap);
		this.showHand = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HandTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showHand, false);
		TweenExtensions.Pause<Tweener>(this.showHand);
		this.hideHand = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HandTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideHand, false);
		TweenExtensions.Pause<Tweener>(this.hideHand);
		this.showHide = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-84f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HideTransform.anchoredPosition = x;
		}, new Vector2(-84f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showHide, false);
		TweenExtensions.Pause<Tweener>(this.showHide);
		this.hideHide = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-84f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HideTransform.anchoredPosition = x;
		}, new Vector2(-84f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideHide, false);
		TweenExtensions.Pause<Tweener>(this.hideHide);
		this.showSit = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.SitTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showSit, false);
		TweenExtensions.Pause<Tweener>(this.showSit);
		this.hideSit = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.SitTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideSit, false);
		TweenExtensions.Pause<Tweener>(this.hideSit);
		this.showComputer = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-92f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.ComputerTransform.anchoredPosition = x;
		}, new Vector2(-92f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showComputer, false);
		TweenExtensions.Pause<Tweener>(this.showComputer);
		this.hideComputer = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-92f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.ComputerTransform.anchoredPosition = x;
		}, new Vector2(-92f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideComputer, false);
		TweenExtensions.Pause<Tweener>(this.hideComputer);
		this.showPower = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.PowerTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showPower, false);
		TweenExtensions.Pause<Tweener>(this.showPower);
		this.hidePower = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.PowerTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hidePower, false);
		TweenExtensions.Pause<Tweener>(this.hidePower);
		this.showComputerOn = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-84f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.ComputerOnTransform.anchoredPosition = x;
		}, new Vector2(-84f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showComputerOn, false);
		TweenExtensions.Pause<Tweener>(this.showComputerOn);
		this.hideComputerOn = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-84f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.ComputerOnTransform.anchoredPosition = x;
		}, new Vector2(-84f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideComputerOn, false);
		TweenExtensions.Pause<Tweener>(this.hideComputerOn);
		this.showKnob = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-92f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.KnobTransform.anchoredPosition = x;
		}, new Vector2(-92f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showKnob, false);
		TweenExtensions.Pause<Tweener>(this.showKnob);
		this.hideKnob = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-92f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.KnobTransform.anchoredPosition = x;
		}, new Vector2(-92f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideKnob, false);
		TweenExtensions.Pause<Tweener>(this.hideKnob);
		this.showDollMakerMarker = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.DollMakerMarkerTransform.anchoredPosition = x;
		}, new Vector2(-80f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showDollMakerMarker, false);
		TweenExtensions.Pause<Tweener>(this.showDollMakerMarker);
		this.hideDollMakerMarker = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-80f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.DollMakerMarkerTransform.anchoredPosition = x;
		}, new Vector2(-80f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideDollMakerMarker, false);
		TweenExtensions.Pause<Tweener>(this.hideDollMakerMarker);
		this.showEnterBraceMode = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(82f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.EnterBraceTransform.anchoredPosition = x;
		}, new Vector2(82f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showEnterBraceMode, false);
		TweenExtensions.Pause<Tweener>(this.showEnterBraceMode);
		this.hideEnterBraceMode = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(82f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.EnterBraceTransform.anchoredPosition = x;
		}, new Vector2(82f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideEnterBraceMode, false);
		TweenExtensions.Pause<Tweener>(this.hideEnterBraceMode);
		this.showHoldMode = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, 66f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HoldTransform.anchoredPosition = x;
		}, new Vector2(-86f, -30f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showHoldMode, false);
		TweenExtensions.Pause<Tweener>(this.showHoldMode);
		this.hideHoldMode = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-86f, -30f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.HoldTransform.anchoredPosition = x;
		}, new Vector2(-86f, 66f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideHoldMode, false);
		TweenExtensions.Pause<Tweener>(this.hideHoldMode);
		if (UIInteractionManager.<>f__mg$cache0 == null)
		{
			UIInteractionManager.<>f__mg$cache0 = new DOGetter<Vector2>(Vector2.get_zero);
		}
		this.showEBar = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(UIInteractionManager.<>f__mg$cache0, delegate(Vector2 x)
		{
			LookUp.PlayerUI.EBarTransform.anchoredPosition = x;
		}, new Vector2(-55f, 0f), 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showEBar, false);
		TweenExtensions.Pause<Tweener>(this.showEBar);
		this.hideEBar = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => new Vector2(-55f, 0f), delegate(Vector2 x)
		{
			LookUp.PlayerUI.EBarTransform.anchoredPosition = x;
		}, Vector2.zero, 0.2f), 1);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideEBar, false);
		TweenExtensions.Pause<Tweener>(this.hideEBar);
	}

	public static UIInteractionManager Ins;

	private Tweener showLeftMouseClick;

	private Tweener hideLeftMouseClick;

	private Tweener showRightMouseClick;

	private Tweener hideRightMouseClick;

	private Tweener showOpenDoor;

	private Tweener hideOpenDoor;

	private Tweener showLock;

	private Tweener hideLock;

	private Tweener showUnLock;

	private Tweener hideUnLock;

	private Tweener showLightOn;

	private Tweener hideLightOn;

	private Tweener showLightOff;

	private Tweener hideLightOff;

	private Tweener showPeep;

	private Tweener hidePeep;

	private Tweener showLeap;

	private Tweener hideLeap;

	private Tweener showHand;

	private Tweener hideHand;

	private Tweener showHide;

	private Tweener hideHide;

	private Tweener showSit;

	private Tweener hideSit;

	private Tweener showComputer;

	private Tweener hideComputer;

	private Tweener showPower;

	private Tweener hidePower;

	private Tweener showComputerOn;

	private Tweener hideComputerOn;

	private Tweener showKnob;

	private Tweener hideKnob;

	private Tweener showDollMakerMarker;

	private Tweener hideDollMakerMarker;

	private Tweener showEnterBraceMode;

	private Tweener hideEnterBraceMode;

	private Tweener showHoldMode;

	private Tweener hideHoldMode;

	private Tweener showEBar;

	private Tweener hideEBar;

	[CompilerGenerated]
	private static DOGetter<Vector2> <>f__mg$cache0;
}
