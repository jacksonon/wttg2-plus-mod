using System;
using Colorful;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class ComputerCameraManager : MonoBehaviour
{
	public RenderTexture FinalRenderTexture
	{
		get
		{
			return this.finalRenderTexture;
		}
	}

	public void BecomeMaster()
	{
		this.myCamera.targetTexture = null;
		this.mainCamera.enabled = false;
	}

	public void BecomeSlave()
	{
		this.myCamera.targetTexture = this.finalRenderTexture;
		this.mainCamera.enabled = true;
	}

	public void BeginHackAni()
	{
		this.postFXAnalogTV.enabled = true;
		this.postFXGlitch.enabled = true;
		this.postBright.enabled = true;
		this.postFXGlitch.SettingsTearing.MaxDisplacement = 0.215f;
		TweenExtensions.Restart(this.beginHackAniSeq, true, -1f);
	}

	public void TriggerShowEndLocation()
	{
		this.postFXAnalogTV.enabled = true;
		this.postFXGlitch.enabled = true;
		this.postBright.enabled = true;
		this.postFXGlitch.SettingsTearing.MaxDisplacement = 0.215f;
		TweenExtensions.Restart(this.showLocationAniSeq, true, -1f);
	}

	public void TriggerHackingTerminalDumpGlitch()
	{
		this.postFXGlitch.enabled = true;
		this.postFXGlitch.Mode = Glitch.GlitchingMode.Complete;
		TweenExtensions.Restart(this.triggerHackingTerminalDumpGlitchSeq, true, -1f);
	}

	public void TriggerHackingTerminalSkullEFXs()
	{
		this.postFXLED.enabled = true;
		this.postFXGlitch.enabled = true;
		this.postFXGlitch.Mode = Glitch.GlitchingMode.Interferences;
		this.postFXGlitch.SettingsInterferences.Speed = 10f;
		this.postFXGlitch.SettingsInterferences.Density = 30f;
		this.postFXGlitch.SettingsInterferences.MaxDisplacement = 0.215f;
	}

	public void TriggerHackBlockedEFXs()
	{
		this.postBright.enabled = true;
		this.postFXAnalogTV.enabled = true;
		this.postFXGlitch.enabled = true;
		this.postBloom.enabled = true;
		this.postFXAnalogTV.NoiseIntensity = 0.03f;
		this.postFXAnalogTV.ScanlinesIntensity = 0.59f;
		this.postFXAnalogTV.ScanlinesCount = 772;
		this.postFXAnalogTV.Distortion = -0.75f;
		this.postFXAnalogTV.CubicDistortion = -0.75f;
		this.postFXAnalogTV.Scale = 0.2f;
		this.postFXGlitch.Mode = Glitch.GlitchingMode.Complete;
		this.postFXGlitch.SettingsInterferences.Speed = 4.5f;
		this.postFXGlitch.SettingsInterferences.Density = 4f;
		this.postFXGlitch.SettingsInterferences.MaxDisplacement = 8f;
		this.postFXGlitch.SettingsTearing.Speed = 0.95f;
		this.postFXGlitch.SettingsTearing.Intensity = 0.02f;
		this.postFXGlitch.SettingsTearing.MaxDisplacement = 0.125f;
		this.postBloom.bloomIntensity = 4.4f;
		this.postBloom.bloomThreshold = 0.5f;
		this.postBloom.bloomBlurIterations = 2;
		TweenExtensions.Restart(this.hackedBlockSeq, true, -1f);
	}

	public void TriggerHackedEFXs()
	{
		this.postBright.enabled = true;
		this.postFXLED.enabled = true;
		this.postFXGlitch.enabled = true;
		this.postBloom.enabled = true;
		this.postFXGlitch.Mode = Glitch.GlitchingMode.Interferences;
		this.postFXGlitch.SettingsInterferences.Speed = 10f;
		this.postFXGlitch.SettingsInterferences.Density = 30f;
		this.postFXGlitch.SettingsInterferences.MaxDisplacement = 0.215f;
		TweenExtensions.Restart(this.hackedSeq, true, -1f);
		GameManager.TimeSlinger.FireTimer(0.55f, delegate()
		{
			this.postFXLED.enabled = false;
			this.postFXGlitch.Mode = Glitch.GlitchingMode.Complete;
		}, 0);
	}

	public void ClearPostFXs()
	{
		this.postFXAnalogTV.enabled = false;
		this.postFXGlitch.enabled = false;
		this.postFXLED.enabled = false;
		this.postBright.enabled = false;
		this.postBloom.enabled = false;
		this.postFXGlitch.Mode = Glitch.GlitchingMode.Tearing;
		this.postFXGlitch.SettingsInterferences.Speed = 0f;
		this.postFXGlitch.SettingsInterferences.Density = 0f;
		this.postFXGlitch.SettingsInterferences.MaxDisplacement = 0f;
		this.postFXGlitch.SettingsTearing.Speed = 0f;
		this.postFXGlitch.SettingsTearing.Intensity = 0f;
		this.postFXGlitch.SettingsTearing.MaxDisplacement = 0f;
		this.postFXAnalogTV.NoiseIntensity = 0f;
		this.postFXAnalogTV.ScanlinesIntensity = 0f;
		this.postFXAnalogTV.ScanlinesCount = 0;
		this.postFXAnalogTV.Distortion = 0f;
		this.postFXAnalogTV.CubicDistortion = 0f;
		this.postFXAnalogTV.Scale = 1f;
		this.postBright.Brightness = 0f;
		this.postBright.Contrast = 0f;
		this.postBright.Gamma = 1f;
		this.postBloom.bloomIntensity = 0f;
		this.postBloom.bloomThreshold = 0f;
		this.postBloom.bloomBlurIterations = 0;
	}

	private void Awake()
	{
		ComputerCameraManager.Ins = this;
		this.myCamera = base.GetComponent<Camera>();
		this.finalRenderTexture = new RenderTexture(Screen.width, Screen.height, 24, 0);
		this.finalRenderTexture.wrapMode = 1;
		this.finalRenderTexture.filterMode = 2;
		this.myCamera.orthographicSize = (float)Screen.height / 2f;
		base.transform.localPosition = new Vector3((float)Screen.width / 2f, (float)(-(float)(Screen.height / 2)), base.transform.localPosition.z);
		this.myCamera.targetTexture = this.finalRenderTexture;
		this.postFXAnalogTV = base.GetComponent<AnalogTV>();
		this.postFXGlitch = base.GetComponent<Glitch>();
		this.postFXLED = base.GetComponent<Led>();
		this.postBright = base.GetComponent<BrightnessContrastGamma>();
		this.postBloom = base.GetComponent<Bloom>();
		this.beginHackAniSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXAnalogTV.Distortion, delegate(float x)
		{
			this.postFXAnalogTV.Distortion = x;
		}, -0.09f, 0.1f), 20));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Contrast, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, -100f, 0.2f), 1));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Gamma, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, 9.9f, 0.2f), 1));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXAnalogTV.Distortion, delegate(float x)
		{
			this.postFXAnalogTV.Distortion = x;
		}, 0f, 0.2f), 21));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Contrast, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, 0f, 0.15f), 1));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Gamma, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, 1f, 0.15f), 1));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0.45f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Speed = x;
		}, 4f, 0.7f), 1));
		TweenSettingsExtensions.Insert(this.beginHackAniSeq, 0.45f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Intensity, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Intensity = x;
		}, 0.64f, 0.5f), 1));
		TweenExtensions.Pause<Sequence>(this.beginHackAniSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.beginHackAniSeq, false);
		this.showLocationAniSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.showLocationAniSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXAnalogTV.Distortion, delegate(float x)
		{
			this.postFXAnalogTV.Distortion = x;
		}, -0.09f, 0.1f), 20));
		TweenSettingsExtensions.Insert(this.showLocationAniSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXAnalogTV.Distortion, delegate(float x)
		{
			this.postFXAnalogTV.Distortion = x;
		}, 0f, 0.2f), 21));
		TweenSettingsExtensions.Insert(this.showLocationAniSeq, 0.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Speed = x;
		}, 4f, 0.7f), 1));
		TweenSettingsExtensions.Insert(this.showLocationAniSeq, 0.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Intensity, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Intensity = x;
		}, 0.64f, 0.5f), 1));
		TweenExtensions.Pause<Sequence>(this.showLocationAniSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.showLocationAniSeq, false);
		this.triggerHackingTerminalDumpGlitchSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.Speed = x;
		}, 10f, 0.25f), 1));
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.Density, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.Density = x;
		}, 3.1f, 0.25f), 1));
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.MaxDisplacement, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.MaxDisplacement = x;
		}, 1.32f, 0.25f), 1));
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Speed = x;
		}, 30f, 0.25f), 1));
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Intensity, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Intensity = x;
		}, 0.145f, 0.25f), 1));
		TweenSettingsExtensions.Insert(this.triggerHackingTerminalDumpGlitchSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.MaxDisplacement, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.MaxDisplacement = x;
		}, 0.028f, 0.25f), 1));
		TweenExtensions.Pause<Sequence>(this.triggerHackingTerminalDumpGlitchSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.triggerHackingTerminalDumpGlitchSeq, false);
		this.hackedBlockSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.hackedBlockSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXAnalogTV.Scale, delegate(float x)
		{
			this.postFXAnalogTV.Scale = x;
		}, 1f, 0.35f), 17));
		TweenSettingsExtensions.Insert(this.hackedBlockSeq, 1.85f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Brightness, delegate(float x)
		{
			this.postBright.Brightness = x;
		}, 100f, 0.4f), 1));
		TweenSettingsExtensions.Insert(this.hackedBlockSeq, 1.85f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Contrast, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, -100f, 0.4f), 1));
		TweenSettingsExtensions.Insert(this.hackedBlockSeq, 1.85f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Gamma, delegate(float x)
		{
			this.postBright.Gamma = x;
		}, 9.9f, 0.4f), 1));
		TweenExtensions.Pause<Sequence>(this.hackedBlockSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.hackedBlockSeq, false);
		this.hackedSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBloom.bloomIntensity, delegate(float x)
		{
			this.postBloom.bloomIntensity = x;
		}, 4.4f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBloom.bloomThreshold, delegate(float x)
		{
			this.postBloom.bloomThreshold = x;
		}, 0.5f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<Tweener>(DOTween.To(() => this.postBloom.bloomBlurIterations, delegate(int x)
		{
			this.postBloom.bloomBlurIterations = x;
		}, 2, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.Speed = x;
		}, 4.5f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.Density, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.Density = x;
		}, 4f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsInterferences.MaxDisplacement, delegate(float x)
		{
			this.postFXGlitch.SettingsInterferences.MaxDisplacement = x;
		}, 8f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Speed, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Speed = x;
		}, 0.95f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.Intensity, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.Intensity = x;
		}, 0.02f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 0.55f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postFXGlitch.SettingsTearing.MaxDisplacement, delegate(float x)
		{
			this.postFXGlitch.SettingsTearing.MaxDisplacement = x;
		}, 0.125f, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 2.5f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Brightness, delegate(float x)
		{
			this.postBright.Brightness = x;
		}, 100f, 0.4f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 2.5f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Contrast, delegate(float x)
		{
			this.postBright.Contrast = x;
		}, -100f, 0.4f), 1));
		TweenSettingsExtensions.Insert(this.hackedSeq, 2.5f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.postBright.Gamma, delegate(float x)
		{
			this.postBright.Gamma = x;
		}, 9.9f, 0.4f), 1));
		TweenExtensions.Pause<Sequence>(this.hackedSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.hackedSeq, false);
		CameraManager.Get(CAMERA_ID.MAIN, out this.mainCamera);
	}

	public static ComputerCameraManager Ins;

	private Camera myCamera;

	private Camera mainCamera;

	private AnalogTV postFXAnalogTV;

	private Glitch postFXGlitch;

	private Led postFXLED;

	private BrightnessContrastGamma postBright;

	private Bloom postBloom;

	private Sequence beginHackAniSeq;

	private Sequence triggerHackingTerminalDumpGlitchSeq;

	private Sequence hackedBlockSeq;

	private Sequence hackedSeq;

	private Sequence showLocationAniSeq;

	private RenderTexture finalRenderTexture;
}
