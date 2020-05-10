using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Linq;

public class RtsManager : MonoBehaviour {

	public static RtsManager Current = null;

	public List<PlayerSetupDefinition> Players = new List<PlayerSetupDefinition>();

	public TerrainCollider MapCollider;

	public bool IsGameObjectSafeToPlace(GameObject go)
	{
		var verts = go.GetComponent<MeshFilter>().mesh.vertices;  //获取全部顶点

		var obstacles = GameObject.FindObjectsOfType<NavMeshObstacle>();  //得到障碍物集合
		var cols = new List<Collider>();
		foreach (var o in obstacles)
		{
			if (o.gameObject != go)
			{
				cols.Add(o.gameObject.GetComponent<Collider>());
			}
		}
		//检测每一个顶点，对于每个顶点都要检测其是否在可通过的地形上，
		foreach (var v in verts)
		{
			NavMeshHit hit;
			var vReal = go.transform.TransformPoint(v);   //取得顶点的世界位置坐标，
			NavMesh.SamplePosition(vReal, out hit, 20, NavMesh.AllAreas);

			//接下来需要检测顶点是不是在核实的XZ平面或碰撞体内，因为不关心离地多高，所以可以忽略Y轴，只关心点在不在障碍物内，
			bool onXAxis = Mathf.Abs(hit.position.x - vReal.x) < 0.5f;  //如果这两个点的距离小于0.5  我们认为在 X 轴上是可行的
			bool onZAxis = Mathf.Abs(hit.position.z - vReal.z) < 0.5f;   //如果这两个点的距离小于0.5  我们认为在 Z 轴上是可行的
			bool hitCollider = cols.Any(c => c.bounds.Contains(vReal));  //如果任何一个碰撞体的范围内有这个顶点，则HitCollider为true

			if (!onXAxis || !onZAxis || hitCollider)
			{
				return false;
			}
		}

		return true;
	}
	public Vector3? ScreenPointToMapPosition(Vector2 point)
	{
		var ray = Camera.main.ScreenPointToRay (point);
		RaycastHit hit;
		if (!MapCollider.Raycast (ray, out hit, Mathf.Infinity))
			return null;

		return hit.point;
	}

	// Use this for initialization
	void Start () {
		Current = this;
		foreach (var p in Players) {
			foreach (var u in p.StartingUnits)
			{
				var go = (GameObject)GameObject.Instantiate(u, p.Location.position, p.Location.rotation);

				var player = go.AddComponent<Player>();
				player.Info = p;
				if (!p.IsAi)
				{
					if (Player.Default == null) Player.Default = p;
					go.AddComponent<RightClickNavigation>();
					go.AddComponent<ActionSelect>();
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
