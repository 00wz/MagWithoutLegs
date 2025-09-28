using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] 
    private Transform Bar;
    private float _defaultSize;

    private void Awake()
    {
        _defaultSize = Bar.localScale.x;
    }

    public void SetProgress(float healthRatio)
    {
        healthRatio = Mathf.Clamp01(healthRatio);
        Vector3 localScale = Bar.localScale;
        localScale.x = healthRatio * _defaultSize;
        Bar.localScale = localScale;
    }
}
