using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject dynamite;
    public float explosionRadius = 0.8f;
    private DynamiteController DC;
    private bool Explosion = false;

    void Start()
    {
        DC = dynamite.GetComponent<DynamiteController>();
        Explosion = DC.isExplode;
    }

    void Update()
    {
        Explosion = DC.isExplode;

        if(Explosion)
        {
            Explode();
        }
    }

    private void Explode()
    {
        CircleCollider2D explosionCollider = gameObject.AddComponent<CircleCollider2D>();
        explosionCollider.isTrigger = true;
        explosionCollider.radius = explosionRadius;

        StartCoroutine(DestroyCollider(0.1f));
    }

    IEnumerator DestroyCollider(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject);
    }
}
