using UnityEngine;

namespace Code
{
	public class TowerTypeB : Tower
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