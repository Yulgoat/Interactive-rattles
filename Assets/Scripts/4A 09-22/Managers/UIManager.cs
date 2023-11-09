using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    /*
     * Affected each time we enter a different scene, by the GameObject containing the
     * CanvasSceneTransitionManager script
     */
    public CanvasGroup blackScreenUI;

    private void Awake()
    {
        // To emulate the Singleton, since we don't use the usual constructor in Unity
        if(instance == null)
        {
            instance = this;
        }
    }

    /* Useles for now, but good to know that we can do things like below
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }
    */


    public IEnumerator FadeBlack(int fadeSpeed = 1)
    {
        while (blackScreenUI.alpha < 1)
        {
            Debug.Log("Fading");
            blackScreenUI.alpha += Time.deltaTime / fadeSpeed;
            yield return null;
        }
    }

    public IEnumerator UnfadeBlack(int fadeSpeed = 1)
    {
        while (blackScreenUI.alpha > 0)
        {
            Debug.Log("Unfading");
            blackScreenUI.alpha -= Time.deltaTime / fadeSpeed;
            yield return null;
        }
    }

    // Method exposed so we can fade and unfade from #SEVEN
    // If one day we will be able to start coroutine directly from #SEVEN, then this method
    // will probably become useless
    public void CallFadeOrUnfadeBlack(bool fade)
    {
        if(fade)
            StartCoroutine(FadeBlack());
        else
            StartCoroutine(UnfadeBlack());
    }
}
