using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string LevelSong;
    public int minimunFollowers;
    public float WhenToSpawnFinalFloor;
    public GameObject Player;
    public Transform PlayerFinalPos;
    public WinUIAnim winUIAnim;
    public WinUIAnim loseUIAnim;
    private Animator PlayerAnim;
    
    
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
        PlayerAnim = Player.GetComponent<Animator>();
    }

    public void StopSpawningFloors()
    {
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(false);
    }

    public void DisableFinalFloor()
    {
        if (SpawnerBehaviour.instance.GetFinalFloor() != null)
        {
            SpawnerBehaviour.instance.GetFinalFloor().GetComponent<ObjectsMoveBehaviour>().SetCanMove(true);
            SpawnerBehaviour.instance.GetFinalFloor().transform.position = Vector3.zero;
            SpawnerBehaviour.instance.GetFinalFloor().SetActive(false);
        }
    }

    public void InitializeLevel()
    {
        DisableFinalFloor();
        if(AudioManager.instance != null)
        {
            AudioManager.instance.StopAllSounds();
            AudioManager.instance.PlaySound(LevelSong);
        }
        if(SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StartSpawning();
        }
        if(PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StartSpawning();
        }
        RangeSpawner.instance.StartSpawning();
        SpawnerBehaviour.instance.Init();
        CanvasTextChanger.instance.Showtext();
        CameraPositions.instance.ChangePositionTo("Playing");
        if(winUIAnim != null)
        {
            winUIAnim.Hide();
        }
        if(loseUIAnim != null)
        {
            loseUIAnim.Hide();
        }
        Player.transform.position = Vector3.zero;
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
        PlayerAnim.SetInteger("Run", 1);
        PlayerAnim.SetBool("Victory", false);
        PlayerAnim.SetBool("Lose", false);
        Player.GetComponent<PlayerController>().SetCanMove(true);
        for (int i = 0; i < SpawnerBehaviour.SpawnedFloors.Count; i++)
        {
            SpawnerBehaviour.SpawnedFloors[i].GetComponent<ObjectsMoveBehaviour>().SetCanMove(true);
        }
    }

    public void Freeze()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StopSpawning();
        }
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StopSpawning();
        }
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(false);
        RangeSpawner.instance.StopSpawning();
        for (int i = 0; i < SpawnerBehaviour.SpawnedFloors.Count; i++)
        {
            SpawnerBehaviour.SpawnedFloors[i].GetComponent<ObjectsMoveBehaviour>().SetCanMove(false);
        }
        PlayerAnim.SetInteger("Run", 0);
    }

    public void Unfreeze()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StartSpawning();
        }
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StartSpawning();
        }
        DeSpawnerBehaviour.Instance.SetCanSpawnFloor(true);
        RangeSpawner.instance.StartSpawning();
        for (int i = 0; i < SpawnerBehaviour.SpawnedFloors.Count; i++)
        {
            SpawnerBehaviour.SpawnedFloors[i].GetComponent<ObjectsMoveBehaviour>().SetCanMove(true);
        }
        PlayerAnim.SetInteger("Run", 1);
    }

    public void Finished()
    {
        if (SingleTimedSpawner.Instance != null)
        {
            SingleTimedSpawner.Instance.StopSpawning();
        }
        RangeSpawner.instance.StopSpawning();
        RangeSpawner.instance.RemoveNotPickedFollowers();
        FollowerCounter.instance.DisableAllFollowers();
        CameraPositions.instance.ChangePositionTo("Finish");
        if (PointsSpawner.Instace != null)
        {
            PointsSpawner.Instace.StopSpawning();
        }
        for (int i = 0; i < SpawnerBehaviour.SpawnedFloors.Count; i++)
        {
            SpawnerBehaviour.SpawnedFloors[i].GetComponent<ObjectsMoveBehaviour>().SetCanMove(false);
        }
        Player.GetComponent<PlayerController>().SetCanMove(false);
        Player.transform.position = PlayerFinalPos.position;
        Player.transform.rotation = Quaternion.Euler(0, 180, 0);
        PlayerAnim.SetInteger("Run", 0);

        if (FollowerCounter.instance.GetCurrentFollowers() >= minimunFollowers)
        {
            Win();
        }
        else
        {
            Lose();
        }
    }

    public void Win()
    {
        PlayerAnim.SetBool("Victory", true);
        winUIAnim.StartAnimation();
    }

    public void Lose()
    {
        PlayerAnim.SetBool("Lose", true);
        loseUIAnim.StartAnimation();
    }
}