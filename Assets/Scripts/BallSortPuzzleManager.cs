/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: BallSortPuzzleManager manages the Ball Sort puzzle game in the alchemy escape room and contains functions used to
                    check the win condition for the game as well as reset the global variable isSelectedThis used between BallSortPuzzleManager and TubeClicked.
                This script is attached to the empty GameObject under which the GameObjects used to play the game (flasks, marbles, temporary selected marble container, and puzzle camera)
                    are nested. When the puzzle is successfully completed, it signals to ModeSwap to disable the puzzle/related assets.
    Citations:  Code developed by following Dream Game Creations's 3D Ball Sort tutorial on Youtube. (https://www.youtube.com/watch?v=5kI2bIeec0U&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=8)
                The code to check the win condition for the game is self-created.
*/


using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class BallSortPuzzleManager : MonoBehaviour
{
    public static BallSortPuzzleManager instanceBall;
    public GameObject ballSortManager;
    [SerializeField] private GameObject[] Flasks;
    [SerializeField] private GameObject prizeDrawer;
    [SerializeField] private GameObject prizes;
    [SerializeField] private GameObject uiText1;

    [HideInInspector] public float solved;
    [HideInInspector] public bool isSelected;
    private System.Object threadLocker = new System.Object();

    private void Awake()
    {
        instanceBall = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        solved = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Check if ball sort puzzle is completed
    public void CheckWinCondition()
    {
        solved = 0f;
        foreach (GameObject flask in Flasks)
        {
            if (flask.transform.childCount == 3)
            {
                if (flask.transform.GetChild(0).CompareTag(flask.transform.GetChild(1).tag) && flask.transform.GetChild(0).CompareTag(flask.transform.GetChild(2).tag))
                {
                    solved++;
                }        
            }
        }
        
        if (solved == 3f)
        {
            ModeSwap.gameInstance.playerCamera.enabled = true;
            ModeSwap.gameInstance.ballSortCamera.enabled = false;
            // Resume first-person mode, remove UI text, and destroy game instance
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Destroy(instanceBall);
            uiText1.SetActive(false);
            // Transform out drawer and puzzle 1 prize items
            prizeDrawer.transform.localPosition = new Vector3(0f, 0.2278563f, 0.7f);
            prizes.transform.position = new Vector3(6.506f, 1.0369f, 1.0369f);
            // Update global variable donePuzzle1 to prevent player from reentering ball sort camera
            ModeSwap.gameInstance.donePuzzle1 = true;
        }    
    }

    public void ResetSelected()
    {
        lock (threadLocker)
        {
            foreach (GameObject flask in Flasks)
            {
                flask.GetComponent<TubeClicked>().isSelectedThis = false;
            }
        }
    }
}
