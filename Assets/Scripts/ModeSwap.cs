/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: ModeSwap is responsible for changing the player's POV from first person mode (the default camera and movement abilities that the player spawns into the room with) to the different
                    puzzle modes/POVs around the room. When the player begins playing a puzzle (i.e. Puzzle #1 Ball Sort), a different camera is enabled and the puzzle is made active for the player. 
                    Once done, the puzzle and its respective camera is disabled for the player. This script is placed on the FirstPersonController GameObject.
    Citations:  Code developed by following a Unity community post asking for help regarding changing between multiple cameras in one scene. (https://discussions.unity.com/t/changing-between-cameras/3254)
                Additional code was added to differentiate between multiple puzzle cameras, determine which camera would be enabled upon interaction, and when which camera(s) would be disabled upon puzzle
                    completion.
*/


using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwap : MonoBehaviour
{
    public Camera playerCamera;
    public Camera ballSortCamera;
    public Camera tiltMazeCamera;
    public static ModeSwap gameInstance;
    [HideInInspector] public bool donePuzzle1;
    [HideInInspector] public bool donePuzzle2;
    [HideInInspector] public bool donePuzzle3;

    private Component [] marbles;

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask puzzleLayerMask;
    

    void Start()
    {
        playerCamera.enabled = true;
        ballSortCamera.enabled = false;
        tiltMazeCamera.enabled = false;
        donePuzzle1 = false;
        donePuzzle2 = false;
        donePuzzle3 = false;
    }

    void Awake()
    {
        gameInstance = this;
    }

    // Update is called once per frame
    public void Update()
    {
        // If Player Camera is active (not in puzzle), see if hitting E hits puzzle object.
        if (Input.GetKeyDown(KeyCode.E) && playerCamera.enabled)
        {
            // playerCameraTransform.position represents the origin of the raycast
            float puzzleDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, 
                out RaycastHit raycastHit, puzzleDistance, puzzleLayerMask))
            {
                // If the object collider that was hit has the tag "BallSort", enter the ballSortCamera for the ball sorting puzzle.
                if (raycastHit.collider.CompareTag("BallSort") && !donePuzzle1)
                {
                    Debug.Log(raycastHit.collider.tag);
                    // Switch to Ball Sort puzzle camera
                    ballSortCamera.enabled = !ballSortCamera.enabled;
                    playerCamera.enabled = !playerCamera.enabled;
                    // Enable cursor visibility for puzzle                
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                // Else if the object collider hit has the tag "TiltMaze", enter the tiltmaze puzzle
                else if (raycastHit.collider.CompareTag("TiltMaze") && !donePuzzle2)
                {
                    Debug.Log(raycastHit.collider.tag);
                    // Switch to Tilt Maze puzzle camera
                    tiltMazeCamera.enabled = !tiltMazeCamera.enabled;
                    playerCamera.enabled = !playerCamera.enabled;
                }
            }
        // Else if player is in ball sort mode, exit Ball Sort camera mode
        } else if (Input.GetKeyDown(KeyCode.E) && ballSortCamera.enabled)
        {
            ballSortCamera.enabled = !ballSortCamera.enabled;
            playerCamera.enabled = !playerCamera.enabled;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
