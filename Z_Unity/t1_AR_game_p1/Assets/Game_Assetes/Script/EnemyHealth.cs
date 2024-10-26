using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider Healthslider;
    public float enemy_health = 1000;
    public float maxhealth = 1000f;
    
    void Satrt(){
        enemy_health = maxhealth;
    }

    private void Update() {

        if(Healthslider.value != enemy_health){
            Healthslider.value = enemy_health;
        }

    }

    public void takeDamege(int damage){
        enemy_health -= damage;

        if(enemy_health <=0){
            Destroy(gameObject);
        }
    } 

}
