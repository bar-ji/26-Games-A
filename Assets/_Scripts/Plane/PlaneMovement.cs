using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaneMovement : MonoBehaviour
{
    [SerializeField] private GridManager manager;

    [SerializeField] private float rotationSpeed = 10;    


    const float minInputDelta = 0.5f;
    float timeOfLastInput;

    public float score {get; set;}


    Vector3 newPosition;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;

    Vector3 posLastFrame;

    void Start()
    {
        transform.position = manager.GetStartPos();
        newPosition = transform.position;
        timeOfLastInput = Time.time;
    }

    public void Update()
    {
        AnimationManager();

        if(Time.time - timeOfLastInput < minInputDelta) return;

        float yInput = Input.GetAxisRaw("Vertical");
        float xInput = Input.GetAxisRaw("Horizontal");

        if(yInput != 0 || xInput != 0)
            Move(new Vector2(xInput, yInput));
    }

    public void Move(Vector2 direction)
    {
        newPosition = manager.GetNewGridPosition(newPosition, direction);
        transform.DOMove(newPosition, minInputDelta).SetEase(Ease.Linear);

        timeOfLastInput = Time.time;
    }

    void AnimationManager()
    {
        Vector3 velocity = (transform.position - posLastFrame) / Time.deltaTime;

        Vector3 newUp = new Vector3(1 * Mathf.Sign(velocity.x), 0.5f, 0).normalized;
        Vector3 newForward = new Vector3(0, 1 * Mathf.Sign(velocity.y), 0.5f).normalized;

        Quaternion endRot = Quaternion.identity;

        if(velocity.x !=  0)
            endRot = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
        else
            endRot = Quaternion.identity;

        if(velocity.y != 0)
            endRot *= Quaternion.FromToRotation(transform.forward, newForward) * Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        else
            endRot *= Quaternion.identity;
            

        transform.rotation = Quaternion.Slerp(transform.rotation, endRot, Time.deltaTime * rotationSpeed);


        posLastFrame = transform.position;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.TryGetComponent(out Obstacle obstc))
        {
            OnDeath.Invoke();
            Destroy(gameObject);
        }
        else if(col.TryGetComponent(out Star star))
        {
            score += 50;
        }
    }
}
