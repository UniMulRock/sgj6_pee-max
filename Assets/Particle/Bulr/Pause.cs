using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    [SerializeField]
    GameObject pauseUI;

    [SerializeField]
    Gauss gauss;

    bool pauseFlag = false;
    float intencity = 0;

    float prevTime;

    public bool IsPause()
    {
        return pauseFlag;
    }

    void Update () {
        var deltaTime = Time.realtimeSinceStartup - prevTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pauseFlag = !pauseFlag;
        }

        if (pauseFlag)
        {
            pauseUI.SetActive(true);
            intencity += deltaTime * 8;
            Time.timeScale = 0;
        }
        else
        {
            pauseUI.SetActive(false);
            intencity -= deltaTime * 8;
            Time.timeScale = 1;
        }
        intencity = Mathf.Clamp01(intencity);
        gauss.Resolution = (int)(intencity * 10);

        prevTime = Time.realtimeSinceStartup;
    }

    public void ExecutePause()
    {
        pauseFlag = !pauseFlag;
    }
}