/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: Timer is a script used to create the countdown timer on the UI for each of the project's escape rooms. The timer is
                displayed in minutes and seconds.
    Citations:  Code was written following the tutorial on how to make a countdown timer in Unity by Game Dev Beginner
                on Youtube (https://www.youtube.com/watch?v=HmHPJL-OcQE&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=15).
*/

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeValue = 1200;

    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private GameObject tryAgainScreen;
    [SerializeField] private GameObject FPC;




    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
        // frames update at random times, so subtracting Time.deltaTime keeps countdown even
        timeValue -= Time.deltaTime;
        } else
        {
            // lock time value to zero, disable FPC actions, display Try Again screen and mouse
            timeValue = 0;
            tryAgainScreen.SetActive(true);
            FPC.transform.GetComponent<FirstPersonController>().playerCanMove = false;
            FPC.transform.GetComponent<FirstPersonController>().cameraCanMove = false;
            FPC.transform.GetComponent<FirstPersonController>().enableHeadBob = false;
            FPC.transform.GetComponent<FirstPersonController>().enableCrouch = false;
            FPC.transform.GetComponent<FirstPersonController>().enableJump = false;
            FPC.transform.GetComponent<FirstPersonController>().enableZoom = false;                   
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // curly brackets in Format are parameters for minutes and second; value to the left of the colon (0 and 1) refer to which values to use (minutes and seconds)
        // and value to the right of colon is how the value should be formatted (2 digit number by putting 2 zeroes)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}