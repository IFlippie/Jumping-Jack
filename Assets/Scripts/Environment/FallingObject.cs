using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private float speed = 5f;
    private readonly int MinY = -5;
    [SerializeField] GameObject fragileTile;
    [SerializeField] GameObject superFragileTile;


    //Chance in % 
    private readonly float FieldBreakChance = 45;
    private readonly float FragileBreakChance = 20;
    private readonly float SuperFragileBreakChance = 10;
    private float breakChance;


    // Update is called once per frame
    void FixedUpdate()
    {
        //fall
        transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;

        //fall in water
        if (transform.position.y < MinY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
		{
            case "field":
                breakChance = Random.Range(0, 100);

                if (breakChance <= FieldBreakChance)
                {
                    //state worsens > fragile tile
                    Instantiate(fragileTile, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);
                }
                break;

            case "Fragile":
                breakChance = Random.Range(0, 100);

                if (breakChance <= FragileBreakChance)
                {
                    //state worsens > super fragile tile
                    Instantiate(superFragileTile, other.transform.position, other.transform.rotation);
                    Destroy(other.gameObject);
                }
                break;

            case "SuperFragile":
                breakChance = Random.Range(0, 100);

                if (breakChance <= SuperFragileBreakChance)
                {
                    //state worsens > gone
                    Destroy(other.gameObject);
                }
                break;

        }
        Destroy(gameObject);
    }
}
