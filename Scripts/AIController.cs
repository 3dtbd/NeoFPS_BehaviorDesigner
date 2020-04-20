﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NeoFPS.BehaviourDesigner
{
    public class AIController : MonoBehaviour
    {
        [SerializeField, Tooltip("Characters main collider that is used when not a ragdoll.")]
        Collider m_MainCollider;
        [SerializeField, Tooltip("The time until the character gets back up after becoming a ragdoll.")]
        float m_GetBackUpTime = 5f;

        Rigidbody[] m_Rigidbodies;
        bool m_IsRagDoll = false;
        NavMeshAgent agent;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            m_Rigidbodies = GetComponentsInChildren<Rigidbody>();
            ToggleRagdoll(true);
        }

        private void OnEnable()
        {
            GetComponent<IHealthManager>().onIsAliveChanged += OnIsAliveChanged;
        }
        private void OnDisable()
        {
            GetComponent<IHealthManager>().onIsAliveChanged -= OnIsAliveChanged;
        }

        protected virtual void OnIsAliveChanged(bool isAlive)
        {
            if (!isAlive)
            {
                ToggleRagdoll(false);
                agent.isStopped = true;
                StartCoroutine(ReturnFromDeath());
            } else
            {
                ToggleRagdoll(true);
                agent.isStopped = false;
            }
        }

        void ToggleRagdoll(bool isAnimating)
        {
            m_IsRagDoll = !isAnimating;
            m_MainCollider.enabled = isAnimating;
            for (int i = 0; i < m_Rigidbodies.Length; i++)
            {
                m_Rigidbodies[i].isKinematic = isAnimating;
            }
            
            GetComponent<Animator>().enabled = isAnimating;
        }

        IEnumerator ReturnFromDeath()
        {
            yield return new WaitForSeconds(m_GetBackUpTime);
            GetComponent<IHealthManager>().AddHealth(GetComponent<IHealthManager>().healthMax);
        }
    }
}