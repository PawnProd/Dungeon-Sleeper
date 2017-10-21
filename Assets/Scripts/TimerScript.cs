using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public static float timer;


    // Use this before initialization
    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;
        GetComponent<Text>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(timer / 60), timer % 60);

    }

}