using UnityEngine;
using System.Collections;

public class FindBuildingSite : MonoBehaviour
{

	public float Cost = 200;   //建造费用
	public float MaxBuildDistance = 30;  //建造距离
	public GameObject BuildingPrefab;    //建筑预设
	public PlayerSetupDefinition Info;   //玩家信息
	public Transform Source;           //资源的位置坐标，用来判断是否可建造

	Renderer rend;
	Color Red = new Color(1, 0, 0, 0.5f);
	Color Green = new Color(0, 1, 0, 0.5f);

	void Start()
	{
		MouseManager.Current.enabled = false;
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update()
	{
		var tempTarget = RtsManager.Current.ScreenPointToMapPosition(Input.mousePosition);
		if (tempTarget.HasValue == false)
			return;

		transform.position = tempTarget.Value;

		if (Vector3.Distance(transform.position, Source.position) > MaxBuildDistance)
		{
			rend.material.color = Red;
			return;
		}

		if (RtsManager.Current.IsGameObjectSafeToPlace(gameObject))
		{
			rend.material.color = Green;
			if (Input.GetMouseButtonDown(0))
			{
				var go = GameObject.Instantiate(BuildingPrefab);
				go.AddComponent<ActionSelect>();   //增加选择动作
				go.transform.position = transform.position;
				Info.Credits -= Cost;
				go.AddComponent<Player>().Info = Info;
				Destroy(this.gameObject);
			}
		}
		else
		{
			rend.material.color = Red;
		}
	}

	void OnDestroy()
	{
		MouseManager.Current.enabled = true;
	}

}