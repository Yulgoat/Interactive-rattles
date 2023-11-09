using UnityEngine;

// XR Origin can't be placed with others DDOL objects, otherwise it won't be able
// to interact with objects in the scene.
// Therefore, we need a new instance of this object for each scene.
// Since with VR, the UI need to be attached to the camera used for the headset to have decent behavior,
// we need to tell the UIManager (which persists through scenes) the new UI for loading each time we switch scenes.
// And also because we can't set a persistent UI, we need to have a default one attached to the MainCamera,
// otherwise the user will see the new scene before the blackscreen shows up (because we can't have the same UI(blackScreen) through 2 different scenes)
// Don't forget to manually link the MainCamera found in the XR Origin GameObject to the RenderCamera of your Canvas and set the plane
// distance to 0.1

public class CanvasSceneTransitionManager : MonoBehaviour
{
    #region Fields

    public CanvasGroup blackScreenUI;

    #endregion

    #region Methods

    private void Start()
    {
        // Link the current transition UI to the UIManager, since it is scene based
        UIManager.instance.blackScreenUI = blackScreenUI;
    }

    #endregion
}
