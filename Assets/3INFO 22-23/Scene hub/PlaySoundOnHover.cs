using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnHover : MonoBehaviour
{
    private AudioSource audioSource;
    private XRSimpleInteractable interactable;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.onHoverEntered.AddListener(OnHoverEnter);
        interactable.onHoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(XRBaseInteractor interactor)
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    void OnHoverExit(XRBaseInteractor interactor)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
