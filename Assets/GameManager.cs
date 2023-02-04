using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private HackableDevice goalDevice;
    [SerializeField] private GameObject winState;
    [SerializeField] private GameObject lossState;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Config")] [SerializeField] private LayerMask hackLosLayerMask;
    [SerializeField] private float levelTime;
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public bool DayActive { get; private set; } = true;

    private readonly List<HackableDevice> hackedDevices = new();

    private float startTime;
    private float timeRemaining;

    void Awake()
    {
        _instance = this;
        timeRemaining = levelTime;
        startTime = Time.time;
        winState.SetActive(false);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            var timeRemaining = levelTime - (Time.time - startTime);
            var minutes = Mathf.FloorToInt(timeRemaining / 60.0f);
            var seconds = Mathf.FloorToInt(timeRemaining % 60.0f);

            timer.text = $"{minutes:00}:{seconds:00}";

            if (timeRemaining < 0)
            {
                DayActive = false;
                lossState.SetActive(true);
            }
        }
        else
        {
            timeRemaining = 0;
        }
    }

    public void AddHackedDevice(HackableDevice device)
    {
        if (device == goalDevice)
            winState.SetActive(true);

        hackedDevices.Add(device);
    }

    public bool CheckHackLos(Vector3 point, float maxDistance = 3)
    {
        foreach (var device in hackedDevices)
        {
            var dist = Vector3.Distance(point, device.transform.position);

            if (Physics.Raycast(device.transform.position, point - device.transform.position,
                    out var hitInfo,
                    dist,
                    hackLosLayerMask)) continue;

            if (dist <= maxDistance)
            {
                return true;
            }
        }

        return false;
    }
}