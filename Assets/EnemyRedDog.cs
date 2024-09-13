using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedDog : Enemy
{
    PlayerMoveControl playerMoveControl;
    public Transform GroundChecker;
    public Transform WallChecker; 
    public LayerMask layerToCheck;

    private bool detectGround;
    private bool detectWall; 
    public float radius;

    public float speed = 1.0f;
    private int direction = -1;

 
    
    void Start() { }
   private void FixedUpdate()
    { 
       Filp();
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private void Filp() 
    { 
        detectGround = Physics2D.OverlapCircle(
            GroundChecker.position, radius, layerToCheck);         
        detectWall = Physics2D.OverlapCircle(
            WallChecker.position, radius, layerToCheck);

        if (!detectGround || detectWall) 
        {
            direction *= -1;
           transform.localScale = new Vector2(-transform.localScale.x, 1);
            
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundChecker.position, radius);
        Gizmos.DrawWireSphere(WallChecker.position, radius);
    } 


}