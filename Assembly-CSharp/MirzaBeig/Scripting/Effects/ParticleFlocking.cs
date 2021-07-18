using System;
using UnityEngine;

namespace MirzaBeig.Scripting.Effects
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleFlocking : MonoBehaviour
	{
		private void Start()
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
			this.particleSystemMainModule = this.particleSystem.main;
		}

		private void OnBecameVisible()
		{
			this.visible = true;
		}

		private void OnBecameInvisible()
		{
			this.visible = false;
		}

		private void buildVoxelGrid()
		{
			int num = this.voxelsPerAxis * this.voxelsPerAxis * this.voxelsPerAxis;
			this.voxels = new ParticleFlocking.Voxel[num];
			float num2 = this.voxelVolume / (float)this.voxelsPerAxis;
			float num3 = num2 / 2f;
			float num4 = this.voxelVolume / 2f;
			Vector3 position = base.transform.position;
			int num5 = 0;
			for (int i = 0; i < this.voxelsPerAxis; i++)
			{
				float num6 = -num4 + num3 + (float)i * num2;
				for (int j = 0; j < this.voxelsPerAxis; j++)
				{
					float num7 = -num4 + num3 + (float)j * num2;
					for (int k = 0; k < this.voxelsPerAxis; k++)
					{
						float num8 = -num4 + num3 + (float)k * num2;
						this.voxels[num5].particleCount = 0;
						this.voxels[num5].bounds = new Bounds(position + new Vector3(num6, num7, num8), Vector3.one * num2);
						num5++;
					}
				}
			}
		}

		private void LateUpdate()
		{
			if (this.alwaysUpdate || this.visible)
			{
				if (this.useVoxels)
				{
					int num = this.voxelsPerAxis * this.voxelsPerAxis * this.voxelsPerAxis;
					if (this.voxels == null || this.voxels.Length < num)
					{
						this.buildVoxelGrid();
					}
				}
				int maxParticles = this.particleSystemMainModule.maxParticles;
				if (this.particles == null || this.particles.Length < maxParticles)
				{
					this.particles = new ParticleSystem.Particle[maxParticles];
					this.particlePositions = new Vector3[maxParticles];
					if (this.useVoxels)
					{
						for (int i = 0; i < this.voxels.Length; i++)
						{
							this.voxels[i].particles = new int[maxParticles];
						}
					}
				}
				this.timer += Time.deltaTime;
				if (this.timer >= this.delay)
				{
					float num2 = this.timer;
					this.timer = 0f;
					this.particleSystem.GetParticles(this.particles);
					int particleCount = this.particleSystem.particleCount;
					float num3 = this.cohesion * num2;
					float num4 = this.separation * num2;
					for (int j = 0; j < particleCount; j++)
					{
						this.particlePositions[j] = this.particles[j].position;
					}
					if (this.useVoxels)
					{
						int num5 = this.voxels.Length;
						float num6 = this.voxelVolume / (float)this.voxelsPerAxis;
						for (int k = 0; k < particleCount; k++)
						{
							for (int l = 0; l < num5; l++)
							{
								if (this.voxels[l].bounds.Contains(this.particlePositions[k]))
								{
									this.voxels[l].particles[this.voxels[l].particleCount] = k;
									ParticleFlocking.Voxel[] array = this.voxels;
									int num7 = l;
									array[num7].particleCount = array[num7].particleCount + 1;
									break;
								}
							}
						}
						for (int m = 0; m < num5; m++)
						{
							if (this.voxels[m].particleCount > 1)
							{
								for (int n = 0; n < this.voxels[m].particleCount; n++)
								{
									Vector3 vector = this.particlePositions[this.voxels[m].particles[n]];
									Vector3 vector2;
									if (this.voxelLocalCenterFromBounds)
									{
										vector2 = this.voxels[m].bounds.center - this.particlePositions[this.voxels[m].particles[n]];
									}
									else
									{
										for (int num8 = 0; num8 < this.voxels[m].particleCount; num8++)
										{
											if (num8 != n)
											{
												vector += this.particlePositions[this.voxels[m].particles[num8]];
											}
										}
										vector /= (float)this.voxels[m].particleCount;
										vector2 = vector - this.particlePositions[this.voxels[m].particles[n]];
									}
									float sqrMagnitude = vector2.sqrMagnitude;
									vector2.Normalize();
									Vector3 vector3 = Vector3.zero;
									vector3 += vector2 * num3;
									vector3 -= vector2 * ((1f - sqrMagnitude / num6) * num4);
									Vector3 velocity = this.particles[this.voxels[m].particles[n]].velocity;
									velocity.x += vector3.x;
									velocity.y += vector3.y;
									velocity.z += vector3.z;
									this.particles[this.voxels[m].particles[n]].velocity = velocity;
								}
								this.voxels[m].particleCount = 0;
							}
						}
					}
					else
					{
						float num9 = this.maxDistance * this.maxDistance;
						for (int num10 = 0; num10 < particleCount; num10++)
						{
							int num11 = 1;
							Vector3 vector4 = this.particlePositions[num10];
							for (int num12 = 0; num12 < particleCount; num12++)
							{
								if (num12 != num10)
								{
									Vector3 vector5;
									vector5.x = this.particlePositions[num10].x - this.particlePositions[num12].x;
									vector5.y = this.particlePositions[num10].y - this.particlePositions[num12].y;
									vector5.z = this.particlePositions[num10].z - this.particlePositions[num12].z;
									float num13 = Vector3.SqrMagnitude(vector5);
									if (num13 <= num9)
									{
										num11++;
										vector4 += this.particlePositions[num12];
									}
								}
							}
							if (num11 != 1)
							{
								vector4 /= (float)num11;
								Vector3 vector6 = vector4 - this.particlePositions[num10];
								float sqrMagnitude2 = vector6.sqrMagnitude;
								vector6.Normalize();
								Vector3 vector7 = Vector3.zero;
								vector7 += vector6 * num3;
								vector7 -= vector6 * ((1f - sqrMagnitude2 / num9) * num4);
								Vector3 velocity2 = this.particles[num10].velocity;
								velocity2.x += vector7.x;
								velocity2.y += vector7.y;
								velocity2.z += vector7.z;
								this.particles[num10].velocity = velocity2;
							}
						}
					}
					this.particleSystem.SetParticles(this.particles, particleCount);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			float num = this.voxelVolume / (float)this.voxelsPerAxis;
			float num2 = num / 2f;
			float num3 = this.voxelVolume / 2f;
			Vector3 position = base.transform.position;
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(position, Vector3.one * this.voxelVolume);
			Gizmos.color = Color.white;
			for (int i = 0; i < this.voxelsPerAxis; i++)
			{
				float num4 = -num3 + num2 + (float)i * num;
				for (int j = 0; j < this.voxelsPerAxis; j++)
				{
					float num5 = -num3 + num2 + (float)j * num;
					for (int k = 0; k < this.voxelsPerAxis; k++)
					{
						float num6 = -num3 + num2 + (float)k * num;
						Gizmos.DrawWireCube(position + new Vector3(num4, num5, num6), Vector3.one * num);
					}
				}
			}
		}

		[Header("N^2 Mode Settings")]
		public float maxDistance = 0.5f;

		[Header("Forces")]
		public float cohesion = 0.5f;

		public float separation = 0.25f;

		[Header("Voxel Mode Settings")]
		public bool useVoxels = true;

		public bool voxelLocalCenterFromBounds = true;

		public float voxelVolume = 8f;

		public int voxelsPerAxis = 5;

		private int previousVoxelsPerAxisValue;

		private ParticleFlocking.Voxel[] voxels;

		private ParticleSystem particleSystem;

		private ParticleSystem.Particle[] particles;

		private Vector3[] particlePositions;

		private ParticleSystem.MainModule particleSystemMainModule;

		[Header("General Performance Settings")]
		[Range(0f, 1f)]
		public float delay;

		private float timer;

		public bool alwaysUpdate;

		private bool visible;

		public struct Voxel
		{
			public Bounds bounds;

			public int[] particles;

			public int particleCount;
		}
	}
}
