using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day2: 고양이의 배변 훈련에 대한 대화 및 선택지를 처리하는 스크립트
public class Day2DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;          // 대사를 출력할 텍스트 UI
    public GameObject choose1Button;            // 선택지 1 버튼
    public GameObject choose2Button;            // 선택지 2 버튼
    public GameObject choose3Button;            // 선택지 3 버튼

    public TextMeshProUGUI choose1Text;         // 선택지 1의 텍스트
    public TextMeshProUGUI choose2Text;         // 선택지 2의 텍스트
    public TextMeshProUGUI choose3Text;         // 선택지 3의 텍스트

    private int dialogueIndex = 0;              // 현재 대사 인덱스

    // 초기 대사들: 선택 전까지 출력되는 컷신 대사
    private string[] initialDialogues = new string[] {
        "고양이 화장실... 인터넷에서 봤는데, 생각보다 까다롭네.",
        "이 아이는 어디에 익숙해져야 할까..."
    };

    private string[] resultDialogues = null;    // 선택 결과에 따라 출력될 대사 배열
    private int resultIndex = 0;                // 선택 결과 대사 진행 인덱스

    private bool awaitingChoice = false;        // 선택지를 기다리는 중인지 여부
    private bool showingResult = false;         // 선택 결과 대사를 출력 중인지 여부

    void Start()
    {
        // 시작 시 선택지 숨기기, 대사 초기화
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    // 대화창 클릭 시 호출
    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return; // 선택 대기 중이면 무시

        if (showingResult)
        {
            ShowNextResult(); // 결과 출력 중이면 계속 출력
        }
        else
        {
            ShowNextDialogue(); // 일반 대사 출력
        }
    }

    // 다음 대사 출력
    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();           // 대사가 끝났으면 선택지 표시
            awaitingChoice = true;
        }
    }

    // 선택지 텍스트와 버튼 활성화
    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "고양이가 자주 가는 구석에 모래 화장실을 둔다";
        choose2Text.text = "마른 수건으로 배를 문지르며 자극한다";
        choose3Text.text = "그냥 두면 알아서 하지 않을까?";
    }

    // 선택지 1 클릭 시: 호감도 +20
    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이는 조심스럽게 냄새를 맡고, 다음엔 자연스럽게 사용하기 시작한다.",
            "다행이다... 조금 안심이네.",
            "호감도 + 20"
        });
    }

    // 선택지 2 클릭 시: 호감도 -20
    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 몸을 틀며 강하게 밀쳐낸다.",
            "앗... 너무 과했나...",
            "호감도 - 20"
        });
    }

    // 선택지 3 클릭 시: 호감도 변화 없음
    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 방 한 구석에 실수를 하고 만다.",
            "...내가 너무 방심했나 봐.",
            "호감도 + 0"
        });
    }

    // 결과 대사 시작 및 선택지 숨기기
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
            // 결과 대사 종료 -> 메인 씬으로 전환
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}
