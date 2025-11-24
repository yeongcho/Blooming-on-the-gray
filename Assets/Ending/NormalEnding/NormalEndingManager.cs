using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Normal Ending 씬에서 대사를 순차적으로 출력하고, 종료 시 TitleScene으로 전환하는 스크립트
public class NormalEndingManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText; // UI에 표시될 대사 텍스트

    private int dialogueIndex = 0;     // 현재 출력 중인 대사 인덱스

    // 출력할 노멀 엔딩 대사 배열
    private string[] dialogues = new string[]
    {
        "오늘도 이 아이는 같은 자리에 앉아 있었다.",
        "아직은 조금 멀게 느껴진다.",
        "하지만, 예전처럼 숨거나 등을 돌리진 않는다.",
        "같이 있는 시간이 길어졌지만,",
        "완전히 마음을 연 건 아니야.",
        "그래도... 떠나지 않았다는 것만으로도",
        "왠지 안도감이 든다.",
        "나를 보는 건 아닐지도 모르지만,",
        "그 조용한 반응 하나에",
        "‘아직 기회는 있구나’ 싶었다.",
        "「Normal Ending : 창밖을 보는 아이」\n\t- 아직은, 멀지만 닿을 수 있을지도 몰라."
    };

    void Start()
    {
        dialogText.text = "";  // 초기 대사 비우기
        ShowNextLine();        // 첫 번째 대사 출력
    }

    // 클릭 시 다음 대사 출력
    public void OnDialogClicked()
    {
        ShowNextLine();
    }

    // 대사를 순차적으로 출력
    void ShowNextLine()
    {
        if (dialogueIndex < dialogues.Length)
        {
            dialogText.text = dialogues[dialogueIndex];
            dialogueIndex++;
        }
        else
        {
            // 모든 대사 출력이 끝나면 타이틀 씬으로 전환
            StartCoroutine(GoToTitleScene());
        }
    }

    // 씬 전환 전 약간의 여유를 두고 타이틀 씬으로 이동
    IEnumerator GoToTitleScene()
    {
        PlayerPrefs.DeleteAll(); // 저장된 모든 정보 초기화 (호감도, 배고픔, 코인 등)
        PlayerPrefs.Save();

        yield return new WaitForSeconds(1.5f);// 약간의 여운 시간
        SceneManager.LoadScene("TitleScene");
    }
}
