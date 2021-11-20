using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Button QuitButton;
    [SerializeField] Transform PlanetXform;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        PlanetXform.RotateAround(transform.up, -2f * Time.deltaTime);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
