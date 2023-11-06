using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryTester : MonoBehaviour
{
    public static MemoryTester instance;
    public static StreamWriter log;
    private bool Scanning;
    public float timeToScan;
    private float TScan;
    private int state;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(Scanning)
            {
                Scanning = false;
                state = 3;
            }
            else
            {
                Scanning = true;
                state = 1;
                TScan = timeToScan;
            }
        }

        if(state == 1)//open File
        {
            log = new StreamWriter(Application.dataPath + "/Memory.csv",true);
            log.WriteLine("Scene, Memory Used");
            state = 2;
        }
        else if(state == 2) //Write
        {
            if(TScan <= 0)
            {
                TScan = timeToScan;
                LogMemory();
            }
            else
            {
                TScan -= Time.deltaTime;
            }
        }
        else if(state == 3) //Close
        {
            log.Close();
        }
    }

    static void LogMemory()
    {
        long reserved;
        reserved = GC.GetTotalMemory(false);

        log.WriteLine(SceneManager.GetActiveScene().name + "," + reserved);
        log.Flush();
    }
}
