using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HumanAgent : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private NpcRoutePoint[] schedule;
    [SerializeField] private Transform animParent;
    
    [Header("Config")] 
    [SerializeField] private float speedAnimMultiplier = 1.5f;

    [Serializable]
    private struct NpcRoutePoint
    {
        public float waitTime;
        public Transform targetPoint;
    }

    private int currentScheduleIndex;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        StartCoroutine(DoSchedule());
    }

    private void Update()
    {
            anim.SetFloat(Speed, agent.isStopped ? 0 : agent.velocity.magnitude * speedAnimMultiplier);
        animParent.localRotation = Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);
    }

    private IEnumerator DoSchedule()
    {
        while (GameManager.Instance.DayActive)
        {
            Debug.Log("Moving to destination " + schedule[currentScheduleIndex].targetPoint.name);
            agent.SetDestination(schedule[currentScheduleIndex].targetPoint.position);
            
            yield return new WaitForSeconds(0.5f);
            
            while (agent.pathPending || agent.remainingDistance > 0.1f)
            {
                yield return null;
            }
            
            Debug.Log("Reached destination");
            
            yield return new WaitForSeconds(schedule[currentScheduleIndex].waitTime);
            
            currentScheduleIndex = (currentScheduleIndex + 1) % schedule.Length;
        }
    }
}
