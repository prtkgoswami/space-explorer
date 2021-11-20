using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button QuitButton;

    [Header("Options")]
    [SerializeField] Slider ShowAxis;
    [SerializeField] Slider ShowOrbit;
    [SerializeField] Slider ShowDistances;
    [SerializeField] Slider ShowTrails;
    [SerializeField] Slider PlayMusic;

    bool isLive;
    SpaceCraftMovement sc;
    AudioManager am;
    CelestialStat[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(delegate
        {
            isLive = !isLive;
            ChangeActivityStatus();
        });
        QuitButton.onClick.AddListener(QuitApplication);

        ShowAxis.value = 1;
        ShowOrbit.value = 1;
        ShowDistances.value = 1;
        ShowTrails.value = 1;
        PlayMusic.value = 1;

        ShowAxis.onValueChanged.AddListener(ChangeAxisVisibility);
        ShowOrbit.onValueChanged.AddListener(ChangeOrbitVisibility);
        ShowDistances.onValueChanged.AddListener(ChangeDistanceVisibility);
        ShowTrails.onValueChanged.AddListener(ChangeTrailVisibility);
        PlayMusic.onValueChanged.AddListener(ChangeSoundStatus);

        sc = FindObjectOfType<SpaceCraftMovement>();
        am = FindObjectOfType<AudioManager>();
        celestials = FindObjectsOfType<CelestialStat>();
        isLive = true;
        ChangeActivityStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLive = !isLive;
            ChangeActivityStatus();
        }
    }

    public bool IsLive()
    {
        return isLive;
    }

    void ChangeActivityStatus()
    {
        if (isLive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            PausePanel.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            PausePanel.SetActive(true);
        }
    }

    void QuitApplication()
    {
        Application.Quit();
    }

    void ChangeAxisVisibility(float value)
    {
        Debug.Log(value);
        if (value == 0f)
        {
            foreach (CelestialStat celestial in celestials)
            {
                celestial.EnableAxis(false);
            }
        }
        else
        {
            foreach (CelestialStat celestial in celestials)
            {
                celestial.EnableAxis(true);
            }
        }
    }

    void ChangeOrbitVisibility(float value)
    {
        if (value == 0)
        {
            foreach (CelestialStat celestial in celestials)
            {
                celestial.EnableOrbits(false);
            }
        }
        else
        {
            foreach (CelestialStat celestial in celestials)
            {
                celestial.EnableOrbits(true);
            }
        }
    }

    void ChangeTrailVisibility(float value)
    {
        if (value == 0)
        {
            sc.EnableTrail(false);
        }
        else
        {
            sc.EnableTrail(true);
        }
    }

    void ChangeDistanceVisibility(float value)
    {
        if (value == 0)
        {
            sc.EnableDistances(false);
        }
        else
        {
            sc.EnableDistances(true);
        }
    }

    void ChangeSoundStatus(float value)
    {
        if (value == 0)
        {
            am.MuteAudio();
        }
        else
        {
            am.UnmuteAudio();
        }
    }
}
