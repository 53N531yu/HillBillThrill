using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float speed = 5f;
    public float angleRad;
    public float playerLookAngle;
    private Rigidbody2D rb;
    public Camera mainCam;
    public PolygonCollider2D flameCollider;
    public ParticleSystem flameParticles;
    private Vector3 mousePos;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        rb.velocity = new Vector2(speed * horizontal, speed * vertical);

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;

        playerLookAngle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, playerLookAngle);

        var emission = flameParticles.emission;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FlameOn());
            emission.rateOverTime = 50;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            flameCollider.enabled = false;
            emission.rateOverTime = 0;
        }
    }

    public IEnumerator FlameOn()
    {
        yield return new WaitForSeconds(0.25f);
        flameCollider.enabled = true;
    }
}
