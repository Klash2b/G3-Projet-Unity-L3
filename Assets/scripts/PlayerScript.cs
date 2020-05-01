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
<<<<<<< Updated upstream
  public Vector2 speed = new Vector2(60, 60);
  public float maxSpeed = 20;
  

  // 2 - Stockage du mouvement
  private Vector2 movement;
  private bool estAuSol;
  private bool saute;
  private bool toucheSautEnfoncee;
  private float forceSaut = CalculSaut(9.81f, 60.0f);

=======
  public Vector2 vitesse = new Vector2(80, 80);
  public float vitesseMax = 25;

  private float gravite;

  public float hauteurSaut = 60;
  

  // 2 - Stockage du mouvement
  private Vector2 movement;
  private bool estAuSol;
  private bool saute;
  private bool toucheSautEnfoncee;

>>>>>>> Stashed changes
  private float distanceSol;

 void Start()
 {
   distanceSol = GetComponent<BoxCollider2D>().bounds.extents.y;
<<<<<<< Updated upstream
   estAuSol = false;;
=======
   estAuSol = false;
   gravite = Physics2D.gravity.magnitude;
>>>>>>> Stashed changes
 }

  void Update()
  {
    // 3 - Récupérer les informations du clavier/manette
    float inputX = Input.GetAxis("Horizontal");
    float inputY = Input.GetAxis("Vertical");

    

    // 4 - Calcul du mouvement et de la direction du sprite
<<<<<<< Updated upstream
    movement = new Vector2(speed.x * inputX, speed.y * inputY);
=======
    movement = new Vector2(vitesse.x * inputX, vitesse.y * inputY);
>>>>>>> Stashed changes

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

    if(Input.GetButtonDown("Jump"))
    {
      toucheSautEnfoncee = true;
      if(estAuSol)
      {
        saute = true;
<<<<<<< Updated upstream
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f * forceSaut * GetComponent<Rigidbody2D>().mass,
=======
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1f * CalculSaut(gravite, hauteurSaut) * GetComponent<Rigidbody2D>().mass,
>>>>>>> Stashed changes
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
    
<<<<<<< Updated upstream
    if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed
=======
    if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > vitesseMax
>>>>>>> Stashed changes
    && Input.GetAxis("Horizontal")*GetComponent<Rigidbody2D>().velocity.x>0)
    /*La 2ème condition permet de vérifier que la direction de l'input est identique
    à la direction actuelle de déplacement. Cela évite une perte instantanée de la vélocité
    accumulée si l'on change de direction alors que l'on est à vitesse maximale*/
    
    {

      Vector2 vect = GetComponent<Rigidbody2D>().velocity;
<<<<<<< Updated upstream
      vect.x = Input.GetAxis("Horizontal")*maxSpeed;
      GetComponent<Rigidbody2D>().velocity = vect;
    }
  }

  void LateUpdate()
  {
    if(saute)
    {
      if(!toucheSautEnfoncee && GetComponent<Rigidbody2D>().velocity.y>0)
      {
          GetComponent<Rigidbody2D>().AddForce(-1.5f * Vector2.up * forceSaut * GetComponent<Rigidbody2D>().mass);
      }
    }  

    if (estAuSol && GetComponent<Rigidbody2D>().velocity.y<0)
    {
=======
      vect.x = Input.GetAxis("Horizontal")*vitesseMax;
      GetComponent<Rigidbody2D>().velocity = vect;
    }
  }

  void LateUpdate()
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
>>>>>>> Stashed changes
      Vector2 v = GetComponent<Rigidbody2D>().velocity;
      v.y = 0;
      GetComponent<Rigidbody2D>().velocity = v;
    }
  }

<<<<<<< Updated upstream
  public static float CalculSaut(float gravite, float hauteur)
=======
  public float CalculSaut(float gravite, float hauteur)
>>>>>>> Stashed changes
  {
    return Mathf.Sqrt(2 * gravite * hauteur);
  }

  public bool estSol()
  {
    return Physics2D.Raycast(transform.position, -1f*Vector2.up, distanceSol+0.1f);
  }

  
<<<<<<< Updated upstream
}

=======
}
>>>>>>> Stashed changes
