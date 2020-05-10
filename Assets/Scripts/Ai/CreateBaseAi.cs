using UnityEngine;
using System.Collections;

public class CreateBaseAi : AiBehavior
{

	private AiSupport support = null;  //获取支持类

	public float Cost = 200;          //建造花费

	public int UnitsPerBase = 5;       //表示一个建筑可以生产多少个单位

	public float RangeFromDrone = 30;   //离单位多远才能创建建筑

	public int TriesPerDrone = 3;       //一个单位要尝试创建多少次才能轮到下个单位

	public GameObject BasePrefab;       //建筑物预设

	public override float GetWeight()    //获得建造权重
	{
		if (support == null)
			support = AiSupport.GetSupport(gameObject);

		if (support.Player.Credits < Cost || support.Drones.Count == 0)  //判断有足够的钱和无人机
			return 0;

		if (support.CommandBases.Count == 0)  //如果没有建筑，则优先建造建筑
			return 1;

		if (support.CommandBases.Count * UnitsPerBase <= support.Drones.Count)
			return 1;

		return 0;
	}

	public override void Execute()    //
	{
		Debug.Log("Creating Base");

		var go = GameObject.Instantiate(BasePrefab);
		go.AddComponent<Player>().Info = support.Player;

		foreach (var drone in support.Drones)
		{
			for (int i = 0; i < TriesPerDrone; i++)
			{
				var pos = drone.transform.position;
				pos += Random.insideUnitSphere * RangeFromDrone;
				pos.y = Terrain.activeTerrain.SampleHeight(pos); //确保在地表之上

				go.transform.position = pos;

				if (RtsManager.Current.IsGameObjectSafeToPlace(go))  //判断是否在可建造的位置
				{
					support.Player.Credits -= Cost;
					return;
				}
			}
		}
		Destroy(go);
	}
}