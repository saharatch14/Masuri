using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public ScreenManager gameManager;

    void OnCollisionEnter()
    {
        gameManager.CompleteLevel();
    }
}
