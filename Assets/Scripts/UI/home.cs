using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class home : MonoBehaviour
{
    public Button button_start;
    public Button button_leave;
    // Start is called before the first frame update
    void Start()
    {
        button_start.onClick.AddListener(delegate ()
        {
            Debug.Log("start");
            SceneManager.LoadScene("Infomation");//場景3,build settint放場景的順序
        });
        button_leave.onClick.AddListener(delegate ()
        {
            Debug.Log("leave");
            Application.Quit();
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
