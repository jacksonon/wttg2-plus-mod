﻿using System;
using System.IO;
using UnityEngine;

public class DOSTwitch : MonoBehaviour
{
	public void Start()
	{
		this.conCount = 0;
		GameManager.SetDOSTwitch(this);
		GameManager.TimeSlinger.FireTimer(5f, new Action(this.prepDOSTwitch), 0);
	}

	public void Update()
	{
		if (this.EarlyGamePollWindowActive && Time.time - this.EarlyGamePollTimeStamp >= this.EarlyGamePollTimeWindow)
		{
			this.EarlyGamePollWindowActive = false;
			if (ModsManager.Nightmare)
			{
				this.triggerDiscountPoll();
			}
			else
			{
				this.triggerEarlyGamePoll();
			}
			this.generateVirusPollWindow();
		}
		if (this.discountPollWindowActive && Time.time - this.discountPollTimeStamp >= this.discountPollTimeWindow)
		{
			this.discountPollWindowActive = false;
			this.triggerDiscountPoll();
			this.generateVirusPollWindow();
		}
		if (this.wifiPollWindowActive && Time.time - this.wifiPollTimeStamp >= this.wifiPollTimeWindow)
		{
			this.wifiPollWindowActive = false;
			this.triggerWiFiPoll();
			this.generateSpeedPollWindow();
		}
		if (this.speedPollWindowActive && Time.time - this.speedPollTimeStamp >= this.speedPollTimeWindow)
		{
			this.speedPollWindowActive = false;
			this.triggerSpeedPoll();
			this.generateKeyPollWindow();
		}
		if (this.keyPollWindowActive && Time.time - this.keyPollTimeStamp >= this.keyPollTimeWindow)
		{
			this.keyPollWindowActive = false;
			this.triggerKeyPoll();
			this.generateTarotPollWindow();
		}
		if (this.tarotPollWindowActive && Time.time - this.tarotPollTimeStamp >= this.tarotPollTimeWindow)
		{
			this.tarotPollWindowActive = false;
			this.triggerTarotPoll();
			this.generateVirusPollWindow();
		}
		if (this.VirusPollWindowActive && Time.time - this.VirusPollTimeStamp >= this.VirusPollTimeWindow)
		{
			this.VirusPollWindowActive = false;
			this.triggerVirusPoll();
			this.generateDOSCoinPollWindow();
		}
		if (this.DOSCoinPollWindowActive && Time.time - this.DOSCoinPollTimeStamp >= this.DOSCoinPollTimeWindow)
		{
			this.DOSCoinPollWindowActive = false;
			this.triggerDOSCoinPoll();
			this.generateTrollPollWindow();
		}
		if (this.trollPollWindowActive && Time.time - this.trollPollTimeStamp >= this.trollPollTimeWindow)
		{
			this.trollPollWindowActive = false;
			this.triggerTrollPoll();
			this.generateHackerPollWindow();
		}
		if (this.hackerPollWindowActive && Time.time - this.hackerPollTimeStamp >= this.hackerPollTimeWindow)
		{
			this.hackerPollWindowActive = false;
			this.triggerHackerPoll();
			this.generateWiFiPollWindow();
		}
	}

	public DOSTwitch()
	{
		this.DOSCoinPollMinWindow = 120f;
		this.DOSCoinPollMaxWindow = 300f;
		this.EarlyGamePollMinWindow = 120f;
		this.EarlyGamePollMaxWindow = 240f;
		this.VirusPollMinWindow = 120f;
		this.VirusPollMaxWindow = 300f;
		this.hackerPollMinWindow = 120f;
		this.hackerPollMaxWindow = 360f;
		this.trollPollMinWindow = 60f;
		this.trollPollMaxWindow = 180f;
		this.discountPollMinWindow = 10f;
		this.discountPollMaxWindow = 60f;
		this.wifiPollMinWindow = 120f;
		this.wifiPollMaxWindow = 360f;
		this.speedPollMinWindow = 180f;
		this.speedPollMaxWindow = 240f;
		this.keyPollMinWindow = 120f;
		this.keyPollMaxWindow = 240f;
		this.tarotPollMinWindow = 180f;
		this.tarotPollMaxWindow = 300f;
	}

