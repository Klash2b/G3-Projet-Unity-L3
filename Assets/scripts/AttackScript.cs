using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Transform playerWeapon;

    private float tempsRecharge = 0f;
    private float tempsAttaque = 0f;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerWeapon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
  {
    playerWeapon.transform.position = gameObject.transform.position;
    bool attaque = Input.GetButtonDown("Fire1");
    attaque |= Input.GetButtonDown("Fire2");


    
    if (tempsRecharge <= 0)
    {
      if (attaque)
      {
          tempsRecharge = 1.5f;
          tempsAttaque = 1.0f;
          anim.SetBool("isAttacking", true);
          playerWeapon.gameObject.SetActive(true);
      }
    } 
    
    else if (tempsRecharge > 0)
    {
      anim.SetBool("isAttacking", false);
      anim.SetFloat("reloadTime", tempsRecharge);
      tempsRecharge -= Time.deltaTime;
      tempsAttaque -= Time.deltaTime;
    }

    if (tempsAttaque <= 0)
    {
        playerWeapon.gameObject.SetActive(false);
    }



     // ...
  }
}
