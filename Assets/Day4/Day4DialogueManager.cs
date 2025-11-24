using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day 4: 고양이와 놀아주기 관련 선택지 처리
public class Day4DialogueManager : MonoBehaviour
{
    // 대화 텍스트 UI
    public TextMeshProUGUI dialogText;

    // 선택지 버튼들
    public GameObject choose1Button;
    public GameObject choose2Button;
    public GameObject choose3Button;

    // 선택지 텍스트들
    public TextMeshProUGUI choose1Text;
    public TextMeshProUGUI choose2Text;
    public TextMeshProUGUI choose3Text;

    // 컷신 대사 인덱스
    private int dialogueIndex = 0;

    // 컷신 대사 배열
    private string[] initialDialogues = new string[] {
        "같이 놀고 싶은데, 계속 등을 돌리네.",
        "내가 뭘 잘못한 걸까?"
    };

    // 선택 결과 대사
    private string[] resultDialogues = null;
    private int resultIndex = 0;

    // 현재 선택 대기 상태 여부
    private bool awaitingChoice = false;

    // 선택 결과 대사 출력 중인지 여부
    private bool showingResult = false;

    // 초기화
    void Start()
    {
        // 버튼 숨기고 대사 초기화
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);
        dialogText.text = "";
        ShowNextDialogue();
    }

    // 대화창 클릭 시 호출되는 함수
    public void OnDialogBarClicked()
    {
        if (awaitingChoice) return;

        if (showingResult)
        {
            ShowNextResult(); // 결과 대사 출력 중이면 계속 출력
        }
        else
        {
            ShowNextDialogue(); // 컷신 대사 진행
        }
    }

    // 컷신 대사 진행 함수
    void ShowNextDialogue()
    {
        if (dialogueIndex < initialDialogues.Length)
        {
            dialogText.text = initialDialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            ShowChoices();        // 컷신 끝나면 선택지 표시
            awaitingChoice = true;
        }
    }

    // 선택지 버튼 및 텍스트 활성화
    void ShowChoices()
    {
        choose1Button.SetActive(true);
        choose2Button.SetActive(true);
        choose3Button.SetActive(true);

        choose1Text.text = "고양이가 좋아하는 장난감을 찾으려고 종류를 바꿔본다";
        choose2Text.text = "한 번 놀자고 던졌는데 안 와서 포기한다";
        choose3Text.text = "고양이는 혼자 노는 거 좋아하지 않나?";
    }

    // 선택지 1 클릭 시: 호감도 +20
    public void OnChoose1()
    {
        PlayerPrefs.SetInt("affection", PlayerPrefs.GetInt("affection", 0) + 20);
        StartResult(new string[] {
            "고양이의 눈이 반짝인다.",
            "꼬리를 살랑이며 장난감을 향해 달린다!",
            "역시 맞는 게 있었구나!",
            "호감도 + 20"
        });
    }

    // 선택지 2 클릭 시: 호감도 -20
    public void OnChoose2()
    {
        PlayerPrefs.SetInt("affection", Mathf.Max(0, PlayerPrefs.GetInt("affection", 0) - 20));
        StartResult(new string[] {
            "고양이는 조용히 창밖을 바라본다.",
            "그냥... 오늘은 관심이 없는 걸까...",
            "호감도 - 20"
        });
    }

    // 선택지 3 클릭 시: 변화 없음
    public void OnChoose3()
    {
        StartResult(new string[] {
            "고양이는 가끔 장난감을 툭툭 건드릴 뿐이다.",
            "...그래도 나랑 노는 건 싫은 건 아닐까?",
            "호감도 + 0"
        });
    }

    // 선택 결과 대사 시작
    void StartResult(string[] lines)
    {
        // 선택지 버튼 숨김
        choose1Button.SetActive(false);
        choose2Button.SetActive(false);
        choose3Button.SetActive(false);

        resultDialogues = lines;
        resultIndex = 0;
        showingResult = true;
        awaitingChoice = false;
        ShowNextResult();
    }

    // 결과 대사 순차 출력
    void ShowNextResult()
    {
        if (resultDialogues != null && resultIndex < resultDialogues.Length)
        {
            dialogText.text = resultDialogues[resultIndex];
            resultIndex++;
        }
        else
        {
            // 결과 대사 끝 → 메인 씬으로 이동
            showingResult = false;
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
    }
}
