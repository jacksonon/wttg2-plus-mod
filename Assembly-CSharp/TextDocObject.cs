﻿using System;
using TMPro;
using UnityEngine;

public class TextDocObject : MonoBehaviour
{
	public void SoftBuild()
	{
		this.myRT = base.GetComponent<RectTransform>();
		this.myBringWindowToFront = base.GetComponent<BringWindowToFrontBehaviour>();
		this.myBringWindowToFront.parentTrans = LookUp.DesktopUI.WINDOW_HOLDER;
		this.myRT.anchoredPosition = new Vector2(-this.myRT.sizeDelta.x, 0f);
	}

	public void Build(string SetTitle, string SetText)
	{
		float num = MagicSlinger.GetStringHeight(SetText, this.docFont, this.fontSize, this.myRT.sizeDelta) + 60f;
		this.myRT.sizeDelta = new Vector2(this.myRT.sizeDelta.x, num);
		float num2 = Mathf.Round(Random.Range(15f, (float)Screen.width - this.myRT.sizeDelta.x - 15f));
		float num3 = -Mathf.Round(Random.Range(56f, (float)Screen.height - 40f - this.myRT.sizeDelta.y - 15f));
		this.titleText.SetText(SetTitle);
		this.docText.text = SetText;
		this.myRT.anchoredPosition = new Vector2(num2, num3);
	}

	public void BumpMe()
	{
		this.myBringWindowToFront.forceTap();
	}

	[SerializeField]
	private TextMeshProUGUI titleText;

	[SerializeField]
	private TMP_InputField docText;

	[SerializeField]
	private Font docFont;

	[SerializeField]
	private int fontSize;

	private const float HEIGHT_BUFFER = 60f;

	private RectTransform myRT;

	private BringWindowToFrontBehaviour myBringWindowToFront;
}
