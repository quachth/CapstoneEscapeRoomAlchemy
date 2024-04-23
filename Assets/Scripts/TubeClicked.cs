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
        if (BallSortPuzzleManager.instance.isSelected && isSelectedThis)
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
        BallSortPuzzleManager.instance.CheckWinCondition();
    }

    private void OnMouseDown()  // Called when user has left-clicked while over a collider; event is sent to all scripts of the GameObject with collider (parent/children do not receive the event)
    {
        // If the player has entered the puzzle camera/puzzle mode
        if (ModeSwap.gameInstance.ballSortCamera.enabled)
        {
            // If a marble is not currently selected
            if (!BallSortPuzzleManager.instance.isSelected && !isSelectedThis)
            {
                isSelectedThis = true;                              // Marble from this flask was selected
                BallSortPuzzleManager.instance.isSelected = true;   // A marble globally selected
                // Index into the flask that was clicked on to get the bottom-most child (in hierarchy - top most visually), move it into the temporary holder, and disable gravity on it. Make global isSelected true.
                marbleHolder.transform.GetChild(marbleHolder.transform.childCount - 1).gameObject.transform.parent = selectedMarbleHolder.transform;
                selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;
                selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            } else if (BallSortPuzzleManager.instance.isSelected)
            // If a marble is already selected
            {
                // If selected marble is from this flask, deselect it locally and globally, turn back on gravity, and placing it as the child of this flask again (returns to this parent).
                if (isSelectedThis)
                {
                    lock (threadLocker)
                    {    
                    selectedMarbleHolder.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = true;
                        selectedMarbleHolder.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
                        selectedMarbleHolder.transform.GetChild(0).gameObject.transform.parent = marbleHolder.transform;
                        BallSortPuzzleManager.instance.isSelected = false;
                        BallSortPuzzleManager.instance.ResetSelected();
                    }
                } else

                // Marble selected not from this flask, deselect it globally, move selected marble from its position above its respective flask over to 
                // this flask's starting position, turn on gravity, and place it as child of this flask (this flask was clicked on)
                // Reset all isSelectedThis variables
                {
                    lock (threadLocker)
                    {
                        BallSortPuzzleManager.instance.isSelected = false;
                        BallSortPuzzleManager.instance.ResetSelected();
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
