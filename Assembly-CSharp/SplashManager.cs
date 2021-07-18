using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class SplashManager : MonoBehaviour
{
	private void presentRSLogo()
	{
		this.myAS.Play();
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), new TweenCallback(this.loadTitleScreen));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.rsLeftRT.anchoredPosition, delegate(Vector2 x)
		{
			this.rsLeftRT.anchoredPosition = x;
		}, this.defaultLogoLeftPOS, 0.6f), 2));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.rsRightRT.anchoredPosition, delegate(Vector2 x)
		{
			this.rsRightRT.anchoredPosition = x;
		}, this.defaultLogoRightPOS, 0.6f), 2));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsLeftCG.alpha, delegate(float x)
		{
			this.rsLeftCG.alpha = x;
		}, 0.5f, 0.25f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsRightCG.alpha, delegate(float x)
		{
			this.rsRightCG.alpha = x;
		}, 0.5f, 0.25f), 1));
		TweenSettingsExtensions.Insert(sequence, 1.4f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsLeftCG.alpha, delegate(float x)
		{
			this.rsLeftCG.alpha = x;
		}, 0f, 1f), 1));
		TweenSettingsExtensions.Insert(sequence, 1.4f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsRightCG.alpha, delegate(float x)
		{
			this.rsRightCG.alpha = x;
		}, 0f, 1f), 1));
		TweenSettingsExtensions.Insert(sequence, 1.4f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsLogoCG.alpha, delegate(float x)
		{
			this.rsLogoCG.alpha = x;
		}, 0.6f, 1f), 1));
		TweenSettingsExtensions.Insert(sequence, 1.6f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsTextCG.alpha, delegate(float x)
		{
			this.rsTextCG.alpha = x;
		}, 0.6f, 1f), 1));
		TweenSettingsExtensions.Insert(sequence, 3.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsWebsiteCG.alpha, delegate(float x)
		{
			this.rsWebsiteCG.alpha = x;
		}, 0.6f, 1f), 1));
		TweenSettingsExtensions.Insert(sequence, 4.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsLogoCG.alpha, delegate(float x)
		{
			this.rsLogoCG.alpha = x;
		}, 1f, 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 4.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsTextCG.alpha, delegate(float x)
		{
			this.rsTextCG.alpha = x;
		}, 1f, 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 4.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsWebsiteCG.alpha, delegate(float x)
		{
			this.rsWebsiteCG.alpha = x;
		}, 1f, 0.75f), 1));
		TweenSettingsExtensions.Insert(sequence, 4.75f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.camBloom.bloomIntensity, delegate(float x)
		{
			this.camBloom.bloomIntensity = x;
		}, 0.25f, 2.5f), 1));
		TweenSettingsExtensions.Insert(sequence, 7.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsLogoCG.alpha, delegate(float x)
		{
			this.rsLogoCG.alpha = x;
		}, 0f, 0.25f), 1));
		TweenSettingsExtensions.Insert(sequence, 7.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsTextCG.alpha, delegate(float x)
		{
			this.rsTextCG.alpha = x;
		}, 0f, 0.25f), 1));
		TweenSettingsExtensions.Insert(sequence, 7.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.rsWebsiteCG.alpha, delegate(float x)
		{
			this.rsWebsiteCG.alpha = x;
		}, 0f, 0.25f), 1));
		TweenSettingsExtensions.Insert(sequence, 7.25f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.camBloom.bloomIntensity, delegate(float x)
		{
			this.camBloom.bloomIntensity = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void loadTitleScreen()
	{
		SceneManager.LoadScene(1);
	}

	private void Awake()
	{
		this.myAS = base.GetComponent<AudioSource>();
		this.camBloom = this.myCamera.GetComponent<Bloom>();
		this.myCamera.orthographicSize = (float)Screen.height / 2f;
		this.myCamera.transform.position = new Vector3((float)Screen.width / 2f, (float)(-(float)(Screen.height / 2)), this.myCamera.transform.position.z);
		this.defaultLogoLeftPOS = this.rsLeftRT.anchoredPosition;
		this.defaultLogoRightPOS = this.rsRightRT.anchoredPosition;
		this.rsLeftRT.anchoredPosition = new Vector2(-((float)Screen.width / 2f + this.rsLeftRT.sizeDelta.x / 2f), this.rsLeftRT.anchoredPosition.y);
		this.rsRightRT.anchoredPosition = new Vector2((float)Screen.width / 2f + this.rsRightRT.sizeDelta.x / 2f, this.rsRightRT.anchoredPosition.y);
	}

	private void Start()
	{
		if (Screen.height < 720)
		{
			Screen.SetResolution(1280, 720, false);
		}
		this.startTimeStamp = Time.time;
		this.startActive = true;
	}

	private void Update()
	{
		if (this.startActive && Time.time - this.startTimeStamp >= 0.5f)
		{
			this.startActive = false;
			this.presentRSLogo();
		}
	}

	[SerializeField]
	private Camera myCamera;

	[SerializeField]
	private RectTransform rsLeftRT;

	[SerializeField]
	private RectTransform rsRightRT;

	[SerializeField]
	private CanvasGroup rsLogoCG;

	[SerializeField]
	private CanvasGroup rsLeftCG;

	[SerializeField]
	private CanvasGroup rsRightCG;

	[SerializeField]
	private CanvasGroup rsTextCG;

	[SerializeField]
	private CanvasGroup rsWebsiteCG;

	private Bloom camBloom;

	private AudioSource myAS;

	private Vector2 defaultLogoLeftPOS;

	private Vector2 defaultLogoRightPOS;

	private float startTimeStamp;

	private bool startActive;
}
