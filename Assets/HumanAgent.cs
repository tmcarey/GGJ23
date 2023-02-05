using System;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class HumanAgent : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    [SerializeField] private NpcRoutePoint[] schedule;
    [SerializeField] private Transform animParent;

    [Header("Config")] [SerializeField]
    private float speedAnimMultiplier = 1.5f;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private string name;

    [Serializable]
    private struct NpcRoutePoint
    {
        public Transform targetPoint;
    }

    private int currentScheduleIndex;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        GameManager.Instance.RegisterHuman(this);
        
        GetComponentInChildren<Interactable>().onHover.AddListener(OnHover);
        infoText.text = name;
        infoText.enabled = false;
    }

    private void OnHover(bool hover)
    {
        infoText.enabled = hover;
    }

    private void Update()
    {
        anim.SetFloat(Speed,
            agent.isStopped
                ? 0
                : agent.velocity.magnitude * speedAnimMultiplier);
        if (agent.desiredVelocity.magnitude > 0.1f)
        {
            animParent.localRotation =
                Quaternion.LookRotation(agent.desiredVelocity, Vector3.up);
        }
    }

    public void DoSchedule(int i)
    {
        if (i >= schedule.Length || !schedule[i].targetPoint)
            return;

        agent.SetDestination(schedule[i].targetPoint.position);
    }
}