using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Samples.ControllerSample;

public class SceneTeleportSelector : MonoBehaviour
{
    public UIManager UIController;

    public XRRayInteractor rHand;
    public XRRayInteractor lHand;

    public string scene;
    public XRSimpleInteractable obj;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    // Not use the update method to call Coroutine if it uses VR related things
    // For instance, when I grab an obj, the if condition is validated two times before I get teleported
    // in the other scene, so it run two times the code inside the if (and start two identical coroutines here)
    void Update()
    {
        if (rHand.IsSelecting(obj) || lHand.IsSelecting(obj)) {
            /*
            Debug.Log($"Before : {SceneManager.sceneCount}");
            SceneManager.LoadScene(scene);
            Debug.Log("HAYYA HAYYA");
            Debug.Log($"After : {SceneManager.sceneCount}");
            */

            StartCoroutine(AsyncLoadScene());
        }
    }

    private IEnumerator AsyncLoadScene()
    {
        yield return UIController.FadeBlack();
        Debug.Log("Ended fade ?");


        /*
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        while(!asyncLoad.isDone)
        {
            // Des conneries, update the loading screen ? 
            yield return (0);
        }
        // Doesn't fire because the scene is killed before it can be executed, I think
        //yield return StartCoroutine(SetLoadingScreen(asyncLoad));
        Debug.Log("Loading screen end");
        */
    }

    private IEnumerator SetLoadingScreen(AsyncOperation loadingScene)
    {
        // TODO: Initialize Loading screen
        Debug.Log("Loading screen initialized");

        while(!loadingScene.isDone) {
            yield return UpdateLoadingScreen();
        }
    }

    private IEnumerator UpdateLoadingScreen()
    {
        Debug.Log("Loading screen being updated");
        return null;
    }
}
