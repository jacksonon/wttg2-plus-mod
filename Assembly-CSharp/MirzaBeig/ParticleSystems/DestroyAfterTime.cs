using System;
using UnityEngine;

namespace MirzaBeig.ParticleSystems
{
	public class DestroyAfterTime : MonoBehaviour
	{
		private void Start()
		{
			Object.Destroy(base.gameObject, this.time);
		}

		public float time = 2f;
	}
}
