using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TopMenuIconBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEventSystemHandler
{
	private void Awake()
	{
		this.showHoverSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.showHoverSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.smallScale, delegate(Vector3 x)
		{
			this.hoverIMGRT.localScale = x;
		}, this.fullScale, 0.3f), 1));
		TweenSettingsExtensions.Insert(this.showHoverSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => 0f, delegate(float x)
		{
			this.hoverIMGCG.alpha = x;
		}, 1f, 0.2f), 1));
		TweenExtensions.Pause<Sequence>(this.showHoverSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.showHoverSeq, false);
		this.hideOverSeq = DOTween.Sequence();
		TweenSettingsExtensions.Insert(this.hideOverSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(DOTween.To(() => this.fullScale, delegate(Vector3 x)
		{
			this.hoverIMGRT.localScale = x;
		}, this.smallScale, 0.2f), 1));
		TweenSettingsExtensions.Insert(this.hideOverSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => 1f, delegate(float x)
		{
			this.hoverIMGCG.alpha = x;
		}, 0f, 0.2f), 1));
		TweenExtensions.Pause<Sequence>(this.hideOverSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.hideOverSeq, false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		TweenExtensions.Restart(this.showHoverSeq, true, -1f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		TweenExtensions.Restart(this.hideOverSeq, true, -1f);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (this.clickAction != null)
		{
			this.clickAction.Invoke();
		}
	}

	[SerializeField]
	private RectTransform hoverIMGRT;

	[SerializeField]
	private CanvasGroup hoverIMGCG;

	[SerializeField]
	private UnityEvent clickAction;

	private Vector3 fullScale = Vector3.one;

	private Vector3 smallScale = new Vector3(0.25f, 0.25f, 0.25f);

	private Sequence showHoverSeq;

	private Sequence hideOverSeq;
}
