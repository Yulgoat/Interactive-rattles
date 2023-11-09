using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

// SceneManager is already existing
public class SceneManagerCustom : MonoBehaviour
{
    #region Fields

    public static SceneManagerCustom instance;

    #endregion

    #region Methods

    public void TriggerSwitchingScene(SceneIndexes from, SceneIndexes to)
    {
        StartCoroutine(LoadAsync(from, to));
    }

    [ContextMenu("Load Scene Crafting")]
    public void LoadSceneCrafting()
    {
        StartCoroutine(LoadAsync(SceneIndexes.HUB, SceneIndexes.CRAFTING));
    }

    [ContextMenu("Load Scene Discovery")]
    public void LoadSceneDiscovery()
    {
        StartCoroutine(LoadAsync(SceneIndexes.HUB, SceneIndexes.DISCOVERY));
    }

    [ContextMenu("Load Scene Usage")]
    public void LoadSceneUsage()
    {
        StartCoroutine(LoadAsync(SceneIndexes.HUB, SceneIndexes.USAGE));
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private IEnumerator LoadAsync(SceneIndexes sceneToUnload, SceneIndexes sceneToLoad)
    {
        // If we don't do this, we will get teleported before the fade ends, because our scenes are too light for now
        // Edit : and since we don't do async loading anymore, we must fade before loading the new scene
        yield return UIManager.instance.FadeBlack();

        SceneManager.LoadScene((int)sceneToLoad);

        // The async way to load scenes breaks the link between the VR headset/controllers and XR components
        // It may be because 2 XR Origin objects can be (partially) loaded at the same time, and XR doesn't handle that correctly atm
        /*
        List<AsyncOperation> scenesLoading = new List<AsyncOperation>
        {
            SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive),
            SceneManager.UnloadSceneAsync((int)sceneToUnload)
        };

        // Basically waiting for a complete unload of the current Scene and complete load of the new one
        foreach (AsyncOperation sceneLoad in scenesLoading)
        {
            while (!sceneLoad.isDone)
            {
                yield return null;
            }
        }
        */

        // The new Camera should have been automatically set in the Awake method of the CanvasSceneTransitionManager script
        // linked to the UI in the scene.
        // And it won't break because the SceneManagerCustom is never located in one of the two scenes
        yield return UIManager.instance.UnfadeBlack();
    }

    #endregion
}

public enum SceneIndexes
{
    PRELOAD = 0,
    HUB = 1,
    CRAFTING = 2,
    DISCOVERY = 3,
    USAGE = 4
}