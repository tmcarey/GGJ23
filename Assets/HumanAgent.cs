using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HumanAgent : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private NpcRoutePoint[] schedule;

    [Serializable]
    private struct NpcRoutePoint
    {
        public float waitTime;
        public Transform targetPoint;
    }
    
    private int currentScheduleIndex;

    private void Start()
    {
        StartCoroutine(DoSchedule());
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
