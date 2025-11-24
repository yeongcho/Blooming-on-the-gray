using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("Day0Scene");
    }
}