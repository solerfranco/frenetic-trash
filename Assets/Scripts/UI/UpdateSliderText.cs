using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UpdateSliderText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _sliderText;
    
    [SerializeField]
    private Slider _slider;

    public void ChangeSliderText(float value)
    {
        _sliderText.text = ((_slider.maxValue * 100) * value).ToString("0") + "%";
    }
}
