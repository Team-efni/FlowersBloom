using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchEffectManager : MonoBehaviour
{
    public ParticleSystem particlePrefab;
    private string canvasName = "Canvas";
    private Canvas canvas;

    void Start()
    {
        FindCanvas();
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (canvas == null) FindCanvas();
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 touchPosition = GetTouchPosition();
            ParticleSystem newParticle = Instantiate(particlePrefab, touchPosition, Quaternion.identity);
            newParticle.Play();
            Destroy(newParticle.gameObject, 1.0f);
        }
    }

    void FindCanvas()
    {
        Debug.Log("find");
        canvas = GameObject.Find(canvasName).GetComponent<Canvas>();
    }

    Vector2 GetTouchPosition()
    {
        Vector2 touchPosition = Input.mousePosition;
        switch (canvas.renderMode)
        {
            case RenderMode.ScreenSpaceOverlay:
                break;
            case RenderMode.ScreenSpaceCamera:
                touchPosition = canvas.worldCamera.ScreenToWorldPoint(touchPosition);
                break;
            case RenderMode.WorldSpace:
                touchPosition = canvas.transform.InverseTransformPoint(touchPosition);
                break;
        }
        if(SceneManager.GetActiveScene().name == "Play")
        {
            Debug.Log("play");
            touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        }
        Debug.Log(touchPosition);
        return touchPosition;
    }
}
