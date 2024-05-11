/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: ScreenChanger is a script used to change scenes and implements a fade animation between scenes of the project build. It uses the index of the
                scenes to change between them.
    Citations: This script was written following Brackeys "How to Fase Between Scenes in Unity" tutorial on Youtube (https://www.youtube.com/watch?v=Oadq-IrOazg&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=13).
                Additional scene loop script adapted from similar inquiry on the unity forums (https://forum.unity.com/threads/solved-cycling-scenes-back-to-the-first-scene.582394/).
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private int newSceneIdx;

    void Start()
    {
        newSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);                          // return to start after reaching last scene
            } else
            {
                newSceneIdx++;
                FadeScreens(newSceneIdx);
            }
        }
    }

    public void FadeScreens(int sceneIndex)
    {
        newSceneIdx = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(newSceneIdx);
    }
}
