using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMap
{
    [System.Serializable]
    public class floatMainMax
    {
        public float min;
        public float max;

        public float GetValue()
        {
            return Random.Range(min, max);
        }
    }
}

namespace GameMap
{
    [System.Serializable]
    public class IntMinMax
    {
        public int min;
        public int max;

        public int GetValue()
        {

         return Random.Range(min, max + 1);
        }
    }

}

