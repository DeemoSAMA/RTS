using UnityEngine;
using System.Collections;

public class DestroyOnNoHealth : MonoBehaviour
{

	public GameObject ExplosionPrefab;   //死亡效果
	private ShowUnitInfo info;           //显示信息

	// Use this for initialization
	void Start()
	{
		info = GetComponent<ShowUnitInfo>();
	}

	// Update is called once per frame
	void Update()
	{
		if (info.CurrentHealth <= 0)
		{
			Destroy(this.gameObject);  //销毁物体  
			GameObject.Instantiate(ExplosionPrefab, transform.position, Quaternion.identity); //添加爆炸效果
		}
	}
}