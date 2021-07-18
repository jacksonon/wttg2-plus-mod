using System;
using System.Diagnostics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZeroDayProductBTNBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEventSystemHandler
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ZeroDayProductBTNBehaviour.BoolActions BuyItem;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ZeroDayProductBTNBehaviour.VoidActions InstallItem;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event ZeroDayProductBTNBehaviour.VoidActions CantBuy;

	public void SetToBuy()
	{
		this.isDisabled = false;
		base.GetComponent<Image>().sprite = this.DefaultSprite;
		this.btnText.text = "BUY";
		this.btnText.fontStyle = 0;
	}

	public void SetToOwned()
	{
		this.isDisabled = true;
		base.GetComponent<Image>().sprite = this.DisabledSprite;
		this.btnText.text = "OWNED";
		this.btnText.fontStyle = 2;
	}

	public void SetToDisabled()
	{
		this.isDisabled = true;
		base.GetComponent<Image>().sprite = this.DisabledSprite;
		this.btnText.fontStyle = 2;
	}

	public void InstallAni(float setTime)
	{
		this.isDisabled = true;
		base.GetComponent<Image>().sprite = this.DisabledSprite;
		this.btnText.text = "Installing...";
		this.btnText.fontStyle = 2;
		this.btnText.fontSize = 14;
		this.installIMGSize.x = base.GetComponent<RectTransform>().sizeDelta.x;
		this.installIMGSize.y = 0f;
		Sequence sequence = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), delegate()
		{
			this.installIMGSize.x = 0f;
			this.installIMGSize.y = this.InstallingIMG.GetComponent<RectTransform>().sizeDelta.y;
			this.InstallingIMG.GetComponent<RectTransform>().sizeDelta = this.installIMGSize;
		});
		TweenSettingsExtensions.Insert(sequence, 0f, TweenSettingsExtensions.SetRelative<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.InstallingIMG.GetComponent<RectTransform>().sizeDelta, delegate(Vector2 x)
		{
			this.InstallingIMG.GetComponent<RectTransform>().sizeDelta = x;
		}, this.installIMGSize, setTime), 1), true));
		TweenExtensions.Play<Sequence>(sequence);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!this.isDisabled)
		{
			GameManager.ManagerSlinger.CursorManager.PointerCursorState(true);
			base.GetComponent<Image>().sprite = this.HoverSprite;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!this.isDisabled)
		{
			GameManager.ManagerSlinger.CursorManager.PointerCursorState(false);
			base.GetComponent<Image>().sprite = this.DefaultSprite;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!this.isDisabled)
		{
			if (this.BuyItem != null)
			{
				if (this.BuyItem())
				{
					if (this.InstallItem != null)
					{
						this.InstallItem();
					}
				}
				else
				{
					if (this.CantBuy != null)
					{
						this.CantBuy();
					}
					this.SetToDisabled();
					GameManager.TimeSlinger.FireTimer(3f, new Action(this.SetToBuy), 0);
				}
			}
			GameManager.ManagerSlinger.CursorManager.PointerCursorState(false);
		}
	}

	public Text btnText;

	public Image InstallingIMG;

	public Sprite DefaultSprite;

	public Sprite HoverSprite;

	public Sprite DisabledSprite;

	private bool isDisabled;

	private Vector2 installIMGSize = Vector2.zero;

	public delegate bool BoolActions();

	public delegate void VoidActions();
}
