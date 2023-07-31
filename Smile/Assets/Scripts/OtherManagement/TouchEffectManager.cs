using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectManager : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = Input.mousePosition;
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            ParticleSystem newParticle = Instantiate(particlePrefab, touchPosition, Quaternion.identity);
            newParticle.Play();
            Destroy(newParticle.gameObject, 1.0f);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
