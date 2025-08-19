using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float speed;

    void Update()
    {
        //Move coin to left / scrolling
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //Destroy coin if off-screen
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float coinSpeed)
    {
        speed = coinSpeed;
    }

    //collect coin
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Update the score
            GameManager.instance.AddScore(GameManager.instance.coinValue);

            // Destroy coin
            Destroy(gameObject);
        }
    }
}