	private void prepDOSTwitch()
	{
		this.ChatDevUsername = string.Empty;
		string setOAuth = File.ReadAllText("Twitch/oauth.txt");
		string text = File.ReadAllText("Twitch/username.txt");
		this.StreamerUsername = text;
		this.myTwitchIRC = new TwitchIRC();
		this.myTwitchIRC.StartTwitch(setOAuth, text);
		GameManager.TimeSlinger.FireTimer(5f, new Action(this.warmDOSTwitch), 0);
	}

	private void checkCon()
	{
		if (this.myTwitchIRC.isConnected)
		{
			this.amConnected = true;
			this.displayTwitchConnected();
			return;
		}
		this.conCount += 1;
		if (this.conCount < 5)
		{
			GameManager.TimeSlinger.FireTimer(30f, new Action(this.checkCon), 0);
			return;
		}
		Debug.Log("Could not connect to Twitch. FeelsBadMan");
	}

	private void warmDOSTwitch()
	{
		this.mySpeedPoll = new SpeedPoll();
		this.mySpeedPoll.myDOSTwitch = this;
		this.myWiFiPoll = new WiFiPoll();
		this.myWiFiPoll.myDOSTwitch = this;
		this.myDOSCoinPoll = new DOSCoinPoll();
		this.myDOSCoinPoll.myDOSTwitch = this;
		this.myEarlyGamePoll = new EarlyGamePoll();
		this.myEarlyGamePoll.myDOSTwitch = this;
		this.myVirusPoll = new VirusPoll();
		this.myVirusPoll.myDOSTwitch = this;
		this.myHackerPoll = new HackerPoll();
		this.myHackerPoll.myDOSTwitch = this;
		this.myTrollPoll = new TrollPoll();
		this.myTrollPoll.myDOSTwitch = this;
		this.myDiscountPoll = new DiscountPoll();
		this.myDiscountPoll.myDOSTwitch = this;
		this.myKeyPoll = new KeyPoll();
		this.myKeyPoll.myDOSTwitch = this;
		this.myTarotPoll = new TarotPoll();
		this.myTarotPoll.myDOSTwitch = this;
		if (!this.myTwitchIRC.isConnected)
		{
			GameManager.TimeSlinger.FireTimer(30f, new Action(this.checkCon), 0);
			return;
		}
		this.amConnected = true;
		this.displayTwitchConnected();
		if (DataManager.LeetMode)
		{
			this.generateDiscountPollWindow();
			return;
		}
		this.generateEarlyGamePollWindow();
		this.GiveTarotCardsDelayed(200f);
	}

	private void displayTwitchConnected()
	{
		Debug.Log("Twitch Integration Now Live! FeelsGoodMan");
		this.myTwitchIRC.SendMsg("WTTG2+ Mod by nasko222 [v" + ModsManager.ModVersion + "] - Twitch Integration Live - FeelsGoodMan Clap");
		DOSTwitch.dosTwitchEnabled = true;
		Debug.Log("DOSTwitch was enabled and put in an instance.");
	}

	public void chatMessageRecv(string theMSG)
	{
		try
		{
			string[] array = theMSG.Split(new string[]
			{
				"PRIVMSG"
			}, StringSplitOptions.None);
			string[] array2 = array[0].Split(new string[]
			{
				"!"
			}, StringSplitOptions.None);
			string[] array3 = array[1].Split(new string[]
			{
				":"
			}, StringSplitOptions.None);
			string text = array2[0].Replace(":", string.Empty);
			string text2 = array3[1];
			string command = text2;
			text2 = text2.ToUpper();
			if (this.pollActive)
			{
				this.currentPollAction(text, text2);
			}
			Debug.Log(text + ": " + text2);
			text2 = text2.ToLower();
			if (text2 == "!dev")
			{
				this.myTwitchIRC.SendMsg("Chat DevTools: Please ask WTTG2+ Mod developer for full list of DEV Commands!");
			}
			if (text2.StartsWith("!dev "))
			{
				if (ModsManager.DevToolsActive)
				{
					this.ChatDeveloperSystem(text, command);
				}
				else
				{
					this.myTwitchIRC.SendMsg("DevTools are off!");
				}
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
	}

	private void generateDOSCoinPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.DOSCoinPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.DOSCoinPollTimeWindow = UnityEngine.Random.Range(this.DOSCoinPollMinWindow, this.DOSCoinPollMaxWindow);
		}
		this.DOSCoinPollTimeStamp = Time.time;
		this.DOSCoinPollWindowActive = true;
	}

