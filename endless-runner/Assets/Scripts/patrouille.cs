using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrouille : MonoBehaviour
{

    public float vitessepatrouille;// Dupont va � 2f et Niessen � 3
    private float distance;//distance entre le m�chant et le player
    public Transform player;
    public float auxabords = 2f;// la distance apd laquelle on consid�re que proche

    void Start()
    {
        //playerposition = GameObject.Find("Player").transform.position;
    }

    private void FixedUpdate()
    {

        /*distanceenx = player.position.x-transform.position.x;
        if( distance < auxabords)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position.x, vitessepatrouille);
            
        }
        else { */
        //this.transform.position += Vector3.right * Time.deltaTime * vitessepatrouille;
        transform.Translate(Vector2.right * Time.deltaTime * vitessepatrouille);
        // }


    }
    // j'aimerais que la direction change qd je rentre en collision avec un mur
    private void OnTriggerEnter2D(Collider2D other)
    // on peut ne pas sp�cifier car pr le cube, qlq soit e collider rencontr�, on se percute
    {
        if (other.gameObject.CompareTag("limite"))
        {
            vitessepatrouille *= -1; // pour faire aller dans autre sens quand touche les limites      
        }
    }

}
