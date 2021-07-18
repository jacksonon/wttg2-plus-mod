using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CurrencyMenuTriggerBehaviour : MonoBehaviour
{
	public void TriggerCurrencyMenu()
	{
		if (!this.currencyMenuAniActive)
		{
			this.currencyMenuAniActive = true;
			if (this.currencyMenuActive)
			{
				this.currencyMenuActive = false;
				Vector2 vector;
				vector..ctor(LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition.x, LookUp.DesktopUI.CURRENCY_MENU.sizeDelta.y);
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition, delegate(Vector2 x)
				{
					LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition = x;
				}, vector, 0.25f), 5), delegate()
				{
					this.currencyMenuAniActive = false;
				});
			}
			else
			{
				this.currencyMenuActive = true;
				Vector2 vector2;
				vector2..ctor(LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition.x, -41f);
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition, delegate(Vector2 x)
				{
					LookUp.DesktopUI.CURRENCY_MENU.anchoredPosition = x;
				}, vector2, 0.25f), 6), delegate()
				{
					this.currencyMenuAniActive = false;
				});
			}
		}
	}

	private bool currencyMenuAniActive;

	private bool currencyMenuActive;
}
