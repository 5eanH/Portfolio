#include "Enemy.h"
#include "TextureHolder.h"
#include <cstdlib>
#include <ctime>

using namespace std;


void Enemy::spawn(float startX, float startY, int type, int seed)
{

	switch (type)
	{
	case 0:
		// Bloater
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/enemy1.png"));

		m_Speed = 40;
		m_Health = 5;
		break;

	case 1:
		// Chaser
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/chaser.png"));

		m_Speed = 70;
		m_Health = 1;
		break;

	case 2:
		// Crawler
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/crawler.png"));

		m_Speed = 20;
		m_Health = 3;
		break;

	case 3:
		// Predator
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/predatorr.png"));

		m_Speed = 20;
		m_Health = 2;
		break;
	
	case 4:
		// Clubber
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/clubber.png"));

		m_Speed = 80;
		m_Health = 4;
		break;


	case 5:
		// Boney
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/boney.png"));

		m_Speed = 90;
		m_Health = 1;
		break;

	case 6:
		// Final Boss
		m_Sprite = Sprite(TextureHolder::GetTexture(
			"graphics/finalBoss.png"));
		m_Speed = 30;
		m_Health = 10;

}





	// Modify the speed to make the zombie unique
	// Every zombie is unique. Create a speed modifier
	srand((int)time(0) * seed);
	// Somewhere between 80 an 100
	float modifier = (rand() % MAX_VARRIANCE) + OFFSET;
	// Express as a fraction of 1
	modifier /= 100; // Now equals between .7 and 1
	m_Speed *= modifier;

	m_Position.x = startX;
	m_Position.y = startY;

	m_Sprite.setOrigin(25, 25);
	m_Sprite.setPosition(m_Position);
}

bool Enemy::hit()
{
	m_Health--;

	if (m_Health < 0)
	{
		// dead
		m_Alive = false;
		m_Sprite.setTexture(TextureHolder::GetTexture(
			"graphics/blood.png"));

		return true;
	}

	// injured but not dead yet
	return false;
}

bool Enemy::isAlive()
{
	return m_Alive;
}

FloatRect Enemy::getPosition()
{
	return m_Sprite.getGlobalBounds();
}


Sprite Enemy::getSprite()
{
	return m_Sprite;
}

void Enemy::update(float elapsedTime,
	Vector2f playerLocation)
{
	float playerX = playerLocation.x;
	float playerY = playerLocation.y;

	// Update the zombie position variables
	if (playerX > m_Position.x)
	{
		m_Position.x = m_Position.x +
			m_Speed * elapsedTime;
	}

	if (playerY > m_Position.y)
	{
		m_Position.y = m_Position.y +
			m_Speed * elapsedTime;
	}

	if (playerX < m_Position.x)
	{
		m_Position.x = m_Position.x -
			m_Speed * elapsedTime;
	}

	if (playerY < m_Position.y)
	{
		m_Position.y = m_Position.y -
			m_Speed * elapsedTime;
	}

	// Move the sprite
	m_Sprite.setPosition(m_Position);

	// Face the sprite in the correct direction
	float angle = (atan2(playerY - m_Position.y,
		playerX - m_Position.x)
		* 180) / 3.141;

	m_Sprite.setRotation(angle);


}