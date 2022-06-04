using UnityEngine;

public class BarRenderer : MonoBehaviour
{
    public Transform background;
    public Transform bar;

    public void Render(float value, float maxValue)
    {
        float scaleRatio = Mathf.Clamp(value / maxValue, 0, 1);
        bar.localScale = new Vector3(scaleRatio, 1, 1);
    }
}