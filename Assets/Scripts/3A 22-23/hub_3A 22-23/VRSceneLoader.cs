using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSceneLoader : MonoBehaviour
{
    public XRSimpleInteractable[] objectsToClick;
    private Dictionary<string, AsyncOperation> sceneLoadOperations;

    void Start()
    {
        sceneLoadOperations = new Dictionary<string, AsyncOperation>();

        foreach (XRSimpleInteractable interactable in objectsToClick)
        {
            interactable.selectEntered.AddListener(OnObjectClicked);
            SceneInfo sceneInfo = interactable.GetComponent<SceneInfo>();

            if (sceneInfo != null && !sceneLoadOperations.ContainsKey(sceneInfo.targetSceneName))
            {
                StartCoroutine(PreloadScene(sceneInfo.targetSceneName));
            }
        }
    }

    IEnumerator PreloadScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
        sceneLoadOperations.Add(sceneName, asyncOperation);

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }
            yield return null;
        }
    }

    void OnObjectClicked(SelectEnterEventArgs args)
    {
        XRSimpleInteractable interactable = args.interactable as XRSimpleInteractable;
        SceneInfo sceneInfo = interactable.GetComponent<SceneInfo>();

        if (sceneInfo != null && sceneLoadOperations.ContainsKey(sceneInfo.targetSceneName))
        {
            sceneLoadOperations[sceneInfo.targetSceneName].allowSceneActivation = true;
        }
    }
}
