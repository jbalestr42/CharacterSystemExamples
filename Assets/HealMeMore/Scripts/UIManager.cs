using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject _characterPanelPrefab = null;

    [SerializeField]
    GameObject _leftTeam = null;

    [SerializeField]
    GameObject _rightTeam = null;
    
    public BaseCharacter AddLeftTeam()
    {
        GameObject characterPanel = Instantiate(_characterPanelPrefab);
        characterPanel.transform.SetParent(_leftTeam.transform);
        characterPanel.GetComponent<BaseCharacter>().Team = Teams.Left;
        return characterPanel.GetComponent<BaseCharacter>();
    }
    
    public BaseCharacter AddRightTeam()
    {
        GameObject characterPanel = Instantiate(_characterPanelPrefab);
        characterPanel.transform.SetParent(_rightTeam.transform);
        characterPanel.GetComponent<BaseCharacter>().Team = Teams.Right;
        return characterPanel.GetComponent<BaseCharacter>();
    }
}
