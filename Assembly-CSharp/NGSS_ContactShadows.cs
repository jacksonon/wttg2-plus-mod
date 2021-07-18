using System;
using UnityEngine;
using UnityEngine.Rendering;

[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]
public class NGSS_ContactShadows : MonoBehaviour
{
	private Camera mCamera
	{
		get
		{
			if (this._mCamera == null)
			{
				this._mCamera = base.GetComponent<Camera>();
				if (this._mCamera == null)
				{
					this._mCamera = Camera.main;
				}
				if (this._mCamera == null)
				{
					Debug.LogError("NGSS Error: No MainCamera found, please provide one.", this);
				}
				else
				{
					this._mCamera.depthTextureMode |= 1;
				}
			}
			return this._mCamera;
		}
	}

	private Material mMaterial
	{
		get
		{
			if (this._mMaterial == null)
			{
				if (this.contactShadowsShader == null)
				{
					Shader.Find("Hidden/NGSS_ContactShadows");
				}
				this._mMaterial = new Material(this.contactShadowsShader);
				if (this._mMaterial == null)
				{
					Debug.LogWarning("NGSS Warning: can't find NGSS_ContactShadows shader, make sure it's on your project.", this);
					base.enabled = false;
					return null;
				}
			}
			return this._mMaterial;
		}
	}

	private void AddCommandBuffers()
	{
		this.computeShadowsCB = new CommandBuffer
		{
			name = "NGSS ContactShadows: Compute"
		};
		this.blendShadowsCB = new CommandBuffer
		{
			name = "NGSS ContactShadows: Mix"
		};
		bool flag = this.mCamera.actualRenderingPath == 1;
		if (this.mCamera)
		{
			foreach (CommandBuffer commandBuffer in this.mCamera.GetCommandBuffers((!flag) ? 6 : 1))
			{
				if (commandBuffer.name == this.computeShadowsCB.name)
				{
					return;
				}
			}
			this.mCamera.AddCommandBuffer((!flag) ? 6 : 1, this.computeShadowsCB);
		}
		if (this.mainDirectionalLight)
		{
			foreach (CommandBuffer commandBuffer2 in this.mainDirectionalLight.GetCommandBuffers(3))
			{
				if (commandBuffer2.name == this.blendShadowsCB.name)
				{
					return;
				}
			}
			this.mainDirectionalLight.AddCommandBuffer(3, this.blendShadowsCB);
		}
	}

	private void RemoveCommandBuffers()
	{
		this._mMaterial = null;
		bool flag = this.mCamera.actualRenderingPath == 1;
		if (this.mCamera)
		{
			this.mCamera.RemoveCommandBuffer((!flag) ? 6 : 1, this.computeShadowsCB);
		}
		if (this.mainDirectionalLight)
		{
			this.mainDirectionalLight.RemoveCommandBuffer(3, this.blendShadowsCB);
		}
		this.isInitialized = false;
	}

	private void Init()
	{
		if (this.isInitialized || this.mainDirectionalLight == null)
		{
			return;
		}
		if (this.mCamera.actualRenderingPath == null)
		{
			Debug.LogWarning("Vertex Lit Rendering Path is not supported by NGSS Contact Shadows. Please set the Rendering Path in your game camera or Graphics Settings to something else than Vertex Lit.", this);
			base.enabled = false;
			return;
		}
		this.AddCommandBuffers();
		int num = Shader.PropertyToID("NGSS_ContactShadowRT");
		int num2 = Shader.PropertyToID("NGSS_ContactShadowRT2");
		int num3 = Shader.PropertyToID("NGSS_DepthSourceRT");
		this.computeShadowsCB.GetTemporaryRT(num, -1, -1, 0, 1, 16);
		this.computeShadowsCB.GetTemporaryRT(num2, -1, -1, 0, 1, 16);
		this.computeShadowsCB.GetTemporaryRT(num3, -1, -1, 0, 0, 14);
		this.computeShadowsCB.Blit(num, num3, this.mMaterial, 0);
		this.computeShadowsCB.Blit(num3, num, this.mMaterial, 1);
		this.computeShadowsCB.SetGlobalVector("ShadowsKernel", new Vector2(0f, 1f));
		this.computeShadowsCB.Blit(num, num2, this.mMaterial, 2);
		this.computeShadowsCB.SetGlobalVector("ShadowsKernel", new Vector2(1f, 0f));
		this.computeShadowsCB.Blit(num2, num, this.mMaterial, 2);
		this.computeShadowsCB.SetGlobalVector("ShadowsKernel", new Vector2(0f, 2f));
		this.computeShadowsCB.Blit(num, num2, this.mMaterial, 2);
		this.computeShadowsCB.SetGlobalVector("ShadowsKernel", new Vector2(2f, 0f));
		this.computeShadowsCB.Blit(num2, num, this.mMaterial, 2);
		this.computeShadowsCB.SetGlobalTexture("NGSS_ContactShadowsTexture", num);
		this.blendShadowsCB.Blit(1, 1, this.mMaterial, 3);
		this.isInitialized = true;
	}

