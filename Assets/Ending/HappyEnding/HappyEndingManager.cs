using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 해피엔딩 대사와 배경 전환을 관리하는 스크립트
public class HappyEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;  // 대사 텍스트 출력용 UI

    // 배경 이미지 출력을 위한 SpriteRenderer와 이미지들
    public SpriteRenderer backgroundRenderer;
    public Sprite initialBackground;     // 초기 회색 배경 (Rain_Gray)
    public Sprite happyBackground;       // 따뜻해진 배경 (Happy_Ending_Background)
    public Sprite trustBackground;       // 고양이가 신뢰를 보인 배경 (Cat_Trust_Background)

    private int dialogueIndex = 0;       // 현재 대사 인덱스

    // 해피엔딩에서 순차적으로 보여줄 대사들
    private string[] dialogues = new string[]
    {
        "처음엔... 모든 게 흐렸어.",
        "사람도, 집도, 나 자신도.",
        "무서워서, 숨는 것밖에 못 했어.",
        "그런데 너는 기다려줬어.",
        "다가오지 않고, 도망치지도 않고.",
        "어느 순간, 세상이 조금씩... 색을 갖기 시작했어.",
        "너의 손길, 너의 목소리, 너의 존재로...",
        "그리고 알았어.",
        "널 믿어도 된다는 걸. \n나도... 사랑받아도 되는 존재라는 걸.",
        "「Happy Ending : 텅 빈 방」\n\t- 이제, 나의 세상은 당신으로 물들어요."
    };

    void Start()
    {
        dialogText.text = "";                           // 초기 텍스트 비우기
        backgroundRenderer.sprite = initialBackground;  // 첫 배경 설정
        ShowNextLine();                                 // 첫 대사 출력
    }

    // 클릭 시 다음 대사 출력
    public void OnDialogClicked()
    {
        ShowNextLine();
    }

    // 다음 대사를 출력하고, 타이밍에 맞춰 배경 변경
    void ShowNextLine()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogText.text = dialogues[dialogueIndex];

            // 5번째 대사 출력 시 배경을 따뜻한 이미지로 교체
            if (dialogueIndex == 4)
            {
                backgroundRenderer.sprite = happyBackground;
            }
            // 7번째 대사 출력 시 배경을 신뢰 표현 이미지로 교체
            else if (dialogueIndex == 6)
            {
                backgroundRenderer.sprite = trustBackground;
            }

            dialogueIndex++;
        }
        else
        {
            // 마지막 대사 후 타이틀 씬으로 이동
            StartCoroutine(GoToTitleScene());
        }
    }

    // 1.5초 후 타이틀 씬으로 전환
    IEnumerator GoToTitleScene()
    {
        PlayerPrefs.DeleteAll(); // 저장된 모든 정보 초기화 (호감도, 배고픔, 코인 등)
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1.5f); // 약간의 여운 시간
        SceneManager.LoadScene("TitleScene");
    }
}
