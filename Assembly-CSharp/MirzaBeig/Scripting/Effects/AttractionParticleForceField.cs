using System;
using UnityEngine;

namespace MirzaBeig.Scripting.Effects
{
	[AddComponentMenu("Effects/Particle Force Fields/Attraction Particle Force Field")]
	public class AttractionParticleForceField : ParticleForceField
	{
		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void LateUpdate()
		{
			float x = base.transform.lossyScale.x;
			this.arrivalRadiusSqr = this.arrivalRadius * this.arrivalRadius * x;
			this.arrivedRadiusSqr = this.arrivedRadius * this.arrivedRadius * x;
			base.LateUpdate();
		}

		protected override Vector3 GetForce()
		{
			Vector3 result;
			if (this.parameters.distanceToForceFieldCenterSqr < this.arrivedRadiusSqr)
			{
				result.x = 0f;
				result.y = 0f;
				result.z = 0f;
			}
			else if (this.parameters.distanceToForceFieldCenterSqr < this.arrivalRadiusSqr)
			{
				float num = 1f - this.parameters.distanceToForceFieldCenterSqr / this.arrivalRadiusSqr;
				result = Vector3.Normalize(this.parameters.scaledDirectionToForceFieldCenter) * num;
			}
			else
			{
				result = Vector3.Normalize(this.parameters.scaledDirectionToForceFieldCenter);
			}
			return result;
		}

		protected override void OnDrawGizmosSelected()
		{
			if (base.enabled)
			{
				base.OnDrawGizmosSelected();
				float x = base.transform.lossyScale.x;
				float num = this.arrivalRadius * x;
				float num2 = this.arrivedRadius * x;
				Vector3 vector = base.transform.position + this.center;
				Gizmos.color = Color.yellow;
				Gizmos.DrawWireSphere(vector, num);
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(vector, num2);
			}
		}

		[Header("ForceField Controls")]
		[Tooltip("Tether force based on linear inverse particle distance to force field center.")]
		public float arrivalRadius = 1f;

		[Tooltip("Dead zone from force field center in which no additional force is applied.")]
		public float arrivedRadius = 0.5f;

		private float arrivalRadiusSqr;

		private float arrivedRadiusSqr;
	}
}
