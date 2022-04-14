using UnityEngine;
using UnityEngine.UI;
public class LookAtPlayer : MonoBehaviour
{
    public GameObject JohnLemon;
    private Vector3 _angles;

    void Start()
    {
        // Reuse rather than creating this every update.
        _angles = new Vector3(0.0f, 1.0f, 0.0f);
    }
    void FixedUpdate()
    {
        transform.Rotate(_angles * 50.0f * Time.deltaTime);
                
        // Calculate vector from pickup to player.
        Vector3 d = JohnLemon.transform.position - transform.position;
        // Normalize to a direction.
        d.Normalize();

        //Calculate Angle
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Vector3.forward, d));
        _angles.y = angle;

        //Update model
        transform.eulerAngles = _angles;

    }
}