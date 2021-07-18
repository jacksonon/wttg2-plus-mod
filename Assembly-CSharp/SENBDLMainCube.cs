using System;
using UnityEngine;

public class SENBDLMainCube : MonoBehaviour
{
	private void Start()
	{
		this.glowColors[0] = new Color(1f, 0.470588237f, 0.0509803928f);
		this.glowColors[2] = new Color(0.329411775f, 0.6392157f, 1f);
		this.glowColors[1] = new Color(0.607843161f, 1f, 0.117647059f);
		this.glowColors[3] = new Color(1f, 0.184313729f, 0f);
		this.currentColor = this.glowColors[0];
		SENBDLGlobal.sphereOfCubesRotation = Quaternion.identity;
		for (int i = 0; i < 150; i++)
		{
			Object.Instantiate<GameObject>(this.orbitingCube, Vector3.zero, Quaternion.identity);
		}
		for (int j = 0; j < 19; j++)
		{
			Object.Instantiate<GameObject>(this.glowingOrbitingCube, Vector3.zero, Quaternion.identity);
		}
		Camera.main.backgroundColor = new Color(0.08f, 0.08f, 0.08f);
		SENBDLGlobal.mainCube = this;
		this.bloomShader = Camera.main.GetComponent<SENaturalBloomAndDirtyLens>();
	}

	private void OnGUI()
	{
	}

	private void Update()
	{
		this.deltaTime = Time.deltaTime / Time.timeScale;
		this.AnimateColor();
		this.RotateSphereOfCubes();
		float num = 40f;
		Vector3 vector = Vector3.up * num;
		vector = Quaternion.Euler(Vector3.right * Time.time * num * 0.5f) * vector;
		base.transform.Rotate(vector * Time.deltaTime);
		this.IncrementCounters();
		this.GetInput();
		this.UpdateShaderValues();
		this.SmoothFPSCounter();
	}

	private void AnimateColor()
	{
		if (this.newColorCounter >= 8f)
		{
			this.newColorCounter = 0f;
			this.currentColorIndex = (this.currentColorIndex + 1) % this.glowColors.Length;
			this.previousColor = this.currentColor;
			this.currentColor = this.glowColors[this.currentColorIndex];
		}
		float num = Mathf.Clamp01(this.newColorCounter / 8f * 5f);
		this.glowColor = Color.Lerp(this.previousColor, this.currentColor, num);
		Color color = this.glowColor * Mathf.Pow(Mathf.Sin(Time.time) * 0.48f + 0.52f, 4f);
		this.cubeEmissivePart.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
		base.GetComponent<Light>().color = color;
		Color color2 = default(Color);
		color2.r = 1f - this.glowColor.r;
		color2.g = 1f - this.glowColor.g;
		color2.b = 1f - this.glowColor.b;
		color2 = Color.Lerp(color2, Color.white, 0.1f);
		this.particles.GetComponent<Renderer>().material.SetColor("_TintColor", color2);
	}

	private void RotateSphereOfCubes()
	{
		SENBDLGlobal.sphereOfCubesRotation = Quaternion.Euler(Vector3.up * Time.time * 20f);
	}

	private void IncrementCounters()
	{
		this.newColorCounter += Time.deltaTime;
	}

	private void GetInput()
	{
		if (Input.GetKey(275))
		{
			this.bloomAmount += 0.2f * this.deltaTime;
		}
		if (Input.GetKey(276))
		{
			this.bloomAmount -= 0.2f * this.deltaTime;
		}
		if (Input.GetKey(273))
		{
			this.lensDirtAmount += 0.4f * this.deltaTime;
		}
		if (Input.GetKey(274))
		{
			this.lensDirtAmount -= 0.4f * this.deltaTime;
		}
		if (Input.GetKey(46))
		{
			Time.timeScale += 0.5f * this.deltaTime;
		}
		if (Input.GetKey(44))
		{
			Time.timeScale -= 0.5f * this.deltaTime;
		}
		this.bloomAmount = Mathf.Clamp(this.bloomAmount, 0f, 0.4f);
		this.lensDirtAmount = Mathf.Clamp(this.lensDirtAmount, 0f, 0.95f);
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0.1f, 1f);
		if (Input.GetKeyDown(32))
		{
			this.bloomAmount = 0.05f;
			this.lensDirtAmount = 0.1f;
			Time.timeScale = 1f;
		}
	}

	private void UpdateShaderValues()
	{
		this.bloomShader.bloomIntensity = this.bloomAmount;
		this.bloomShader.lensDirtIntensity = this.lensDirtAmount;
	}

	private void SmoothFPSCounter()
	{
		this.fps = Mathf.Lerp(this.fps, 1f / this.deltaTime, 5f * this.deltaTime);
	}

	private Color[] glowColors = new Color[4];

	public GameObject orbitingCube;

	public GameObject glowingOrbitingCube;

	public GameObject cubeEmissivePart;

	public GameObject particles;

	private const float newColorFrequency = 8f;

	private float newColorCounter;

	private Color currentColor;

	private Color previousColor;

	[HideInInspector]
	public Color glowColor;

	private int currentColorIndex;

	private float bloomAmount = 0.04f;

	private float lensDirtAmount = 0.3f;

	private float fps;

	private float deltaTime;

	private SENaturalBloomAndDirtyLens bloomShader;
}
