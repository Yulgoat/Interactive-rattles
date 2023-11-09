using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Basically just a script to load the Hub Scene after the preload scene is complete
 */
public class HubSceneLoader : MonoBehaviour
{
    const SceneIndexes hubIndex = SceneIndexes.HUB;

    void Start()
    {
        SceneManager.LoadScene((int)hubIndex);
    }
}
