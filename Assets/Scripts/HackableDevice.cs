using UnityEngine;

public class HackableDevice : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject visionConePrefab;
    
    public void Hack()
    {
        Instantiate(visionConePrefab, transform);
    }
}
