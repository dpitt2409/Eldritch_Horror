using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestModel : MVC
{
    public int healthHeal = 1;
    public int sanityHeal = 1;

    // Start is called before the first frame update
    void Start()
    {
        healthHeal = 1;
        sanityHeal = 1;
    }
}