	private void generateEarlyGamePollWindow()
	{
		this.EarlyGamePollTimeWindow = UnityEngine.Random.Range(this.EarlyGamePollMinWindow, this.EarlyGamePollMaxWindow);
		this.EarlyGamePollTimeStamp = Time.time;
		this.EarlyGamePollWindowActive = true;
	}

	public void setPollInactive()
	{
		this.pollActive = false;
	}

	private void triggerDOSCoinPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myDOSCoinPoll.CastVote);
			this.pollActive = true;
			this.myDOSCoinPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(28f, 69f), new Action(this.triggerDOSCoinPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(42f, 128f), new Action(this.triggerDOSCoinPoll), 0);
	}

	private void triggerEarlyGamePoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myEarlyGamePoll.CastVote);
			this.pollActive = true;
			this.myEarlyGamePoll.BeginVote();
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(42f, 128f), new Action(this.triggerEarlyGamePoll), 0);
	}

	private void generateVirusPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.VirusPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.VirusPollTimeWindow = UnityEngine.Random.Range(this.VirusPollMinWindow, this.VirusPollMaxWindow);
		}
		this.VirusPollTimeStamp = Time.time;
		this.VirusPollWindowActive = true;
	}

	private void triggerVirusPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myVirusPoll.CastVote);
			this.pollActive = true;
			this.myVirusPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerVirusPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(42f, 128f), new Action(this.triggerVirusPoll), 0);
	}

	private void generateHackerPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.hackerPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.hackerPollTimeWindow = UnityEngine.Random.Range(this.hackerPollMinWindow, this.hackerPollMaxWindow);
		}
		this.hackerPollTimeStamp = Time.time;
		this.hackerPollWindowActive = true;
	}

	private void triggerHackerPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myHackerPoll.CastVote);
			this.pollActive = true;
			this.myHackerPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerHackerPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(45f, 128f), new Action(this.triggerHackerPoll), 0);
	}

	private void generateTrollPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.trollPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.trollPollTimeWindow = UnityEngine.Random.Range(this.trollPollMinWindow, this.trollPollMaxWindow);
		}
		this.trollPollTimeStamp = Time.time;
		this.trollPollWindowActive = true;
	}

	private void triggerTrollPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myTrollPoll.CastVote);
			this.pollActive = true;
			this.myTrollPoll.BeginVote();
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(32f, 68f), new Action(this.triggerTrollPoll), 0);
	}

	private void triggerDiscountPoll()
	{
		if (this.pollActive)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(32f, 81f), new Action(this.triggerDiscountPoll), 0);
			return;
		}
		if (ModsManager.Nightmare)
		{
			this.currentPollAction = new Action<string, string>(this.myDiscountPoll.CastVoteNightmare);
			this.pollActive = true;
			this.myDiscountPoll.BeginVoteNightmare();
			return;
		}
		this.currentPollAction = new Action<string, string>(this.myDiscountPoll.CastVote);
		this.pollActive = true;
		this.myDiscountPoll.BeginVote();
	}

	private void generateDiscountPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.discountPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.discountPollTimeWindow = UnityEngine.Random.Range(this.discountPollMinWindow, this.discountPollMaxWindow);
		}
		this.discountPollTimeStamp = Time.time;
		this.discountPollWindowActive = true;
	}

	private void generateWiFiPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.wifiPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.wifiPollTimeWindow = UnityEngine.Random.Range(this.wifiPollMinWindow, this.wifiPollMaxWindow);
		}
		this.wifiPollTimeStamp = Time.time;
		this.wifiPollWindowActive = true;
	}

	private void triggerWiFiPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myWiFiPoll.CastVote);
			this.pollActive = true;
			this.myWiFiPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerWiFiPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(42f, 128f), new Action(this.triggerWiFiPoll), 0);
	}

	private void generateSpeedPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.speedPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.speedPollTimeWindow = UnityEngine.Random.Range(this.speedPollMinWindow, this.speedPollMaxWindow);
		}
		this.speedPollTimeStamp = Time.time;
		this.speedPollWindowActive = true;
	}

	private void triggerSpeedPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.mySpeedPoll.CastVote);
			this.pollActive = true;
			this.mySpeedPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerSpeedPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(49f, 101f), new Action(this.triggerSpeedPoll), 0);
	}

	public void OnDisable()
	{
		this.myTwitchIRC.SendMsg("Twitch integration unloaded successfully. Have a nice day! :)");
		this.myTwitchIRC.stopThreads = true;
		Debug.Log("Twitch integration unloaded successfully.");
	}

	private void ChatDeveloperSystem(string username, string command)
	{
		bool flag = false;
		if (username == "nasko222n" || username == "fiercethundr_" || username == "spiderhako" || username == "ampercz1" || username == "kotzwurst")
		{
			flag = true;
		}
		if (this.ChatDevUsername != string.Empty && username == this.ChatDevUsername)
		{
			flag = true;
		}
		if (username == this.StreamerUsername && !this.PunishedBefore)
		{
			this.myTwitchIRC.SendMsg("CHEATER!!! TIME FOR PUNISHMENT!!!");
			if (!GameManager.HackerManager.theSwan.isActivatedBefore)
			{
				GameManager.HackerManager.theSwan.ActivateTheSwan();
			}
			CurrencyManager.RemoveCurrency(CurrencyManager.CurrentCurrency / 2f);
			if (GameManager.ManagerSlinger.WifiManager.getCurrentWiFi() != null && !GameManager.ManagerSlinger.WifiManager.getCurrentWiFi().affectedByDosDrainer)
			{
				GameManager.ManagerSlinger.WifiManager.getCurrentWiFi().affectedByDosDrainer = true;
			}
			AdamLOLHook.Ins.Spawn();
			EnemyManager.CultManager.attemptSpawn();
			GameManager.TheCloud.ForceKeyDiscover();
			GameManager.TheCloud.ForceKeyDiscover();
			GameManager.TheCloud.ForceKeyDiscover();
			GameManager.HackerManager.virusManager.ForceVirus();
			GameManager.HackerManager.virusManager.ForceVirus();
			GameManager.HackerManager.virusManager.ForceVirus();
			GameManager.HackerManager.virusManager.ForceVirus();
			GameManager.HackerManager.virusManager.ForceVirus();
			this.PunishedBefore = true;
			return;
		}
		if (flag)
		{
			this.ExecuteDevCommand(command);
			return;
		}
		this.myTwitchIRC.SendMsg("Chat DevTools: You are not a chat developer!");
	}

	private void ExecuteDevCommand(string command)
	{
		string[] array = command.Replace("!dev ", string.Empty).Split(new char[]
		{
			' '
		});
		if (array.Length < 2)
		{
			this.myTwitchIRC.SendMsg("Chat DevTools: parameter must be greater than 0");
			return;
		}
		DevTools.DOSTwitchDevResponse(new DevResponse
		{
			Action = array[0],
			Additional = array[1],
			GameHash = DevTools.Ins.myHash
		});
		this.myTwitchIRC.SendMsg(string.Concat(new string[]
		{
			"Chat DevTools: trying to pass DevResponse with action (",
			array[0],
			"), and additional information of (",
			array[1],
			")"
		}));
	}

	private void generateKeyPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.keyPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.keyPollTimeWindow = UnityEngine.Random.Range(this.keyPollMinWindow, this.keyPollMaxWindow);
		}
		this.keyPollTimeStamp = Time.time;
		this.keyPollWindowActive = true;
	}

	private void triggerKeyPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myKeyPoll.CastVote);
			this.pollActive = true;
			this.myKeyPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerKeyPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(49f, 101f), new Action(this.triggerKeyPoll), 0);
	}

	private void GiveTarotCardsDelayed(float seconds)
	{
		GameManager.TimeSlinger.FireTimer(seconds, delegate()
		{
			if (!TarotCardsBehaviour.Owned)
			{
				TarotCardsBehaviour.Ins.MoveMe(new Vector3(1.393f, 40.68f, 2.489f), new Vector3(0f, -20f, 180f), new Vector3(0.3f, 0.3f, 0.3f));
			}
		}, 0);
	}

	private void generateTarotPollWindow()
	{
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			this.tarotPollTimeWindow = UnityEngine.Random.RandomRange(90f, 120f);
		}
		else
		{
			this.tarotPollTimeWindow = UnityEngine.Random.Range(this.tarotPollMinWindow, this.tarotPollMaxWindow);
		}
		this.tarotPollTimeStamp = Time.time;
		this.tarotPollWindowActive = true;
	}

	private void triggerTarotPoll()
	{
		if (!this.pollActive)
		{
			this.currentPollAction = new Action<string, string>(this.myTarotPoll.CastVote);
			this.pollActive = true;
			this.myTarotPoll.BeginVote();
			return;
		}
		if (DataManager.LeetMode || ModsManager.Nightmare)
		{
			GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(25f, 69f), new Action(this.triggerTarotPoll), 0);
			return;
		}
		GameManager.TimeSlinger.FireTimer(UnityEngine.Random.Range(49f, 101f), new Action(this.triggerTarotPoll), 0);
	}

	private short conCount;

	public TwitchIRC myTwitchIRC;

	private bool amConnected;

	private bool DOSCoinPollWindowActive;

	private float DOSCoinPollTimeWindow;

	private float DOSCoinPollTimeStamp;

	private float DOSCoinPollMinWindow;

	private float DOSCoinPollMaxWindow;

	private bool EarlyGamePollWindowActive;

	private float EarlyGamePollTimeWindow;

	private float EarlyGamePollTimeStamp;

	private float EarlyGamePollMinWindow;

	private float EarlyGamePollMaxWindow;

	private bool pollActive;

	private Action<string, string> currentPollAction;

	public DOSCoinPoll myDOSCoinPoll;

	private EarlyGamePoll myEarlyGamePoll;

	private float VirusPollTimeWindow;

	private float VirusPollTimeStamp;

	private bool VirusPollWindowActive;

	private float VirusPollMinWindow;

	private float VirusPollMaxWindow;

	private VirusPoll myVirusPoll;

	public float hackerPollMinWindow;

	public float hackerPollMaxWindow;

	private bool hackerPollWindowActive;

	private float hackerPollTimeWindow;

	private float hackerPollTimeStamp;

	private HackerPoll myHackerPoll;

	public float trollPollMinWindow;

	public float trollPollMaxWindow;

	private bool trollPollWindowActive;

	private float trollPollTimeWindow;

	private float trollPollTimeStamp;

	private TrollPoll myTrollPoll;

	private DiscountPoll myDiscountPoll;

	private float discountPollMinWindow;

	private float discountPollMaxWindow;

	private float discountPollTimeStamp;

	private float discountPollTimeWindow;

	private bool discountPollWindowActive;

	private bool wifiPollWindowActive;

	private float wifiPollTimeWindow;

	private float wifiPollTimeStamp;

	private float wifiPollMinWindow;

	private float wifiPollMaxWindow;

	private WiFiPoll myWiFiPoll;

	private bool speedPollWindowActive;

	private float speedPollTimeWindow;

	private float speedPollTimeStamp;

	private float speedPollMinWindow;

	private float speedPollMaxWindow;

	private SpeedPoll mySpeedPoll;

	public static bool dosTwitchEnabled;

	public string ChatDevUsername;

	private string StreamerUsername;

	private bool PunishedBefore;

	private bool keyPollWindowActive;

	private float keyPollTimeWindow;

	private float keyPollTimeStamp;

	private float keyPollMinWindow;

	private float keyPollMaxWindow;

	private KeyPoll myKeyPoll;

	private bool tarotPollWindowActive;

	private float tarotPollTimeWindow;

	private float tarotPollTimeStamp;

	private float tarotPollMinWindow;

	private float tarotPollMaxWindow;

	private TarotPoll myTarotPoll;
}
