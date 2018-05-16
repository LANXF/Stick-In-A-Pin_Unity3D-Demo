using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Transform startPoint;
    private Transform spawnPoint;
    public GameObject pinPrefab;
    private Pin currPin;
    private Camera mainCamera;
    private float colorSeppd = 5;

    //游戏分数
    private int score = 0;
    public Text scoreText;

    //游戏是否结束
    private bool isGameOver = false;


	// Use this for initialization
	void Start () {
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        SpawnPin();
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if (isGameOver) return;
		if(Input.GetMouseButtonDown(0))
        {
            score++;
            scoreText.text = score.ToString();
            currPin.startFly();
            SpawnPin();
        }
	}

    void SpawnPin() {
        currPin = GameObject.Instantiate(pinPrefab, spawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
    }

    public void GameOver()
    {
        if (isGameOver) return;

        GameObject.Find("Circle").GetComponent<RotateSelf>().enabled = false;
        StartCoroutine(GameOverAnimation());

        isGameOver = true;
    }

    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, colorSeppd * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, colorSeppd * Time.deltaTime);
            if(Mathf.Abs(mainCamera.orthographicSize - 4) < .01f)
            {
                break;
            }
            yield return 0;
        }

        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
