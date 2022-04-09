using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    //spawns the player at the start of the game
    public void StartGame()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
