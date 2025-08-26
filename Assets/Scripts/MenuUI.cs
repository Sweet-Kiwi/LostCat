using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string firstLevelScene = "Level_1_Forest";

    [Header("Optional volume slider (0..1)")]
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        // restore saved volume
        float v = PlayerPrefs.GetFloat("masterVol", 1f);
        AudioListener.volume = v;
        if (volumeSlider) { volumeSlider.value = v; }
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene(firstLevelScene);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OnVolumeChanged(float v)
    {
        AudioListener.volume = v;                 // simple global volume
        PlayerPrefs.SetFloat("masterVol", v);     // save setting
    }
}