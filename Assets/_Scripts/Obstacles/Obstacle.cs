using UnityEngine;
using DG.Tweening;


public class Obstacle : MonoBehaviour 
{
    bool isAlive;

    const float despawnZPos = -5;
    const float spawnTime = 0.2f;

    Vector3 spinAxis;

    float alpha = 0;
    Renderer r;

    void Start()
    {
        r = GetComponent<Renderer>();
        spinAxis = Random.insideUnitSphere;
        transform.localScale = Vector3.zero;
        Spawn();
    }

    void Update()
    {
        if(transform.position.z < despawnZPos && isAlive)
            Despawn();

        transform.Rotate(spinAxis.normalized * Time.deltaTime * Random.Range(3, 32));
        
        alpha += Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        r.material.SetFloat("_Alpha", alpha);

    }


    public void Spawn()
    {
        transform.DOScale(Vector3.one * Random.Range(0.3f, 0.8f), spawnTime).SetEase(Ease.OutElastic);
        isAlive = true;
    }

    public void Despawn()
    {
        isAlive = false;
        Destroy(gameObject);
    }
}