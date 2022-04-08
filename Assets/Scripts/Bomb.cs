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
        _animator.SetBool("Explode", true);
        bombRigidbody.isKinematic = true;
        bombRigidbody.velocity = Vector2.zero;
        MessageBroker.Default.Publish(new BombHitEventArgs(bombValue));
        Destroy(gameObject, 0.8f);

    }
}
