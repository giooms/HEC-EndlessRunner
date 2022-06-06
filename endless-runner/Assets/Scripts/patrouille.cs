using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrouille : MonoBehaviour
{

    public float vitessepatrouille;// Dupont va à 2f et Niessen à 3
    public float distance;//distance entre le méchant et le joueur
    public float auxabords = 3f;// la distance apd laquelle on considère que proche
    private Transform joueur;
    private Vector2 joueursurx;
    
    public GameObject Player;
    public GameObject Thomas;
    public GameObject Marie;
    public GameObject Heloise;
    public GameObject Nathan;
    public GameObject joueurgame;
    public int sens =1;

    void Start()
    {
        
        // on veut prendre le transform du joueur choisi par la personne s'amusant avec notre jeu

        if (TempManager.singleton.load_Nathan)
        {
            GameObject joueurgame = GameObject.Find("Nathan");
            joueur = joueurgame.transform;
        }
        else if (TempManager.singleton.load_Thomas)
        {
            GameObject joueurgame = GameObject.Find("Thomas");
            joueur = joueurgame.transform;
        }
        else if (TempManager.singleton.load_Marie)
        {
            GameObject joueurgame = GameObject.Find("Marie");
            joueur = joueurgame.transform;
        }
        else if (TempManager.singleton.load_Heloise)
        {
            GameObject joueurgame = GameObject.Find("Heloise");
            joueur = joueurgame.transform;
        }
        else
        {
            GameObject joueurgame = GameObject.Find("Player");
            joueur = joueurgame.transform;
        }
       
    }
    private void FixedUpdate()
    {
        
        distance = Mathf.Abs(joueur.position.x - transform.position.x);// distance en valeur absolue entre la position sur x du joueur et du mechant
        

        if (distance > auxabords)
        {
            transform.Translate(Vector2.right * Time.deltaTime * vitessepatrouille* sens);
            
            
        }
        else {
            joueursurx = new Vector2(joueur.position.x, transform.position.y);// ceci permet d'avoir un vecteur de position utilisable a la ligne 68 qui ne prend pas en compte si le joueur saute car la valeur en y est celle du mechant
            transform.position = Vector2.MoveTowards(transform.position, joueursurx, Time.deltaTime * vitessepatrouille);

        }

    }
    // j'aimerais que la direction change qd je rentre en collision avec une limite
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("limite"))
        {
            sens *= -1; // pour faire aller dans autre sens

        }

    }
    
    

}


   