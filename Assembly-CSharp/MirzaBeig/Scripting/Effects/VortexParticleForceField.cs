using System;
using UnityEngine;

namespace MirzaBeig.Scripting.Effects
{
	[AddComponentMenu("Effects/Particle Force Fields/Vortex Particle Force Field")]
	public class VortexParticleForceField : ParticleForceField
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
			base.LateUpdate();
		}

		private void UpdateAxisOfRotation()
		{
			this.axisOfRotation = Quaternion.Euler(this.axisOfRotationOffset) * base.transform.up;
		}

		protected override void PerParticleSystemSetup()
		{
			this.UpdateAxisOfRotation();
		}

		protected override Vector3 GetForce()
		{
			return Vector3.Normalize(Vector3.Cross(this.axisOfRotation, this.parameters.scaledDirectionToForceFieldCenter));
		}

		protected override void OnDrawGizmosSelected()
		{
			if (base.enabled)
			{
				base.OnDrawGizmosSelected();
				Gizmos.color = Color.red;
				Vector3 vector;
				if (Application.isPlaying && base.enabled)
				{
					this.UpdateAxisOfRotation();
					vector = this.axisOfRotation;
				}
				else
				{
					vector = Quaternion.Euler(this.axisOfRotationOffset) * base.transform.up;
				}
				Vector3 vector2 = base.transform.position + this.center;
				Gizmos.DrawLine(vector2, vector2 + vector * base.scaledRadius);
			}
		}

		private Vector3 axisOfRotation;

		[Header("ForceField Controls")]
		[Tooltip("Internal offset for the axis of rotation.\n\nUseful if the force field and particle system are on the same game object, and you need a seperate rotation for the system, and the affector, but don't want to make the two different game objects.")]
		public Vector3 axisOfRotationOffset = Vector3.zero;
	}
}
