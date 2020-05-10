using UnityEngine;
using System.Collections;

public class StrikeAi : AiBehavior
{

	public int DronesRequired = 10;  //无人机数量

	public float TimeDelay = 5;     //等待时间

	public float SquadSize = 0.5f;   //

	public int IncreastPerWave = 10; //每波增加单位数量


	public override void Execute()
	{
		var ai = AiSupport.GetSupport(this.gameObject);  //获得AI
		Debug.Log(ai.Player.Name + " is attacking");

		int wave = (int)(ai.Drones.Count * SquadSize);
		DronesRequired += IncreastPerWave;

		foreach (var p in RtsManager.Current.Players)
		{
			if (p.IsAi)
				continue;

			for (int i = 0; i < wave; i++)
			{
				var drone = ai.Drones[i];
				var nav = drone.GetComponent<RightClickNavigation>();
				nav.SendToTarget(p.Location.position);
			}
			return;
		}
	}

	public override float GetWeight()
	{
		if (TimePassed < TimeDelay)   //判断是否到了下一波攻击时间
			return 0;
		TimePassed = 0;

		var ai = AiSupport.GetSupport(this.gameObject);
		if (ai.Drones.Count < DronesRequired)   //判断是否决定攻击
			return 0;

		return 1;
	}
}