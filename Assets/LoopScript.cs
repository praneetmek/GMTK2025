using UnityEngine;

public class LoopScript : MonoBehaviour
{
    public OrbUIScript orbPrefab;
    public RectTransform turtle;
    public RectTransform downTrack;
    public RectTransform upTrack;
    public RectTransform bag;
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
        OrbUIScript orb = Instantiate(orbPrefab, downTrack.transform.position + new Vector3 (downTrack.rect.width/2,300,0), Quaternion.identity, downTrack);
        orb.target_y = turtle.rect.y + turtle.rect.height;
        orb.dir = -1;
    }

    public void AddTurtle()
    {
        OrbUIScript orb = Instantiate(orbPrefab, upTrack.transform.position + new Vector3(-upTrack.rect.width / 2, -300, 0), Quaternion.identity, upTrack);
        orb.target_y = bag.transform.position.y - bag.rect.height;
    
        orb.dir = 1;
    }
}
