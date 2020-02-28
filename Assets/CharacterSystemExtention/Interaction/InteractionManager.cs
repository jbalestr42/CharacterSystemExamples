using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionManager : Singleton<InteractionManager>
{
    enum Mode
    {
        UI,
        GameObject
    }

    AInteraction _interaction = null;
    ISelectable _selectable = null;
    bool _isOver = false;
    Ray ray;
    RaycastHit hit;

    [SerializeField]
    GraphicRaycaster _raycaster = null;

    [SerializeField]
    EventSystem _eventSystem = null;

    PointerEventData _pointerEventData;
    List<RaycastResult> _results = new List<RaycastResult>();
    
    Mode _mode = Mode.UI;

    void Update()
    {
        if (_interaction != null)
        {
            UpdateInteraction();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateRay(go => 
                {
                    ISelectable selectable = go?.GetComponent<ISelectable>();
                    if (selectable != null)
                    {
                        Debug.Log("Hit " + go.gameObject.name);

                        if (_selectable != selectable)
                        {
                            // Unselect current selectable
                            if (_selectable != null)
                            {
                                _selectable.UnSelect();
                                _selectable = null;
                            }

                            // Select new selectable
                            if (selectable != null)
                            {
                                _selectable = selectable;
                                _selectable.Select();
                            }
                        }
                    }
                });

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (_selectable != null)
                    {
                        _selectable.UnSelect();
                        _selectable = null;
                    }
                }
            }
            // TODO: faire un effet si un element en dessous de la souris est interactable. IInteractable ?
        }
    }

    public void UpdateInteraction()
    {
        CreateRay(go => 
        {
            if (go != null)
            {
                if (!_isOver)
                {
                    _isOver = true;
                    _interaction.OnMouseEnter(go, hit.point);
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _interaction.OnMouseClick(go, hit.point);
                    }
                    else
                    {
                        _interaction.OnMouseOver(go, hit.point);
                    }
                }
            }
            else if (_isOver)
            {
                _isOver = false;
                _interaction.OnMouseExit();
            }
        });

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelInteraction();
        }
    }

    void CreateRay(Action<GameObject> action)
    {
        bool hitObject = false;

        switch (_mode)
        {
        case Mode.GameObject:
            int layerMask = _interaction != null ? 1 << _interaction.GetLayer() : 0;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && IsValidTarget(hit.collider.gameObject))
            {
                action(hit.collider.gameObject);
                hitObject = true;
            }
            break;

        case Mode.UI:
            _pointerEventData = new PointerEventData(_eventSystem);
            _pointerEventData.position = Input.mousePosition;
            _results.Clear();
            _raycaster.Raycast(_pointerEventData, _results);
            foreach (RaycastResult result in _results)
            {
                if (IsValidTarget(result.gameObject))
                {
                    action(result.gameObject);
                    hitObject = true;
                    break;
                }
            }
            break;

        default:
            throw new NotImplementedException();
        }

        if (!hitObject)
        {
            action(null);
        }
    }

    bool IsValidTarget(GameObject p_target)
    {
        return _interaction != null ? _interaction.IsValidTarget(p_target) : p_target.GetComponent<ISelectable>() != null;
    }

    public AInteraction GetInteraction()
    {
        return _interaction;
    }

    public void SetInteraction(AInteraction interaction)
    {
        CancelInteraction();
        _interaction = interaction;
    }

    public void CancelInteraction()
    {
        if (_interaction != null)
        {
            _interaction.OnInteractionCancelled();
            _interaction.Cancel();
            _interaction = null;
        }
    }

    public void EndInteraction()
    {
        if (_interaction != null)
        {
            _interaction.OnInteractionDone();
            _interaction.End();
            _interaction = null;
        }
    }
}