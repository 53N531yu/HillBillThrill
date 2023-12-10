using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Transform Variables")]
    public Transform target;

    [Header("Float Variables")]
    public float damping;
    public float duration = 1f;

    public bool canBoost = false;
    public bool canShake = true;

    public Vector3 offset;
    public AnimationCurve curve;

    private Vector3 velocity = Vector3.zero;
    Vector3 movePosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        float elapsedTime = 0f;

        while(Input.GetKey("w") && canBoost)
        {
            originalPos = transform.position;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = originalPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = originalPos;

        yield return new WaitForSeconds(0.2f);
        canShake = false;

        yield return new WaitForSeconds(1f);
        canShake = true;
    }
}
