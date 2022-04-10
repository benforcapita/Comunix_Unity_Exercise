using UnityEngine;

public class InitBombEventArgs
{
    public GameObject bomb;

    public InitBombEventArgs(GameObject bomb)
    {
        this.bomb = bomb;
    }
}