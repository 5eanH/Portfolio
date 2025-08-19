#include "DesertWasteland.h"
#include "Enemy.h"

Enemy* createGang(int numEnemies, IntRect arena)
{
	Enemy* enemies = new Enemy[numEnemies];

	int maxY = arena.height - 40;
	int minY = arena.top + 40;
	int maxX = arena.width - 40;
	int minX = arena.left + 40;

	for (int i = 0; i < numEnemies; i++)
	{

		// Which side should the zombie spawn
		srand((int)time(0) * i);
		int side = (rand() % 4);
		float x, y;

		switch (side)
		{
		case 0:
			// left
			x = minX;
			y = (rand() % maxY) + minY;
			break;

		case 1:
			// right
			x = maxX;
			y = (rand() % maxY) + minY;
			break;

		case 2:
			// top
			x = (rand() % maxX) + minX;
			y = minY;
			break;

		case 3:
			// bottom
			x = (rand() % maxX) + minX;
			y = maxY;
			break;
		}

		// Bloater, crawler, clubber, boney
		srand((int)time(0) * i * 2);
		int type = (rand() % 5);

		// Spawn the new zombie into the array
		enemies[i].spawn(x, y, type, i);

	}
	return enemies;
}





























































































































































































































	





	