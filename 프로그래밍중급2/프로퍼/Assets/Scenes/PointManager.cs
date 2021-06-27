using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
   //int a = point; GET
   //int point = 100; SET
   public int point{
       get{
           //Debug.Log("get");
           Debug.Log(m_point);
           return m_point;
       }
       set{
           //Debug.Log("set");
           if(value < 0) // "="을 통해 들어온 값을 value로 받는다.
           {
               m_point = 0;
           }
           else{
               m_point = value;
           }
       }
   }

   private int m_point = 0;
}
