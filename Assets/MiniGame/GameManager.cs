using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cat;                     // 고양이 오브젝트
    public GameObject mousePrefab;            // 생성할 쥐 프리팹

    public GameObject[] hearts;               // 생명 표시 하트 (3개)
    public TextMeshProUGUI timerText;         // 상단에 표시할 남은 시간 텍스트

    private int currentCatIndex = 0;          // 고양이 현재 위치 인덱스
    private float gameTime = 20f;             // 총 게임 시간 (초)
    private int heartIndex = 2;               // 남은 하트 인덱스 (2 → 1 → 0)

    private bool isGameOver = false;

    private Vector3[] catPositions;           // 고양이의 세로 위치 배열
    private Vector3[] mouseSpawnPoints;       // 쥐가 생성될 위치 배열

    private int day = 1;                      // 현재 날짜 (MainScene에서 이어받음)

    void Start()
    {
        // 저장된 날짜 정보 로드 (밥 가격/로직 변경 등에 활용 가능)
        day = PlayerPrefs.GetInt("day", 1);

        // 고양이 위치 정의 (4단계 수직 위치)
        catPositions = new Vector3[]
        {
            new Vector3(-6.7543f, 2.9099f, 0),
            new Vector3(-6.7742f, 0.6024f, 0),
            new Vector3(-9.0257f, -1.1102f, 0),
            new Vector3(-4.7262f, -2.5253f, 0)
        };

        // 쥐 스폰 위치 (고양이 반대쪽 오른쪽에서 등장)
        mouseSpawnPoints = new Vector3[]
        {
            new Vector3(6.43f, 2.9549f, 0),
            new Vector3(6.43f, 0.6745002f, 0),
            new Vector3(6.43f, -1.1373f, 0),
            new Vector3(6.43f, -2.6100f, 0)
        };

        // 고양이 초기 위치 설정
        cat.transform.position = catPositions[currentCatIndex];

        // 쥐 주기적으로 생성 시작
        StartCoroutine(SpawnMouseRoutine());
    }

    void Update()
    {
        if (isGameOver) return;

        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                gameTime = 0;
                GameOver();
            }

            // 남은 시간 UI 갱신
            timerText.text = Mathf.CeilToInt(gameTime).ToString();
        }
    }

    // 게임 종료 처리
    void GameOver()
    {
        StopAllCoroutines();
        isGameOver = true;

        bool isSuccess = heartIndex >= 0; // 하트가 1개 이상 남았으면 성공

        // 미니게임 결과 저장 (MainScene에서 판단용)
        PlayerPrefs.SetInt("MiniGameResult", isSuccess ? 1 : 0);
        PlayerPrefs.Save();

        // 메인 씬으로 복귀
        SceneManager.LoadScene("MainScene");
    }

    // 외부 접근용 (예: 쥐가 맞았는지 판단 시)
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // 쥐 생성 코루틴 (2초마다 랜덤 위치에 쥐 생성)
    IEnumerator SpawnMouseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(0, mouseSpawnPoints.Length);
            GameObject mouse = Instantiate(mousePrefab, mouseSpawnPoints[rand], Quaternion.identity);

            // 쥐에게 고양이 위치와 GameManager 전달
            mouse.GetComponent<Mouse>().Initialize(cat.transform, this);
        }
    }

    // 쥐와 고양이 충돌 시 호출 (하트 감소)
    public void OnMouseHit()
    {
        if (heartIndex < 0)
            return;

        hearts[heartIndex].SetActive(false);
        heartIndex--;

        // 마지막 하트까지 소모했으면 게임 종료
        if (heartIndex < 0)
        {
            GameOver();
        }
    }

    // 버튼으로 고양이 위로 이동
    public void MoveCatUp()
    {
        if (currentCatIndex > 0)
        {
            currentCatIndex--;
            cat.transform.position = catPositions[currentCatIndex];
        }
    }

    // 버튼으로 고양이 아래로 이동
    public void MoveCatDown()
    {
        if (currentCatIndex < catPositions.Length - 1)
        {
            currentCatIndex++;
            cat.transform.position = catPositions[currentCatIndex];
        }
    }

    // 외부에서 현재 고양이 위치 가져올 때 사용
    public Vector3 GetCatPosition()
    {
        return cat.transform.position;
    }

    // 외부에서 현재 날짜 확인용 (속도 조정 등 커스터마이징 가능)
    public int GetCurrentDay()
    {
        return day;
    }
}
