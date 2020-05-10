using UnityEngine;
using System.Collections;

public class DoNothingAi : AiBehavior
{
	public float ReturnWeight = 0.5f;  //方便设计者设置的明确的权重

	public override float GetWeight()
	{
		return ReturnWeight;
	}

	public override void Execute()
	{
		Debug.Log("Doing Nothing");
	}
}