using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
    // UI 요소들
    public TextMeshProUGUI heartAmountText;
    public TextMeshProUGUI coinAmountText;
    public TextMeshProUGUI eatAmountText;
    public Image[] heartImages; // 호감도 20 ~ 100 단위 하트 이미지
    public Image eatImage;

    // 하트(호감도) 및 포만감(배고픔) 상태에 따른 스프라이트들
    public Sprite heartGray;
    public Sprite heartPink;
    public Sprite eatGray;
    public Sprite eatFull;

    public Animator catAnimator; // 고양이 색 변화 애니메이션 컨트롤용

    // 게임 상태 값들
    private int affection = 0;    // 호감도 (0~100)
    private int coins = 0;        // 코인 개수
    private int fullness = 0;     // 포만감 (0~100)
    private int day = 1;          // 현재 날짜 (1~5)
    private bool hasFedToday = false;    // 오늘 밥 줬는지 여부
    private bool hasPlayedToday = false; // 오늘 놀아줬는지 여부

    void Start()
    {
        LoadState(); // 저장된 상태 불러오기

        // 미니게임 결과 반영 (성공 여부 1이면 성공, 0이면 실패)
        if (PlayerPrefs.HasKey("MiniGameResult"))
        {
            int result = PlayerPrefs.GetInt("MiniGameResult");
            bool isSuccess = result == 1;
            OnMiniGameResult(isSuccess);

            PlayerPrefs.DeleteKey("MiniGameResult");
            SaveState();
        }

        UpdateUI(); // UI 업데이트
    }

    // 밥 주기 버튼 클릭 시 호출
    public void OnFeedButtonClicked()
    {
        int cost = (day >= 3) ? 2 : 1; // 3일차부터 사료값 2냥

        if (hasFedToday || coins < cost)
            return;

        coins -= cost;
        fullness = 100;      // 포만감 100으로 채움
        hasFedToday = true;
        SaveState();
        UpdateUI();
    }

    // 놀아주기 버튼 클릭 시 호출
    public void OnPlayButtonClicked()
    {
        if (hasPlayedToday)
            return;

        hasPlayedToday = true;
        SaveState();
        SceneManager.LoadScene("MiniGameScene");
    }

    // 하루 넘기기 버튼 클릭 시 호출
    public void OnNextDayButtonClicked()
    {
        // 밥을 안 줘서 포만감이 0이면 배드엔딩
        if (fullness == 0)
        {
            SceneManager.LoadScene("BadEndingScene");
            return;
        }

        day++; // 날짜 증가
        fullness = 0;
        hasFedToday = false;
        hasPlayedToday = false;

        string nextSceneName;

        // 6일차는 엔딩 분기
        if (day == 6)
        {
            if (affection >= 80)
                nextSceneName = "HappyEndingScene";
            else if (affection >= 50)
                nextSceneName = "NormalEndingScene";
            else
                nextSceneName = "BadEndingScene";
        }
        else
        {
            nextSceneName = $"Day{day}Scene"; // Day2Scene, Day3Scene 등으로 이동
        }

        SaveState();
        SceneManager.LoadScene(nextSceneName);
    }

    // 미니게임 결과에 따라 affection과 coins 업데이트
    public void OnMiniGameResult(bool success)
    {
        if (success)
        {
            coins += 2;
            affection += 10;
        }
        else
        {
            affection = Mathf.Max(0, affection - 10);
        }

        UpdateUI();
    }

    // UI 텍스트와 이미지 갱신
    void UpdateUI()
    {
        coinAmountText.text = $"{coins} 코인";
        eatAmountText.text = $"{fullness} / 100";
        eatImage.sprite = (fullness > 0) ? eatFull : eatGray;

        // 호감도 하트 갱신
        for (int i = 0; i < heartImages.Length; i++)
        {
            int threshold = (i + 1) * 20;
            heartImages[i].sprite = (affection >= threshold) ? heartPink : heartGray;
        }

        heartAmountText.text = $"{affection} / 100";

        // 고양이 색 애니메이션 전환
        if (catAnimator != null)
        {
            catAnimator.ResetTrigger("ToGray");
            catAnimator.ResetTrigger("ToSemiColor");
            catAnimator.ResetTrigger("ToColor");

            if (affection < 40)
                catAnimator.SetTrigger("ToGray");
            else if (affection < 80)
                catAnimator.SetTrigger("ToSemiColor");
            else
                catAnimator.SetTrigger("ToColor");
        }
    }

    // 현재 상태 저장
    void SaveState()
    {
        PlayerPrefs.SetInt("affection", affection);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("fullness", fullness);
        PlayerPrefs.SetInt("day", day);
        PlayerPrefs.SetInt("fed", hasFedToday ? 1 : 0);
        PlayerPrefs.SetInt("played", hasPlayedToday ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 저장된 상태 불러오기
    void LoadState()
    {
        affection = PlayerPrefs.GetInt("affection", 0);
        coins = PlayerPrefs.GetInt("coins", 0);
        fullness = PlayerPrefs.GetInt("fullness", 0);
        day = PlayerPrefs.GetInt("day", 1);
        hasFedToday = PlayerPrefs.GetInt("fed", 0) == 1;
        hasPlayedToday = PlayerPrefs.GetInt("played", 0) == 1;
    }
}
