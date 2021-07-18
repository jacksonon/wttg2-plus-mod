﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.SceneManagement;

public static class UIManager
{
	public static void FadeScreen(float ToValue, float Duration)
	{
		TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.BlackScreenCG.alpha, delegate(float x)
		{
			LookUp.PlayerUI.BlackScreenCG.alpha = x;
		}, ToValue, Duration), 1);
	}

	public static void TriggerGameOver(string Reason)
	{
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.UNIVERSAL, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.PLAYER, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.MUSIC, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.COMPUTER_SFX, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.HACKING_SFX, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENVIRONMENT, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.OUTSIDE, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.DEAD_DROP, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENEMY, 0f, 0.5f);
		GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.GameOver);
		GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.GameOverHit);
		LookUp.PlayerUI.GameOverReasonText.SetText(Reason);
		UIManager.FadeScreen(1f, 0.5f);
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.GameOverGC.alpha, delegate(float x)
		{
			LookUp.PlayerUI.GameOverGC.alpha = x;
		}, 1f, 1f), 1), 0.75f);
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.GameOverReasonCG.alpha, delegate(float x)
		{
			LookUp.PlayerUI.GameOverReasonCG.alpha = x;
		}, 1f, 1f), 1), 2.4f);
		GameManager.TimeSlinger.FireTimer(6f, delegate()
		{
			SceneManager.LoadScene(1);
		}, 0);
	}

	public static void TriggerHardGameOver(string Reason)
	{
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.UNIVERSAL, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.PLAYER, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.MUSIC, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.COMPUTER_SFX, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.HACKING_SFX, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENVIRONMENT, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.OUTSIDE, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.DEAD_DROP, 0f, 0f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENEMY, 0f, 0f);
		GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.GameOver);
		GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.GameOverHit);
		LookUp.PlayerUI.GameOverReasonText.SetText(Reason);
		LookUp.PlayerUI.BlackScreenCG.alpha = 1f;
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.GameOverGC.alpha, delegate(float x)
		{
			LookUp.PlayerUI.GameOverGC.alpha = x;
		}, 1f, 1f), 1), 0.25f);
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.GameOverReasonCG.alpha, delegate(float x)
		{
			LookUp.PlayerUI.GameOverReasonCG.alpha = x;
		}, 1f, 1f), 1), 1.9f);
		GameManager.TimeSlinger.FireTimer(6f, delegate()
		{
			SceneManager.LoadScene(0);
		}, 0);
	}

	public static void TriggerLoadEnding()
	{
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.UNIVERSAL, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.PLAYER, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.MUSIC, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.COMPUTER_SFX, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.HACKING_SFX, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENVIRONMENT, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.OUTSIDE, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.DEAD_DROP, 0f, 0.5f);
		GameManager.AudioSlinger.MuffleAudioLayer(AUDIO_LAYER.ENEMY, 0f, 0.5f);
		UIManager.FadeScreen(1f, 0.5f);
		MainCameraHook.Ins.ClearARF(4f);
		GameManager.TimeSlinger.FireTimer(5f, delegate()
		{
			if (DataManager.LeetMode)
			{
				SceneManager.LoadScene(7);
			}
			else
			{
				SceneManager.LoadScene(5);
			}
		}, 0);
	}

	public static void FlashPlayer()
	{
		LookUp.PlayerUI.FlashScreenCG.alpha = 1f;
		TweenSettingsExtensions.SetDelay<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => LookUp.PlayerUI.FlashScreenCG.alpha, delegate(float x)
		{
			LookUp.PlayerUI.FlashScreenCG.alpha = x;
		}, 0f, 1.2f), 1), 3.5f);
	}
}
