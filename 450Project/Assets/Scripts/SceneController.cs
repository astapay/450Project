using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && Input.GetKeyDown("space"))
        {
            Destroy(scoreController.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    public void endGame()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(LoadEndGameSceneAndExecuteCoroutine());
    }

    IEnumerator LoadEndGameSceneAndExecuteCoroutine()
    {
        // Load the end game scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        // Wait until the end game scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Now that the scene is loaded, execute your method
        scoreController.getFinalScore();
    }
}
