using UnityEngine;

public class Customer : MonoBehaviour
{
    private Color currentColor;
    private Color targetColor;
    public GameObject headPart;
    public GameObject basePart;
    public float jumpForce;
    Rigidbody rb;

    private Renderer headRenderer;
    private Renderer baseRenderer;

    private float transitionDuration = 2f;
    private float transitionProgress = 0f;
    private bool isTransitioning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (headPart != null) headRenderer = headPart.GetComponent<Renderer>();
        if (basePart != null) baseRenderer = basePart.GetComponent<Renderer>();

        currentColor = baseRenderer.material.color;
    }

    void Update()
    {
        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            transitionProgress = Mathf.Clamp01(transitionProgress);

            Color lerpedColor = Color.Lerp(currentColor, targetColor, transitionProgress);

            if (headRenderer != null) headRenderer.material.color = lerpedColor;
            if (baseRenderer != null) baseRenderer.material.color = lerpedColor;

            if (transitionProgress >= 1f)
            {
                isTransitioning = false;
                currentColor = targetColor;
            }
        }
    }

    public void StartColorTransition(Color newTargetColor)
    {
        if (!isTransitioning)
        {
            targetColor = newTargetColor;
            transitionProgress = 0f;
            isTransitioning = true;
        }
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
