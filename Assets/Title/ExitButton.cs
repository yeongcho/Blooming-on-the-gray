using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    void OnMouseEnter()
    {
        spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        spriteRenderer.sprite = defaultSprite;
    }

    void OnMouseDown()
    {
        Application.Quit();

        // 유니티 에디터에서 종료 테스트용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}