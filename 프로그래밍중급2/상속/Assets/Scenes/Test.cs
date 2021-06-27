using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     Cat nate = new Cat();
     nate.name = "Nate";
     nate.weight = 1.5f;
     nate.year = 3;

     Dog jack = new Dog();
     jack.name = "Jack";
     jack.weight = 5f;
     jack.year = 2;

     /////

     nate.Stealth();
     nate.Print();

     jack.Hunt();
     jack.Print();   
    }
}
