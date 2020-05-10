using UnityEngine;
using System.Collections;

public class CreateBuildingAction : ActionBehavior
{

	public float Cost = 0;     //建筑花费 
	public GameObject BuildingPrefab;//建筑预设
	public float MaxBuildDistance = 30; //最远可建造距离


	public GameObject GhostBuildingPrefab;
	private GameObject active = null;

	public override System.Action GetClickAction()
	{
		return delegate () {
			var player = GetComponent<Player>().Info;
			if (player.Credits < Cost)
			{
				Debug.Log("Not enough, this costs " + Cost);
				return;
			}
			//建造建筑逻辑处理
			var go = GameObject.Instantiate(GhostBuildingPrefab);
			var finder = go.AddComponent<FindBuildingSite>();
			finder.BuildingPrefab = BuildingPrefab;
			finder.MaxBuildDistance = MaxBuildDistance;
			finder.Info = player;
			finder.Source = transform;
			active = go;
		};
	}

	void Update()
	{
		if (active == null)
			return;

		if (Input.GetKeyDown(KeyCode.Escape)) //取消建造建筑
			GameObject.Destroy(active);
	}

	void OnDestroy()
	{
		if (active == null)
			return;

		Destroy(active);
	}
}