using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinnedAppObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEventSystemHandler
{
	public void SoftBuild()
	{
		base.GetComponent<RectTransform>().anchoredPosition = this.MyStartPOS;
	}

	public void BuildMe(SOFTWARE_PRODUCTS SetMyProduct, int MyCount)
	{
		this.MyProduct = SetMyProduct;
		this.title1Text.text = LookUp.SoftwareProducts.Get(SetMyProduct).MinProductTitle;
		this.title2Text.text = LookUp.SoftwareProducts.Get(SetMyProduct).MinProductTitle;
		this.appIconIMG.sprite = LookUp.SoftwareProducts.Get(SetMyProduct).MinProductSprite;
		float x2;
		if (MyCount != 0)
		{
			x2 = (float)MyCount * MinnedAppObject.MIN_APP_WIDTH + (float)MyCount * MinnedAppObject.MIN_APP_SPACING + MinnedAppObject.MIN_APP_SPACING;
		}
		else
		{
			x2 = MinnedAppObject.MIN_APP_SPACING;
		}
		this.MyStartPOS.x = x2;
		this.MyShowPOS.x = x2;
		base.GetComponent<RectTransform>().anchoredPosition = this.MyStartPOS;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => base.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
		{
			base.GetComponent<RectTransform>().anchoredPosition = x;
		}, this.MyShowPOS, 0.2f), 1);
	}

	public void BuildMe(SoftwareProductDefinition SetMyProduct, int MyCount)
	{
		this.MyProductData = SetMyProduct;
		this.title1Text.text = this.MyProductData.MinProductTitle;
		this.title2Text.text = this.MyProductData.MinProductTitle;
		this.appIconIMG.sprite = this.MyProductData.MinProductSprite;
		float x2;
		if (MyCount != 0)
		{
			x2 = (float)MyCount * MinnedAppObject.MIN_APP_WIDTH + (float)MyCount * MinnedAppObject.MIN_APP_SPACING + MinnedAppObject.MIN_APP_SPACING;
		}
		else
		{
			x2 = MinnedAppObject.MIN_APP_SPACING;
		}
		this.MyStartPOS.x = x2;
		this.MyShowPOS.x = x2;
		base.GetComponent<RectTransform>().anchoredPosition = this.MyStartPOS;
		TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => base.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
		{
			base.GetComponent<RectTransform>().anchoredPosition = x;
		}, this.MyShowPOS, 0.2f), 1);
	}

	public void RePOSMe(int setIndex)
	{
		float x2;
		if (setIndex != 0)
		{
			x2 = (float)setIndex * MinnedAppObject.MIN_APP_WIDTH + (float)setIndex * MinnedAppObject.MIN_APP_SPACING + MinnedAppObject.MIN_APP_SPACING;
		}
		else
		{
			x2 = MinnedAppObject.MIN_APP_SPACING;
		}
		this.MyShowPOS.x = x2;
		this.MyStartPOS.x = x2;
		Sequence sequence = DOTween.Sequence();
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => base.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
		{
			base.GetComponent<RectTransform>().anchoredPosition = x;
		}, this.MyShowPOS, 0.1f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void ForceDismissMe()
	{
		this.dismissMe();
	}

	private void dismissMe()
	{
		TweenExtensions.Kill(this.hoverSEQ, false);
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), new TweenCallback(this.unMinApp));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.tabHoverIMG.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			this.tabHoverIMG.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.2f), 1));
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => base.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
		{
			base.GetComponent<RectTransform>().anchoredPosition = x;
		}, this.MyStartPOS, 0.2f), 1));
		TweenExtensions.Play<Sequence>(sequence);
	}

	private void unMinApp()
	{
		if (this.MyProductData != null)
		{
			GameManager.ManagerSlinger.AppManager.UnMinApp(this.MyProductData);
		}
		else
		{
			GameManager.ManagerSlinger.AppManager.UnMinApp(this.MyProduct);
		}
	}

	public void Start()
	{
		this.hoverSEQ = DOTween.Sequence();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		TweenExtensions.Kill(this.hoverSEQ, false);
		TweenSettingsExtensions.Insert(this.hoverSEQ, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.tabHoverIMG.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			this.tabHoverIMG.GetComponent<CanvasGroup>().alpha = x;
		}, 1f, 0.2f), 1));
		TweenExtensions.Play<Sequence>(this.hoverSEQ);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		TweenExtensions.Kill(this.hoverSEQ, false);
		TweenSettingsExtensions.Insert(this.hoverSEQ, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.tabHoverIMG.GetComponent<CanvasGroup>().alpha, delegate(float x)
		{
			this.tabHoverIMG.GetComponent<CanvasGroup>().alpha = x;
		}, 0f, 0.2f), 1));
		TweenExtensions.Play<Sequence>(this.hoverSEQ);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		this.dismissMe();
	}

	public static float MIN_APP_SPACING = 5f;

	public static float MIN_APP_WIDTH = 137f;

	public Image appIconIMG;

	public Image tabHoverIMG;

	public Text title1Text;

	public Text title2Text;

	private SOFTWARE_PRODUCTS MyProduct;

	private SoftwareProductDefinition MyProductData;

	private Vector2 MyStartPOS = new Vector2(0f, -50f);

	private Vector2 MyShowPOS = new Vector2(0f, -4f);

	private Sequence hoverSEQ;

	private string myKey;
}
