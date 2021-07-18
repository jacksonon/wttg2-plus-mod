using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
	[AddComponentMenu("DOTween/DOTween Animation")]
	public class DOTweenAnimation : ABSAnimationComponent
	{
		private void Awake()
		{
			if (!this.isActive || !this.isValid)
			{
				return;
			}
			if (this.animationType != 1 || !this.useTargetAsV3)
			{
				this.CreateTween();
				this._tweenCreated = true;
			}
		}

		private void Start()
		{
			if (this._tweenCreated || !this.isActive || !this.isValid)
			{
				return;
			}
			this.CreateTween();
			this._tweenCreated = true;
		}

		private void OnDestroy()
		{
			if (this.tween != null && TweenExtensions.IsActive(this.tween))
			{
				TweenExtensions.Kill(this.tween, false);
			}
			this.tween = null;
		}

		public void CreateTween()
		{
			if (this.target == null)
			{
				Debug.LogWarning(string.Format("{0} :: This tween's target is NULL, because the animation was created with a DOTween Pro version older than 0.9.255. To fix this, exit Play mode then simply select this object, and it will update automatically", base.gameObject.name), base.gameObject);
				return;
			}
			if (this.forcedTargetType != null)
			{
				this.targetType = this.forcedTargetType;
			}
			if (this.targetType == null)
			{
				this.targetType = DOTweenAnimation.TypeToDOTargetType(this.target.GetType());
			}
			switch (this.animationType)
			{
			case 1:
				if (this.useTargetAsV3)
				{
					this.isRelative = false;
					if (this.endValueTransform == null)
					{
						Debug.LogWarning(string.Format("{0} :: This tween's TO target is NULL, a Vector3 of (0,0,0) will be used instead", base.gameObject.name), base.gameObject);
						this.endValueV3 = Vector3.zero;
					}
					else if (this.targetType == 5)
					{
						RectTransform rectTransform = this.endValueTransform as RectTransform;
						if (rectTransform == null)
						{
							Debug.LogWarning(string.Format("{0} :: This tween's TO target should be a RectTransform, a Vector3 of (0,0,0) will be used instead", base.gameObject.name), base.gameObject);
							this.endValueV3 = Vector3.zero;
						}
						else
						{
							RectTransform rectTransform2 = this.target as RectTransform;
							if (rectTransform2 == null)
							{
								Debug.LogWarning(string.Format("{0} :: This tween's target and TO target are not of the same type. Please reassign the values", base.gameObject.name), base.gameObject);
							}
							else
							{
								this.endValueV3 = DOTweenUtils46.SwitchToRectTransform(rectTransform, rectTransform2);
							}
						}
					}
					else
					{
						this.endValueV3 = this.endValueTransform.position;
					}
				}
				switch (this.targetType)
				{
				case 5:
					this.tween = ShortcutExtensions46.DOAnchorPos3D((RectTransform)this.target, this.endValueV3, this.duration, this.optionalBool0);
					break;
				case 8:
					this.tween = ShortcutExtensions.DOMove((Rigidbody)this.target, this.endValueV3, this.duration, this.optionalBool0);
					break;
				case 9:
					this.tween = ShortcutExtensions43.DOMove((Rigidbody2D)this.target, this.endValueV3, this.duration, this.optionalBool0);
					break;
				case 11:
					this.tween = ShortcutExtensions.DOMove((Transform)this.target, this.endValueV3, this.duration, this.optionalBool0);
					break;
				}
				break;
			case 2:
				this.tween = ShortcutExtensions.DOLocalMove(base.transform, this.endValueV3, this.duration, this.optionalBool0);
				break;
			case 3:
			{
				TargetType targetType = this.targetType;
				if (targetType != 11)
				{
					if (targetType != 9)
					{
						if (targetType == 8)
						{
							this.tween = ShortcutExtensions.DORotate((Rigidbody)this.target, this.endValueV3, this.duration, this.optionalRotationMode);
						}
					}
					else
					{
						this.tween = ShortcutExtensions43.DORotate((Rigidbody2D)this.target, this.endValueFloat, this.duration);
					}
				}
				else
				{
					this.tween = ShortcutExtensions.DORotate((Transform)this.target, this.endValueV3, this.duration, this.optionalRotationMode);
				}
				break;
			}
			case 4:
				this.tween = ShortcutExtensions.DOLocalRotate(base.transform, this.endValueV3, this.duration, this.optionalRotationMode);
				break;
			case 5:
				this.tween = ShortcutExtensions.DOScale(base.transform, (!this.optionalBool0) ? this.endValueV3 : new Vector3(this.endValueFloat, this.endValueFloat, this.endValueFloat), this.duration);
				break;
			case 6:
				this.isRelative = false;
				switch (this.targetType)
				{
				case 3:
					this.tween = ShortcutExtensions46.DOColor((Image)this.target, this.endValueColor, this.duration);
					break;
				case 4:
					this.tween = ShortcutExtensions.DOColor((Light)this.target, this.endValueColor, this.duration);
					break;
				case 6:
					this.tween = ShortcutExtensions.DOColor(((Renderer)this.target).material, this.endValueColor, this.duration);
					break;
				case 7:
					this.tween = ShortcutExtensions43.DOColor((SpriteRenderer)this.target, this.endValueColor, this.duration);
					break;
				case 10:
					this.tween = ShortcutExtensions46.DOColor((Text)this.target, this.endValueColor, this.duration);
					break;
				}
				break;
			case 7:
				this.isRelative = false;
				switch (this.targetType)
				{
				case 2:
					this.tween = ShortcutExtensions46.DOFade((CanvasGroup)this.target, this.endValueFloat, this.duration);
					break;
				case 3:
					this.tween = ShortcutExtensions46.DOFade((Image)this.target, this.endValueFloat, this.duration);
					break;
				case 4:
					this.tween = ShortcutExtensions.DOIntensity((Light)this.target, this.endValueFloat, this.duration);
					break;
				case 6:
					this.tween = ShortcutExtensions.DOFade(((Renderer)this.target).material, this.endValueFloat, this.duration);
					break;
				case 7:
					this.tween = ShortcutExtensions43.DOFade((SpriteRenderer)this.target, this.endValueFloat, this.duration);
					break;
				case 10:
					this.tween = ShortcutExtensions46.DOFade((Text)this.target, this.endValueFloat, this.duration);
					break;
				}
				break;
			case 8:
			{
				TargetType targetType2 = this.targetType;
				if (targetType2 == 10)
				{
					this.tween = ShortcutExtensions46.DOText((Text)this.target, this.endValueString, this.duration, this.optionalBool0, this.optionalScrambleMode, this.optionalString);
				}
				break;
			}
			case 9:
			{
				TargetType targetType3 = this.targetType;
				if (targetType3 != 5)
				{
					if (targetType3 == 11)
					{
						this.tween = ShortcutExtensions.DOPunchPosition((Transform)this.target, this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0, this.optionalBool0);
					}
				}
				else
				{
					this.tween = ShortcutExtensions46.DOPunchAnchorPos((RectTransform)this.target, this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0, this.optionalBool0);
				}
				break;
			}
			case 10:
				this.tween = ShortcutExtensions.DOPunchRotation(base.transform, this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0);
				break;
			case 11:
				this.tween = ShortcutExtensions.DOPunchScale(base.transform, this.endValueV3, this.duration, this.optionalInt0, this.optionalFloat0);
				break;
			case 12:
			{
				TargetType targetType4 = this.targetType;
				if (targetType4 != 5)
				{
					if (targetType4 == 11)
					{
						this.tween = ShortcutExtensions.DOShakePosition((Transform)this.target, this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool0, true);
					}
				}
				else
				{
					this.tween = ShortcutExtensions46.DOShakeAnchorPos((RectTransform)this.target, this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, this.optionalBool0, true);
				}
				break;
			}
			case 13:
				this.tween = ShortcutExtensions.DOShakeRotation(base.transform, this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, true);
				break;
			case 14:
				this.tween = ShortcutExtensions.DOShakeScale(base.transform, this.duration, this.endValueV3, this.optionalInt0, this.optionalFloat0, true);
				break;
			case 15:
				this.tween = ShortcutExtensions.DOAspect((Camera)this.target, this.endValueFloat, this.duration);
				break;
			case 16:
				this.tween = ShortcutExtensions.DOColor((Camera)this.target, this.endValueColor, this.duration);
				break;
			case 17:
				this.tween = ShortcutExtensions.DOFieldOfView((Camera)this.target, this.endValueFloat, this.duration);
				break;
			case 18:
				this.tween = ShortcutExtensions.DOOrthoSize((Camera)this.target, this.endValueFloat, this.duration);
				break;
			case 19:
				this.tween = ShortcutExtensions.DOPixelRect((Camera)this.target, this.endValueRect, this.duration);
				break;
			case 20:
				this.tween = ShortcutExtensions.DORect((Camera)this.target, this.endValueRect, this.duration);
				break;
			case 21:
				this.tween = ShortcutExtensions46.DOSizeDelta((RectTransform)this.target, (!this.optionalBool0) ? this.endValueV2 : new Vector2(this.endValueFloat, this.endValueFloat), this.duration, false);
				break;
			}
			if (this.tween == null)
			{
				return;
			}
			if (this.isFrom)
			{
				TweenSettingsExtensions.From<Tweener>((Tweener)this.tween, this.isRelative);
			}
			else
			{
				TweenSettingsExtensions.SetRelative<Tween>(this.tween, this.isRelative);
			}
			TweenSettingsExtensions.OnKill<Tween>(TweenSettingsExtensions.SetAutoKill<Tween>(TweenSettingsExtensions.SetLoops<Tween>(TweenSettingsExtensions.SetDelay<Tween>(TweenSettingsExtensions.SetTarget<Tween>(this.tween, base.gameObject), this.delay), this.loops, this.loopType), this.autoKill), delegate()
			{
				this.tween = null;
			});
			if (this.isSpeedBased)
			{
				TweenSettingsExtensions.SetSpeedBased<Tween>(this.tween);
			}
			if (this.easeType == 37)
			{
				TweenSettingsExtensions.SetEase<Tween>(this.tween, this.easeCurve);
			}
			else
			{
				TweenSettingsExtensions.SetEase<Tween>(this.tween, this.easeType);
			}
			if (!string.IsNullOrEmpty(this.id))
			{
				TweenSettingsExtensions.SetId<Tween>(this.tween, this.id);
			}
			TweenSettingsExtensions.SetUpdate<Tween>(this.tween, this.isIndependentUpdate);
			if (this.hasOnStart)
			{
				if (this.onStart != null)
				{
					TweenSettingsExtensions.OnStart<Tween>(this.tween, new TweenCallback(this.onStart.Invoke));
				}
			}
			else
			{
				this.onStart = null;
			}
			if (this.hasOnPlay)
			{
				if (this.onPlay != null)
				{
					TweenSettingsExtensions.OnPlay<Tween>(this.tween, new TweenCallback(this.onPlay.Invoke));
				}
			}
			else
			{
				this.onPlay = null;
			}
			if (this.hasOnUpdate)
			{
				if (this.onUpdate != null)
				{
					TweenSettingsExtensions.OnUpdate<Tween>(this.tween, new TweenCallback(this.onUpdate.Invoke));
				}
			}
			else
			{
				this.onUpdate = null;
			}
			if (this.hasOnStepComplete)
			{
				if (this.onStepComplete != null)
				{
					TweenSettingsExtensions.OnStepComplete<Tween>(this.tween, new TweenCallback(this.onStepComplete.Invoke));
				}
			}
			else
			{
				this.onStepComplete = null;
			}
			if (this.hasOnComplete)
			{
				if (this.onComplete != null)
				{
					TweenSettingsExtensions.OnComplete<Tween>(this.tween, new TweenCallback(this.onComplete.Invoke));
				}
			}
			else
			{
				this.onComplete = null;
			}
			if (this.hasOnRewind)
			{
				if (this.onRewind != null)
				{
					TweenSettingsExtensions.OnRewind<Tween>(this.tween, new TweenCallback(this.onRewind.Invoke));
				}
			}
			else
			{
				this.onRewind = null;
			}
			if (this.autoPlay)
			{
				TweenExtensions.Play<Tween>(this.tween);
			}
			else
			{
				TweenExtensions.Pause<Tween>(this.tween);
			}
			if (this.hasOnTweenCreated && this.onTweenCreated != null)
			{
				this.onTweenCreated.Invoke();
			}
		}

		public override void DOPlay()
		{
			DOTween.Play(base.gameObject);
		}

		public override void DOPlayBackwards()
		{
			DOTween.PlayBackwards(base.gameObject);
		}

		public override void DOPlayForward()
		{
			DOTween.PlayForward(base.gameObject);
		}

		public override void DOPause()
		{
			DOTween.Pause(base.gameObject);
		}

		public override void DOTogglePause()
		{
			DOTween.TogglePause(base.gameObject);
		}

		public override void DORewind()
		{
			this._playCount = -1;
			DOTweenAnimation[] components = base.gameObject.GetComponents<DOTweenAnimation>();
			for (int i = components.Length - 1; i > -1; i--)
			{
				Tween tween = components[i].tween;
				if (tween != null && TweenExtensions.IsInitialized(tween))
				{
					TweenExtensions.Rewind(components[i].tween, true);
				}
			}
		}

		public override void DORestart(bool fromHere = false)
		{
			this._playCount = -1;
			if (this.tween == null)
			{
				if (Debugger.logPriority > 1)
				{
					Debugger.LogNullTween(this.tween);
				}
				return;
			}
			if (fromHere && this.isRelative)
			{
				this.ReEvaluateRelativeTween();
			}
			DOTween.Restart(base.gameObject, true, -1f);
		}

		public override void DOComplete()
		{
			DOTween.Complete(base.gameObject, false);
		}

		public override void DOKill()
		{
			DOTween.Kill(base.gameObject, false);
			this.tween = null;
		}

		public void DOPlayById(string id)
		{
			DOTween.Play(base.gameObject, id);
		}

		public void DOPlayAllById(string id)
		{
			DOTween.Play(id);
		}

		public void DOPauseAllById(string id)
		{
			DOTween.Pause(id);
		}

		public void DOPlayBackwardsById(string id)
		{
			DOTween.PlayBackwards(base.gameObject, id);
		}

		public void DOPlayBackwardsAllById(string id)
		{
			DOTween.PlayBackwards(id);
		}

		public void DOPlayForwardById(string id)
		{
			DOTween.PlayForward(base.gameObject, id);
		}

		public void DOPlayForwardAllById(string id)
		{
			DOTween.PlayForward(id);
		}

		public void DOPlayNext()
		{
			DOTweenAnimation[] components = base.GetComponents<DOTweenAnimation>();
			while (this._playCount < components.Length - 1)
			{
				this._playCount++;
				DOTweenAnimation dotweenAnimation = components[this._playCount];
				if (dotweenAnimation != null && dotweenAnimation.tween != null && !TweenExtensions.IsPlaying(dotweenAnimation.tween) && !TweenExtensions.IsComplete(dotweenAnimation.tween))
				{
					TweenExtensions.Play<Tween>(dotweenAnimation.tween);
					break;
				}
			}
		}

		public void DORewindAndPlayNext()
		{
			this._playCount = -1;
			DOTween.Rewind(base.gameObject, true);
			this.DOPlayNext();
		}

		public void DORestartById(string id)
		{
			this._playCount = -1;
			DOTween.Restart(base.gameObject, id, true, -1f);
		}

		public void DORestartAllById(string id)
		{
			this._playCount = -1;
			DOTween.Restart(id, true, -1f);
		}

		public List<Tween> GetTweens()
		{
			List<Tween> list = new List<Tween>();
			DOTweenAnimation[] components = base.GetComponents<DOTweenAnimation>();
			foreach (DOTweenAnimation dotweenAnimation in components)
			{
				list.Add(dotweenAnimation.tween);
			}
			return list;
		}

		public static TargetType TypeToDOTargetType(Type t)
		{
			string text = t.ToString();
			int num = text.LastIndexOf(".");
			if (num != -1)
			{
				text = text.Substring(num + 1);
			}
			if (text.IndexOf("Renderer") != -1 && text != "SpriteRenderer")
			{
				text = "Renderer";
			}
			return (TargetType)Enum.Parse(typeof(TargetType), text);
		}

		private void ReEvaluateRelativeTween()
		{
			if (this.animationType == 1)
			{
				((Tweener)this.tween).ChangeEndValue(base.transform.position + this.endValueV3, true);
			}
			else if (this.animationType == 2)
			{
				((Tweener)this.tween).ChangeEndValue(base.transform.localPosition + this.endValueV3, true);
			}
		}

		public float delay;

		public float duration = 1f;

		public Ease easeType = 6;

		public AnimationCurve easeCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0f),
			new Keyframe(1f, 1f)
		});

		public LoopType loopType;

		public int loops = 1;

		public string id = string.Empty;

		public bool isRelative;

		public bool isFrom;

		public bool isIndependentUpdate;

		public bool autoKill = true;

		public bool isActive = true;

		public bool isValid;

		public Component target;

		public DOTweenAnimationType animationType;

		public TargetType targetType;

		public TargetType forcedTargetType;

		public bool autoPlay = true;

		public bool useTargetAsV3;

		public float endValueFloat;

		public Vector3 endValueV3;

		public Vector2 endValueV2;

		public Color endValueColor = new Color(1f, 1f, 1f, 1f);

		public string endValueString = string.Empty;

		public Rect endValueRect = new Rect(0f, 0f, 0f, 0f);

		public Transform endValueTransform;

		public bool optionalBool0;

		public float optionalFloat0;

		public int optionalInt0;

		public RotateMode optionalRotationMode;

		public ScrambleMode optionalScrambleMode;

		public string optionalString;

		private bool _tweenCreated;

		private int _playCount = -1;
	}
}
