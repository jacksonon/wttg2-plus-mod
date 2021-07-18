using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class NodeHexObject : MonoBehaviour
{
	public void Clear()
	{
		this.SetNonPlayable();
		if (this.CardinalConnectorsCG.alpha == 1f || this.NonCardinalConnectorsCG.alpha == 1f)
		{
			TweenExtensions.Restart(this.hideMeSeq, true, -1f);
		}
		else
		{
			TweenExtensions.Restart(this.hideMeTween, true, -1f);
		}
		this.Type = HACK_NODE_HEXER_NODE_TYPE.DEAD;
		this.myDirection = MATRIX_STACK_CLOCK_POSITION.NEUTRAL;
		this.WasConnected = false;
		this.IsReadyToConnect = false;
	}

	public void PoolBuild(NodeHexerHack SetNodeHexerHack)
	{
		this.myNodeHexerHack = SetNodeHexerHack;
	}

	public void SoftBuild(MatrixStackCord SetCord, HACK_NODE_HEXER_NODE_TYPE SetType)
	{
		this.MyCord = SetCord;
		this.Type = SetType;
		this.myDirection = MATRIX_STACK_CLOCK_POSITION.NEUTRAL;
	}

	public void ChangeType(HACK_NODE_HEXER_NODE_TYPE NewType)
	{
		this.Type = NewType;
	}

	public void Build(int MatrixSize, int MyIndex, bool NeedsToBeTagged = false)
	{
		this.myDirection = MATRIX_STACK_CLOCK_POSITION.NEUTRAL;
		this.myPOS.x = (float)this.MyCord.X * 50f;
		this.myPOS.y = -((float)this.MyCord.Y * 50f);
		if (this.MyCord.X != 0)
		{
			this.myPOS.x = this.myPOS.x + 10f * (float)this.MyCord.X;
		}
		if (this.MyCord.Y != 0)
		{
			this.myPOS.y = this.myPOS.y - 10f * (float)this.MyCord.Y;
		}
		this.MyRT.anchoredPosition = this.myPOS;
		HACK_NODE_HEXER_NODE_TYPE type = this.Type;
		if (type != HACK_NODE_HEXER_NODE_TYPE.ALPHA)
		{
			if (type == HACK_NODE_HEXER_NODE_TYPE.BETA)
			{
				this.TypeBetaCG.alpha = 0.35f;
			}
		}
		else
		{
			this.TypeAlphaCG.alpha = 0.35f;
		}
		if (NeedsToBeTagged)
		{
			this.needsToBeTagged = true;
		}
		this.presentMeDelay = (float)MyIndex * 0.2f;
		this.presentMeTimeStamp = Time.time;
		this.presentMeActivated = true;
	}

	public void ActivateNeedsToBeTagged(int index)
	{
		TweenExtensions.Restart(this.showActiveBase, true, (float)index * 0.3f);
		GameManager.TimeSlinger.FireTimer((float)index * 0.3f, delegate()
		{
			GameManager.AudioSlinger.PlaySound(this.ShowNeedsToBeTaggedSFX);
		}, 0);
	}

	public void Activate(HACK_NODE_HEXER_NODE_TYPE LastType)
	{
		if (LastType == this.Type)
		{
			this.myNodeHexerHack.ForceGameOver();
		}
		else
		{
			bool flag = true;
			if (this.needsToBeTagged)
			{
				this.needsToBeTagged = false;
				TweenExtensions.Restart(this.hideActiveBase, true, -1f);
				if (this.myNodeHexerHack.IsGameWon())
				{
					flag = false;
				}
			}
			if (flag)
			{
				if (this.myDirection == MATRIX_STACK_CLOCK_POSITION.NEUTRAL)
				{
					this.myNodeHexerHack.ForceGameOver();
				}
				else if (this.WasConnected)
				{
					this.myNodeHexerHack.ForceGameOver();
				}
				else if (this.myNodeHexerHack.CurrentMatrix.TryAndGetValueByClock(out this.connectingNode, this.MyCord, this.myDirection))
				{
					this.WasConnected = true;
					this.CardinalConnectorsCG.alpha = 1f;
					this.NonCardinalConnectorsCG.alpha = 1f;
					HACK_NODE_HEXER_NODE_TYPE type = this.Type;
					if (type != HACK_NODE_HEXER_NODE_TYPE.ALPHA)
					{
						if (type == HACK_NODE_HEXER_NODE_TYPE.BETA)
						{
							this.TypeBetaCG.alpha = 1f;
						}
					}
					else
					{
						this.TypeAlphaCG.alpha = 1f;
					}
					switch (this.myDirection)
					{
					case MATRIX_STACK_CLOCK_POSITION.NOON:
						TweenExtensions.Restart(this.hand12OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.ONE:
						TweenExtensions.Restart(this.hand1OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.THREE:
						TweenExtensions.Restart(this.hand3OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.FOUR:
						TweenExtensions.Restart(this.hand4OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.SIX:
						TweenExtensions.Restart(this.hand6OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.SEVEN:
						TweenExtensions.Restart(this.hand7OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.NINE:
						TweenExtensions.Restart(this.hand9OnTween, true, -1f);
						break;
					case MATRIX_STACK_CLOCK_POSITION.TEN:
						TweenExtensions.Restart(this.hand10OnTween, true, -1f);
						break;
					}
					GameManager.AudioSlinger.PlaySound(this.ConnectSFX);
					this.fireNextNodeTimeStamp = Time.time;
					this.fireNextNodeActive = true;
				}
				else
				{
					this.myNodeHexerHack.ForceGameOver();
				}
			}
		}
	}

	public void HoverCounterDirection(MATRIX_STACK_CLOCK_POSITION CounterPOS)
	{
		switch (CounterPOS)
		{
		case MATRIX_STACK_CLOCK_POSITION.NOON:
			this.InteractObjects[4].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.ONE:
			this.InteractObjects[5].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.THREE:
			this.InteractObjects[6].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.FOUR:
			this.InteractObjects[7].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SIX:
			this.InteractObjects[0].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SEVEN:
			this.InteractObjects[1].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.NINE:
			this.InteractObjects[2].CounterDirectionMouseOver();
			break;
		case MATRIX_STACK_CLOCK_POSITION.TEN:
			this.InteractObjects[3].CounterDirectionMouseOver();
			break;
		}
	}

	public void ExitCounterDirection(MATRIX_STACK_CLOCK_POSITION CounterPOS)
	{
		switch (CounterPOS)
		{
		case MATRIX_STACK_CLOCK_POSITION.NOON:
			this.InteractObjects[4].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.ONE:
			this.InteractObjects[5].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.THREE:
			this.InteractObjects[6].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.FOUR:
			this.InteractObjects[7].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SIX:
			this.InteractObjects[0].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SEVEN:
			this.InteractObjects[1].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.NINE:
			this.InteractObjects[2].CounterDirectionMouseOut();
			break;
		case MATRIX_STACK_CLOCK_POSITION.TEN:
			this.InteractObjects[3].CounterDirectionMouseOut();
			break;
		}
	}

	public void ActivateCounterDirection(MATRIX_STACK_CLOCK_POSITION CounterPOS)
	{
		this.SetPlayable();
		switch (CounterPOS)
		{
		case MATRIX_STACK_CLOCK_POSITION.NOON:
			this.InteractObjects[4].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.ONE:
			this.InteractObjects[5].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.THREE:
			this.InteractObjects[6].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.FOUR:
			this.InteractObjects[7].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SIX:
			this.InteractObjects[0].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.SEVEN:
			this.InteractObjects[1].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.NINE:
			this.InteractObjects[2].ActivateCounterDirection();
			break;
		case MATRIX_STACK_CLOCK_POSITION.TEN:
			this.InteractObjects[3].ActivateCounterDirection();
			break;
		}
	}

	public void DeActivateCounterDirection()
	{
		this.IsReadyToConnect = false;
		this.SetNonPlayable();
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].DeActivateCounterDirection();
			this.InteractObjects[i].ClearState();
		}
		if (this.connectingNode != null)
		{
			this.connectingNode.DeActivateCounterDirection();
			this.connectingNode = null;
		}
	}

	public void SetPlayable()
	{
		TweenExtensions.Restart(this.hideNonPlayableCG, true, -1f);
		TweenExtensions.Restart(this.showBaseCG, true, -1f);
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].Playable = true;
		}
	}

	public void SetNonPlayable()
	{
		TweenExtensions.Restart(this.hideBaseCG, true, -1f);
		TweenExtensions.Restart(this.showNonPlayableCG, true, -1f);
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].Playable = false;
		}
	}

	private bool setMyDirection(MATRIX_STACK_CLOCK_POSITION SetPOS)
	{
		NodeHexObject nodeHexObject;
		if (!this.myNodeHexerHack.CurrentMatrix.TryAndGetValueByClock(out nodeHexObject, this.MyCord, SetPOS))
		{
			return false;
		}
		if (!nodeHexObject.IsReadyToConnect)
		{
			GameManager.AudioSlinger.PlaySound(this.ClickDirectionSFX);
			this.myDirection = SetPOS;
			this.myNodeHexerHack.TotalMoves++;
			for (int i = 0; i < this.InteractObjects.Length; i++)
			{
				this.InteractObjects[i].ClearState();
			}
			if (SetPOS == MATRIX_STACK_CLOCK_POSITION.NEUTRAL)
			{
				this.connectingNode.DeActivateCounterDirection();
				this.connectingNode = null;
				this.IsReadyToConnect = false;
			}
			else
			{
				if (this.connectingNode != null)
				{
					this.connectingNode.DeActivateCounterDirection();
					this.connectingNode = null;
				}
				if (this.myNodeHexerHack.CurrentMatrix.TryAndGetValueByClock(out this.connectingNode, this.MyCord, SetPOS))
				{
					this.connectingNode.ActivateCounterDirection(SetPOS);
				}
				this.myNodeHexerHack.AddTimeBoost(this.MyCord);
				this.IsReadyToConnect = true;
			}
			return true;
		}
		return false;
	}

	private void counterNodeDirectionMouseOver(MATRIX_STACK_CLOCK_POSITION NodeDirection)
	{
		GameManager.AudioSlinger.PlaySound(this.HoverDirectionSFX);
		NodeHexObject nodeHexObject;
		if (this.myNodeHexerHack.CurrentMatrix.TryAndGetValueByClock(out nodeHexObject, this.MyCord, NodeDirection) && !nodeHexObject.IsReadyToConnect)
		{
			nodeHexObject.HoverCounterDirection(NodeDirection);
		}
	}

	private void counterNodeDirectionMouseExit(MATRIX_STACK_CLOCK_POSITION NodeDirection)
	{
		NodeHexObject nodeHexObject;
		if (this.myNodeHexerHack.CurrentMatrix.TryAndGetValueByClock(out nodeHexObject, this.MyCord, NodeDirection) && !nodeHexObject.IsReadyToConnect)
		{
			nodeHexObject.ExitCounterDirection(NodeDirection);
		}
	}

	private void finalClear()
	{
		this.TypeAlphaCG.alpha = 0f;
		this.TypeBetaCG.alpha = 0f;
		this.Hand12On.fillAmount = 0f;
		this.Hand3On.fillAmount = 0f;
		this.Hand6On.fillAmount = 0f;
		this.Hand9On.fillAmount = 0f;
		this.Hand1On.anchoredPosition = Vector2.zero;
		this.Hand4On.anchoredPosition = Vector2.zero;
		this.Hand7On.anchoredPosition = Vector2.zero;
		this.Hand10On.anchoredPosition = Vector2.zero;
		this.myPOS = Vector2.zero;
		this.MyRT.anchoredPosition = this.myPOS;
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].DeActivateCounterDirection();
			this.InteractObjects[i].ClearState();
		}
		this.needsToBeTagged = false;
		this.ActiveBaseCG.alpha = 0f;
		this.connectingNode = null;
		this.fireNextNodeActive = false;
		this.fireNextNodeTimeStamp = 0f;
		this.CardinalConnectorsCG.alpha = 0f;
		this.NonCardinalConnectorsCG.alpha = 0f;
	}

	private void Awake()
	{
		this.hand12OnTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.Hand12On.fillAmount, delegate(float x)
		{
			this.Hand12On.fillAmount = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand12OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand12OnTween, false);
		this.hand3OnTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.Hand3On.fillAmount, delegate(float x)
		{
			this.Hand3On.fillAmount = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand3OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand3OnTween, false);
		this.hand6OnTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.Hand6On.fillAmount, delegate(float x)
		{
			this.Hand6On.fillAmount = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand6OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand6OnTween, false);
		this.hand9OnTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.Hand9On.fillAmount, delegate(float x)
		{
			this.Hand9On.fillAmount = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand9OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand9OnTween, false);
		this.hand1OnTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.Hand1On.anchoredPosition, delegate(Vector2 x)
		{
			this.Hand1On.anchoredPosition = x;
		}, new Vector2(29f, 29f), 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand1OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand1OnTween, false);
		this.hand4OnTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.Hand4On.anchoredPosition, delegate(Vector2 x)
		{
			this.Hand4On.anchoredPosition = x;
		}, new Vector2(29f, -29f), 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand4OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand4OnTween, false);
		this.hand7OnTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.Hand7On.anchoredPosition, delegate(Vector2 x)
		{
			this.Hand7On.anchoredPosition = x;
		}, new Vector2(-29f, -29f), 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand7OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand7OnTween, false);
		this.hand10OnTween = TweenSettingsExtensions.SetEase<TweenerCore<Vector2, Vector2, VectorOptions>>(DOTween.To(() => this.Hand10On.anchoredPosition, delegate(Vector2 x)
		{
			this.Hand10On.anchoredPosition = x;
		}, new Vector2(-29f, 29f), 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hand10OnTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hand10OnTween, false);
		this.hideActiveBaseTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ActiveBaseCG.alpha, delegate(float x)
		{
			this.ActiveBaseCG.alpha = x;
		}, 0f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hideActiveBaseTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideActiveBaseTween, false);
		this.presentMeTween = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MyCG.alpha, delegate(float x)
		{
			this.MyCG.alpha = x;
		}, 1f, 0.25f), 1);
		TweenExtensions.Pause<Tweener>(this.presentMeTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.presentMeTween, false);
		this.hideMeTween = TweenSettingsExtensions.OnComplete<TweenerCore<float, float, FloatOptions>>(TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MyCG.alpha, delegate(float x)
		{
			this.MyCG.alpha = x;
		}, 0f, 0.25f), 1), new TweenCallback(this.finalClear));
		TweenExtensions.Pause<Tweener>(this.hideMeTween);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideMeTween, false);
		this.showBaseCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.BaseCG.alpha, delegate(float x)
		{
			this.BaseCG.alpha = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.showBaseCG);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showBaseCG, false);
		this.hideBaseCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.BaseCG.alpha, delegate(float x)
		{
			this.BaseCG.alpha = x;
		}, 0f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hideBaseCG);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideBaseCG, false);
		this.showNonPlayableCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.NotPlayableCG.alpha, delegate(float x)
		{
			this.NotPlayableCG.alpha = x;
		}, 1f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.showNonPlayableCG);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showNonPlayableCG, false);
		this.hideNonPlayableCG = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.NotPlayableCG.alpha, delegate(float x)
		{
			this.NotPlayableCG.alpha = x;
		}, 0f, 0.15f), 1);
		TweenExtensions.Pause<Tweener>(this.hideNonPlayableCG);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideNonPlayableCG, false);
		this.showActiveBase = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ActiveBaseCG.alpha, delegate(float x)
		{
			this.ActiveBaseCG.alpha = x;
		}, 1f, 0.5f), 1);
		TweenExtensions.Pause<Tweener>(this.showActiveBase);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.showActiveBase, false);
		this.hideActiveBase = TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.ActiveBaseCG.alpha, delegate(float x)
		{
			this.ActiveBaseCG.alpha = x;
		}, 0f, 0.35f), 1);
		TweenExtensions.Pause<Tweener>(this.hideActiveBase);
		TweenSettingsExtensions.SetAutoKill<Tweener>(this.hideActiveBase, false);
		this.hideMeSeq = TweenSettingsExtensions.OnComplete<Sequence>(DOTween.Sequence(), new TweenCallback(this.finalClear));
		TweenSettingsExtensions.Insert(this.hideMeSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.CardinalConnectorsCG.alpha, delegate(float x)
		{
			this.CardinalConnectorsCG.alpha = x;
		}, 0f, 0.15f), 1));
		TweenSettingsExtensions.Insert(this.hideMeSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.NonCardinalConnectorsCG.alpha, delegate(float x)
		{
			this.NonCardinalConnectorsCG.alpha = x;
		}, 0f, 0.15f), 1));
		TweenSettingsExtensions.Insert(this.hideMeSeq, 0f, TweenSettingsExtensions.SetEase<TweenerCore<float, float, FloatOptions>>(DOTween.To(() => this.MyCG.alpha, delegate(float x)
		{
			this.MyCG.alpha = x;
		}, 0f, 0.25f), 1));
		TweenExtensions.Pause<Sequence>(this.hideMeSeq);
		TweenSettingsExtensions.SetAutoKill<Sequence>(this.hideMeSeq, false);
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].SetNodeHexDirection += this.setMyDirection;
			this.InteractObjects[i].CounterDirectionMouseEnter += this.counterNodeDirectionMouseOver;
			this.InteractObjects[i].CounterDirectionMouseExit += this.counterNodeDirectionMouseExit;
		}
	}

	private void Update()
	{
		if (this.fireNextNodeActive && Time.time - this.fireNextNodeTimeStamp >= 0.15f)
		{
			this.fireNextNodeActive = false;
			if (this.connectingNode != null)
			{
				this.connectingNode.Activate(this.Type);
			}
		}
		if (this.presentMeActivated && Time.time - this.presentMeTimeStamp >= this.presentMeDelay)
		{
			this.presentMeActivated = false;
			TweenExtensions.Restart(this.presentMeTween, true, -1f);
		}
	}

	private void OnDestroy()
	{
		for (int i = 0; i < this.InteractObjects.Length; i++)
		{
			this.InteractObjects[i].SetNodeHexDirection -= this.setMyDirection;
			this.InteractObjects[i].CounterDirectionMouseEnter -= this.counterNodeDirectionMouseOver;
			this.InteractObjects[i].CounterDirectionMouseExit -= this.counterNodeDirectionMouseExit;
		}
	}

	public HACK_NODE_HEXER_NODE_TYPE Type;

	public MatrixStackCord MyCord;

	public bool WasConnected;

	public bool IsReadyToConnect;

	public CanvasGroup MyCG;

	public RectTransform MyRT;

	public CanvasGroup BaseCG;

	public CanvasGroup ActiveBaseCG;

	public CanvasGroup TypeAlphaCG;

	public CanvasGroup TypeBetaCG;

	public CanvasGroup NotPlayableCG;

	public CanvasGroup CardinalConnectorsCG;

	public CanvasGroup NonCardinalConnectorsCG;

	public Image Hand12On;

	public Image Hand3On;

	public Image Hand6On;

	public Image Hand9On;

	public RectTransform Hand1On;

	public RectTransform Hand4On;

	public RectTransform Hand7On;

	public RectTransform Hand10On;

	public NodeHexInteractObject[] InteractObjects;

	public AudioFileDefinition ShowNeedsToBeTaggedSFX;

	public AudioFileDefinition HoverDirectionSFX;

	public AudioFileDefinition ClickDirectionSFX;

	public AudioFileDefinition ConnectSFX;

	private const float FIRE_NEXT_NODE_DELAY_TIME = 0.15f;

	private const float PRESENT_ME_DELAY_TIME = 0.2f;

	private NodeHexerHack myNodeHexerHack;

	private MATRIX_STACK_CLOCK_POSITION myDirection;

	private Vector2 myPOS = Vector2.zero;

	private NodeHexObject connectingNode;

	private bool fireNextNodeActive;

	private bool needsToBeTagged;

	private bool presentMeActivated;

	private float fireNextNodeTimeStamp;

	private float presentMeTimeStamp;

	private float presentMeDelay;

	private Tweener hand12OnTween;

	private Tweener hand3OnTween;

	private Tweener hand6OnTween;

	private Tweener hand9OnTween;

	private Tweener hand1OnTween;

	private Tweener hand4OnTween;

	private Tweener hand7OnTween;

	private Tweener hand10OnTween;

	private Tweener hideActiveBaseTween;

	private Tweener presentMeTween;

	private Tweener hideMeTween;

	private Tweener hideBaseCG;

	private Tweener showBaseCG;

	private Tweener hideNonPlayableCG;

	private Tweener showNonPlayableCG;

	private Tweener showActiveBase;

	private Tweener hideActiveBase;

	private Sequence hideMeSeq;
}
