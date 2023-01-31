using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    float beginTime = 0;
    bool m_GameIsPaused = false;

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform castle;
    [SerializeField] private GameObject NetworkUI;
    [SerializeField] private GameObject XROrigin;

    [SerializeField] private Camera classicCamera;

    private void Start()
    {
        StartTime();

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevices(inputDevices);

        if(inputDevices.Count > 0)
        {
            if (inputDevices[0].manufacturer.Equals("Oculus"))
            {
                XROrigin.SetActive(true);
                classicCamera.gameObject.SetActive(false);
            }
            else
            {
                XROrigin.SetActive(false);
                classicCamera.gameObject.SetActive(true);
            }
        }

    }

    public void StartTime()
    {
        beginTime = Time.time;
    }
    public float GetTime()
    {
        return Time.time - beginTime;
    }

    public List<Transform> GetSpawnPointList()
    {
        return spawnPoints;
    }

    public Vector3 GetCastlePosition()
    {
        return castle.position;
    }
}