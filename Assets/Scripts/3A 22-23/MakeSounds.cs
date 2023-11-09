//Ce script permet pour l'instant de lancer ou d'arreter un son/une musique.
// Ce script a été récupéré depuis : https://docs.unity3d.com/ScriptReference/AudioSource.html

// L'objectif est de faire que ce script produise un son d'amplitude décroissante selon un modèle physique réaliste.
// Les principaux soucis étant de savoir exactement comment rattacher ce script à chaque hochet,
//comment récupérer la magnitude des collissions des billes dans le hochet,
//et comment moduler un son pré-enregistré sous unity pour donner une impression de réalisme.

// Ce script vient en remplacement du script d'Emilie Humel, qui convenait très bien pour du retour haptique
//mais qui ne modulait pas le son émit... On continue ici de s'inspirer de ce script en l'adaptant pour 
//moduler le son.

//Assign an AudioSource to a GameObject and attach an Audio Clip in the Audio Source. Attach this script to the GameObject.
//trouver ce que ça veut dire.
using UnityEngine;

public class MakeSounds : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    public GameObject SoundsFolder;
    //Ce booléen est utilisé dans le script d'inspiration comme une variable mute. Pas certain que ça nous soit utile.
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times.
    bool m_ToggleChange;

    float meanMagnitude; // pour gérer l'intensité des sons via l'intensité des collisions.
    float volume; // pour modifier le volume du son.
    public float threshold = 0.1f; // seuil de force des collisions à partir duquel on emet plus le son.

    public AudioSource[] soundsArray;
    void Start()
    {
        // récupère un array d'AudioSource du hochet auquel la bille est attachée.
        soundsArray = SoundsFolder.GetComponentsInChildren<AudioSource>();
        m_MyAudioSource = soundsArray[Random.Range(0, soundsArray.Length)]; // Choisir un des sons au hasard
        Debug.Log(m_MyAudioSource);
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
        meanMagnitude = 0f;
    }

    void Update()
    {
        //Check to see if you just set the toggle to positive
        if (m_Play == true && m_ToggleChange == true)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Play();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }
        //Check if you just set the toggle to false
        if (m_Play == false && m_ToggleChange == true)
        {
            //Stop the audio
            m_MyAudioSource.Stop();
            //Ensure audio doesn’t play more than once
            m_ToggleChange = false;
        }
    }
    void OnCollisionEnter(Collision collision){
        if (collision.relativeVelocity.magnitude > threshold) {
            m_MyAudioSource.volume = collision.relativeVelocity.magnitude;
            m_MyAudioSource.Play(); 
        }
    }
}