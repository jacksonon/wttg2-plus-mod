using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class VPNManager : MonoBehaviour
{
	public VPN_LEVELS CurrentVPN
	{
		get
		{
			return this.currentActiveVPN;
		}
	}

	public void TriggerVPNMenu()
	{
		if (!this.vpnMenuAniActive)
		{
			this.vpnMenuAniActive = true;
			if (this.vpnMenuActive)
			{
				this.vpnMenuActive = false;
				Vector2 vector;
				vector..ctor(LookUp.DesktopUI.VPN_MENU.anchoredPosition.x, LookUp.DesktopUI.VPN_MENU.sizeDelta.y);
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.DesktopUI.VPN_MENU.anchoredPosition, delegate(Vector2 x)
				{
					LookUp.DesktopUI.VPN_MENU.anchoredPosition = x;
				}, vector, 0.25f), 5), delegate()
				{
					this.vpnMenuAniActive = false;
				});
			}
			else
			{
				this.vpnMenuActive = true;
				Vector2 vector2;
				vector2..ctor(LookUp.DesktopUI.VPN_MENU.anchoredPosition.x, -41f);
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => LookUp.DesktopUI.VPN_MENU.anchoredPosition, delegate(Vector2 x)
				{
					LookUp.DesktopUI.VPN_MENU.anchoredPosition = x;
				}, vector2, 0.25f), 6), delegate()
				{
					this.vpnMenuAniActive = false;
				});
			}
		}
	}

	public void ConnectToVPN(VPN_LEVELS SetLevel)
	{
		this.currentActiveVPN = SetLevel;
	}

	public void DisconnectFromVPN()
	{
		this.currentActiveVPN = VPN_LEVELS.LEVEL0;
	}

	private void Awake()
	{
		GameManager.ManagerSlinger.VPNManager = this;
	}

	private bool vpnMenuActive;

	private bool vpnMenuAniActive;

	private VPN_LEVELS currentActiveVPN;
}
