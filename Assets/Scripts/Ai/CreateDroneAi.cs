using UnityEngine;
using System.Collections;

public class CreateDroneAi : AiBehavior
{

	public int DronesPerBase = 5;   //一个建筑可以供养几个无人机
	public float Cost = 25;         //建筑无人机花费
	private AiSupport support;      //

	/// <summary>
	/// 得到生产无人机权重
	/// </summary>
	/// <returns></returns>
	public override float GetWeight()
	{
		if (support == null)
			support = AiSupport.GetSupport(gameObject);

		if (support.Player.Credits < Cost)
			return 0;

		var drones = support.Drones.Count;
		var bases = support.CommandBases.Count;

		if (bases == 0)  //判断是否有生产无人机的建筑
			return 0;

		if (drones >= bases * DronesPerBase) return 0;  //判断无人机总量是否大于建筑可供给的数量

		return 1;

	}

	public override void Execute()
	{
		Debug.Log(support.Player.Name + " is creating a drone.");

		var bases = support.CommandBases;
		var index = Random.Range(0, bases.Count);
		var commandBase = bases[index];
		commandBase.GetComponent<CreateUnitAction>().GetClickAction()();
	}
}