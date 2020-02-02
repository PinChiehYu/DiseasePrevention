using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    private GameManager gameManager;
    public Text text_sec;
    public Text[] text_scr;
    
    public Sprite[] hp_icon;
    public Button button_logo;
    public Button[] button_mode;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.hpUpdate += HpUIUpdate;
        gameManager.scoreUpdate += ScoreUIUpdate;

        ShowPausePanel(false);
        button_logo.onClick.AddListener(delegate ()
        {
            if (gameManager.isPause) return;
            gameManager.isPause = true;
            ShowPausePanel(true);
            Debug.Log("Paused");
        });

        button_mode[0].onClick.AddListener(delegate ()
        {
            Debug.Log("click home");
            SceneManager.LoadScene(0);
        });

        button_mode[1].onClick.AddListener(delegate ()
        {
            Debug.Log("click leave");
            Application.Quit();
        });

        button_mode[2].onClick.AddListener(delegate ()
        {
            Debug.Log("click continue");
            gameManager.isPause = false;
            ShowPausePanel(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.nowtime > 0)
        {
            //m_slider.value = nowtime;
            text_sec.text = string.Format("{0}", (int)(gameManager.nowtime));
        }
        else
        {
            Debug.Log("time up!");
            SceneManager.LoadScene("Result");//build settint放場景的順序
        }
    }
    void HpUIUpdate(int hp)
    {
        button_logo.GetComponent<Image>().sprite = hp_icon[hp];
    }
    void ScoreUIUpdate(int score)
    {
        text_scr[0].text = string.Format("{0}", (score % 10));
        text_scr[1].text = string.Format("{0}", (int)((score / 10) % 10));
        text_scr[2].text = string.Format("{0}", (int)(score / 100));
    }
    void ShowPausePanel(bool show)
    {
        int delta = 1000;
        if (!show) delta = -1000;
        Debug.Log(this.name + "PausePanel opened = " + show);
        for (int i = 0; i < 3; i++)
        {
            Vector3 p = button_mode[i].transform.position;
            p.x += delta;// (float)location[i];
            button_mode[i].transform.position = p;
        }
    }
}
