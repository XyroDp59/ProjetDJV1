using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform fill;
    [SerializeField] bool isWorldSpace;

    public void UpdateFill(float f)
    {
        fill.anchorMax = new Vector2 (f, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if(isWorldSpace) transform.LookAt(Camera.main.transform.position);
    }
}
