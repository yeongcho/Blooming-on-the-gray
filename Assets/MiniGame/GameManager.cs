using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject cat;
    public GameObject mousePrefab;
    public GameObject[] hearts;
    public TextMeshProUGUI timerText;

    private int currentCatIndex = 0;
    private float gameTime = 20f;
    private int heartIndex = 2;
    private bool isGameOver = false;
    private Vector3[] catPositions;
    private Vector3[] mouseSpawnPoints;
    private int day = 1;

    void Start()
    {
        day = PlayerPrefs.GetInt("day", 1);

        catPositions = new Vector3[]
        {
            new Vector3(-6.7543f, 2.9099f, 0),
            new Vector3(-6.7742f, 0.6024f, 0),
            new Vector3(-9.0257f, -1.1102f, 0),
            new Vector3(-4.7262f, -2.5253f, 0)
        };

        mouseSpawnPoints = new Vector3[]
        {
            new Vector3(6.43f, 2.9549f, 0),
            new Vector3(6.43f, 0.6745002f, 0),
            new Vector3(6.43f, -1.1373f, 0),
            new Vector3(6.43f, -2.6100f, 0)
        };

        cat.transform.position = catPositions[currentCatIndex];

        StartCoroutine(SpawnMouseRoutine());
    }

    void Update()
    {
        if (isGameOver) return;

        if (gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                gameTime = 0;
                GameOver();
            }
            timerText.text = Mathf.CeilToInt(gameTime).ToString();
        }
    }

    void GameOver()
    {
        StopAllCoroutines();
        isGameOver = true;

        bool isSuccess = heartIndex >= 0;
        int resultValue;

        if (isSuccess)
        {
            resultValue = 1;
        }
        else
        {
            resultValue = 0;
        }

        PlayerPrefs.SetInt("MiniGameResult", resultValue);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainScene");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    IEnumerator SpawnMouseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            int rand = Random.Range(0, mouseSpawnPoints.Length);
            GameObject mouse = Instantiate(mousePrefab, mouseSpawnPoints[rand], Quaternion.identity);
            mouse.GetComponent<Mouse>().Initialize(cat.transform, this);
        }
    }

    public void OnMouseHit()
    {
        if (heartIndex < 0)
            return;

        hearts[heartIndex].SetActive(false);
        heartIndex--;

        if (heartIndex < 0)
        {
            GameOver();
        }
    }

    public void MoveCatUp()
    {
        if (currentCatIndex > 0)
        {
            currentCatIndex--;
            cat.transform.position = catPositions[currentCatIndex];
        }
    }

    public void MoveCatDown()
    {
        if (currentCatIndex < catPositions.Length - 1)
        {
            currentCatIndex++;
            cat.transform.position = catPositions[currentCatIndex];
        }
    }

    public Vector3 GetCatPosition()
    {
        return cat.transform.position;
    }

    public int GetCurrentDay()
    {
        return day;
    }
}