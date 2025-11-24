using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day 5: 고양이와의 정서적 교감 시도
public class Day5DialogueManager : MonoBehaviour
{
    // 대사 출력 텍스트 UI
    public TextMeshProUGUI dialogText;

    // 선택지 버튼 오브젝트
    public GameObject choose1Button;
    public GameObject choose2Button;
    public GameObject choose3Button;

    // 선택지 텍스트 UI
    public TextMeshProUGUI choose1Text;
    public TextMeshProUGUI choose2Text;
    public TextMeshProUGUI choose3Text;

    // 컷신 대사 인덱스
    private int dialogueIndex = 0;

    // 컷신 대사 내용
    private string[] initialDialogues = new string[] {
        "오늘은 조금 더 가까워지고 싶어.",
        "뭔가 더... 마음이 닿는 방법 없을까?"
    };

    // 선택 이후의 결과 대사 배열 및 인덱스
    private string[] resultDialogues = null;
    private int resultIndex = 0;

    // 선택 대기 상태
    private bool awaitingChoice = false;

    // 결과 대사 출력 중 여부
    private bool showingResult = false;

    // 시작 시 실행
    void Start()
    {
        // 선택지 숨김, 대사 초기화
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    // 대사창 클릭 시
    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return;

        if (showingResult)
        {
            ShowNextResult();  // 결과 대사 출력 중이면 다음 줄 출력
        }
        else
        {
            ShowNextDialogue(); // 컷신 대사 출력
        }
    }

    // 컷신 대사 한 줄씩 출력
    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();          // 컷신 끝나면 선택지 표시
            awaitingChoice = true;
        }
    }

    // 선택지 버튼 활성화 및 텍스트 지정
    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "그냥 곁에 앉아서 기다린다";
        choose2Text.text = "막 쓰다듬고 장난친다";
        choose3Text.text = "장난감 던져준다";
    }

    // 선택지 1: 호감도 +20
    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이가 천천히 다가와 무릎에 살짝 기대 앉는다.",
            "...이제 조금, 내 마음이 닿은 걸까?",
            "호감도 + 20"
        });
    }

    // 선택지 2: 호감도 -20
    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이가 짧게 야옹! 하고 도망간다.",
            "아... 또 너무 앞서갔나 봐.",
            "호감도 - 20"
        });
    }

    // 선택지 3: 호감도 변화 없음
    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 장난감을 향해 시선을 주다가 돌아눕는다.",
            "조금만 더 노력해볼까...",
            "호감도 + 0"
        });
    }

    // 선택 결과 대사 시작
    void StartResult(string[] lines)
    {
        // 선택지 숨김 처리
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);

        resultDialogues = lines;
        resultIndex = 0;
        showingResult = true;
        awaitingChoice = false;
        ShowNextResult();
    }

    // 선택 결과 대사 순차 출력
    void ShowNextResult()
    {
        if (resultDialogues != null && resultIndex < resultDialogues.Length)
        {
            dialogText.text = resultDialogues[resultIndex];
            resultIndex++;
        }
        else
        {
            // 결과 끝 → 메인 씬 이동
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}
