using System;
using UnityEngine;

public class FooBar : MonoBehaviour
{
	private void test()
	{
	}

	private void foo()
	{
	}

	private void Awake()
	{
	}

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
