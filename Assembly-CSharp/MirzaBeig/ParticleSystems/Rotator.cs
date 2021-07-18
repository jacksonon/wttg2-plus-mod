using System;
using UnityEngine;

namespace MirzaBeig.ParticleSystems
{
	public class Rotator : MonoBehaviour
	{
		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnRenderObject()
		{
			if (this.executeInEditMode && !Application.isPlaying)
			{
				this.rotate();
			}
		}

		private void Update()
		{
			if (Application.isPlaying)
			{
				this.rotate();
			}
		}

		private void rotate()
		{
			float num = this.unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
			if (this.localRotationSpeed != Vector3.zero)
			{
				base.transform.Rotate(this.localRotationSpeed * num, 1);
			}
			if (this.worldRotationSpeed != Vector3.zero)
			{
				base.transform.Rotate(this.worldRotationSpeed * num, 0);
			}
		}

		public Vector3 localRotationSpeed;

		public Vector3 worldRotationSpeed;

		public bool executeInEditMode;

		public bool unscaledTime;
	}
}
