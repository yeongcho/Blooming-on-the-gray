using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public bool isUp;
    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (gameManager == null) return;
        if (isUp)
            gameManager.MoveCatUp();
        else
            gameManager.MoveCatDown();
    }
}