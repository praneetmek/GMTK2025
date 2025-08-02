using UnityEngine;

public class LoopScript : MonoBehaviour
{
    public GameObject essencePrefab;
    public RectTransform downTrack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEssence()
    {
        Instantiate(essencePrefab, downTrack.transform.position + new Vector3 (downTrack.rect.width/2,300,0), Quaternion.identity, downTrack);
    }
}
