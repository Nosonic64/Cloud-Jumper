using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    // Class that holds info about the player. 
    // This info is helpful for other gameobjects to be able to reference easily and quickly, which is why we have a class to store them.
    static public float playerX;
    static public float playerY;
    static public float playerYVelocity;
    static public bool playerGrounded;
    static public bool touchingCeiling;
    static public int playerLives;
    static public bool hasDoubleJump;
    static public float hasInvulnerable;
    static public bool gameOver;
    static public bool respawn;
    static public bool spriteCarry;
    static public int retryCount;

}
