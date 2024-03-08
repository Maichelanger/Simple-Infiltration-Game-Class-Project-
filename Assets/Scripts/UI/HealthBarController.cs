using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    internal Slider healthBarSlider;

    private void Start()
    {
        healthBarSlider = GetComponent<Slider>();
    }

    public void UpdateValue(float value)
    {
        healthBarSlider.value = value;
    }
}
