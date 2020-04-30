using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeoFPS.BehaviourDesigner
{
	/// <summary>
	/// AiBasicWeapon configures a weapon for use by an AI. Drop
	/// this component onto any weapon that an AI might use.
	/// </summary>
    public class AiBasicWeapon : MonoBehaviour, IAiWeapon
    {
		[Header("Damage")]
		[SerializeField, Tooltip("The damage the weapon does.")]
		private float m_Damage = 50f;
		[SerializeField, Tooltip("The minimum range that the melee weapon can reach.")]

		[Header("Range")]
		private float m_MinimumRange = 1f;
		[SerializeField, Tooltip("The maximum range that the melee weapon can reach.")]
		private float m_MaximumRange = 1.2f;

		[Header("Animation")]
		[SerializeField, Tooltip("The name of the animation layer to use when this weapon is equipped. If null the Base layer will be used.")]
		private string m_AnimationLayerName = null;

		[Header("Timing")]
		[SerializeField, Tooltip("The delay from starting the attack to checking for an impact. Should be synced with the striking point in the animation.")]
		private float m_TimeToImpact = 0.1f;
		[SerializeField, Tooltip("The recovery time after a hit.")]
		private float m_RecoveryTime = 1f;
		
		[Header("Kicker")]
		[SerializeField, Tooltip("The kick distance for the player when hit by this weapon.")]
		private float m_KickDistance = 0.02f;
		[SerializeField, Tooltip("The kick rotation for the player when hit by this weapon.")]
		private float m_KickRotation = 5f;
		[SerializeField, Tooltip("The kick duration when hit by this weapon.")]
		private float m_KickDuration = 0.5f;

		private Animator m_Animator;
		private int m_AnimationLayerIndex;

		private bool useAnimationLayer
		{
			get
			{
				return animator != null && !string.IsNullOrEmpty(m_AnimationLayerName);
			}
		}

		Animator animator
		{
			get
			{
				if (m_Animator != null) return m_Animator;

				m_Animator = GetComponentInParent<Animator>();
				if (m_Animator == null)
				{
					Debug.LogWarning("Cannot find animator on " + gameObject.transform.root + " for " + gameObject);
				}
				else
				{
					if (useAnimationLayer)
					{
						m_AnimationLayerIndex = m_Animator.GetLayerIndex(m_AnimationLayerName);
					}
				}

				return m_Animator;
			}
		}

		void OnEnable ()
		{
			if (useAnimationLayer) {
				animator.SetLayerWeight(m_AnimationLayerIndex, 1f);
			}
		}

		void OnDisable()
		{
			if (useAnimationLayer)
			{
				animator.SetLayerWeight(m_AnimationLayerIndex, 0);
			}
		}

		public float damageAmount
		{
			get { return m_Damage; }
		}

		public float recoveryTime
		{
			get { return m_RecoveryTime; }
		}

		public float timeToImpact
		{
			get { return m_TimeToImpact; }
		}

		public float kickDuration
		{
			get { return m_KickDistance; }
		}

		public float kickDistance
		{
			get { return m_KickDistance; }
		}

		public float kickRotation
		{
			get { return m_KickRotation; }
		}

	}
}
