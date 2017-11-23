using UnityEngine;

namespace Code
{
	public class TowerTypeC : Tower
	{

		public GameObject Initialize(int row, int col)
		{
			Cost = 100;
			Row = row;
			Col = col;
			return gameObject;
		}


	}
}