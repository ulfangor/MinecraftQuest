using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text textField;
    private float timer = 0;
    private bool timerPaused = true;
    public Rigidbody Player;
    public GameObject endMenu;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (endMenu.activeSelf)
        {
            pauseTimer();
        }

        if (!timerPaused)
        {
            timer += Time.deltaTime;
            textField.text = timer.ToString();
        }
        else
        {
            textField.text += timer.ToString();
        }
    }

    public void unpauseTimer()
    {
        timerPaused = false;
    }

    public void pauseTimer()
    {
        timerPaused = true;
    }
}
