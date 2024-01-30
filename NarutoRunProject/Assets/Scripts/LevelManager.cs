using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public string levelSong;
    public int minimunFollowers;
    public float whenToSpawnFinalFloor;
    public PlayerController player;
    public Transform playerFinalPos;
    public Quaternion playerFinalRotation;
    public WinUIAnim winUIAnim;
    public WinUIAnim loseUIAnim;
    public bool finishedLevel;
    public Rigidbody AttackLimitRigidBody;

    private Vector3 defaultPlayerFinalPos;
    private Quaternion defaultPlayerFinalRotation;

    public UnityEvent OnWinLose;

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
        finishedLevel = false;
        defaultPlayerFinalPos = playerFinalPos.position;
        defaultPlayerFinalRotation = Quaternion.Euler(0, 180, 0);
    }

    public void InitializeLevel()
    {
        finishedLevel = false;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayMusic(levelSong);
        }

        player.InitPlayerToPlay();
        playerFinalPos.position = defaultPlayerFinalPos;
        playerFinalRotation = defaultPlayerFinalRotation;

        CanvasTextChanger.instance.Showtext();
        FollowerCounter.instance.Init();
        CameraPositions.instance.ChangePositionTo("Playing");

        if (winUIAnim != null)
        {
            winUIAnim.Hide();
        }
        if (loseUIAnim != null)
        {
            loseUIAnim.Hide();
        }

        SpawnerBehaviour.instance.RemoveAllFloors();
        StartSpawningFloors();
        StartSpawningFollowers();
        StartSpawningObstacles();
        SpawnerBehaviour.instance.Init();
    }

    public void StartSpawningFloors()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StartSpawning();
        }

        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StartSpawning();
        }
    }

    public void StopSpawningFloors()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StopSpawning();
        }
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StopSpawning();
        }
    }

    public void StartSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(true);
    }

    public void StopSpawningNextFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(false);
    }
    public void StartSpawningFollowers()
    {
        RangeSpawner.instance.StartSpawning();
    }

    public void StopSpawningFollowers()
    {
        RangeSpawner.instance.StopSpawning();
    }

    public void StartSpawningObstacles()
    {
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StartSpawning();
        }
    }

    public void StopSpawningObstacles()
    {
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StopSpawning();
        }
    }

    public void DisableObstacles()
    {
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.DisableSpawnedObjs();
        }
    }

    public void Freeze()
    {
        StopSpawningFloors();
        StopSpawningNextFloors();
        StopSpawningFollowers();
        StopSpawningObstacles();
        SpawnerBehaviour.instance.FreezeAllFloors();
        player.Idle();
    }

    public void Unfreeze()
    {
        StartSpawningFloors();
        StartSpawningNextFloors();
        StartSpawningFollowers();
        StartSpawningObstacles();
        SpawnerBehaviour.instance.UnFreezeAllFloors();
        player.StartRunning();
    }

    private void RemoveAllFollowers()
    {
        FollowerCounter.instance.DisableAllFollowers();
        RangeSpawner.instance.CleanObjects();
    }

    public void Finished()
    {
        StopSpawningFollowers();
        RemoveAllFollowers();
        StopSpawningFloors();
        StopSpawningObstacles();
        DisableObstacles();
        SpawnerBehaviour.instance.FreezeAllFloors();
        CameraPositions.instance.ChangePositionTo("Finish");
        player.MoveToFinalPosition(playerFinalPos.position, playerFinalRotation);
        finishedLevel = true;
        OnWinLose.Invoke();
    }

    public void Win()
    {
        Finished();
        player.Win();
        winUIAnim.StartAnimation();
    }

    public void Lose()
    {
        Finished();
        player.Lose();
        loseUIAnim.StartAnimation();
    }

    public void Pause()
    {
        player.Idle();
    }
}