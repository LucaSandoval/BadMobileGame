using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "YourSceneName"; // Default scene to load, you can change this in the Inspector
    // Create a public list of GameObjects that you want to activate.
    public List<GameObject> ObjectForScoresTab;
    public GameObject DefaultScoresButton;


    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void HighScoresTab(bool isOpen) {
        // Iterate through the list and set each GameObject to active.
        foreach (GameObject obj in ObjectForScoresTab)
        {
            obj.SetActive(isOpen);
        }
        DefaultScoresButton.SetActive(!isOpen);
    }
}
