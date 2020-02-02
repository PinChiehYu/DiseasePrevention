using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class descript : MonoBehaviour
{
    public Button button_go;
    public Button button_healthy;
    public Button button_dying;
    public Button button_fever;
    public Button button_nomask;
    public Button button_BT;
    public Sprite[] sprite_healthy;
    public Sprite[] sprite_dying;
    public Sprite[] sprite_fever;
    public Sprite[] sprite_nomask;
    public Sprite[] sprite_BT;
    public Text test_des;
    public Image img_des;
    public pic_mode now_mode;
    public int index;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        now_mode = pic_mode.healthy;
        button_go.onClick.AddListener(delegate ()
        {
            Debug.Log("go");
            SceneManager.LoadScene("Main");//場景3,build settint放場景的順序
        });
        button_healthy.onClick.AddListener(delegate ()
        {
            Debug.Log("button_healthy");
            now_mode = pic_mode.healthy;
            test_des.text = "[守序仔]\n\n白色、很乖的有戴口罩\n\n※不可點擊";
        });
        button_dying.onClick.AddListener(delegate ()
        {
            Debug.Log("button_dying");
            now_mode = pic_mode.dying;
            test_des.text = "[病危仔]\n\n黑色、吐血";
        });
        button_fever.onClick.AddListener(delegate ()
        {
            Debug.Log("sprite_fever");
            now_mode = pic_mode.fever;
            test_des.text = "[發燒仔]\n\n紅色、發燒了不知道在嗨幾點";
        });
        button_nomask.onClick.AddListener(delegate ()
        {
            Debug.Log("sprite_nomask");
            now_mode = pic_mode.nomask;
            test_des.text = "[白目仔]\n\n灰色、白目不戴口罩";
        });
        button_BT.onClick.AddListener(delegate ()
        {
            Debug.Log("button_BT");
            now_mode = pic_mode.BT;
            test_des.text = "[鼻涕仔]\n\n綠色、鼻涕很噁\n\n※長按\n";
        });
    }

    // Update is called once per frame
    void Update()
    {
        switch(now_mode)
        {
            case pic_mode.healthy:
                img_des.sprite = sprite_healthy[index];
                break;
            case pic_mode.dying:
                img_des.sprite = sprite_dying[index];
                break;
            case pic_mode.fever:
                img_des.sprite = sprite_fever[index];
                break;
            case pic_mode.nomask:
                img_des.sprite = sprite_nomask[index];
                break;
            case pic_mode.BT:
                img_des.sprite = sprite_BT[index];
                break;
        }
        timer += Time.deltaTime;
        if (timer > 0.05f)
        {
            index++;
            timer = 0;
        }

        if (index >= 4)
            index = 0;
    }

    public enum pic_mode
    {
        healthy = 0,
        dying,
        fever,
        nomask,
        BT,
        end
    }
}
