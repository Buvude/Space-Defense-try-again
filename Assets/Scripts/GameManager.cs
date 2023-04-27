using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TowerDefenseScript towerDefense;
    public int health;
    public TextMeshProUGUI healthText;
    public float oxygen;
    public TextMeshProUGUI oxygenText;
    public bool isRoundActive = true;
    public TextMeshProUGUI timerText;
    private int secondsToEnd;
    public int timeOfRound = 60;
    public GameObject pauseScreen;
    public bool isGamePaused;
    public bool isGameActive;
    public bool isShipDamaged;
    public float oxygenDrain = 1.0f;
    public GameObject gameOverText;
    public int cooldown = 10;
    public int breakStateMin = 1;
    public int breakStateMax = 7;
    public int breakState;
    public float checkBetween = 5.0f;
    public float repeatRate = 1.0f;
    public TextMeshProUGUI startScreen;
    public TextMeshProUGUI currencyText;
    public Enemy enemy;

    // Start is called before the first frame update
    void Awake()
    {
        health = 100;
        healthText.text = health + "%";
        oxygen = 100.0f;
        oxygenText.text = oxygen + "%";
        secondsToEnd = timeOfRound;
        isGamePaused = false;
        StartGame();//TODO DELETE THIS
        ShipStatus();
    }

    public void resetRountTimer()
    {
        secondsToEnd = 60;
    }
    public int getSecondsLeft()
    {
        return secondsToEnd;
    }
    public void StartGame()
    {
        isGameActive = true;
        //startScreen.gameObject.SetActive(false);
        healthText.gameObject.SetActive(true);
        oxygenText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        currencyText.gameObject.SetActive(true);
        StartCoroutine(Timer());
        currencyText.text = towerDefense.scrap + " Scrap";
    }

    void OxygenDrain()
    {
        if (isGameActive)
        {

            oxygen -= oxygenDrain * Time.deltaTime;
            oxygenText.text = oxygen + "%";


            if (oxygen == 0)
            {
                GameOver();
            }

        }
    }

    public void UpdateTimer()
    {
        timerText.text = $"{secondsToEnd}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P) && !isGamePaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ResumeGame();
        }
        if (breakState == 6)
        {
            isShipDamaged = true;
        }
        if (isShipDamaged)
        {
            OxygenDrain();
        }
    }

    void PauseGame()
    {
        // can't pause in title and game over screen
        if (isGameActive)
        {
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
            isGamePaused = true;
            Debug.Log("Game is paused.");
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
       
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
            Debug.Log("Game will resume");
        
    }

    public void GameOver()
    {
        StartCoroutine(GameOverScreen);
    }

    IEnumerator GameOverScreen()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(false);
        oxygenText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

    }

    IEnumerator Timer()
    {
        if (!isGamePaused && isGameActive)
        {
            while (isRoundActive)
            {
                UpdateTimer();

                if (secondsToEnd == 0)
                {
                    isRoundActive = false;
                }

                yield return new WaitForSeconds(1);
                secondsToEnd--;
            }
        }
    }


    //Ship Damage Stuff
    public void ShipStatus()
    {
        if (isGameActive)
        {
            StartCoroutine(BreakShip());
        }
    }

    public void UpdateHealth(int healthToChange)
    {
        health += healthToChange;
        healthText.text = health + "%";
        if (health == 0)
        {
            GameOver();
        }
    }

    public void UpdateScrap()
    {
        towerDefense.scrap += enemy.scrapToChange;
        currencyText.text = towerDefense.scrap + " Scrap";
    }

    public IEnumerator BreakShip()
    {
        //for (int i = 0; i < breakState; i++)
        while (!isShipDamaged)
        {
            breakState = Random.Range(breakStateMin, breakStateMax);
            yield return new WaitForSeconds(checkBetween);
        }
    }
}