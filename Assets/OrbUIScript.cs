using UnityEngine;

public class OrbUIScript : MonoBehaviour
{
    public float target_y;
    public float dir;


    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + dir * new Vector3(0,  60f * Time.deltaTime, 0);
        if(Mathf.Abs(transform.position.y - target_y) < 10)
        {
            if(dir == -1)
            {
                GameManager.Instance.AddEssence(1);
            }
            else
            {
                GameManager.Instance.AddTurtle();
            }
            Destroy(this.gameObject);
        }
    }
}
