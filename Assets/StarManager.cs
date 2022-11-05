using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private Renderer star;
    [SerializeField] private float starAmount;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float radius;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private Color[] colors = new Color[2];

    void Start()
    {
        for(int i = 0; i < starAmount; i++)
        {
            var _star = Instantiate(star, Random.insideUnitSphere.normalized * radius, Quaternion.identity);
            _star.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            _star.transform.parent = transform;

            float rand = Random.Range(0f, 1f);
            Color targetColor;
            if(rand < 0.5f)
                targetColor = Color.Lerp(colors[0], colors[1], Random.Range(0f, 1f));
            else
                targetColor = Color.Lerp(colors[1], colors[2], Random.Range(0f, 1f));

            _star.material.SetColor("_Color", targetColor);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(1,1,1).normalized * rotateSpeed * Time.deltaTime);
    }


}
