using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private HackableDevice goalDevice;
    [SerializeField] private GameObject winState;
    [SerializeField] private GameObject lossState;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI timer;

    [Header("Config")] [SerializeField] private LayerMask hackLosLayerMask;
    [SerializeField] private float levelTime;
    [SerializeField] private float hourTime;
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    public bool DayActive { get; private set; } = true;

    private readonly List<HackableDevice> hackedDevices = new();

    private float startTime;
    private float timeRemaining;

    public bool IsPaused => pauseMenu.activeSelf;
    
    private List<HumanAgent> humanAgents = new List<HumanAgent>();

    private int scheduleIdx;
    
    public void RegisterHuman(HumanAgent humanAgent)
    {
        humanAgents.Add(humanAgent);
    }

    void Awake()
    {
        _instance = this;
        timeRemaining = levelTime;
        startTime = Time.time;
        winState.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Scheduler());
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining = levelTime - (Time.time - startTime);
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
    
    public void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private IEnumerator Scheduler()
    {
        yield return new WaitForEndOfFrame();
        while (timeRemaining > 0)
        {
            foreach(var agent in humanAgents)
            {
                agent.DoSchedule(scheduleIdx);
            }

            scheduleIdx++;
            yield return new WaitForSeconds(hourTime);
        }
    }
    
}