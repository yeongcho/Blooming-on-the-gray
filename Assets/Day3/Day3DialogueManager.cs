using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day3: 사료 선택 이벤트 처리 스크립트
public class Day3DialogueManager : MonoBehaviour
{
    // 대사 출력용 텍스트
    public TextMeshProUGUI dialogText;

    // 선택지 버튼 3개
    public GameObject choose1Button;
    public GameObject choose2Button;
    public GameObject choose3Button;

    // 선택지에 들어갈 텍스트
    public TextMeshProUGUI choose1Text;
    public TextMeshProUGUI choose2Text;
    public TextMeshProUGUI choose3Text;

    // 초기 대화 진행을 위한 인덱스
    private int dialogueIndex = 0;

    // 시작 시 보여줄 컷신 대사들
    private string[] initialDialogues = new string[] {
        "사료 가격이 이렇게나 비쌌나...",
        "물가 상승으로 인해 사료당 냥코인 2개!",
        "그래도 이 아이 건강이 제일 중요한데."
    };

    // 선택 이후 보여질 결과 대사들
    private string[] resultDialogues = null;
    private int resultIndex = 0;

    private bool awaitingChoice = false;   // 선택지를 기다리는 중인지
    private bool showingResult = false;    // 선택 결과를 출력 중인지

    void Start()
    {
        // 시작 시 버튼 숨기고 첫 대사 출력
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    // 대화창 클릭 시 처리 함수
    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return;

        if (showingResult)
        {
            ShowNextResult();  // 선택 결과 출력 중이면 다음 결과 출력
        }
        else
        {
            ShowNextDialogue();  // 아니면 일반 대사 진행
        }
    }

    // 컷신 대사 진행
    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();        // 컷신 종료 → 선택지 표시
            awaitingChoice = true;
        }
    }

    // 선택지 버튼 표시 및 텍스트 설정
    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "저렴하더라도 성분을 꼭 확인해서 고른다";
        choose2Text.text = "싸면 좋은 거 아냐? 아무거나 산다";
        choose3Text.text = "그냥 전에 쓰던 거 계속 쓴다";
    }

    // 선택지 1 클릭 시: 호감도 +20
    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 사료를 몇 번 킁킁대고, 만족한 듯 먹는다.",
            "입맛엔 맞는 모양이네. 다행이야.",
            "호감도 + 20"
        });
    }

    // 선택지 2 클릭 시: 호감도 -20
    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 냄새만 맡고 뒤돌아선다.",
            "...입에도 안 대네.",
            "호감도 - 20"
        });
    }

    // 선택지 3 클릭 시: 변화 없음
    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 천천히 먹기 시작한다.",
            "뭐, 익숙한 게 나을지도.",
            "호감도 + 0"
        });
    }

    // 결과 대사 배열을 설정하고 첫 줄부터 출력
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

    // 결과 대사 하나씩 출력
    void ShowNextResult()
    {
        if (resultDialogues != null && resultIndex < resultDialogues.Length)
        {
            dialogText.text = resultDialogues[resultIndex];
            resultIndex++;
        }
        else
        {
            // 결과 출력 끝났으면 메인 씬으로 이동
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}
