using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public string MenuMusicname;
    void Start()
    {
        AudioManager.instance.PlayMusic(MenuMusicname);
    }
}
