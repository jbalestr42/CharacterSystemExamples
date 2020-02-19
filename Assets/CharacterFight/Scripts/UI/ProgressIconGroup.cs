using UnityEngine;

public class ProgressIconGroup : MonoBehaviour
{
    [SerializeField]
    GameObject _progressUI = null;

    public ProgressIcon Add(Color p_color, bool p_showText)
    {
        GameObject modifierUI = Instantiate(_progressUI);
        modifierUI.transform.SetParent(transform);
        modifierUI.GetComponent<ProgressIcon>().Init(p_color, p_showText);
        return modifierUI.GetComponent<ProgressIcon>();
    }
}
