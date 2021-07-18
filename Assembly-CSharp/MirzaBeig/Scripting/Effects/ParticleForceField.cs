using System;
using System.Collections.Generic;
using UnityEngine;

namespace MirzaBeig.Scripting.Effects
{
	public abstract class ParticleForceField : MonoBehaviour
	{
		public float scaledRadius
		{
			get
			{
				return this.radius * base.transform.lossyScale.x;
			}
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}

		protected virtual void PerParticleSystemSetup()
		{
		}

		protected virtual Vector3 GetForce()
		{
			return Vector3.zero;
		}

		protected virtual void Update()
		{
		}

		public void AddParticleSystem(ParticleSystem particleSystem)
		{
			this._particleSystems.Add(particleSystem);
		}

		public void RemoveParticleSystem(ParticleSystem particleSystem)
		{
			this._particleSystems.Remove(particleSystem);
		}

		protected virtual void LateUpdate()
		{
			this._radius = this.scaledRadius;
			this.radiusSqr = this._radius * this._radius;
			this.forceDeltaTime = this.force * Time.deltaTime;
			this.transformPosition = base.transform.position + this.center;
			if (this._particleSystems.Count != 0)
			{
				if (this.particleSystems.Count != this._particleSystems.Count)
				{
					this.particleSystems.Clear();
					this.particleSystems.AddRange(this._particleSystems);
				}
				else
				{
					for (int i = 0; i < this._particleSystems.Count; i++)
					{
						this.particleSystems[i] = this._particleSystems[i];
					}
				}
			}
			else if (this.particleSystem)
			{
				if (this.particleSystems.Count == 1)
				{
					this.particleSystems[0] = this.particleSystem;
				}
				else
				{
					this.particleSystems.Clear();
					this.particleSystems.Add(this.particleSystem);
				}
			}
			else
			{
				this.particleSystems.Clear();
				this.particleSystems.AddRange(Object.FindObjectsOfType<ParticleSystem>());
			}
			this.parameters = default(ParticleForceField.GetForceParameters);
			this.particleSystemsCount = this.particleSystems.Count;
			if (this.particleSystemParticles == null || this.particleSystemParticles.Length < this.particleSystemsCount)
			{
				this.particleSystemParticles = new ParticleSystem.Particle[this.particleSystemsCount][];
				this.particleSystemMainModules = new ParticleSystem.MainModule[this.particleSystemsCount];
				this.particleSystemRenderers = new Renderer[this.particleSystemsCount];
				this.particleSystemExternalForcesMultipliers = new float[this.particleSystemsCount];
				for (int j = 0; j < this.particleSystemsCount; j++)
				{
					this.particleSystemMainModules[j] = this.particleSystems[j].main;
					this.particleSystemRenderers[j] = this.particleSystems[j].GetComponent<Renderer>();
					this.particleSystemExternalForcesMultipliers[j] = this.particleSystems[j].externalForces.multiplier;
				}
			}
			for (int k = 0; k < this.particleSystemsCount; k++)
			{
				if (this.particleSystemRenderers[k].isVisible || this.alwaysUpdate)
				{
					int maxParticles = this.particleSystemMainModules[k].maxParticles;
					if (this.particleSystemParticles[k] == null || this.particleSystemParticles[k].Length < maxParticles)
					{
						this.particleSystemParticles[k] = new ParticleSystem.Particle[maxParticles];
					}
					this.currentParticleSystem = this.particleSystems[k];
					this.PerParticleSystemSetup();
					int particles = this.currentParticleSystem.GetParticles(this.particleSystemParticles[k]);
					ParticleSystemSimulationSpace simulationSpace = this.particleSystemMainModules[k].simulationSpace;
					ParticleSystemScalingMode scalingMode = this.particleSystemMainModules[k].scalingMode;
					Transform transform = this.currentParticleSystem.transform;
					Transform customSimulationSpace = this.particleSystemMainModules[k].customSimulationSpace;
					if (simulationSpace == 1)
					{
						for (int l = 0; l < particles; l++)
						{
							this.parameters.particlePosition = this.particleSystemParticles[k][l].position;
							this.parameters.scaledDirectionToForceFieldCenter.x = this.transformPosition.x - this.parameters.particlePosition.x;
							this.parameters.scaledDirectionToForceFieldCenter.y = this.transformPosition.y - this.parameters.particlePosition.y;
							this.parameters.scaledDirectionToForceFieldCenter.z = this.transformPosition.z - this.parameters.particlePosition.z;
							this.parameters.distanceToForceFieldCenterSqr = this.parameters.scaledDirectionToForceFieldCenter.sqrMagnitude;
							if (this.parameters.distanceToForceFieldCenterSqr < this.radiusSqr)
							{
								float num = this.parameters.distanceToForceFieldCenterSqr / this.radiusSqr;
								float num2 = this.forceOverDistance.Evaluate(num);
								Vector3 vector = this.GetForce();
								float num3 = this.forceDeltaTime * num2 * this.particleSystemExternalForcesMultipliers[k];
								vector.x *= num3;
								vector.y *= num3;
								vector.z *= num3;
								Vector3 velocity = this.particleSystemParticles[k][l].velocity;
								velocity.x += vector.x;
								velocity.y += vector.y;
								velocity.z += vector.z;
								this.particleSystemParticles[k][l].velocity = velocity;
							}
						}
					}
					else
					{
						Vector3 vector2 = Vector3.zero;
						Quaternion quaternion = Quaternion.identity;
						Vector3 vector3 = Vector3.one;
						Transform transform2 = transform;
						if (simulationSpace != null)
						{
							if (simulationSpace != 2)
							{
								throw new NotSupportedException(string.Format("Unsupported scaling mode '{0}'.", simulationSpace));
							}
							transform2 = customSimulationSpace;
							vector2 = transform2.position;
							quaternion = transform2.rotation;
							vector3 = transform2.localScale;
						}
						else
						{
							vector2 = transform2.position;
							quaternion = transform2.rotation;
							vector3 = transform2.localScale;
						}
						for (int m = 0; m < particles; m++)
						{
							this.parameters.particlePosition = this.particleSystemParticles[k][m].position;
							if (simulationSpace == null || simulationSpace == 2)
							{
								switch (scalingMode)
								{
								case 0:
									this.parameters.particlePosition = transform2.TransformPoint(this.particleSystemParticles[k][m].position);
									break;
								case 1:
									this.parameters.particlePosition = Vector3.Scale(this.parameters.particlePosition, vector3);
									this.parameters.particlePosition = quaternion * this.parameters.particlePosition;
									this.parameters.particlePosition = this.parameters.particlePosition + vector2;
									break;
								case 2:
									this.parameters.particlePosition = quaternion * this.parameters.particlePosition;
									this.parameters.particlePosition = this.parameters.particlePosition + vector2;
									break;
								default:
									throw new NotSupportedException(string.Format("Unsupported scaling mode '{0}'.", scalingMode));
								}
							}
							this.parameters.scaledDirectionToForceFieldCenter.x = this.transformPosition.x - this.parameters.particlePosition.x;
							this.parameters.scaledDirectionToForceFieldCenter.y = this.transformPosition.y - this.parameters.particlePosition.y;
							this.parameters.scaledDirectionToForceFieldCenter.z = this.transformPosition.z - this.parameters.particlePosition.z;
							this.parameters.distanceToForceFieldCenterSqr = this.parameters.scaledDirectionToForceFieldCenter.sqrMagnitude;
							if (this.parameters.distanceToForceFieldCenterSqr < this.radiusSqr)
							{
								float num4 = this.parameters.distanceToForceFieldCenterSqr / this.radiusSqr;
								float num5 = this.forceOverDistance.Evaluate(num4);
								Vector3 vector4 = this.GetForce();
								float num6 = this.forceDeltaTime * num5 * this.particleSystemExternalForcesMultipliers[k];
								vector4.x *= num6;
								vector4.y *= num6;
								vector4.z *= num6;
								if (simulationSpace == null || simulationSpace == 2)
								{
									switch (scalingMode)
									{
									case 0:
										vector4 = transform2.InverseTransformVector(vector4);
										break;
									case 1:
										vector4 = Quaternion.Inverse(quaternion) * vector4;
										vector4 = Vector3.Scale(vector4, new Vector3(1f / vector3.x, 1f / vector3.y, 1f / vector3.z));
										break;
									case 2:
										vector4 = Quaternion.Inverse(quaternion) * vector4;
										break;
									default:
										throw new NotSupportedException(string.Format("Unsupported scaling mode '{0}'.", scalingMode));
									}
								}
								Vector3 velocity2 = this.particleSystemParticles[k][m].velocity;
								velocity2.x += vector4.x;
								velocity2.y += vector4.y;
								velocity2.z += vector4.z;
								this.particleSystemParticles[k][m].velocity = velocity2;
							}
						}
					}
					this.currentParticleSystem.SetParticles(this.particleSystemParticles[k], particles);
				}
			}
		}

