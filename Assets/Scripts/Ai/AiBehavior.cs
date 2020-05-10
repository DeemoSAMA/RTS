using UnityEngine;
using System.Collections;

public abstract class AiBehavior : MonoBehaviour
{

	public float WeightMultiplier = 1; //可以让设计者设定数值对AI的影响
	public float TimePassed = 0;
	public abstract float GetWeight(); //表示Ai重要度的值，假如有两个AI，建造AI想创建一个指挥部，攻击AI想寻路到敌方，也会坚持有无敌人要攻击，然后根据权重值，来判断优先执行那个AI
	public abstract void Execute();  //执行决策的方法
}