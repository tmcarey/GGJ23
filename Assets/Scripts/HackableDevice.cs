using System;
using System.Collections;
using UnityEngine;

public class HackableDevice : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject visionConePrefab;
    
    private bool isHacked;
    public bool IsHacked => isHacked;

    public bool startHacked;
    public bool canHackFreely;
    
    private void Hack()
    {
        if (isHacked)
            return;
        
        GameManager.Instance.AddHackedDevice(this);
        
        var visionTransform = Instantiate(visionConePrefab).transform;
        visionTransform.SetParent(transform, true);
        visionTransform.localPosition = Vector3.zero;
        
        isHacked = true;
    }

    public void OnHover(bool hoverState)
    {
        GetComponentInChildren<SpriteRenderer>().transform.localScale =
            new Vector3(1,1,1) * (hoverState ? 1.2f : 1.0f);
    }

    public void TryHack()
    {
        if (!GameManager.Instance.CheckHackLos(transform.position))
            return;
        
        Hack();
    }

    private IEnumerator CheckHackProximityRoutine()
    {
        while (!IsHacked)
        {
            yield return null;
            if (GameManager.Instance.CheckHackLos(transform.position, 1.2f))
            {
                Hack();
            }
        }
    }

    private void Start()
    {
        if (startHacked)
        {
            canHackFreely = true;
            Hack();
        }
        else
        {
            //StartCoroutine(CheckHackProximityRoutine());
        }
        GetComponentInChildren<Interactable>().onHover.AddListener(OnHover);
    }
}
