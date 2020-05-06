using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contrôleur du joueur
/// </summary>
public class PlayerScript : MonoBehaviour
{
  /// <summary>
  /// 1 - La vitesse de déplacement
  /// </summary>
  public Vector2 vitesse = new Vector2(80, 80);
  public float vitesseMax = 25;

  private float gravite;

  public float hauteurSaut = 60;
  

  // 2 - Stockage du mouvement
  private Vector2 movement;
  private bool estAuSol;
  private bool saute;
  private bool toucheSautEnfoncee;


  //Demi hauteur du boxcollider (distance entre le centre et le bas), nécessaire pour savoir si l'on peut sauter
  //(on y ajoute un Epsilon pour savoir si l'on est en contact avec le sol, car les Raycasts partent du centre du collider)
  private float demiHauteur;

    public GameObject Player;

 void Start()
 {
   gameObject.GetComponent<PlayerScript2>().enabled = false;
   demiHauteur = GetComponent<BoxCollider2D>().bounds.extents.y;
   estAuSol = false;
   gravite = Physics2D.gravity.magnitude;
 }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            Player.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            Player.transform.parent = null;
        }
    }

    void Update()
  {
    // 3 - Récupérer les informations du clavier/manette
    float inputX = Input.GetAxis("Horizontal");
    float inputY = Input.GetAxis("Vertical");

    

    // 4 - Calcul du mouvement et de la direction du sprite
    movement = new Vector2(vitesse.x * inputX, vitesse.y * inputY);

    float scaleX = transform.localScale.x;
    float scaleY = transform.localScale.y;
    float scaleZ = transform.localScale.z;

    estAuSol = estSol();

    

    if (inputX > 0)
    {
       transform.localScale = new Vector3(Mathf.Abs(scaleX),
        scaleY, scaleZ);
    }
 
    else if (inputX < 0)
    {
       transform.localScale = new Vector3(-1f*Mathf.Abs(scaleX),
        scaleY, scaleZ);
    }

    //Gestion des sauts

    if(Input.GetButtonDown("Jump"))
    {
      toucheSautEnfoncee = true;
      if(estAuSol)
      {
        saute = true;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f * CalculSaut(gravite, hauteurSaut) * GetComponent<Rigidbody2D>().mass,
         ForceMode2D.Impulse);
      }
    }
    else if (Input.GetButtonUp("Jump"))
    {
      toucheSautEnfoncee = false;
    }

  }

  void FixedUpdate()
  {
    // 5 - Déplacement
    
    GetComponent<Rigidbody2D>().AddForce(movement);
    
    if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > vitesseMax
    && Input.GetAxis("Horizontal")*GetComponent<Rigidbody2D>().velocity.x>0)
    /*La 2ème condition permet de vérifier que la direction de l'input est identique
    à la direction actuelle de déplacement. Cela évite une perte instantanée de la vélocité
    accumulée si l'on change de direction alors que l'on est à vitesse maximale*/
    
    {

      Vector2 vect = GetComponent<Rigidbody2D>().velocity;
      vect.x = Input.GetAxis("Horizontal")*vitesseMax;
      GetComponent<Rigidbody2D>().velocity = vect;
    }
  }

  void LateUpdate() //Saut en LateUpdate pour des raisons de problèmes de performance avec FixedUpdate
  {
    if(saute)
    {
      if(!toucheSautEnfoncee && GetComponent<Rigidbody2D>().velocity.y>0)
      {
          GetComponent<Rigidbody2D>().AddForce(-1.5f * Vector2.up * CalculSaut(gravite, hauteurSaut) * GetComponent<Rigidbody2D>().mass);
      }
    }  

    if (estAuSol && GetComponent<Rigidbody2D>().velocity.y<0)
    {
      Vector2 v = GetComponent<Rigidbody2D>().velocity;
      v.y = 0;
      GetComponent<Rigidbody2D>().velocity = v;
    }
  }

  public float CalculSaut(float gravite, float hauteur)
  {
    return Mathf.Sqrt(2 * gravite * hauteur);
  }

  public bool estSol()
  {
    return Physics2D.Raycast(transform.position, -1f*Vector2.up, demiHauteur+0.1f);
  }

    

  
}