using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Obstacle[] obstacles;
    [SerializeField] private Star star;
    [SerializeField] private PlaneMovement movement;
    
    //? Key = Generation, Value = Time Between Spawns
    private (uint, float, float)[] generationList = new (uint, float, float)[]
    {
        (0, 1.5f, 3),
        (5, 1.25f, 4),
        (10, .8f, 6f),
        (20, 0.6f, 9),
        (50, 0.5f, 11f),
        (80, 0.3f, 13f),
        (120, 0.25f, 15f),
        (150, 0.2f, 18f),
    };

    uint currentGeneration;

    Vector3 safeGridPosition;

    public float spawnOffset;

    float currentSpeed;

    
    void Start()
    {
        StartCoroutine(Wave());
    }

    void Update()
    {
        MovementManager();
    }

    private void OnEnable()
    {
        movement.OnDeath += EndGame;
    }

    private void OnDisable()
    {
        movement.OnDeath -= EndGame;
    }

    void EndGame()
    {
        StopAllCoroutines();
        foreach(var obs in GameObject.FindObjectsOfType<Obstacle>())
        {
            Destroy(obs.gameObject);
        }
    }

    #region Waves
    
    IEnumerator Wave()
    {
        SpawnObstacle();
        yield return new WaitForSeconds(GetTimeBetweenSpawns(currentGeneration));
        currentGeneration++;
        StartCoroutine(Wave());
    }

    float GetTimeBetweenSpawns(uint currentGen)
    {
        foreach(var tuple in generationList.Reverse())
        {
            if(currentGen >= tuple.Item1)
            {
                currentSpeed = tuple.Item3;
                return tuple.Item2;
            }
        }
        Debug.LogError("Something brokey.");
        return 100;
    }

    void MovementManager()
    {
        transform.position -= Vector3.forward * currentSpeed * Time.deltaTime;
    }

    #endregion

    #region Spawning

    void SpawnObstacle() //TODO: Potentially only bring harder obstacles later.
    {
        int randomIndex = Random.Range(0, obstacles.Length + 1);
        

        var obs = Instantiate((randomIndex == obstacles.Length) ? star : obstacles[randomIndex], GetObstacleSpawnPoint(), Random.rotation, transform);
        movement.score += 10;
    }

    public Vector3 GetObstacleSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        while(spawnPoint == safeGridPosition)
        {
            spawnPoint = gridManager.GetRandomGridPostion();
            Vector2 safePosIndexOffset = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            safePosIndexOffset.x %= 6;
            safePosIndexOffset.y %= 6;
            if(safePosIndexOffset.x < 0)
                safePosIndexOffset.x = 6 - safePosIndexOffset.x;
            if(safePosIndexOffset.y < 0)
                safePosIndexOffset.y = 6 - safePosIndexOffset.y;
        }
        spawnPoint.z += spawnOffset;
        return spawnPoint;
    }

    #endregion

}
