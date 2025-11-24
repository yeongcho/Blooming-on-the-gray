using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Bad Ending 화면을 담당하는 매니저 클래스
public class BadEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // 대사를 출력할 텍스트 UI
    private int dialogueIndex = 0;     // 현재 출력 중인 대사 인덱스

    // Bad Ending 대사들
    private string[] dialogues = new string[]
    {
        "오늘은, 이상하리만치 조용했다.",
        "이름을 불러도, 인기척이 없다.",
        "작은 쿠션 위에 남은 건...",
        "따뜻했던 온기조차 사라진 발자국뿐.",
        "...",
        "나는 아무 말도 할 수 없었다.",
        "「Bad Ending : 텅 빈 방」\n\t- 떠난 건 아이였을까, 나였을까."
    };

    // 시작 시 첫 대사 출력
    void Start()
    {
        dialogText.text = "";
        ShowNextLine();
    }

    // 대사창 클릭 시 다음 대사 출력
    public void OnDialogClicked()
    {
        ShowNextLine();
    }

    // 대사 순차 출력 함수
    void ShowNextLine()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogText.text = dialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            // 모든 대사 출력 후 타이틀 씬으로 전환
            StartCoroutine(GoToTitleScene());
        }
    }

    // 씬 전환 및 상태 초기화
    IEnumerator GoToTitleScene()
    {
        PlayerPrefs.DeleteAll();   // 저장된 모든 정보 초기화 (호감도, 배고픔, 코인 등)
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1.5f); // 약간의 여운 시간
        SceneManager.LoadScene("TitleScene");  // 타이틀 화면으로 이동
    }
}
