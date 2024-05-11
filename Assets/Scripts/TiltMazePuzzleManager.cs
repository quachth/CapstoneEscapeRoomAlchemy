/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: TiltMazePuzzleManager manages the Marble Tilt Maze puzzle game in the alchemy escape room and contains functions for the trigger that indicates successful completion
                    of the puzzle activates the puzzle from inactive to active (visible board with marble) to playable. It also contains code to control the behavior of the animation present
                    in the prize reveal.
                This script is attached to the empty GameObject under which the GameObjects used to play the game (3 versions of the maze boards, prizes, and puzzle camera)
                    are nested. It is also attached to the transparent cubic surface that covers the top of the puzzle, and the blue trigger square on the Playable version of
                    the tilt maze board. When the puzzle is successfully completed, it signals to ModeSwap to disable the puzzle/related assets.
                First Person Controller movement is disabled during tilt puzzle mode.
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TiltMazePuzzleManager : MonoBehaviour
{
    public static TiltMazePuzzleManager instanceMaze;

    private bool activated;
    
    [SerializeField] private GameObject aoe;
    [SerializeField] private GameObject inactiveMaze;
    [SerializeField] private GameObject inactiveMazeTrigger;
    [SerializeField] private GameObject activeMaze;
    [SerializeField] private GameObject activeMazeTrigger;
    [SerializeField] private GameObject prizes;
    public GameObject instructions;
    public GameObject playableMaze;
    

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    void Awake()
    {
        instanceMaze = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (ModeSwap.gameInstance.donePuzzle2 == false)
            if (activated == true)
            {
                inactiveMazeTrigger.SetActive(false);
                activeMazeTrigger.SetActive(true);
                inactiveMaze.SetActive(false);
                activeMaze.SetActive(true);
            }
            if (ModeSwap.gameInstance.tiltMazeCamera.enabled)
            {
                activeMaze.SetActive(false);
                instructions.SetActive(true);
                playableMaze.SetActive(true);
                ModeSwap.gameInstance.playerCamera.transform.parent.transform.parent.GetComponent<FirstPersonController>().playerCanMove = false;
            }     
    }

    // Method for inactive maze to determine if it should be activated via marble colliding (script placed on inactive maze as well)

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("TiltMazeMarble"))
        {
            activated = true;
            Destroy(collision.gameObject);
        }
    }

    // Method for trigger square on the playable maze board to end tilt maze puzzle and reveal prize.
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ActiveTiltMazeMarble"))
        {
            ModeSwap.gameInstance.playerCamera.enabled= true;
            ModeSwap.gameInstance.tiltMazeCamera.enabled = false;
            ModeSwap.gameInstance.donePuzzle2 = true;
            ModeSwap.gameInstance.playerCamera.transform.parent.transform.parent.GetComponent<FirstPersonController>().playerCanMove = true;
            instructions.SetActive(false);
            playableMaze.SetActive(false);
            inactiveMaze.SetActive(true);
            activeMazeTrigger.SetActive(false);
            prizes.SetActive(true);
            AOE();
        }
    }

    // Method to destroy AOE effect after 5 seconds
    private void AOE()
    {
        if (prizes.activeSelf)
        {
            Destroy(aoe, 10f);
        }
    }
}
