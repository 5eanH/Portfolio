#pragma once
#include <SFML/Graphics.hpp>

using namespace sf;

class TrapDoor
{
private:
	// Where is the player
	Vector2f m_Position;

	// Of course we will need a sprite
	Sprite m_Sprite;

	// And a texture
	// !!Watch this space!!
	Texture m_Texture;

	// What is the screen resolution
	Vector2f m_Resolution;

	// What size is the current arena
	IntRect m_Arena;

	// How big is each tile of the arena
	int m_TileSize;


	// All our public functions will come next
public:

	TrapDoor();

	void spawn(IntRect arena);

	// Where is the trapdoor
	FloatRect getPosition();

	// Send a copy of the sprite to main
	Sprite getSprite();
};



#pragma once
