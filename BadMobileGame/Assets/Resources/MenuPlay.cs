using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuPlay : MonoBehaviour
{
    public string MainScene = "BackGroundTest";
    // Public method to load a scene by name
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(MainScene);
    }
}