	private bool IsNotSupported()
	{
		return SystemInfo.graphicsDeviceType == 8 || SystemInfo.graphicsDeviceType == 12 || SystemInfo.graphicsDeviceType == 19;
	}

	private void OnEnable()
	{
		if (this.IsNotSupported())
		{
			Debug.LogWarning("Unsupported graphics API, NGSS requires at least SM3.0 or higher and DX9 is not supported.", this);
			base.enabled = false;
			return;
		}
		this.Init();
	}

	private void OnDisable()
	{
		if (this.isInitialized)
		{
			this.RemoveCommandBuffers();
		}
	}

	private void OnApplicationQuit()
	{
		if (this.isInitialized)
		{
			this.RemoveCommandBuffers();
		}
	}

	private void OnPreRender()
	{
		this.Init();
		if (!this.isInitialized || this.mainDirectionalLight == null)
		{
			return;
		}
		this.mMaterial.SetVector("LightDir", this.mCamera.transform.InverseTransformDirection(this.mainDirectionalLight.transform.forward));
		this.mMaterial.SetFloat("ShadowsOpacity", 1f - this.mainDirectionalLight.shadowStrength);
		this.mMaterial.SetFloat("ShadowsEdgeTolerance", this.m_shadowsEdgeTolerance * 0.075f);
		this.mMaterial.SetFloat("ShadowsSoftness", this.m_shadowsSoftness * 4f);
		this.mMaterial.SetFloat("ShadowsDistance", this.m_shadowsDistance);
		this.mMaterial.SetFloat("ShadowsFade", this.m_shadowsFade);
		this.mMaterial.SetFloat("ShadowsBias", this.m_shadowsOffset * 0.02f);
		this.mMaterial.SetFloat("RayWidth", this.m_rayWidth);
		this.mMaterial.SetFloat("RaySamples", (float)this.m_raySamples);
		this.mMaterial.SetFloat("RaySamplesScale", this.m_raySamplesScale);
		if (this.m_noiseFilter)
		{
			this.mMaterial.EnableKeyword("NGSS_CONTACT_SHADOWS_USE_NOISE");
		}
		else
		{
			this.mMaterial.DisableKeyword("NGSS_CONTACT_SHADOWS_USE_NOISE");
		}
	}

	public Light mainDirectionalLight;

	public Shader contactShadowsShader;

	[Header("SHADOWS SETTINGS")]
	[Tooltip("Poisson Noise. Randomize samples to remove repeated patterns.")]
	public bool m_noiseFilter;

	[Tooltip("Tweak this value to remove soft-shadows leaking around edges.")]
	[Range(0.01f, 1f)]
	public float m_shadowsEdgeTolerance = 0.25f;

	[Tooltip("Overall softness of the shadows.")]
	[Range(0.01f, 1f)]
	public float m_shadowsSoftness = 0.25f;

	[Tooltip("Overall distance of the shadows.")]
	[Range(1f, 4f)]
	public float m_shadowsDistance = 1f;

	[Tooltip("The distance where shadows start to fade.")]
	[Range(0.1f, 4f)]
	public float m_shadowsFade = 1f;

	[Tooltip("Tweak this value if your objects display backface shadows.")]
	[Range(0f, 2f)]
	public float m_shadowsOffset = 0.325f;

	[Header("RAY SETTINGS")]
	[Tooltip("The higher the value, the ticker the shadows will look.")]
	[Range(0f, 1f)]
	public float m_rayWidth = 0.1f;

	[Tooltip("Number of samplers between each step. The higher values produces less gaps between shadows. Keep this value as low as you can!")]
	[Range(16f, 128f)]
	public int m_raySamples = 64;

	[Tooltip("Samplers scale over distance. Lower this value if you want to speed things up by doing less sampling on far away objects.")]
	[Range(0f, 1f)]
	public float m_raySamplesScale = 1f;

	private CommandBuffer blendShadowsCB;

	private CommandBuffer computeShadowsCB;

	private bool isInitialized;

	private Camera _mCamera;

	private Material _mMaterial;
}
