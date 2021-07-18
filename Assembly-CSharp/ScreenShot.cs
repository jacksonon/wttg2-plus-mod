using System;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(115))
		{
			ScreenCapture.CaptureScreenshot("ScreenShot.png");
		}
	}
}
