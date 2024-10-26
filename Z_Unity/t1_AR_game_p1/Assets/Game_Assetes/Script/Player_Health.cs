using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
   public int palyer_health = 200;
   
   public void takeDamege(int damage){
    palyer_health -= damage;

    if(palyer_health <= 0){
        Destroy(gameObject);
    }
   }
}
