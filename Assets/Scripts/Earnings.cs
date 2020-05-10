using UnityEngine;
using System.Collections;

public class Earnings : MonoBehaviour
{

	public float CreditsPerSecond = 1;  //每秒生产金币数量 
	private PlayerSetupDefinition player;  //所归属玩家信息  

	// Use this for initialization
	void Start()
	{
		player = GetComponent<Player>().Info;   //得到玩家信息
	}

	// Update is called once per frame
	void Update()
	{
		player.Credits += CreditsPerSecond * Time.deltaTime;  //生产金币
	}
}