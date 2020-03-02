using UnityEngine;

public class ProgressIconUI : MonoBehaviour, IProgressTracker
{
    [SerializeField]
    UnityEngine.UI.Image _background = null;

    [SerializeField]
    UnityEngine.UI.Image _overlay = null;

    [SerializeField]
    UnityEngine.UI.Text _text = null;

    bool _showDuration = true;

    public void Init(Sprite sprite, Color p_color, bool p_showDuration)
    {
        _background.sprite = sprite;
        _background.color = p_color;
        _showDuration = p_showDuration;
        EnableTimer(p_showDuration);
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