using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    float beginTime = 0;
    bool m_GameIsPaused = false;

    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform castle;
    [SerializeField] private GameObject NetworkUI;

    private void Start()
    {
        StartTime();
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