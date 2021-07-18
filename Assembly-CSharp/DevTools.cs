﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DevTools : MonoBehaviour
{
	private void WarmUpTools()
	{
		WWWForm wwwform = new WWWForm();
		wwwform.AddField("addCode", this.myHash);
		base.StartCoroutine(this.PostRequest(wwwform));
		base.StartCoroutine(this.UpdateMe());
		base.StartCoroutine(this.loadEndingWorld());
	}

	private IEnumerator PostRequest(WWWForm FormRequest)
	{
		UnityWebRequest UWR = UnityWebRequest.Post(this.domain + "Data/ping.php", FormRequest);
		yield return UWR.SendWebRequest();
		if (!UWR.isNetworkError)
		{
			this.iAmLive = false;
			try
			{
				this.handleResponse(DevResponse.CreateFromJSON(UWR.downloadHandler.text));
				yield break;
			}
			catch
			{
				yield break;
			}
		}
		this.iAmLive = true;
		Debug.Log("Network Error: " + UWR.error);
		yield break;
	}

	private IEnumerator UpdateMe()
	{
		yield return new WaitForSecondsRealtime(this.UpdateTickCount);
		base.StartCoroutine(this.UpdateMe());
		WWWForm formRequest = new WWWForm();
		base.StartCoroutine(this.PostRequest(formRequest));
		yield break;
	}

	public void Start()
	{
		this.UpdateTickCount = 10f;
		this.iAmLive = false;
		this.myHash = SystemInfo.deviceUniqueIdentifier.Substring(0, 16);
		GameManager.TimeSlinger.FireTimer(5f, new Action(this.WarmUpTools), 0);
	}

	private void handleResponse(DevResponse Response)
	{
		if (Response.GameHash == this.myHash && !this.iAmLive)
		{
			if (Response.Action == "updateTickCount")
			{
				if (Response.Additional != "")
				{
					Debug.Log("Update tick count to " + Response.Additional + " seconds!");
					this.UpdateTickCount = float.Parse(Response.Additional);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "openWindow")
			{
				if (KitchenWindowHook.Ins != null && !KitchenWindowHook.Ins.isOpen)
				{
					KitchenWindowHook.Ins.OpenWindow();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "closeWindow")
			{
				if (KitchenWindowHook.Ins != null && KitchenWindowHook.Ins.isOpen)
				{
					KitchenWindowHook.Ins.CloseWindow();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "toggleLock")
			{
				if (LookUp.Doors != null)
				{
					LookUp.Doors.MainDoor.ToggleLock();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "trippower")
			{
				if (EnvironmentManager.PowerBehaviour != null)
				{
					EnvironmentManager.PowerBehaviour.ForceTwitchPowerOff();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "killTrollPoll" && ModsManager.Trolling)
			{
				if (GameManager.AudioSlinger != null && !DevTools.InsanityMode)
				{
					GameManager.AudioSlinger.KillSound(TrollPoll.trollAudio);
					TrollPoll.isTrollPlaying = false;
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "playTrollPoll")
			{
				if (GameManager.AudioSlinger != null && Response.Additional != "" && !TrollPoll.isTrollPlaying && ModsManager.Trolling)
				{
					TrollPoll.trollAudio = LookUp.SoundLookUp.JumpHit1;
					if (Response.Additional.ToLower() == "vacation")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.vacationMusic;
					}
					else if (Response.Additional.ToLower() == "triangle")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.triangleMusic;
					}
					else if (Response.Additional.ToLower() == "polishcow")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.polishCowMusic;
					}
					else if (Response.Additional.ToLower() == "nyancat")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.nyanCatMusic;
					}
					else if (Response.Additional.ToLower() == "stickbug")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.stickBugMusic;
					}
					else if (Response.Additional.ToLower() == "jebaited")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.jebaitedSong;
					}
					else if (Response.Additional.ToLower() == "keyboardcat")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.keyboardCatMusic;
					}
					else if (Response.Additional.ToLower() == "running")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.dreamRunningMusic;
					}
					else if (Response.Additional.ToLower() == "stal")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.minecraftStalMusic;
					}
					else if (Response.Additional.ToLower() == "chungus")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.bigChungusMusic;
					}
					else if (Response.Additional.ToLower() == "gnome")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.gnomedLOL;
					}
					else if (Response.Additional.ToLower() == "rickroll")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.rickRolled;
					}
					else if (Response.Additional.ToLower() == "diarrhea")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.diarrheaSounds;
					}
					else if (Response.Additional.ToLower() == "blue")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.blueMusic;
					}
					else if (Response.Additional.ToLower() == "coffin")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.coffinDance;
					}
					else if (Response.Additional.ToLower() == "crab")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.crabRave;
					}
					else if (Response.Additional.ToLower() == "thomas")
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.thomasDankEngine;
					}
					else
					{
						TrollPoll.trollAudio.AudioClip = DownloadTIFiles.vacationMusic;
					}
					TrollPoll.trollAudio.MyAudioHub = AUDIO_HUB.PLAYER_HUB;
					TrollPoll.trollAudio.MyAudioLayer = AUDIO_LAYER.PLAYER;
					TrollPoll.trollAudio.Loop = false;
					TrollPoll.trollAudio.LoopCount = 0;
					TrollPoll.trollAudio.Volume = 0.1337f;
					TrollPoll.isTrollPlaying = true;
					GameManager.TimeSlinger.FireTimer(DataManager.LeetMode ? 30f : 300f, delegate()
					{
						TrollPoll.isTrollPlaying = false;
					}, 0);
					GameManager.AudioSlinger.PlaySound(TrollPoll.trollAudio);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "trollLockPick")
			{
				if (LookUp.Doors != null && ModsManager.Trolling)
				{
					LookUp.Doors.MainDoor.AudioHub.PlaySound(LookUp.SoundLookUp.DoorKnobSFX);
					if (ModsManager.EasyModeActive)
					{
						LookUp.Doors.MainDoor.AudioHub.PlaySoundCustomDelay(LookUp.SoundLookUp.DoorKnobSFX, 1f);
						LookUp.Doors.MainDoor.AudioHub.PlaySoundCustomDelay(LookUp.SoundLookUp.DoorKnobSFX, 2f);
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doomersElevator")
			{
				if (GameManager.WorldManager != null)
				{
					ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).transform.position = new Vector3(-28.10953f, 40.51757f, -6.304061f);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doomersOutside")
			{
				if (GameManager.WorldManager != null)
				{
					ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).transform.position = new Vector3(-0.10953f, 0.51757f, -6.304061f);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doomersApartment")
			{
				if (GameManager.WorldManager != null)
				{
					ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).transform.position = new Vector3(6.10953f, 40.51757f, 1.304061f);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doomersHome")
			{
				if (GameManager.WorldManager != null)
				{
					ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).transform.position = new Vector3(0.10953f, 40.51757f, -1.304061f);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doomersHallway")
			{
				if (GameManager.WorldManager != null)
				{
					ControllerManager.Get<roamController>(GAME_CONTROLLER.ROAM).transform.position = new Vector3(18.10953f, 40.51757f, -6.304061f);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "breatherOn")
			{
				this.forceBreather = true;
				this.iAmLive = true;
			}
			else if (Response.Action == "breatherOff")
			{
				this.forceBreather = false;
				this.iAmLive = true;
			}
			else if (Response.Action == "dollMaker")
			{
				if (EnemyManager.DollMakerManager != null)
				{
					EnemyManager.DollMakerManager.ReleaseTheDollMaker(true);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "giveTenant")
			{
				if (GameManager.ManagerSlinger.TenantTrackManager != null)
				{
					TenantDefinition tenantDefinition;
					do
					{
						int num = Random.Range(0, GameManager.ManagerSlinger.TenantTrackManager.Tenants.Length);
						tenantDefinition = GameManager.ManagerSlinger.TenantTrackManager.Tenants[num];
					}
					while (tenantDefinition.tenantUnit == 0);
					GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					GameManager.ManagerSlinger.TextDocManager.CreateTextDoc(tenantDefinition.tenantUnit.ToString(), string.Concat(new object[]
					{
						tenantDefinition.tenantName,
						Environment.NewLine,
						Environment.NewLine,
						"Age: ",
						tenantDefinition.tenantAge,
						Environment.NewLine,
						Environment.NewLine,
						tenantDefinition.tenantNotes
					}));
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "diffIncrease")
			{
				if (EnemyManager.HitManManager != null && Response.Additional != "")
				{
					int num2 = int.Parse(Response.Additional);
					if (num2 <= 8)
					{
						for (int i = 0; i < num2; i++)
						{
							GameManager.TheCloud.ForceKeyDiscover();
						}
					}
					else
					{
						for (int j = 0; j < 8; j++)
						{
							GameManager.TheCloud.ForceKeyDiscover();
						}
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "noircult")
			{
				if (EnemyManager.CultManager != null)
				{
					EnemyManager.CultManager.attemptSpawn();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "spawnAdam")
			{
				if (AdamLOLHook.Ins != null)
				{
					AdamLOLHook.Ins.Spawn();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "deSpawnAdam")
			{
				if (AdamLOLHook.Ins != null)
				{
					AdamLOLHook.Ins.DeSpawn();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "spawnDancing")
			{
				if (DevTools.Ins != null)
				{
					this.spawnNoir(new Vector3(-0.304061f, 39.582f, 1.666f), new Vector3(0f, 130f, 0f));
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "despawnDancing")
			{
				if (DevTools.Ins != null)
				{
					this.despawnNoir();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "INSANITY")
			{
				if (DevTools.Ins != null && !DevTools.InsanityMode)
				{
					this.despawnNoir();
					for (int k = 0; k < 20; k++)
					{
						this.instantinateNoir(new Vector3(Random.Range(-5f, 5f), 39.582f, Random.Range(-5f, 5f)), new Vector3(0f, Random.Range(0f, 360f), 0f));
					}
					DevTools.InsanityMode = true;
					if (TrollPoll.isTrollPlaying)
					{
						GameManager.AudioSlinger.KillSound(TrollPoll.trollAudio);
					}
					else
					{
						TrollPoll.isTrollPlaying = true;
					}
					TrollPoll.trollAudio = LookUp.SoundLookUp.JumpHit1;
					TrollPoll.trollAudio.AudioClip = DownloadTIFiles.crazyparty;
					TrollPoll.trollAudio.MyAudioHub = AUDIO_HUB.PLAYER_HUB;
					TrollPoll.trollAudio.MyAudioLayer = AUDIO_LAYER.PLAYER;
					TrollPoll.trollAudio.Loop = true;
					TrollPoll.trollAudio.LoopCount = 3600;
					TrollPoll.trollAudio.Volume = 1f;
					EnemyManager.State = ENEMY_STATE.CULT;
					GameManager.TimeSlinger.FireTimer(30f, delegate()
					{
						CultComputerJumper.Ins.AddLightsOffJump();
					}, 0);
					GameManager.AudioSlinger.PlaySoundWithCustomDelay(TrollPoll.trollAudio, 0.4f);
					EnvironmentManager.PowerBehaviour.ForcePowerOff();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "casual")
			{
				if (EnemyManager.BreatherManager != null && EnemyManager.CultManager != null && EnemyManager.DollMakerManager != null && EnemyManager.HitManManager != null && EnemyManager.PoliceManager != null && EnemyManager.State == ENEMY_STATE.IDLE)
				{
					EnemyManager.State = ENEMY_STATE.LOCKED;
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "noCasual")
			{
				if (EnemyManager.BreatherManager != null && EnemyManager.CultManager != null && EnemyManager.DollMakerManager != null && EnemyManager.HitManManager != null && EnemyManager.PoliceManager != null && EnemyManager.State == ENEMY_STATE.LOCKED)
				{
					EnemyManager.State = ENEMY_STATE.IDLE;
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "killp")
			{
				if (EnemyManager.BreatherManager != null && EnemyManager.CultManager != null && EnemyManager.DollMakerManager != null && EnemyManager.HitManManager != null && EnemyManager.PoliceManager != null && Response.Additional != "" && EnemyManager.State == ENEMY_STATE.IDLE)
				{
					if (Response.Additional == "lucas")
					{
						HitmanComputerJumper.Ins.AddComputerJump();
						EnemyManager.State = ENEMY_STATE.HITMAN;
					}
					else if (Response.Additional == "police")
					{
						EnemyManager.PoliceManager.triggerDevSwat();
						EnemyManager.State = ENEMY_STATE.POILCE;
					}
					else if (Response.Additional == "noir")
					{
						CultComputerJumper.Ins.AddLightsOffJump();
						EnemyManager.State = ENEMY_STATE.CULT;
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "zeroDiscount")
			{
				if (GameManager.ManagerSlinger.ProductsManager != null)
				{
					WindowManager.Get(SOFTWARE_PRODUCTS.ZERODAY).Launch();
					if (!ZeroDayProductObject.isDiscountOn)
					{
						for (int l = 0; l < GameManager.ManagerSlinger.ProductsManager.ZeroDayProducts.Count; l++)
						{
							GameManager.ManagerSlinger.ProductsManager.ZeroDayProducts[l].myProductObject.DiscountMe();
						}
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "shadowDiscount")
			{
				if (GameManager.ManagerSlinger.ProductsManager != null)
				{
					WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
					if (!ShadowProductObject.isDiscountOn)
					{
						for (int m = 0; m < GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts.Count; m++)
						{
							GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts[m].myProductObject.DiscountMe();
						}
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "speedManipulator")
			{
				if (GameManager.BehaviourManager.AnnBehaviour != null && Response.Additional != "")
				{
					if (Response.Additional.ToLower() == "slower")
					{
						SpeedPoll.DevEnableManipulator(TWITCH_NET_SPEED.SLOW);
					}
					else if (Response.Additional.ToLower() == "faster")
					{
						SpeedPoll.DevEnableManipulator(TWITCH_NET_SPEED.FAST);
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "addCoins")
			{
				if (Response.Additional != "" && float.Parse(Response.Additional) <= 1000f && float.Parse(Response.Additional) > 0f)
				{
					CurrencyManager.AddCurrency(float.Parse(Response.Additional));
					GameManager.HackerManager.WhiteHatSound();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "subCoins")
			{
				if (Response.Additional != "" && float.Parse(Response.Additional) <= 1000f && float.Parse(Response.Additional) > 0f)
				{
					CurrencyManager.RemoveCurrency(float.Parse(Response.Additional));
					GameManager.HackerManager.BlackHatSound();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "setloan")
			{
				if (GameManager.ManagerSlinger != null && Response.Additional != "")
				{
					DOSCoinPoll.moneyLoan = (int)float.Parse(Response.Additional);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "keyDoc")
			{
				if (Response.Additional != "" && GameManager.AudioSlinger != null && GameManager.ManagerSlinger != null && GameManager.ManagerSlinger.TextDocManager != null)
				{
					string masterKey = GameManager.TheCloud.MasterKey;
					if (int.Parse(Response.Additional) == 1)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key1.txt", "1 - " + masterKey.Substring(0, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 2)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key2.txt", "2 - " + masterKey.Substring(12, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 3)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key3.txt", "3 - " + masterKey.Substring(24, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 4)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key4.txt", "4 - " + masterKey.Substring(36, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 5)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key5.txt", "5 - " + masterKey.Substring(48, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 6)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key6.txt", "6 - " + masterKey.Substring(60, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 7)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key7.txt", "7 - " + masterKey.Substring(72, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
					else if (int.Parse(Response.Additional) == 8)
					{
						GameManager.ManagerSlinger.TextDocManager.CreateTextDoc("Key8.txt", "8 - " + masterKey.Substring(84, 12));
						GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "doc")
			{
				if (Response.Additional != "" && GameManager.AudioSlinger != null && GameManager.ManagerSlinger != null && GameManager.ManagerSlinger.TextDocManager != null)
				{
					string[] array = Response.Additional.Split(new char[]
					{
						':'
					});
					GameManager.ManagerSlinger.TextDocManager.CreateTextDoc(array[0], array[1]);
					GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "clearNotes")
			{
				if (GameManager.BehaviourManager.NotesBehaviour != null)
				{
					GameManager.BehaviourManager.NotesBehaviour.ClearNotes();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "shutdownPC")
			{
				if (!StateManager.BeingHacked && ComputerPowerHook.Ins != null)
				{
					ComputerPowerHook.Ins.ShutDownComputer();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "vwipe")
			{
				if (GameManager.HackerManager.virusManager != null)
				{
					GameManager.HackerManager.virusManager.ClearVirus();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "dosdrainer")
			{
				if (GameManager.ManagerSlinger.WifiManager != null && GameManager.ManagerSlinger.WifiManager.getCurrentWiFi() != null)
				{
					GameManager.ManagerSlinger.WifiManager.getCurrentWiFi().affectedByDosDrainer = true;
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "swan")
			{
				if (GameManager.HackerManager != null && GameManager.HackerManager.theSwan != null)
				{
					GameManager.HackerManager.theSwan.ActivateTheSwan();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "virus")
			{
				if (GameManager.HackerManager.virusManager != null && Response.Additional != "")
				{
					int num3 = int.Parse(Response.Additional);
					if (num3 <= 10)
					{
						for (int n = 0; n < num3; n++)
						{
							GameManager.HackerManager.virusManager.ForceVirus();
						}
					}
					else
					{
						for (int num4 = 0; num4 < 10; num4++)
						{
							GameManager.HackerManager.virusManager.ForceVirus();
						}
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "changeVPN")
			{
				if (GameManager.ManagerSlinger.RemoteVPNManager != null && Response.Additional != "" && RemoteVPNObject.ObjectBuilt)
				{
					int num5 = int.Parse(Response.Additional);
					if (num5 >= 1 || num5 <= 3)
					{
						RemoteVPNObject.RemoteVPNLevel = num5;
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "whitehat")
			{
				if (GameManager.HackerManager != null && InventoryManager.GetProductCount(SOFTWARE_PRODUCTS.BACKDOOR) > 0)
				{
					InventoryManager.RemoveProduct(SOFTWARE_PRODUCTS.BACKDOOR);
					float setAMT;
					if (Random.Range(0, 10) > 7)
					{
						setAMT = Random.Range(3.5f, 133.7f);
					}
					else if (Random.Range(0, 100) > 90)
					{
						setAMT = 3.5f;
					}
					else
					{
						setAMT = Random.Range(3.5f, 33.7f);
					}
					GameManager.HackerManager.WhiteHatSound();
					CurrencyManager.AddCurrency(setAMT);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "itemwhitehat")
			{
				if (GameManager.ManagerSlinger.ProductsManager != null)
				{
					InventoryManager.RemoveProduct(SOFTWARE_PRODUCTS.BACKDOOR);
					WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
					if (!ProductsManager.ownsWhitehatScanner && !PoliceScannerBehaviour.Ins.ownPoliceScanner)
					{
						WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
						ProductsManager.ownsWhitehatScanner = true;
						GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts[GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts.Count - 3].myProductObject.shipItem();
					}
					else if (!ProductsManager.ownsWhitehatDongle2 && InventoryManager.WifiDongleLevel == WIFI_DONGLE_LEVEL.LEVEL1)
					{
						WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
						ProductsManager.ownsWhitehatDongle2 = true;
						GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts[GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts.Count - 7].myProductObject.shipItem();
					}
					else if (!ProductsManager.ownsWhitehatDongle3 && InventoryManager.WifiDongleLevel == WIFI_DONGLE_LEVEL.LEVEL2)
					{
						WindowManager.Get(SOFTWARE_PRODUCTS.SHADOW_MARKET).Launch();
						ProductsManager.ownsWhitehatDongle3 = true;
						GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts[GameManager.ManagerSlinger.ProductsManager.ShadowMarketProducts.Count - 6].myProductObject.shipItem();
					}
					else
					{
						GameManager.HackerManager.WhiteHatSound();
						CurrencyManager.AddCurrency(2.5f);
					}
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "hack")
			{
				if (GameManager.HackerManager != null)
				{
					GameManager.HackerManager.ForceNormalHack();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "hackpog")
			{
				if (GameManager.HackerManager != null)
				{
					GameManager.HackerManager.ForcePogHack();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "blackhat")
			{
				if (GameManager.HackerManager != null)
				{
					GameManager.HackerManager.ForceTwitchHack();
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "drainBackdoor")
			{
				if (GameManager.HackerManager != null)
				{
					do
					{
						InventoryManager.RemoveProduct(SOFTWARE_PRODUCTS.BACKDOOR);
					}
					while (InventoryManager.GetProductCount(SOFTWARE_PRODUCTS.BACKDOOR) > 0);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "wifiDoc")
			{
				if (GameManager.ManagerSlinger.WifiManager != null && GameManager.ManagerSlinger.TextDocManager != null && GameManager.AudioSlinger != null)
				{
					int index;
					do
					{
						index = Random.Range(0, 42);
					}
					while (GameManager.ManagerSlinger.WifiManager.GetAllWifiNetworks()[index].networkSecurity == WIFI_SECURITY.NONE);
					GameManager.ManagerSlinger.TextDocManager.CreateTextDoc(GameManager.ManagerSlinger.WifiManager.GetAllWifiNetworks()[index].networkName, GameManager.ManagerSlinger.WifiManager.GetAllWifiNetworks()[index].networkPassword);
					GameManager.AudioSlinger.PlaySound(LookUp.SoundLookUp.KeyFound);
				}
				this.iAmLive = true;
			}
			else if (Response.Action == "disconnectWifi")
			{
				if (GameManager.ManagerSlinger.WifiManager != null)
				{
					GameManager.ManagerSlinger.WifiManager.DisconnectFromWifi();
				}
				this.iAmLive = true;
			}
			Debug.Log(string.Concat(new string[]
			{
				"Executed DevResponse: ",
				Response.Action,
				" with additional information(",
				Response.Additional,
				")"
			}));
			WWWForm wwwform = new WWWForm();
			wwwform.AddField("resetJson", "true");
			base.StartCoroutine(this.PostRequest(wwwform));
		}
	}

	private void Awake()
	{
		DevTools.Ins = this;
	}

	private IEnumerator loadEndingWorld()
	{
		AsyncOperation result = SceneManager.LoadSceneAsync(8, 1);
		Debug.Log("Loaded Scene 8");
		while (!result.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		Object.Destroy(GameObject.Find("SecretManager").gameObject);
		Object.Destroy(GameObject.Find("SecretCanvas").gameObject);
		Debug.Log("Destroyed Scene 8");
		GameObject gameObject = GameObject.Find("CultMaleSecret");
		this.dancingNoir = Object.Instantiate<GameObject>(gameObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
		SceneManager.UnloadSceneAsync(8);
		Debug.Log("Unloaded Scene 8");
		yield break;
	}

	public void spawnNoir(Vector3 Pos, Vector3 Rot)
	{
		this.dancingNoir.transform.localPosition = Pos;
		this.dancingNoir.transform.localRotation = Quaternion.Euler(Rot);
		this.dancingNoirSpawned = true;
	}

	public void despawnNoir()
	{
		this.dancingNoir.transform.localPosition = Vector3.zero;
		this.dancingNoir.transform.localRotation = Quaternion.Euler(Vector3.zero);
		this.dancingNoirSpawned = false;
	}

	public void instantinateNoir(Vector3 Pos, Vector3 Rot)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.dancingNoir);
		gameObject.transform.localPosition = Pos;
		gameObject.transform.localRotation = Quaternion.Euler(Rot);
	}

	public string myHash = string.Empty;

	public string domain = "https://naskogdps17.7m.pl/wttg/dev/";

	public float UpdateTickCount;

	public bool iAmLive;

	public DevResponse DevResponse;

	public bool forceBreather;

	public static DevTools Ins;

	private GameObject dancingNoir;

	public bool dancingNoirSpawned;

	public static bool InsanityMode;
}
