using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrouille : MonoBehaviour
{

    public float vitessepatrouille;// Dupont va à 2f et Niessen à 3
    private float distance;//distance entre le méchant et le player
    public Transform player;
    public float auxabords = 2f;// la distance apd laquelle on considère que proche
    private Vector2 joueursurx;

    void Start()
    {
        //playerposition = GameObject.Find("Player").transform.position;
    }

    private void FixedUpdate()
    {

       distance = Mathf.Abs(player.position.x-transform.position.x);

        if( distance < auxabords)
        {
            joueursurx = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, joueursurx, vitessepatrouille * Time.deltaTime);
            
        }
        else { 
        //this.transform.position += Vector3.right * Time.deltaTime * vitessepatrouille;
        transform.Translate(Vector2.right * Time.deltaTime * vitessepatrouille);
         }


    }
    // j'aimerais que la direction change qd je rentre en collision avec un mur
    private void OnTriggerEnter2D(Collider2D other)
    // on peut ne pas spécifier car pr le cube, qlq soit e collider rencontré, on se percute
    {
        if (other.gameObject.CompareTag("limite"))
        {
            vitessepatrouille *= -1; // pour faire aller dans autre sens quand touche les limites      
        }
    }

}

