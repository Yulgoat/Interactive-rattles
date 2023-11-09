using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnClick : MonoBehaviour
{
    private AudioSource audioSource;
    private XRSimpleInteractable interactable;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.onSelectEntered.AddListener(OnSelectEnter);
    }

    void OnSelectEnter(XRBaseInteractor interactor)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
