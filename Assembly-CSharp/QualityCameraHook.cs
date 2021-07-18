using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class QualityCameraHook : MonoBehaviour
{
	private void Awake()
	{
		this.myPPLayer = base.GetComponent<PostProcessLayer>();
		if (this.myPPLayer != null)
		{
			switch (QualitySettings.GetQualityLevel())
			{
			case 0:
				this.myPPLayer.antialiasingMode = 0;
				break;
			case 1:
				this.myPPLayer.antialiasingMode = 1;
				break;
			case 2:
				this.myPPLayer.antialiasingMode = 1;
				break;
			case 3:
				this.myPPLayer.antialiasingMode = 3;
				break;
			case 4:
				this.myPPLayer.antialiasingMode = 3;
				break;
			case 5:
				this.myPPLayer.antialiasingMode = 3;
				break;
			case 6:
				this.myPPLayer.antialiasingMode = 3;
				break;
			default:
				this.myPPLayer.antialiasingMode = 0;
				break;
			}
		}
	}

	private PostProcessLayer myPPLayer;
}
