using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
    public Button button_back;
    public int Score;
    public Image[] image_ch;
    public Sprite[] sprite_play;
    public Sprite[] sprite_fever;
    public Sprite[] sprite_dying;
    public Sprite[] sprite_BT;
    public Text text_score;
    public Text text_word;
    public int index;
    public int ilocation;
    public Vector3 p;
    public Vector3 vscale;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        p = text_score.transform.position;
        p.x += 2000;
        text_score.transform.position = p;
        ilocation = 2000;
        

        button_back.onClick.AddListener(delegate ()
        {
            Debug.Log("back");
            SceneManager.LoadScene(2);//場景3,build settint放場景的順序
        });
    }
    void setText()
    {
        text_score.text = string.Format("{0}", (Score));
        if (Score <= 0)
        {
            text_word.text = "完蛋了!!桃機大淪陷!!";
        }
        else if (Score > 0 && Score <= 50)
        {
            text_word.text = "好險好險!!辛苦了!!";
        }
        else if (Score > 50 && Score <= 100)
        {
            text_word.text = "不錯呦!!給你一個讚讚讚!!";
        }
        else
        {
            text_word.text = "太棒了!!防疫大成功!!";
        }
    }
    // Update is called once per frame
    void Update()
    {
        setText();
        if (ilocation > 0)
        {
            p = text_score.transform.position;
            p.x -= 20;
            ilocation -= 20;
            text_score.transform.position = p;
        }
        timer += Time.deltaTime;
        if (timer > 0.05f)
        {
            float fscale = (float)(1.0 + 0.1 * index);
            vscale = new Vector3(fscale, fscale, fscale);
            timer = 0;
        }
        if (Score <= 0)
        {
            image_ch[0].sprite = sprite_fever[index];
            image_ch[1].sprite = sprite_dying[index];
            image_ch[2].sprite = sprite_BT[index];
        }
        else 
        { 
            image_ch[0].sprite = sprite_play[index];
            image_ch[1].sprite = sprite_play[4];
            image_ch[2].sprite = sprite_play[index];

            image_ch[1].transform.localScale = vscale;
        }   
        

        text_word.transform.localScale = vscale;

        index++;
        if (index >= 4)
        {
            index = 0;
        }
    }
}
