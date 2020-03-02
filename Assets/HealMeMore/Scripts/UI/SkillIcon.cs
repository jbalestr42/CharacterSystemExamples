using UnityEngine;

public class SkillIcon : MonoBehaviour, IProgressTracker
{
    [SerializeField]
    public class DisplayData : ScriptableObject
    {
        public Color color = Color.white;
        public bool showDuration = true;
        public Sprite sprite = null;

        public DisplayData() {}
    }

    [SerializeField]
    UnityEngine.UI.Image _background = null;

    [SerializeField]
    UnityEngine.UI.Image _overlay = null;

    [SerializeField]
    UnityEngine.UI.Text _text = null;

    bool _showDuration = true;

    public void Init(DisplayData p_displayData)
    {
        _background.sprite = p_displayData.sprite;
        _background.color = p_displayData.color;
        _showDuration = p_displayData.showDuration;
        EnableTimer(p_displayData.showDuration);
    }

    public void UpdateProgress(float p_progress, float p_duration)
    {
        _text.text = p_duration.ToString("F1");
        _overlay.fillAmount = p_progress;
        if (p_progress <= 0f)
        {
            EnableTimer(false);
        }
        else
        {
            EnableTimer(true);
        }
    }

    public void OnEnd()
    {
        Destroy(gameObject);
    }

    public void EnableTimer(bool p_enable)
    {
        if (_showDuration)
        {
            _overlay.enabled = p_enable;
            _text.enabled = p_enable;
        }
    }
}