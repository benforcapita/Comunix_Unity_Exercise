using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bomb : MonoBehaviour
{
    
    //starting force of the bomb vector2
    public Vector2 bombForce;
    //bomb rigidbody
     [SerializeField]private Rigidbody2D bombRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        bombRigidbody.AddForce(bombForce, ForceMode2D.Impulse);
    }
}
