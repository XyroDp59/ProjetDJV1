using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TMP_Text GameOverText;
    public TMP_Text Score;
    public TMP_Text Wave;
    public TMP_Text Friends;

    public static HUD singleton;

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        Alien.Score = 0;
        SpaceShip.Wave = 0;
        Cow.Cows.Clear();
        Cow.Friends = 0;
    }
    public void GameOver(string s)
    {
        singleton.GameOverText.text = s;
        singleton.GameOverText.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
