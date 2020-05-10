using UnityEngine;
using System.Collections;

public class AttackInRange : MonoBehaviour
{
	public GameObject ImpactVisual;  //表示可见的冲击波，只是一个射击效果
	public float FindTargetDelay = 1;  // 寻找目标间隔
	public float AttackRange = 20;     //攻击范围
	public float AttackFrequency = 0.25f;  //攻击频率
	public float AttackDamage = 1;      //伤害值

	private ShowUnitInfo target;        //攻击目标的显示信息
	private PlayerSetupDefinition player;   //玩家的所属阵营
	private float findTargetCounter = 0;   //计时器  找到新目标后或者上次攻击后过了多长时间
	private float attackCounter = 0;     //

	// Use this for initialization
	void Start()
	{
		player = GetComponent<Player>().Info;
	}
	/// <summary>
	/// 寻找目标
	/// </summary>
	void FindTarget()
	{
		if (target != null)
		{
			if (Vector3.Distance(transform.position, target.transform.position) > AttackRange)  //目标距离判断
			{
				return;
			}
		}

		foreach (var p in RtsManager.Current.Players)
		{
			if (p == player)
				continue;

			foreach (var unit in p.ActiveUnits)
			{
				if (Vector3.Distance(unit.transform.position, transform.position) < AttackRange)
				{
					target = unit.GetComponent<ShowUnitInfo>();
					return;
				}
			}
		}
		target = null;
	}

	/// <summary>
	/// 攻击逻辑
	/// </summary>
	void Attack()
	{
		if (target == null)
			return;
		target.CurrentHealth -= AttackDamage;
		GameObject.Instantiate(ImpactVisual, target.transform.position, Quaternion.identity);   //产生伤害的时候出现冲击波效果
	}

	// Update is called once per frame
	void Update()
	{
		findTargetCounter += Time.deltaTime;
		if (findTargetCounter > FindTargetDelay)   //寻找目标
		{
			FindTarget();
			findTargetCounter = 0;
		}

		attackCounter += Time.deltaTime;
		if (attackCounter > AttackFrequency)   //攻击
		{
			Attack();
			attackCounter = 0;
		}
	}
}