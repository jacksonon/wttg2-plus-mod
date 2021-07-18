using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public static class UIInventoryManager
{
	public static void ShowWifiDongle()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.WifiDongleIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.WifiDongleIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvFullSize, 0.25f), 18));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.WifiDongleIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.WifiDongleIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 1f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void HideWifiDongle()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.WifiDongleIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.WifiDongleIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvMinSize, 0.25f), 17));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.WifiDongleIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.WifiDongleIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void ShowMotionSensor()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.MotionSensorIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.MotionSensorIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvFullSize, 0.25f), 18));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.MotionSensorIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.MotionSensorIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 1f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void HideMotionSensor()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.MotionSensorIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.MotionSensorIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvMinSize, 0.25f), 17));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.MotionSensorIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.MotionSensorIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void ShowRemoteVPN()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.RemoteVPNIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.RemoteVPNIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvFullSize, 0.25f), 18));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.RemoteVPNIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.RemoteVPNIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 1f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void HideRemoteVPN()
	{
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
		{
			LookUp.PlayerUI.RemoteVPNIcon.GetComponent<Image>().sprite = LookUp.PlayerUI.RemoteVPNZeroBar;
		});
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.RemoteVPNIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.RemoteVPNIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvMinSize, 0.25f), 17));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.RemoteVPNIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.RemoteVPNIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void ShowDollMakerMarker()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvFullSize, 0.25f), 18));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 1f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public static void HideDollMakerMarker()
	{
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<RectTransform>().localScale, delegate(Vector3 x)
		{
			LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<RectTransform>().localScale = x;
		}, UIInventoryManager._iconInvMinSize, 0.25f), 17));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			LookUp.PlayerUI.DollMakerMarkerIcon.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private static Vector3 _iconInvFullSize = Vector3.one;

	private static Vector3 _iconInvMinSize = new Vector3(0.1f, 0.1f, 0.1f);
}
