using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Playing, Complete, Dead }

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.Playing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetGameState(GameState newState)
    {
        if (CurrentGameState == newState) return;
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.Complete:
                HandleLevelComplete();
                break;
            case GameState.Dead:
                ResetScene();
                break;
            case GameState.Playing:
                break;
        }
    }

    private void HandleLevelComplete()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        int next = index + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            // No more scenes in Build Settings – reload first level or stay
            SceneManager.LoadScene(0);
        }
    }

    private void ResetScene()
    {
        // short delay so death animation/sound can play
        Invoke(nameof(ResetSceneNow), 1.5f);
    }

    private void ResetSceneNow()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}