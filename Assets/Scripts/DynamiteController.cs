    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteController : MonoBehaviour
{
    private Rigidbody2D dynamiteRigid;
    private Animator anim;
    private float dynamiteSpeed = 15f;
    private bool isRotate = true;
    private aboutCamera cameraScript;

    public bool isExplode = false;

    void Start()
    {
        dynamiteRigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cameraScript = Camera.main.GetComponent<aboutCamera>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        dynamiteRigid.velocity = direction * dynamiteSpeed;
        GameManager.Instance.weaponDamage = 150f;
    }

    void Update()
    {
        StopDynamite();
        SpinDynamite();
    }

    IEnumerator ShakeForDuration(float duration)
    {
        cameraScript.StartShake();
        yield return new WaitForSeconds(duration);
        cameraScript.StopShake();
    }

    private void StopDynamite()
    {
        dynamiteRigid.velocity = Vector2.Lerp(dynamiteRigid.velocity, Vector2.zero, 1.5f * Time.deltaTime);

        if(dynamiteRigid.velocity.magnitude <= 0.5f)
        {
            transform.Rotate(new Vector3(0, 0, transform.rotation.z));
            isRotate = false;
            Ignite();
        }
    }

    private void SpinDynamite()
    {
        if(isRotate)
        {
            transform.Rotate(new Vector3(0, 0, dynamiteSpeed * 0.1f));
        }
    }

    private void Ignite()
    {
        anim.SetTrigger("Ignite");
    }

    public void Explode()
    {
        anim.SetTrigger("Explode");
        StartCoroutine(ShakeForDuration(0.3f));
        isExplode = true;
    }

    public void DynamiteDestroy()
    {
        gameObject.SetActive(false);
    }
}
