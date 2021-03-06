﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Transform playerWeapon;

    private float tempsRecharge = 0f;
    private float tempsAttaque = 0f;

    private float dureeAttaque;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        playerWeapon.gameObject.SetActive(false);
        AnimationClip animAttaque = Array.Find(anim.runtimeAnimatorController.animationClips, clip => clip.name == "attack");
        dureeAttaque = animAttaque.length;
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
          tempsRecharge = dureeAttaque + 0.5f;
          tempsAttaque = dureeAttaque;
          anim.SetBool("isAttacking", true);
          playerWeapon.gameObject.SetActive(true);
          SoundEffectsHelper.Instance.MakePlayerAttackSound();
      }
    } 
    
    else if (tempsRecharge > 0)
    {
      anim.SetFloat("reloadTime", tempsRecharge);
      tempsRecharge -= Time.deltaTime;
      tempsAttaque -= Time.deltaTime;
    }

    if (tempsAttaque <= 0)
    {
        anim.SetBool("isAttacking", false);
        playerWeapon.gameObject.SetActive(false);
    }



     // ...
  }
}
