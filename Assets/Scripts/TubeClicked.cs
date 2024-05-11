/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: TubeClicked contains the logic used to implement the game mechanics for the Ball Sort puzzle game in the alchemy room, including specifying which flask contains what marbles,
                    which marble has been selected from which flask, and which marbles are being moved to which flask. This movement is determined by user input using the left mouse button.
                This script is placed on each of the three interactable flasks involved in the puzzle.
    Citations:  Code developed by following Dream Game Creations's 3D Ball Sort tutorial on Youtube. (https://www.youtube.com/watch?v=5kI2bIeec0U&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=8)
                Additional code was added to lock sections of the script that change variables used by multiple scripts as well as to ensure the puzzle was only playable when the player was in a
                    certain mode (aka when a certain camera was enabled specifically for the puzzle).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class TubeClicked : MonoBehaviour
{

    [SerializeField] GameObject marbleHolder;           // The flask the marble is in
    [SerializeField] Transform flaskStartPos;           // The selected marble start position above the flask
    [SerializeField] GameObject selectedMarbleHolder;   // A temporary holder for the selected marble.
    [HideInInspector] public bool isSelectedThis;
    
    private System.Object threadLocker = new System.Object();
    
    // Start is called before the first frame update
    void Start()
    {
        isSelectedThis = false;
    }


    void FixedUpdate()
    {
        // If a marble has been selected
        if (BallSortPuzzleManager.instanceBall.isSelected && isSelectedThis)
        {
            if (Vector3.Distance(selectedMarbleHolder.transform.GetChild(0).position, flaskStartPos.position) >= 0.05f && selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic == true)
            {
                lock (threadLocker)
                {
                    // Move the marble towards the start position
                    Vector3 newPosition = Vector3.Lerp(selectedMarbleHolder.transform.GetChild(0).position, flaskStartPos.position, Time.deltaTime * 10f); 
                    selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().MovePosition(newPosition);
                }
            }
        }
        BallSortPuzzleManager.instanceBall.CheckWinCondition();
    }

    private void OnMouseDown()  // Called when user has left-clicked while over a collider; event is sent to all scripts of the GameObject with collider (parent/children do not receive the event)
    {
        // If the player has entered the puzzle camera/puzzle mode
        if (ModeSwap.gameInstance.ballSortCamera.enabled)
        {
            // If a marble is not currently selected
            if (!BallSortPuzzleManager.instanceBall.isSelected && !isSelectedThis)
            {
                isSelectedThis = true;                              // Marble from this flask was selected
                BallSortPuzzleManager.instanceBall.isSelected = true;   // A marble globally selected
                // Index into the flask that was clicked on to get the bottom-most child (in hierarchy - top most visually), move it into the temporary holder, disable gravity on it, turn it kinematic. Make global isSelected true.
                marbleHolder.transform.GetChild(marbleHolder.transform.childCount - 1).gameObject.transform.parent = selectedMarbleHolder.transform;
                selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
                selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            } else if (BallSortPuzzleManager.instanceBall.isSelected)
            // If a marble is already selected
            {
                // If selected marble is from this flask, deselect it locally and globally, turn back on gravity, make it non-kinematic, and place it as the child of this flask again (returns to this parent).
                if (isSelectedThis)
                {
                    lock (threadLocker)
                    {    
                        selectedMarbleHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                        selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                        selectedMarbleHolder.transform.GetChild(0).gameObject.transform.parent = marbleHolder.transform;
                        BallSortPuzzleManager.instanceBall.isSelected = false;
                        BallSortPuzzleManager.instanceBall.ResetSelected();
                    }
                } else

                // Marble selected not from this flask, deselect it globally, move selected marble from its position above its respective flask over to 
                // this flask's starting position, turn on gravity, turn off kinematic, and place it as child of this flask (this flask was clicked on)
                // Reset all isSelectedThis variables
                {
                    lock (threadLocker)
                    {
                        BallSortPuzzleManager.instanceBall.isSelected = false;
                        BallSortPuzzleManager.instanceBall.ResetSelected();
                        selectedMarbleHolder.transform.GetChild(0).gameObject.transform.position = flaskStartPos.position;
                        selectedMarbleHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                        selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                        selectedMarbleHolder.transform.GetChild(0).gameObject.transform.parent = marbleHolder.transform;
                    }
                }
            }
        }
    }
}
