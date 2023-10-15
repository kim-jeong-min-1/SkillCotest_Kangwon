using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string targetTag;
    public float Damage { get; set; }

    public void SetBullet(Vector3 pos, Vector3 rot, float _damage)
    {
        transform.position = pos;
        transform.rotation = Quaternion.Euler(rot);
        Damage = _damage;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if(other.TryGetComponent(out IDamagable target))
            {
                target.ApplyDamage(Damage);
                Destroy(gameObject);
            }
        }    
    }
}
