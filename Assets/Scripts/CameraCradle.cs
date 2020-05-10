using UnityEngine;
using System.Collections;

public class CameraCradle : MonoBehaviour
{

	public float Speed = 20;
	public float Height = 80;   //距离地面的高度

	// Use this for initialization
	void Start()
	{
		foreach (var p in RtsManager.Current.Players)   //判断玩家位置，并把摄像机的位置放置玩家的上方
		{
			if (p.IsAi)
				continue;

			var pos = p.Location.position;
			pos.y = Height;
			transform.position = pos;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(
			Input.GetAxis("Horizontal") * Speed * Time.deltaTime,
			Input.GetAxis("Vertical") * Speed * Time.deltaTime,
			0);
	}
}