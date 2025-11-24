using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day1: 컷신에서 고양이와의 첫 상호작용과 선택지를 처리하는 클래스
public class Day1DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;           // 대사를 출력할 텍스트 필드
    public GameObject choose1Button;             // 선택지 1 버튼
    public GameObject choose2Button;             // 선택지 2 버튼
    public GameObject choose3Button;             // 선택지 3 버튼

    public TextMeshProUGUI choose1Text;          // 선택지 1 텍스트
    public TextMeshProUGUI choose2Text;          // 선택지 2 텍스트
    public TextMeshProUGUI choose3Text;          // 선택지 3 텍스트

    private int dialogueIndex = 0;               // 현재 대사 인덱스
    private string[] initialDialogues = new string[] {
        "이 아이... 아직도 날 경계하는 걸까?",
        "어떻게 다가가야... 부담스럽지 않을까?"
    };

    private string[] resultDialogues = null;     // 선택 결과에 따른 대사들
    private int resultIndex = 0;                 // 선택 결과 대사 인덱스

    private bool awaitingChoice = false;         // 선택지를 기다리는 상태인지
    private bool showingResult = false;          // 선택 결과 대사를 출력 중인지

    void Start()
    {
        // 처음에 버튼은 숨기고, 대사는 비워둠
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    // 대사창 클릭 시 호출되는 함수
    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return; // 선택 대기 중이면 무시

        if (showingResult)
        {
            ShowNextResult();       // 선택 결과 대사 출력 중이면 그걸 계속 출력
        }
        else
        {
            ShowNextDialogue();     // 아니면 일반 대사 진행
        }
    }

    // 다음 기본 대사 출력
    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();         // 기본 대사 끝나면 선택지 표시
            awaitingChoice = true;
        }
    }

    // 선택지 버튼 및 텍스트 표시
    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "손등을 내밀고 냄새를 맡게 해본다";
        choose2Text.text = "일단 안아 들고 털을 만져본다";
        choose3Text.text = "그냥 혼자 있게 둔다";
    }

    // 선택지 1: 호감도 +20
    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 조심스럽게 냄새를 맡는다. 그리고 눈을 깜빡인다.",
            "...조금 마음의 문을 열어준 건가?",
            "호감도 + 20"
        });
    }

    // 선택지 2: 호감도 -20
    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 몸을 잔뜩 웅크리며 도망간다.",
            "앗... 너무 성급했나 봐.",
            "호감도 - 20"
        });
    }

    // 선택지 3: 호감도 변화 없음
    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 가만히 엎드린 채, 눈만 깜빡인다.",
            "...아직은 준비가 안 됐을지도 몰라.",
            "호감도 + 0"
        });
    }

    // 선택 결과 출력 시작
    void StartResult(string[] lines)
    {
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);

        resultDialogues = lines;
        resultIndex = 0;
        showingResult = true;
        awaitingChoice = false;
        ShowNextResult();
    }

    // 선택 결과 대사를 하나씩 출력
    void ShowNextResult()
    {
        if (resultDialogues != null && resultIndex < resultDialogues.Length)
        {
            dialogText.text = resultDialogues[resultIndex];
            resultIndex++;
        }
        else
        {
            // 결과 출력 완료 -> MainScene으로 이동
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}
