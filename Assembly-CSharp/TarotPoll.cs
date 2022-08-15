using System;
using System.Collections.Generic;

public class TarotPoll
{
	public void BeginVote()
	{
		this.currentVotes = new Dictionary<string, string>();
		this.myDOSTwitch.myTwitchIRC.SendMsg("Tarot Cards Poll");
		this.myDOSTwitch.myTwitchIRC.SendMsg("Should the Tarot Cards deck get a refill?");
		this.myDOSTwitch.myTwitchIRC.SendMsg("!YES");
		this.myDOSTwitch.myTwitchIRC.SendMsg("!NO");
		this.voteIsLive = true;
		GameManager.TimeSlinger.FireTimer(60f, new Action(this.PollEnd), 0);
	}

	public void CastVote(string userName, string theVote)
	{
		if (this.voteIsLive && theVote.Contains("!"))
		{
			string text = theVote.Replace("!", string.Empty);
			if (!this.currentVotes.ContainsKey(userName) && (text == "YES" || text == "NO"))
			{
				this.currentVotes.Add(userName, text);
			}
		}
	}

	private void PollEnd()
	{
		int num = 0;
		int num2 = 0;
		bool flag = false;
		this.voteIsLive = false;
		this.myDOSTwitch.myTwitchIRC.SendMsg("The Tarot Cards Poll Has Ended!");
		this.myDOSTwitch.myTwitchIRC.SendMsg("Tallying Results!");
		foreach (KeyValuePair<string, string> keyValuePair in this.currentVotes)
		{
			if (keyValuePair.Value == "YES")
			{
				num++;
			}
			else if (keyValuePair.Value == "NO")
			{
				num2++;
			}
		}
		this.myDOSTwitch.myTwitchIRC.SendMsg("The Results Are In!");
		this.myDOSTwitch.myTwitchIRC.SendMsg("YES: " + num.ToString());
		this.myDOSTwitch.myTwitchIRC.SendMsg("NO: " + num2.ToString());
		if (num == 0 && num2 == 0)
		{
			this.myDOSTwitch.myTwitchIRC.SendMsg("No one voted, Story of my life!");
			this.myDOSTwitch.setPollInactive();
			return;
		}
		if (num > num2)
		{
			this.myDOSTwitch.myTwitchIRC.SendMsg("The Tarot Cards have been refilled!");
			TarotRefiller.RefillCards();
		}
		else if (num2 > num)
		{
			this.myDOSTwitch.myTwitchIRC.SendMsg("The Tarot Cards won't be refilled");
		}
		else
		{
			this.myDOSTwitch.myTwitchIRC.SendMsg("There is a tie! RE-VOTE!");
			GameManager.TimeSlinger.FireTimer(10f, new Action(this.BeginVote), 0);
			flag = true;
		}
		if (!flag)
		{
			this.myDOSTwitch.setPollInactive();
		}
	}

	public DOSTwitch myDOSTwitch;

	private Dictionary<string, string> currentVotes;

	private bool voteIsLive;
}
