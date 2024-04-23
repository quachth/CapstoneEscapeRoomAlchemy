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
    public static ModeSwap gameInstance;
    [HideInInspector] public bool donePuzzle1;

    private Component [] marbles;

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask puzzleLayerMask;

    void Start()
    {
        playerCamera.enabled = true;
        ballSortCamera.enabled = false;
        donePuzzle1 = false;
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
            float puzzleDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, 
                out RaycastHit raycastHit, puzzleDistance, puzzleLayerMask))
            {
                // If the object collider that was hit has the tag "BallSort", enter the ballSortCamera for the ball sorting puzzle.
                if (raycastHit.collider.CompareTag("BallSort") && donePuzzle1 == false)
                {
                    Debug.Log(raycastHit.collider.tag);
                    // Switch to Ball Sort puzzle camera, change framerate to accomodate concurrent execution of puzzle scripts.
                    ballSortCamera.enabled = !ballSortCamera.enabled;
                    playerCamera.enabled = !playerCamera.enabled;
                    // Enable cursor visibility for puzzle                
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
                // Else if the object collider hit has the tag "TiltMaze", enter the tiltmaze puzzle
                else if (raycastHit.collider.CompareTag("TiltMaze"))
                {

                }
                // No conditional branch for Magnet Puzzle(#3); will use FPC pickup/drop object system
            }
        // Else, exit Ball Sort camera mode.
        } else if (Input.GetKeyDown(KeyCode.E) && ballSortCamera.enabled)
        {
            Time.fixedDeltaTime = 0.02f;
            Time.maximumDeltaTime = 0.1f;
            ballSortCamera.enabled = !ballSortCamera.enabled;
            playerCamera.enabled = !playerCamera.enabled;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
