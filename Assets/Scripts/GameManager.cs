using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    float beginTime = 0;
    bool m_GameIsPaused = false;

    [SerializeField] private List<Transform> spawnPoint;

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

    public Vector3 GetRandomSpawnPoint()
    {
        int rand = Random.Range(0, 3);
        return spawnPoint[rand].position;
    }
}