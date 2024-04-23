using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class BallSortPuzzleManager : MonoBehaviour
{
    public static BallSortPuzzleManager instance;
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
        instance = this;
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
            Destroy(instance);
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
