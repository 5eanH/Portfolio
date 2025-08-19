#include "TrapDoor.h"
#include "TextureHolder.h"

TrapDoor::TrapDoor()
{

	// Associate a texture with the sprite
	// !!Watch this space!!
	m_Sprite = Sprite(TextureHolder::GetTexture(
		"graphics/Trapdoor.png"));

	// Set the origin of the sprite to the centre, 
	// for smooth rotation
	m_Sprite.setOrigin(25, 25);
}

Sprite TrapDoor::getSprite()
{
	return m_Sprite;
}

FloatRect TrapDoor::getPosition()
{
	return m_Sprite.getGlobalBounds();
}

void TrapDoor::spawn(IntRect arena)
{
	// Place the player in the middle of the arena
	m_Position.x = arena.width / 2;
	m_Position.y = arena.height / 2;

	// Copy the details of the arena to the player's m_Arena
	m_Arena.left = arena.left;
	m_Arena.width = arena.width;
	m_Arena.top = arena.top;
	m_Arena.height = arena.height;

	

	m_Sprite.setPosition(m_Position.x, m_Position.y);

}



