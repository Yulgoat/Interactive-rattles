using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null)
        {
            Debug.LogError("No XRSimpleInteractable component found on the GameObject.");
            return;
        }

        interactable.selectEntered.AddListener(OnSelectEnter);
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEnter);
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
