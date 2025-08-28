using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps music alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
}
