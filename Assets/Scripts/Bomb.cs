using System;
using System.Collections;
using System.Collections.Generic;
using GameManager;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Bomb : MonoBehaviour
{
    
    //starting force of the bomb vector2
    public Vector2 bombForce;
    //bomb rigidbody
     [SerializeField]private Rigidbody2D bombRigidbody;
     [SerializeField] private GameObject nextBomb;
     [SerializeField] private Animator _animator;
     [SerializeField] private int bombValue;
     
    
    // Start is called before the first frame update
    void Start()
    {
        bombRigidbody.AddForce(bombForce, ForceMode2D.Impulse);
        MessageBroker.Default.Publish(new InitBombEventArgs(gameObject));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.gameObject.CompareTag("Damager")) return;
        Debug.Log("Bullet hit");
        if (nextBomb != null)
        {
            var position = bombRigidbody.position;
            var ball1 =  Instantiate(nextBomb, position+Vector2.right, Quaternion.identity);
            var ball2 = Instantiate(nextBomb, position+Vector2.left, Quaternion.identity);
            var randomX = UnityEngine.Random.Range(-5f, 5f);
            var randomY = UnityEngine.Random.Range(-5f, 5f);
            ball1.GetComponent<Bomb>().bombForce = new Vector2(randomX, randomY);
            ball2.GetComponent<Bomb>().bombForce = new Vector2(-randomX, -randomY);
          
        }
        else
        {
            MessageBroker.Default.Publish(new LastBombExplodedEventArgs());
        }
        /*
         * <summary>
         * I added this Destroy delay mainly to give the bomb a chance to use the explode animation before it is destroyed.
         * One of its fun side effects was that the player could keep hitting the bomb before it explodes, allowing it to spawn smaller bombs and letting the player earn more points.
         * I decided to keep this behavior so that every playthrough could have a different result and not just the same one.
         * Also, I think it's more fun and interesting to allow the player to modify the level difficulty levels and reward them accordingly.
         * So it might seem like a bug, but for me, its a nice feature :) 
         * </summary>
         */
        _animator.SetBool("Explode", true);
        bombRigidbody.isKinematic = true;
        bombRigidbody.velocity = Vector2.zero;
        Destroy(gameObject, 0.8f);

    }

    private void OnDestroy()
    {
        MessageBroker.Default.Publish(new BombHitEventArgs(bombValue));
    }
}