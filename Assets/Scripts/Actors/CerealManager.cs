using System;
using UnityEngine;

public class CerealManager : MonoBehaviour
{
    [HideInInspector] public Action<CerealManager> dieEvent;
    [HideInInspector] public Vector2 targetPosition = Vector2.zero;
    
    public float speed = 10f;
    [HideInInspector] public bool canMove = true;

    private void FixedUpdate()
    {
        if(canMove)
        {
            Vector2 direction = (this.targetPosition - (Vector2)this.transform.position).normalized;
            this.transform.position += speed * Time.fixedDeltaTime * (Vector3)direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (dieEvent != null)
                dieEvent.Invoke(this);
        }
    }
}