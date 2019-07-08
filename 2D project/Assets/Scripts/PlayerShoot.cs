using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float weaponSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.GetComponent<Bullet>().dirVector = (new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - 
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;

            bullet.GetComponent<Bullet>().speed = weaponSpeed;
        }

    }
}
