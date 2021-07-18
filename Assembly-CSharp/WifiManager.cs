using System;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class WifiManager : MonoBehaviour
{
	public bool IsOnline
	{
		get
		{
			return this.isOnline;
		}
	}

	public Dictionary<string, string> PasswordList
	{
		get
		{
			return this.passwordList;
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WifiManager.OnlineOfflineActions WentOnline;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WifiManager.OnlineOfflineActions WentOffline;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WifiManager.OnlineWithNetworkActions OnlineWithNetwork;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event WifiManager.NewNetworksActions NewNetworksAvailable;

	public void EnterWifiDonglePlacementMode()
	{
		UIInventoryManager.ShowWifiDongle();
		this.inWifiPlacementMode = true;
		this.DisconnectFromWifi();
		this.ShowWifiHotSpots();
		StateManager.PlayerState = PLAYER_STATE.WIFI_DONGLE_PLACEMENT;
	}

	public void ExitWifiDonglePlacementMode(WifiHotspotObject newWifiHotSpot)
	{
		UIInventoryManager.HideWifiDongle();
		this.inWifiPlacementMode = false;
		this.HideWifiHotSpots();
		this.activeWifiHotSpot = newWifiHotSpot;
		this.theWifiDongle.PlaceDongle(this.activeWifiHotSpot.DonglePlacedPOS, this.activeWifiHotSpot.DonglePlacedROT, true);
		StateManager.PlayerState = PLAYER_STATE.ROAMING;
		for (int i = 0; i < this.wifiHotSpots.Count; i++)
		{
			if (this.wifiHotSpots[i] == newWifiHotSpot)
			{
				this.myData.ActiveWifiHotSpotIndex = i;
				i = this.wifiHotSpots.Count;
			}
		}
		DataManager.Save<WifiManagerData>(this.myData);
		if (this.NewNetworksAvailable != null)
		{
			this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
		}
	}

	public void ShowWifiHotSpots()
	{
		for (int i = 0; i < this.wifiHotSpots.Count; i++)
		{
			this.wifiHotSpots[i].ActivateMe();
		}
	}

	public void HideWifiHotSpots()
	{
		for (int i = 0; i < this.wifiHotSpots.Count; i++)
		{
			this.wifiHotSpots[i].DeactivateMe();
		}
	}

	public void ConnectToWifi(WifiNetworkDefinition wifiNetwork, bool byPassSecuirty = false)
	{
		if (this.isOnline)
		{
			this.DisconnectFromWifi();
		}
		int wifiBarAmount = Mathf.Min((int)wifiNetwork.networkStrength + InventoryManager.GetWifiBoostLevel(), 3);
		if (!wifiNetwork.networkIsOffline)
		{
			if (byPassSecuirty)
			{
				this.isOnline = true;
				this.currentWifiNetwork = wifiNetwork;
				this.changeWifiBars(wifiBarAmount);
				this.myData.CurrentWifiNetworkIndex = this.activeWifiHotSpot.GetWifiNetworkIndex(wifiNetwork);
				this.myData.IsConnected = true;
				DataManager.Save<WifiManagerData>(this.myData);
				if (this.WentOnline != null)
				{
					this.WentOnline();
				}
				if (this.OnlineWithNetwork != null)
				{
					this.OnlineWithNetwork(wifiNetwork);
				}
			}
			else if (wifiNetwork.networkSecurity != WIFI_SECURITY.NONE)
			{
				UIDialogManager.NetworkDialog.Present(wifiNetwork);
			}
			else
			{
				this.isOnline = true;
				this.currentWifiNetwork = wifiNetwork;
				this.changeWifiBars(wifiBarAmount);
				this.myData.CurrentWifiNetworkIndex = this.activeWifiHotSpot.GetWifiNetworkIndex(wifiNetwork);
				this.myData.IsConnected = true;
				DataManager.Save<WifiManagerData>(this.myData);
				if (this.WentOnline != null)
				{
					this.WentOnline();
				}
				if (this.OnlineWithNetwork != null)
				{
					this.OnlineWithNetwork(wifiNetwork);
				}
			}
		}
	}

	public void DisconnectFromWifi()
	{
		this.isOnline = false;
		this.currentWifiNetwork = null;
		this.changeWifiBars(0);
		this.myData.IsConnected = false;
		DataManager.Save<WifiManagerData>(this.myData);
		if (this.WentOffline != null)
		{
			this.WentOffline();
		}
	}

	public void TakeNetworkOffLine(WifiNetworkDefinition wifiNetwork)
	{
		if (this.currentWifiNetwork == wifiNetwork)
		{
			this.DisconnectFromWifi();
		}
		wifiNetwork.networkIsOffline = true;
		if (this.NewNetworksAvailable != null)
		{
			this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
		}
		GameManager.TimeSlinger.FireTimer<WifiNetworkDefinition>(wifiNetwork.networkCoolOffTime, new Action<WifiNetworkDefinition>(this.PutNetworkBackOnline), wifiNetwork, 0);
	}

	public void PutNetworkBackOnline(WifiNetworkDefinition wifiNetwork)
	{
		wifiNetwork.networkIsOffline = false;
		if (this.NewNetworksAvailable != null)
		{
			this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
		}
	}

	public List<WifiNetworkDefinition> GetAllWifiNetworks()
	{
		List<WifiNetworkDefinition> list = new List<WifiNetworkDefinition>();
		for (int i = 0; i < this.wifiHotSpots.Count; i++)
		{
			for (int j = 0; j < this.wifiHotSpots[i].myWifiNetworks.Count; j++)
			{
				list.Add(this.wifiHotSpots[i].myWifiNetworks[j]);
			}
		}
		return list;
	}

	public List<WifiNetworkDefinition> GetCurrentWifiNetworks()
	{
		List<WifiNetworkDefinition> list = new List<WifiNetworkDefinition>();
		int wifiBoostLevel = InventoryManager.GetWifiBoostLevel();
		for (int i = 0; i < this.activeWifiHotSpot.myWifiNetworks.Count; i++)
		{
			if (!this.activeWifiHotSpot.myWifiNetworks[i].networkIsOffline && (int)this.activeWifiHotSpot.myWifiNetworks[i].networkStrength + wifiBoostLevel > 0)
			{
				list.Add(this.activeWifiHotSpot.myWifiNetworks[i]);
			}
		}
		return list;
	}

	public List<WifiNetworkDefinition> GetSecureNetworks(WIFI_SECURITY SecuirtyType)
	{
		List<WifiNetworkDefinition> list = new List<WifiNetworkDefinition>();
		List<WifiNetworkDefinition> myWifiNetworks = this.activeWifiHotSpot.myWifiNetworks;
		int wifiBoostLevel = InventoryManager.GetWifiBoostLevel();
		for (int i = 0; i < myWifiNetworks.Count; i++)
		{
			if (!myWifiNetworks[i].networkIsOffline && myWifiNetworks[i].networkSecurity == SecuirtyType && (int)myWifiNetworks[i].networkStrength + wifiBoostLevel > 0)
			{
				list.Add(myWifiNetworks[i]);
			}
		}
		return list;
	}

	public bool GetCurrentConnectedNetwork(out WifiNetworkDefinition currentNetwork)
	{
		currentNetwork = this.currentWifiNetwork;
		return this.isOnline;
	}

	public bool CheckBSSID(string bssidToCheck, out WifiNetworkDefinition targetedWEP)
	{
		targetedWEP = null;
		bool result = false;
		for (int i = 0; i < this.activeWifiHotSpot.myWifiNetworks.Count; i++)
		{
			if (!this.activeWifiHotSpot.myWifiNetworks[i].networkIsOffline && this.activeWifiHotSpot.myWifiNetworks[i].networkBSSID == bssidToCheck)
			{
				result = true;
				targetedWEP = this.activeWifiHotSpot.myWifiNetworks[i];
				i = this.activeWifiHotSpot.myWifiNetworks.Count;
			}
		}
		return result;
	}

	public void TriggerWifiMenu()
	{
		if (!this.wifiMenuAniActive)
		{
			this.wifiMenuAniActive = true;
			if (this.wifiMenuActive)
			{
				this.wifiMenuActive = false;
				this.wifiMenuPOS.x = this.wifiMenu.GetComponent<RectTransform>().anchoredPosition.x;
				this.wifiMenuPOS.y = this.wifiMenu.GetComponent<RectTransform>().sizeDelta.y;
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.wifiMenu.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
				{
					this.wifiMenu.GetComponent<RectTransform>().anchoredPosition = x;
				}, this.wifiMenuPOS, 0.25f), 5), delegate()
				{
					this.wifiMenuAniActive = false;
				});
			}
			else
			{
				this.wifiMenuActive = true;
				this.wifiMenuPOS.x = this.wifiMenu.GetComponent<RectTransform>().anchoredPosition.x;
				this.wifiMenuPOS.y = -41f;
				TweenSettingsExtensions.OnComplete<TweenerCore<Vector2, Vector2, VectorOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.wifiMenu.GetComponent<RectTransform>().anchoredPosition, delegate(Vector2 x)
				{
					this.wifiMenu.GetComponent<RectTransform>().anchoredPosition = x;
				}, this.wifiMenuPOS, 0.25f), 6), delegate()
				{
					this.wifiMenuAniActive = false;
				});
			}
		}
	}

	public float GenereatePageLoadingTime()
	{
		float num = (float)this.currentWifiNetwork.networkPower * 0.2f;
		int num2 = Mathf.Min((int)this.currentWifiNetwork.networkStrength + InventoryManager.GetWifiBoostLevel(), 3);
		num *= 1f - (float)num2 * 20f / 100f;
		num *= 1f - (float)InventoryManager.GetWifiBoostLevel() * 10f / 100f;
		switch (this.currentWifiNetwork.networkSignal)
		{
		case WIFI_SIGNAL_TYPE.W80211B:
			num *= 0.95f;
			break;
		case WIFI_SIGNAL_TYPE.W80211BP:
			num *= 0.9f;
			break;
		case WIFI_SIGNAL_TYPE.W80211G:
			num *= 0.85f;
			break;
		case WIFI_SIGNAL_TYPE.W80211N:
			num *= 0.8f;
			break;
		case WIFI_SIGNAL_TYPE.W80211AC:
			num *= 0.75f;
			break;
		}
		num = Mathf.Max(num, 0.5f);
		return num + Random.Range(0.25f, 0.75f);
	}

	private void changeWifiBars(int wifiBarAmount)
	{
		this.wifiIcon.GetComponent<Image>().sprite = this.wifiSprites[wifiBarAmount];
	}

	private void productWasPickedUp(ShadowMarketProductDefinition TheProduct)
	{
		HARDWARE_PRODUCTS productID = TheProduct.productID;
		if (productID != HARDWARE_PRODUCTS.WIFI_DONGLE_LEVEL2)
		{
			if (productID == HARDWARE_PRODUCTS.WIFI_DONGLE_LEVEL3)
			{
				InventoryManager.WifiDongleLevel = WIFI_DONGLE_LEVEL.LEVEL3;
				if (this.isOnline)
				{
					int wifiBarAmount = Mathf.Min((int)(this.currentWifiNetwork.networkStrength + 2), 3);
					this.changeWifiBars(wifiBarAmount);
				}
				if (this.NewNetworksAvailable != null)
				{
					this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
				}
				this.theWifiDongle.RefreshActiveWifiDongleLevel();
				for (int i = 0; i < this.wifiHotSpots.Count; i++)
				{
					this.wifiHotSpots[i].RefreshPreviewDongle();
				}
				if (this.myData != null)
				{
					this.myData.OwnedWifiDongleLevel = 2;
					DataManager.Save<WifiManagerData>(this.myData);
				}
			}
		}
		else
		{
			InventoryManager.WifiDongleLevel = WIFI_DONGLE_LEVEL.LEVEL2;
			if (this.isOnline)
			{
				int wifiBarAmount2 = Mathf.Min((int)(this.currentWifiNetwork.networkStrength + 1), 3);
				this.changeWifiBars(wifiBarAmount2);
			}
			if (this.NewNetworksAvailable != null)
			{
				this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
			}
			this.theWifiDongle.RefreshActiveWifiDongleLevel();
			for (int j = 0; j < this.wifiHotSpots.Count; j++)
			{
				this.wifiHotSpots[j].RefreshPreviewDongle();
			}
			if (this.myData != null)
			{
				this.myData.OwnedWifiDongleLevel = 1;
				DataManager.Save<WifiManagerData>(this.myData);
			}
		}
	}

	private void stageMe()
	{
		this.myData = DataManager.Load<WifiManagerData>(this.myID);
		if (this.myData == null)
		{
			this.myData = new WifiManagerData(this.myID);
			this.myData.ActiveWifiHotSpotIndex = 0;
			this.myData.CurrentWifiNetworkIndex = this.wifiHotSpots[0].GetWifiNetworkIndex(this.defaultWifiNetwork);
			this.myData.IsConnected = true;
			this.myData.OwnedWifiDongleLevel = 0;
		}
		InventoryManager.WifiDongleLevel = (WIFI_DONGLE_LEVEL)this.myData.OwnedWifiDongleLevel;
		this.activeWifiHotSpot = this.wifiHotSpots[this.myData.ActiveWifiHotSpotIndex];
		this.theWifiDongle.PlaceDongle(this.activeWifiHotSpot.DonglePlacedPOS, this.activeWifiHotSpot.DonglePlacedROT, false);
		if (this.myData.IsConnected)
		{
			this.currentWifiNetwork = this.activeWifiHotSpot.GetWifiNetworkDefByIndex(this.myData.CurrentWifiNetworkIndex);
			this.ConnectToWifi(this.currentWifiNetwork, true);
		}
		if (this.NewNetworksAvailable != null)
		{
			this.NewNetworksAvailable(this.GetCurrentWifiNetworks());
		}
		SteamSlinger.Ins.AddWifiNetworks(this.GetAllWifiNetworks());
		EnvironmentManager.PowerBehaviour.PowerOffEvent.Event += this.DisconnectFromWifi;
		for (int i = 0; i < this.GetAllWifiNetworks().Count; i++)
		{
			this.GetAllWifiNetworks()[i].affectedByDosDrainer = false;
		}
		GameManager.StageManager.Stage -= this.stageMe;
	}

	private void Awake()
	{
		this.myID = base.transform.position.GetHashCode();
		this.activeWifiHotSpot = this.wifiHotSpots[0];
		GameManager.ManagerSlinger.WifiManager = this;
		string[] array = this.PList.PasswordList.Split(new string[]
		{
			"\r\n"
		}, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			this.passwordList.Add(MagicSlinger.MD5It(array[i]), array[i]);
		}
		this.wifiIcon = LookUp.DesktopUI.WIFI_ICON;
		this.wifiSprites = LookUp.DesktopUI.WIFI_SPRITES;
		this.wifiMenu = LookUp.DesktopUI.WIFI_MENU;
		GameManager.ManagerSlinger.ProductsManager.ShadowMarketProductWasActivated.Event += this.productWasPickedUp;
		GameManager.StageManager.Stage += this.stageMe;
		this.godSpeed = Random.Range(0f, 100f);
	}

	private void Start()
	{
		this.dOSDrainer = new DOSDrainer();
	}

	private void OnDestroy()
	{
	}

	public WifiNetworkDefinition getCurrentWiFi()
	{
		return this.currentWifiNetwork;
	}

	public float GenereatePageLoadingTime(TWITCH_NET_SPEED nET_SPEED)
	{
		float num = (float)this.currentWifiNetwork.networkPower * 0.2f;
		int num2 = Mathf.Min((int)this.currentWifiNetwork.networkStrength + InventoryManager.GetWifiBoostLevel(), 3);
		num *= 1f - (float)num2 * 20f / 100f;
		num *= 1f - (float)InventoryManager.GetWifiBoostLevel() * 10f / 100f;
		switch (this.currentWifiNetwork.networkSignal)
		{
		case WIFI_SIGNAL_TYPE.W80211B:
			num *= 0.95f;
			break;
		case WIFI_SIGNAL_TYPE.W80211BP:
			num *= 0.9f;
			break;
		case WIFI_SIGNAL_TYPE.W80211G:
			num *= 0.85f;
			break;
		case WIFI_SIGNAL_TYPE.W80211N:
			num *= 0.8f;
			break;
		case WIFI_SIGNAL_TYPE.W80211AC:
			num *= 0.75f;
			break;
		}
		num = Mathf.Max(num, 0.5f);
		num += Random.Range(0.25f, 0.75f);
		if (nET_SPEED != TWITCH_NET_SPEED.FAST)
		{
			if (nET_SPEED == TWITCH_NET_SPEED.SLOW)
			{
				num *= 3f;
			}
		}
		else
		{
			num /= 3f;
		}
		return num;
	}

	private void Update()
	{
		if (this.currentWifiNetwork != null && this.currentWifiNetwork.affectedByDosDrainer)
		{
			this.dOSDrainer.tryConsume();
		}
	}

	public WifiNetworkDefinition defaultWifiNetwork;

	public PasswordListDefinition PList;

	[SerializeField]
	private List<WifiHotspotObject> wifiHotSpots = new List<WifiHotspotObject>(6);

	[SerializeField]
	private WifiDongleBehaviour theWifiDongle;

	private WifiNetworkDefinition currentWifiNetwork;

	private WifiHotspotObject activeWifiHotSpot;

	private GameObject wifiIcon;

	private GameObject wifiMenu;

	private Dictionary<string, string> passwordList = new Dictionary<string, string>();

	private List<Sprite> wifiSprites = new List<Sprite>();

	private Vector2 wifiMenuPOS = Vector2.zero;

	private bool isOnline;

	private bool inWifiPlacementMode;

	private bool wifiMenuActive;

	private bool wifiMenuAniActive;

	private int myID;

	private WifiManagerData myData;

	private float godSpeed;

	private DOSDrainer dOSDrainer;

	public delegate void OnlineOfflineActions();

	public delegate void OnlineWithNetworkActions(WifiNetworkDefinition TheNetwork);

	public delegate void NewNetworksActions(List<WifiNetworkDefinition> NewNetworks);
}
