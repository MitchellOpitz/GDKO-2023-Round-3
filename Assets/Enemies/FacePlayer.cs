using UnityEngine;
using UnityEngine.UI;

public class FacePlayer : MonoBehaviour
{
    public Image healthBar;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - transform.position;

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            healthBar.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
