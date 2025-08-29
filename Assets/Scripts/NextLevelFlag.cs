using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelFlag : MonoBehaviour
{
    [Tooltip("Leave empty to load the NEXT scene in Build Settings. " +
             "Or type the scene name (e.g. Level_3_Home) to force a specific target.")]
    [SerializeField] private string sceneNameOverride = "";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Time.timeScale = 1f; // make sure we’re not paused

        if (!string.IsNullOrEmpty(sceneNameOverride))
        {
            SceneManager.LoadScene(sceneNameOverride, LoadSceneMode.Single);
            return;
        }

        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next, LoadSceneMode.Single);
        else
            Debug.LogError("[Flag] No next scene in Build Settings.");
    }
}