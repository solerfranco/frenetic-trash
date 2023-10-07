using UnityEngine;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
    private class GraphicsIndex
    {
        public int index;
        public string name;

        public GraphicsIndex(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }

    private readonly GraphicsIndex[] graphics =
    {
        new GraphicsIndex(0, "Very Low"),
        new GraphicsIndex(1, "Low"),
        new GraphicsIndex(2, "Medium"),
        new GraphicsIndex(3, "High"),
        new GraphicsIndex(4, "Very High"),
        new GraphicsIndex(5, "Ultra"),
    };

    [SerializeField]
    private GameObject _previousButton, _nextButton;

    [SerializeField]
    private TextMeshProUGUI _textValue;

    private int _index;
    private int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = value;
            _previousButton.SetActive(_index > 0);
            _nextButton.SetActive(_index < graphics.Length - 1);
            ChangeGraphics();
            _textValue.text = graphics[_index].name;
        }
    }

    private void ChangeGraphics()
    {
        QualitySettings.SetQualityLevel(Index);
    }

    public void IncrementIndex() { Index++; }

    public void DecrementIndex() { Index--; }

    private void Awake()
    {
        Index = QualitySettings.GetQualityLevel();
    }
}
