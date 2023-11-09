using UnityEngine;

public class BetterSceneTeleportSelector : MonoBehaviour
{
    private SceneIndexes teleportFrom;
    public SceneIndexes teleportTo;
    private bool isTeleporting = false;

    private void Start()
    {
        teleportFrom = (SceneIndexes)this.gameObject.scene.buildIndex;
    }

    public void TeleportToAnotherScene()
    {
        Debug.LogWarning("Trying to TP, the bool is : " + isTeleporting);
        if(!isTeleporting)
        {
            isTeleporting = true;
            SceneManagerCustom.instance.TriggerSwitchingScene(teleportFrom, teleportTo);
        }
    }
    
}
