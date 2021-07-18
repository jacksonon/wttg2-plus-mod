using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class MicMeterHook : MonoBehaviour
{
	public void PresentMicGroup(float PresentTime = 1f)
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.MicGroupTransform.anchoredPosition, delegate(Vector2 x)
		{
			LookUp.PlayerUI.MicGroupTransform.anchoredPosition = x;
		}, new Vector2(0f, 55f), PresentTime), 8);
	}

	public void DismissMicGroup(float DismissTime = 1f)
	{
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.PlayerUI.MicGroupTransform.anchoredPosition, delegate(Vector2 x)
		{
			LookUp.PlayerUI.MicGroupTransform.anchoredPosition = x;
		}, new Vector2(0f, 0f), DismissTime), 8);
	}

	private void displayPlayersLoudness(float loudLevel)
	{
		float alpha = 1f - loudLevel;
		LookUp.PlayerUI.MicGreenCG.alpha = alpha;
		LookUp.PlayerUI.MicRedCG.alpha = loudLevel;
	}

	private void stageMe()
	{
		GameManager.StageManager.Stage -= this.stageMe;
		GameManager.BehaviourManager.PlayerAudioBehaviour.CurrentPlayersLoudLevel.Event += this.displayPlayersLoudness;
	}

	private void Awake()
	{
		MicMeterHook.Ins = this;
		GameManager.StageManager.Stage += this.stageMe;
	}

	private void OnDestroy()
	{
	}

	public static MicMeterHook Ins;
}
