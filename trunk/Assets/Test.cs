using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private int[] nums;


    // Start is called before the first frame update
    void Start()
    {
	    nums = new int[2];

	    for (int i = 0; i < 5; i++)
	    {
		    nums[i] = i;
	    }

		for (int i = 0; i < nums.Length; i++)
		{
			Debug.Log(nums[i]);
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
