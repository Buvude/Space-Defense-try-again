using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        Debug.Log("Return To Monkey!!!!!");
        SceneManager.LoadScene(0);
    }
}
