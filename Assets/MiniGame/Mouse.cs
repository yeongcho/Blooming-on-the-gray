using UnityEngine;

public class Mouse : MonoBehaviour
{
    private float baseSpeed = 4f;
    private float speed;
    private GameManager gameManager;
    private Vector3 fixedYPosition;

    public void Initialize(Transform cat, GameManager gameManager)
    {
        this.gameManager = gameManager;
        fixedYPosition = transform.position;
        SetSpeedByDay();
    }

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = Object.FindFirstObjectByType<GameManager>();
        }
        if (fixedYPosition == Vector3.zero)
        {
            fixedYPosition = transform.position;
        }
        SetSpeedByDay();
    }

    void SetSpeedByDay()
    {
        int day;

        if (gameManager != null)
        {
            day = gameManager.GetCurrentDay();
        }
        else
        {
            day = 1;
        }

        speed = baseSpeed + (day - 1);
    }

    void Update()
    {
        if (gameManager == null || gameManager.IsGameOver())
            return;

        transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            fixedYPosition.y,
            fixedYPosition.z
        );

        Vector3 catPosition = gameManager.GetCatPosition();

        if (Mathf.Abs(transform.position.x - catPosition.x) < 0.5f && Mathf.Abs(transform.position.y - catPosition.y) < 0.5f)
        {
            gameManager.OnMouseHit();
            Destroy(gameObject);
        }

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}