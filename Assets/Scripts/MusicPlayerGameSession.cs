using UnityEngine;

public class MusicPlayerGameSession : MonoBehaviour
{
    static MusicPlayerGameSession instance;
    /* void Awake() // testing purpose, singleton testing purpose (no used for gameplay or game)
    {
        ManageSingleton();
    } */
    void Start()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        /* if (instance == null || instance == this)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } */
                        // OR
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
