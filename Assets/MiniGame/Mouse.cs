using UnityEngine;

public class Mouse : MonoBehaviour
{
    private float baseSpeed = 4f;           // 1일차 기준 마우스 기본 이동 속도
    private float speed;                    // 실제 적용되는 이동 속도 (날짜에 따라 증가)
    private GameManager gameManager;        // GameManager 참조
    private Vector3 fixedYPosition;         // 쥐가 등장한 Y 좌표를 고정 (세로 움직임 방지)

    // GameManager가 이 쥐 오브젝트를 생성할 때 초기화 호출
    public void Initialize(Transform cat, GameManager gm)
    {
        gameManager = gm;
        fixedYPosition = transform.position;

        SetSpeedByDay();  // 날짜에 따라 속도 조정
    }

    void Start()
    {
        // 에디터에서 직접 생성된 경우 GameManager를 자동 검색
        if (gameManager == null)
        {
            gameManager = Object.FindFirstObjectByType<GameManager>(); // 권장 방식
        }

        // Y 위치가 초기화되지 않았으면 현재 위치로 설정
        if (fixedYPosition == Vector3.zero)
        {
            fixedYPosition = transform.position;
        }

        SetSpeedByDay(); // Start 시에도 속도 보장
    }

    // 날짜(day)에 따라 쥐 속도 증가
    void SetSpeedByDay()
    {
        int day = gameManager != null ? gameManager.GetCurrentDay() : 1;
        speed = baseSpeed + (day - 1); // 예: 1일차는 4, 2일차는 5, ...
    }

    void Update()
    {
        // 게임이 종료됐으면 이동/충돌 중지
        if (gameManager == null || gameManager.IsGameOver())
            return;

        // 쥐 이동 (왼쪽으로 직진)
        transform.position = new Vector3(
            transform.position.x - speed * Time.deltaTime,
            fixedYPosition.y, // Y 고정
            fixedYPosition.z
        );

        // 고양이 위치 가져오기
        Vector3 catPos = gameManager.GetCatPosition();

        // 고양이와 충돌 감지 (X, Y 모두 근접하면 충돌로 간주)
        if (Mathf.Abs(transform.position.x - catPos.x) < 0.5f &&
            Mathf.Abs(transform.position.y - catPos.y) < 0.5f)
        {
            gameManager.OnMouseHit(); // 하트 감소
            Destroy(gameObject);      // 쥐 제거
        }

        // 화면 밖(왼쪽)으로 벗어나면 제거
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
