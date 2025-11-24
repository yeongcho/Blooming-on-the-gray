using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Day0: 컷신(스토리 대사) 및 배경 전환을 관리하는 스크립트
public class Day0Manager : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // 대사를 출력할 UI 텍스트

    public SpriteRenderer backgroundRenderer; // 배경 이미지를 그릴 SpriteRenderer
    public Sprite catRainBackgroundSprite;    // 고양이가 등장하는 장면의 배경 스프라이트

    private int dialogueIndex = 0; // 현재 출력 중인 대사의 인덱스

    // 출력할 대사 목록
    private string[] dialogues = new string[]
    {
        "비 냄새가 진하게 밴 골목에서 \n낡고 젖은 상자가 눈에 들어왔다.",
        "조심스레 상자를 열자,",
        "안에는 작은 고양이 한 마리가 웅크리고 있었다.",
        "말라붙은 눈동자. \n움직이지 않는 몸.",
        "아무 말도, \n아무 소리도 없다.",
        "이상하게도... 나는 망설이지 않았다.",
        "그저 조용히, 이 아이를 품에 안았다.",
        "그렇게, \n나와 이 아이의 시간이 시작되었다."
    };

    void Start()
    {
        dialogText.text = ""; // 처음에는 빈 문자열로 시작
        ShowNextLine();       // 첫 대사 출력
    }

    // 클릭 시 호출되는 함수: 다음 대사를 출력
    public void OnDialogClicked()
    {
        ShowNextLine();
    }

    // 대사 하나를 출력하고, 특정 조건에서 배경도 변경
    void ShowNextLine()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogText.text = dialogues[dialogueIndex];

            // 특정 대사일 경우 배경을 고양이 장면으로 변경
            if (dialogues[dialogueIndex] == "안에는 작은 고양이 한 마리가 웅크리고 있었다.")
            {
                backgroundRenderer.sprite = catRainBackgroundSprite;
            }

            dialogueIndex++; // 다음 대사로 인덱스 이동
        }
        else
        {
            // 모든 대사가 끝난 후 1.5초 기다렸다가 Day1Scene으로 전환
            StartCoroutine(GoToTitleScene());
        }
    }

    // 씬 전환 코루틴: Day1Scene으로 이동
    IEnumerator GoToTitleScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Day1Scene");
    }
}
