using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //public Slider m_slider;
    
    public int hp;
    public int score;
    public bool isPause;
    public float nowtime;
    private bool loaded;

    public Action<int> hpUpdate, scoreUpdate;

    void LoadScene(Scene prev, Scene next)
    {
        if (this == null) return;
        StartCoroutine(SendScoreToUI());
    }
    void LoadDone(Scene scene, LoadSceneMode loadSceneMode) { loaded = true; }
    private IEnumerator SendScoreToUI()
    {
        yield return new WaitUntil(() => { return loaded; });
        FindObjectOfType<score>().Score = score;
        Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += LoadScene;
        SceneManager.sceneLoaded += LoadDone;
        nowtime = 20.2f;
        hp = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPause) return;
        nowtime -= Time.deltaTime;
    }
    public void ChangeHP(int amount) //1 = heal; -1 = damage
    {
        if (amount == 1 && hp == 5) return;
        if (amount == -1 && hp == 0) return;

        hp += amount;
        hpUpdate?.Invoke(hp);

        if (hp == 0)
        {
            ChangeScore(-score);
            Debug.Log("you die!");
            SceneManager.LoadScene("Result");
        }
    }
    public void ChangeScore(int amount)
    {
        score += amount;
        scoreUpdate?.Invoke(score);
    }
}