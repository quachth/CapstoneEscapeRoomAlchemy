/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: TiltMazeBoardMovements manages the Marble Tilt Maze puzzle game mechanics, primarily taking player input from the keyboard (WASD) to control the tilting of the
                    playable maze board to move the marble towards the blue square.
                This script is attached to the playable maze board seen in the tilt maze camera mode.
    Citations: Code adapted from why_i_play TV ch.'s Board Tilting Game tutorial on Youtube. (https://www.youtube.com/watch?v=m4lKpht0WD8&list=PLvw5MDqEo9YLISdYRQmGy9fYN6EtjTtZG&index=7&t=53s)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public float smooth = 5.0f;         // controls how smooth the board tilting movement is
    public float rotateAngle = 30.0f;   // controls degree to which the board tilts
    private Rigidbody board;

    void Start()
    {
        board = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from user
        float xRotation = Input.GetAxis("Vertical") * rotateAngle;
        float zRotation = Input.GetAxis("Horizontal") * -rotateAngle; // Negative due to orientation of maze in room

        // Convert the rotation angles into quarternions
        Quaternion targetRotation = Quaternion.Euler(xRotation, 0.0f, zRotation);

        // Rotate the board smoothly
        board.MoveRotation(Quaternion.Slerp(board.rotation, targetRotation, Time.deltaTime * smooth));
    }
}
