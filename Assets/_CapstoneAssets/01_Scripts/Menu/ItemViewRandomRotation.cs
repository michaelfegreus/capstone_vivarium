using UnityEngine.UI;
using UnityEngine;

public class ItemViewRandomRotation : MonoBehaviour
{
    [SerializeField] Transform rotateTransform;

    [SerializeField] float randomRotationRangeMin;
    [SerializeField] float randomRotationRangeMax;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeRotation();
    }
    private void OnEnable()
    {
        RandomizeRotation();
    }

    public void RandomizeRotation()
    {
        rotateTransform.localRotation = Quaternion.Euler(rotateTransform.localRotation.x, rotateTransform.localRotation.y, Random.Range(randomRotationRangeMin, randomRotationRangeMax));

    }
}