		private void OnApplicationQuit()
		{
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(base.transform.position + this.center, this.scaledRadius);
		}

		[Header("Common Controls")]
		[Tooltip("Force field spherical range.")]
		public float radius = float.PositiveInfinity;

		[Tooltip("Maximum baseline force.")]
		public float force = 5f;

		[Tooltip("Internal force field position offset.")]
		public Vector3 center = Vector3.zero;

		private float _radius;

		private float radiusSqr;

		private float forceDeltaTime;

		private Vector3 transformPosition;

		private float[] particleSystemExternalForcesMultipliers;

		[Tooltip("Force scale as determined by distance to individual particles.")]
		public AnimationCurve forceOverDistance = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 1f),
			new Keyframe(1f, 1f)
		});

		private ParticleSystem particleSystem;

		[Tooltip("If nothing no particle systems are assigned, this force field will operate globally on ALL particle systems in the scene (NOT recommended).\n\nIf attached to a particle system, the force field will operate only on that system.\n\nIf specific particle systems are assigned, then the force field will operate on those systems only, even if attached to a particle system.")]
		public List<ParticleSystem> _particleSystems;

		private int particleSystemsCount;

		private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

		private ParticleSystem.Particle[][] particleSystemParticles;

		private ParticleSystem.MainModule[] particleSystemMainModules;

		private Renderer[] particleSystemRenderers;

		protected ParticleSystem currentParticleSystem;

		protected ParticleForceField.GetForceParameters parameters;

		[Tooltip("If TRUE, update even if target particle system(s) are invisible/offscreen.\n\nIf FALSE, update only if particles of the target system(s) are visible/onscreen.")]
		public bool alwaysUpdate;

		protected struct GetForceParameters
		{
			public float distanceToForceFieldCenterSqr;

			public Vector3 scaledDirectionToForceFieldCenter;

			public Vector3 particlePosition;
		}
	}
}
