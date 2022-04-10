using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    //shoot
    public void Shoot(Vector2 startPosition, float speed)
    {
        this.transform.position = startPosition;
        rb.AddForce(speed*Vector2.up, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       this.gameObject.SetActive(false);
    }
}
