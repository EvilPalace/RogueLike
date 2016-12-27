using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System;

[SerializeField]
public class EvilRange
{
	public int min;
	public int max;
	public EvilRange (int min, int max)
	{
		this.min = min;
		this.max = max;
	}
	public int GetCount()
	{
		return Random.Range (min, max + 1);
	}
}

public class EvilGameManager : MonoBehaviour {
	public static EvilGameManager instance = null;

	public EvilRange WallCount;
	public EvilRange FoodCount;
	public EvilRange EnemyCount;
	public GameObject[] Floors;
	public GameObject[] OutWalls;
	public GameObject[] Walls;
	public GameObject[] Foods;
	public GameObject[] Enemys;


	private List<Vector2> gridPosition = new List<Vector2>();


	private int line = 8;
	private int row = 8;


	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

		InitData ();

	}

	void InitGame()
	{
		CreateFloor ();
		CreateOutWall ();
		CreateWall ();
		CreateFood ();
		CreateEnemy ();
	}

	void InitData()
	{
		gridPosition.Clear ();

		for (int i = -1; i < line + 1; i++) {
			for (int j = -1; j < row + 1; j++) {
				Vector2 temp = new Vector2 (i, j);
				gridPosition.Add (temp);
			}
		}
	}

	void CreateFloor()
	{
		for (int i = 0; i < line; i++) {
			for (int j = 0; j < row; j++) {
				int randomIndex = Random.Range (0, Floors.Length);
				GameObject instantiate_GameObject = Floors [randomIndex];
				Vector3 instantiate_Position = new Vector3 (i, j, 0);
				Instantiate<GameObject> (instantiate_GameObject, instantiate_Position, Quaternion.identity).transform.localScale = Vector3.one;
			}
		}
	}

	void CreateOutWall()
	{
		for (int i = -1; i < line + 1; i++) {
			for (int j = -1; j < row + 1; j++) {
				if (i == -1 || i == line || j == -1 || j == row) {
					int randomIndex = Random.Range (0, OutWalls.Length);
					GameObject instantiate_GameObject = OutWalls [randomIndex];
					Vector3 instantiate_Position = new Vector3 (i, j, 0);
					Instantiate<GameObject> (instantiate_GameObject, instantiate_Position, Quaternion.identity).transform.localScale = Vector3.one;
				}
			}
		}
	}

	void CreateWall()
	{
		int wallCount = WallCount.GetCount ();
		for (int i = 0; i < wallCount; i++) {
			int random_gridIndex = Random.Range (0, gridPosition.Count);
			int random_wallIndex = Random.Range (0, Walls.Length);
			GameObject instantiate_Wall = Walls [random_wallIndex];
			Vector3 instantiate_Position = gridPosition [random_gridIndex];
			Instantiate<GameObject> (instantiate_Wall, instantiate_Position, Quaternion.identity).transform.localScale = Vector3.one;
			gridPosition.RemoveAt (random_gridIndex);
		}
	}

	void CreateFood()
	{
		int foodCount = FoodCount.GetCount ();
		for (int i = 0; i < foodCount; i++) {
			int random_gridIndex = Random.Range (0, gridPosition.Count);
			int random_foodIndex = Random.Range (0, Foods.Length);
			GameObject instantiate_GameObject = Foods [random_foodIndex];
			Vector3 instantiate_Position = gridPosition [random_foodIndex];
			Instantiate<GameObject> (instantiate_GameObject, instantiate_Position, Quaternion.identity).transform.localScale = Vector3.one;
			gridPosition.RemoveAt (random_gridIndex);
		}
	}

	void CreateEnemy()
	{

	}
}
