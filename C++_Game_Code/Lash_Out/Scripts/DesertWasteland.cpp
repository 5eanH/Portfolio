#include <sstream>
#include <fstream>
#include <SFML/Graphics.hpp>
#include <SFML/Audio.hpp>
#include "DesertWasteland.h"
#include "Player.h"
#include "TextureHolder.h"
#include "Bullet.h"
#include "Pickup.h"
#include "TrapDoor.h"

using namespace sf;

int main()
{
	// Here is the instabce of TextureHolder
	TextureHolder holder;

	// The game will always be in one of four states
	enum class State { PAUSED, LEVELING_UP, GAME_OVER, PLAYING };
	// Start with the GAME_OVER state
	State state = State::GAME_OVER;


	// Get the screen resolution and create an SFML window
	Vector2f resolution;
	resolution.x = VideoMode::getDesktopMode().width;
	resolution.y = VideoMode::getDesktopMode().height;

	RenderWindow window(VideoMode(resolution.x, resolution.y),
		"Lash Out", Style::Fullscreen);

	// Create a an SFML View for the main action
	View mainView(sf::FloatRect(0, 0, resolution.x, resolution.y));

	// Here is our clock for timing everything
	Clock clock;
	// How long has the PLAYING state been active
	Time gameTimeTotal;

	// Where is the mouse in relation to world coordinates
	Vector2f mouseWorldPosition;
	// Where is the mouse in relation to screen coordinates
	Vector2i mouseScreenPosition;

	// Create an instance of the Player class
	Player player;

	// Create an instance of the Player class
	TrapDoor TrapDoor;

	// The boundaries of the arena
	IntRect arena;

	

	// Create the background
	VertexArray background;
	// Load the texture for our background vertex array
	Texture textureBackground = TextureHolder::GetTexture(
	"graphics/background_sheet.png");

	// Prepare for a horde of zombies
	int numEnemies;
	int numEnemiesAlive;
	Enemy* enemies = NULL;
	Enemy* boss = NULL;

	// Health bar definition
	Texture healthBarTexture;
	
	//game finished handling
	bool gameEnd = false;

	// 100 bullets should do
	Bullet bullets[100];
	int currentBullet = 0;
	int bulletsSpare = 24;
	int bulletsInClip = 6;
	int clipSize = 6;
	float fireRate = 1;
	int nextLevel = false;
	bool bossDead = false;
	// When was the fire button last pressed?
	Time lastPressed;

	// Hide the mouse pointer and replace it with crosshair
	window.setMouseCursorVisible(false);
	Sprite spriteCrosshair;
	Texture textureCrosshair = TextureHolder::GetTexture("graphics/crosshair.png");
	spriteCrosshair.setTexture(textureCrosshair);
	spriteCrosshair.setOrigin(25, 25);

	// Create a couple of pickups
	Pickup healthPickup(1);
	Pickup ammoPickup(2);
	Pickup shotgun(3);
	Pickup rifle(4);
	Pickup handgun(5);
	Pickup c4Pickup(6);

	// About the game
	int score = 0;
	int hiScore = 0;

	// For the home/game over screen
	Sprite spriteGameOver;
	Texture textureGameOver = TextureHolder::GetTexture("graphics/menu.png");
	spriteGameOver.setTexture(textureGameOver);
	spriteGameOver.setPosition(0, 0);

	// For the home/game over screen
	Sprite spriteUI;
	Texture textureUI = TextureHolder::GetTexture("graphics/Overlay.png");
	spriteUI.setTexture(textureUI);
	spriteUI.setPosition(0, 0);

	// Create a view for the HUD
	View hudView(sf::FloatRect(0, 0, resolution.x, resolution.y));

	// Create a sprite for the ammo icon
	Sprite spriteAmmoIcon;
	Texture textureAmmoIcon = TextureHolder::GetTexture("graphics/ammo_icon.png");
	spriteAmmoIcon.setTexture(textureAmmoIcon);
	spriteAmmoIcon.setPosition(1420, 996);

	//inventory hotbar icon
	Sprite spriteHotBar;
	Texture textureHotBar = TextureHolder::GetTexture("graphics/handgun_thum.png");
	spriteHotBar.setTexture(textureHotBar);
	spriteHotBar.setPosition(1494, 78);

	//coin sprite
	Sprite spriteCoin;
	Texture textureCoin = TextureHolder::GetTexture("graphics/coin.png");
	spriteCoin.setTexture(textureCoin);
	spriteCoin.setPosition(-500, -500);
	


	Sprite passedGame;
	Texture texturePassedGame = TextureHolder::GetTexture("graphics/passedGame.png");
	passedGame.setTexture(texturePassedGame);
	passedGame.setPosition(0,0);

	// Load the font
	Font font;
	font.loadFromFile("fonts/zombiecontrol.ttf");

	// Paused
	Text pausedText;
	pausedText.setFont(font);
	pausedText.setCharacterSize(155);
	pausedText.setFillColor(Color::White);
	pausedText.setPosition(400, 400);
	pausedText.setString("Press Enter \nto continue");

	// Game Over
	Text gameOverText;
	gameOverText.setFont(font);
	gameOverText.setCharacterSize(125);
	gameOverText.setFillColor(Color::White);
	gameOverText.setPosition(250, 850);
	gameOverText.setString("Press Enter to play");

	// Levelling up
	Text levelUpText;
	levelUpText.setFont(font);
	levelUpText.setCharacterSize(80);
	levelUpText.setFillColor(Color::White);
	levelUpText.setPosition(150, 250);
	std::stringstream levelUpStream;
	levelUpStream <<
		"1- Play" <<
		"\n2- Controls" <<
		"\n3- Increased max health" <<
		"\n4- Increased run speed" <<
		"\n5- More and better health pickups" <<
		"\n6- More and better ammo pickups";
	levelUpText.setString(levelUpStream.str());

	// Ammo
	Text ammoText;
	ammoText.setFont(font);
	ammoText.setCharacterSize(35);
	ammoText.setFillColor(Color::White);
	ammoText.setPosition(1510, 1003);

	// Score
	Text scoreText;
	scoreText.setFont(font);
	scoreText.setCharacterSize(35);
	scoreText.setFillColor(Color::White);
	scoreText.setPosition(1690, 97);

	// Load the high score from a text file/
	std::ifstream inputFile("gamedata/scores.txt");
	if (inputFile.is_open())
	{
		inputFile >> hiScore;
		inputFile.close();
	}

	// Hi Score
	Text hiScoreText;
	hiScoreText.setFont(font);
	hiScoreText.setCharacterSize(35);
	hiScoreText.setFillColor(Color::White);
	hiScoreText.setPosition(1690, 25);

	std::stringstream s;
	s << "Hi Score:" << hiScore;
	hiScoreText.setString(s.str());

	// Zombies remaining
	Text zombiesRemainingText;
	zombiesRemainingText.setFont(font);
	zombiesRemainingText.setCharacterSize(35);
	zombiesRemainingText.setFillColor(Color::White);
	zombiesRemainingText.setPosition(340, 1002);
	zombiesRemainingText.setString("Goons: 10");

	// Wave number
	int wave = 0;
	Text waveNumberText;
	waveNumberText.setFont(font);
	waveNumberText.setCharacterSize(35);
	waveNumberText.setFillColor(Color::White);
	waveNumberText.setPosition(1690, 169);
	waveNumberText.setString("Wave: 1");

	// Health bar
	RectangleShape healthBar;
	RectangleShape powerUp;
	healthBar.setFillColor(Color::Red);
	healthBar.setPosition(750, 990);
	powerUp.setFillColor(Color::Yellow);
	powerUp.setPosition(80, 50);
	float timeBarStartWidth = 200;
	float timeBarHeight = 20;
	healthBar.setSize(Vector2f(timeBarStartWidth, timeBarHeight));
	
	bool sprintActive = false;

	// Sprint bar
	RectangleShape sprintBar;
	sprintBar.setFillColor(Color::Blue);
	sprintBar.setPosition(1840, 560);

	
	// When did we last update the HUD?
	int framesSinceLastHUDUpdate = 0;
	// What time was the last update
	Time timeSinceLastUpdate;
	// How often (in frames) should we update the HUD
	int fpsMeasurementFrameInterval = 1000;



	// Prepare the hit sound
	SoundBuffer hitBuffer;
	hitBuffer.loadFromFile("sound/hit.wav");
	Sound hit;
	hit.setBuffer(hitBuffer);

	// Prepare the splat sound
	SoundBuffer splatBuffer;
	splatBuffer.loadFromFile("sound/splat.wav");
	sf::Sound splat;
	splat.setBuffer(splatBuffer);

	// Prepare the shoot sound
	SoundBuffer shootBuffer;
	shootBuffer.loadFromFile("sound/shoot.wav");
	Sound shoot;
	shoot.setBuffer(shootBuffer);

	// Prepare the reload sound
	SoundBuffer reloadBuffer;
	reloadBuffer.loadFromFile("sound/reload.wav");
	Sound reload;
	reload.setBuffer(reloadBuffer);

	// Prepare the failed sound
	SoundBuffer reloadFailedBuffer;
	reloadFailedBuffer.loadFromFile("sound/reload_failed.wav");
	Sound reloadFailed;
	reloadFailed.setBuffer(reloadFailedBuffer);

	// Prepare the powerup sound
	SoundBuffer powerupBuffer;
	powerupBuffer.loadFromFile("sound/powerup.wav");
	Sound powerup;
	powerup.setBuffer(powerupBuffer);

	// Prepare the pickup sound
	SoundBuffer pickupBuffer;
	pickupBuffer.loadFromFile("sound/pickup.wav");
	Sound pickup;
	pickup.setBuffer(pickupBuffer);

	// Prepare the explosion sound
	SoundBuffer expBuffer;
	expBuffer.loadFromFile("sound/explosion.wav");
	Sound explosion;
	explosion.setBuffer(expBuffer);

	
	// The main game loop
	while (window.isOpen())
	{
		/*
		************
		Handle input
		************
		*/

		// Handle events
		Event event;
		while (window.pollEvent(event))
		{
			if (event.type == Event::KeyPressed)
			{
				// Pause a game while playing
				if (event.key.code == Keyboard::Return &&
					state == State::PLAYING)
				{
					state = State::PAUSED;
				}

				// Restart while paused
				else if (event.key.code == Keyboard::Return &&
					state == State::PAUSED)
				{
					state = State::PLAYING;
					// Reset the clock so there isn't a frame jump
					clock.restart();
				}

				// Start a new game while in GAME_OVER state
				else if (event.key.code == Keyboard::Return &&
					state == State::GAME_OVER)
				{
					state = State::LEVELING_UP;
					wave = 0;
					score = 0;

					// Prepare the gun and ammo for next game
					currentBullet = 0;
					bulletsSpare = 24;
					bulletsInClip = 6;
					clipSize = 6;
					fireRate = 1;

					// Reset the player's stats
					player.resetPlayerStats();
				}

				if (state == State::PLAYING)
				{
					// Reloading
					if (event.key.code == Keyboard::R)
					{
						if (bulletsSpare >= clipSize)
						{
							// Plenty of bullets. Reload.
							bulletsInClip = clipSize;
							bulletsSpare -= clipSize;		
							reload.play();
						}
						else if (bulletsSpare > 0)
						{
							// Only few bullets left
							bulletsInClip = bulletsSpare;
							bulletsSpare = 0;				
							reload.play();
						}
						else
						{
							// More here soon?!
							reloadFailed.play();
						}
					}
				}

			}
		}// End event polling


		 // Handle the player quitting
		if (Keyboard::isKeyPressed(Keyboard::Escape))
		{
			window.close();
		}

		// Handle controls while playing
		if (state == State::PLAYING)
		{
			// Handle the pressing and releasing of the WASD keys
			if (Keyboard::isKeyPressed(Keyboard::W))
			{
				player.moveUp();
			}
			else
			{
				player.stopUp();
			}

			if (Keyboard::isKeyPressed(Keyboard::S))
			{
				player.moveDown();
			}
			else
			{
				player.stopDown();
			}

			if (Keyboard::isKeyPressed(Keyboard::A))
			{
				player.moveLeft();
			}
			else
			{
				player.stopLeft();
			}

			if (Keyboard::isKeyPressed(Keyboard::D))
			{
				player.moveRight();
			}
			else
			{
				player.stopRight();
			}


			//sprinting
			if ((Keyboard::isKeyPressed(Keyboard::LShift)) && (player.getSprint() > 10))
			{

				player.startSprintingTimer();
				player.startSprinting();


			}
			else
			{
				player.stopSprinting();
			}

			if ((!Keyboard::isKeyPressed(Keyboard::LShift)) && (player.getSprint() <= 100))
			{

				player.stopSprinting();
				player.stopSprintingTimer();

			}



			// Fire a bullet
			if (sf::Mouse::isButtonPressed(sf::Mouse::Left))
			{

				if (gameTimeTotal.asMilliseconds()
					- lastPressed.asMilliseconds()
					> 1000 / fireRate && bulletsInClip > 0)
				{

					// Pass the centre of the player and the centre of the crosshair
					// to the shoot function
					bullets[currentBullet].shoot(
						player.getCenter().x, player.getCenter().y,
						mouseWorldPosition.x, mouseWorldPosition.y);

					currentBullet++;
					if (currentBullet > 99)
					{
						currentBullet = 0;
					}
					lastPressed = gameTimeTotal;
					shoot.play();
					bulletsInClip--;
				}

			}// End fire a bullet

		}// End WASD while playing

		// Handle the levelling up state
		if (state == State::LEVELING_UP)
		{
			// Handle the player levelling up
			if (event.key.code == Keyboard::Num1)
			{
				// Increase fire rate
				fireRate++;
				state = State::PLAYING;
			}

			if (event.key.code == Keyboard::Num2)
			{
				// Increase clip size
				clipSize += clipSize;
				state = State::PLAYING;
			}

			if (event.key.code == Keyboard::Num3)
			{
				// Increase health
				player.upgradeHealth();
				state = State::PLAYING;
			}

			if (event.key.code == Keyboard::Num4)
			{
				// Increase speed
				player.upgradeSpeed();
				state = State::PLAYING;
			}

			if (event.key.code == Keyboard::Num5)
			{
				healthPickup.upgrade();
				state = State::PLAYING;
			}

			if (event.key.code == Keyboard::Num6)
			{
				ammoPickup.upgrade();
				state = State::PLAYING;
			}


			if (state == State::PLAYING)
			{
				// Increase the wave number
				wave++;

				// Prepare thelevel
				// We will modify the next two lines later
				arena.width = 1000 * wave;
				arena.height = 1000 * wave;
				arena.left = 0;
				arena.top = 10;

				// Pass the vertex array by reference 
				// to the createBackground function

				int tileSize = createBackground(background, arena);

				// Spawn the player in the middle of the arena
				player.spawn(arena, resolution, tileSize);

				// Spawn the Trap door in the middle of the arena
				TrapDoor.spawn(arena);

				// Configure the pick-ups
				healthPickup.setArena(arena);
				ammoPickup.setArena(arena);
				shotgun.setArena(arena);
				rifle.setArena(arena);
				c4Pickup.setArena(arena);
				handgun.setArena(arena);

				// Create a horde of zombies
				numEnemies = 5 * wave;

				// Delete the previously allocated memory (if it exists)
				delete[] enemies;
				enemies = createGang(numEnemies, arena);
				numEnemiesAlive = numEnemies;
				boss = FinalBoss(arena);

				// Play the powerup sound
				powerup.play();

				// Reset the clock so there isn't a frame jump
				clock.restart();
			}
		}// End levelling up

		 /*
		 ****************
		 UPDATE THE FRAME
		 ****************
		 */
		if (state == State::PLAYING)
		{
			// Update the delta time
			Time dt = clock.restart();
			// Update the total game time
			gameTimeTotal += dt;
			// Make a decimal fraction of 1 from the delta time
			float dtAsSeconds = dt.asSeconds();

			// Where is the mouse pointer
			mouseScreenPosition = Mouse::getPosition();

			// Convert mouse position to world coordinates of mainView
			mouseWorldPosition = window.mapPixelToCoords(
				Mouse::getPosition(), mainView);

			// Set the crosshair to the mouse world location
			spriteCrosshair.setPosition(mouseWorldPosition);

			// Update the player
			player.update(dtAsSeconds, Mouse::getPosition());

			// Make a note of the players new position
			Vector2f playerPosition(player.getCenter());

			// Make the view centre around the player				
			mainView.setCenter(player.getCenter());

			// Loop through each Zombie and update them
			for (int i = 0; i < numEnemies; i++)
			{
				if (enemies[i].isAlive())
				{
					enemies[i].update(dt.asSeconds(), playerPosition);
				}
			}
			if (boss->isAlive())
			{
				boss->update(dt.asSeconds(), playerPosition);
			}

			// Update any bullets that are in-flight
			for (int i = 0; i < 100; i++)
			{
				if (bullets[i].isInFlight())
				{
					bullets[i].update(dtAsSeconds);
				}
			}
			if (wave == 1)
			{
				textureBackground = TextureHolder::GetTexture(
					"graphics/background_sheet.png");
				textureGameOver = TextureHolder::GetTexture("graphics/menu.png");
			}
			if (wave == 2)
			{
				textureBackground = TextureHolder::GetTexture(
					"graphics/background_sheet1.png");
				textureGameOver = TextureHolder::GetTexture("graphics/menu1.png");
			}
			if (wave == 3)
			{
				textureBackground = TextureHolder::GetTexture(
					"graphics/background_sheet2.png");
				textureGameOver = TextureHolder::GetTexture("graphics/menu2.png");
			}

			// Update the pickups
			healthPickup.update(dtAsSeconds);
			ammoPickup.update(dtAsSeconds);
			shotgun.update(dtAsSeconds);
			c4Pickup.update(dtAsSeconds);
			rifle.update(dtAsSeconds);
			handgun.update(dtAsSeconds);

			// Collision detection
			// Have any zombies been shot?
			for (int i = 0; i < 100; i++)
			{
				if (bullets[i].isInFlight() &&
					boss->isAlive())
				{
					if (bullets[i].getPosition().intersects
					(boss->getPosition()))
					{

						// Stop the bullet;
						bullets[i].stop();

						// Register the hit and see if it was a kill
						if (boss->hit()) {
							// Not just a hit but a kill too




							if (score >= hiScore)
							{
								hiScore = score;
							}
							if (!boss->isAlive())
							{
								bossDead = true;
							}
						}

						// Make a splat sound
						splat.play();

					}

				}
			}


			for (int i = 0; i < 100; i++)
			{
				for (int j = 0; j < numEnemies; j++)
				{
					if (bullets[i].isInFlight() && 
						enemies[j].isAlive())
					{
						if (bullets[i].getPosition().intersects
							(enemies[j].getPosition()))
						{
							// Stop the bullet
							bullets[i].stop();

							// Register the hit and see if it was a kill
							if (enemies[j].hit()) {
								// Not just a hit but a kill too

								spriteCoin.setPosition(resolution.x/2-500, resolution.y/2-200);

								//score += 10;
								if (score >= hiScore)
								{
									hiScore = score;
								}

								numEnemiesAlive--;

								// When all the zombies are dead (again)
								if (numEnemiesAlive == 0) {

									
										nextLevel = true;
										
									
									
								}
							}	

							// Make a splat sound
							splat.play();
							
						}
					}

				}
			}// End zombie being shot
			sf::FloatRect coinBounds(spriteCoin.getPosition().x, spriteCoin.getPosition().y, spriteCoin.getGlobalBounds().width, spriteCoin.getGlobalBounds().height);
			if (player.getPosition().intersects(coinBounds))
			{
				pickup.play();
				score = score + 10;
				if (score >= hiScore)
				{
					hiScore = score;
				}
				spriteCoin.setPosition(-500, -500);
			}

			if (nextLevel && bossDead &&  wave == 3)
			{
				if (player.getPosition().intersects(TrapDoor.getPosition()))
				{
					gameEnd = true;
					passedGame.setPosition(0, 0);

					if (Keyboard::isKeyPressed(Keyboard::P))
					{
						
						state = State::GAME_OVER;
						passedGame.setPosition(-5000, -5000);
						nextLevel = false;
						textureHotBar = TextureHolder::GetTexture(
							"graphics/handgun_thum.png");
						spriteHotBar.setTexture(textureHotBar);
						bossDead = false;
					}
					
				}


			}
			if (nextLevel && (wave == 1 || wave == 2))
			{
				if (player.getPosition().intersects(TrapDoor.getPosition()))
				{
					
					state = State::LEVELING_UP;
					nextLevel = false;
				}
				
			}

			
			

			// Have any zombies touched the player			
			for (int i = 0; i < numEnemies; i++)
			{
				if (player.getPosition().intersects
					(enemies[i].getPosition()) && enemies[i].isAlive())
				{

					if (player.hit(gameTimeTotal))
					{
						// More here later
						hit.play();
					}

					if (player.getHealth() <= 0)
					{
						state = State::GAME_OVER;

						std::ofstream outputFile("gamedata/scores.txt");
						outputFile << hiScore;
						outputFile.close();
						
					}
				}
			}// End player touched
			if (wave == 3)
			{
				if (player.getPosition().intersects
				(boss->getPosition()) && boss->isAlive())
				{

					if (player.hit(gameTimeTotal))
					{
						// More here later
						hit.play();
					}

					if (player.getHealth() <= 0)
					{
						state = State::GAME_OVER;

						std::ofstream outputFile("gamedata/scores.txt");
						outputFile << hiScore;
						outputFile.close();

					}
				}
			}


			// Has the player touched health pickup
			if (player.getPosition().intersects
				(healthPickup.getPosition()) && healthPickup.isSpawned())
			{
				player.increaseHealthLevel(healthPickup.gotIt());
				// Play a sound
				pickup.play();
				
			}

			// Has the player touched ammo pickup
			if (player.getPosition().intersects
			(ammoPickup.getPosition()) && ammoPickup.isSpawned())
			{
				bulletsSpare += ammoPickup.gotIt();
				// Play a sound
				reload.play();
				
			}

			// Has the player touched a shotgun
			if (player.getPosition().intersects
			(shotgun.getPosition()) && shotgun.isSpawned() && Keyboard::isKeyPressed(Keyboard::E))
			{
				
				fireRate = (7);
				shotgun.gotIt();
				// Play a sound
				textureHotBar = TextureHolder::GetTexture(
					"graphics/shotgun_thum.png");
				spriteHotBar.setTexture(textureHotBar);
				
				

			}
			// Has the player touched a rifle
			if (player.getPosition().intersects
			(rifle.getPosition()) && rifle.isSpawned() && Keyboard::isKeyPressed(Keyboard::E))
			{
				fireRate = (14);
				rifle.gotIt();
				// Play a sound
				textureHotBar = TextureHolder::GetTexture(
					"graphics/rifle_thum.png");
				spriteHotBar.setTexture(textureHotBar);

				

			}
			// Has the player touched a handgun
			if (player.getPosition().intersects
			(handgun.getPosition()) && handgun.isSpawned() && Keyboard::isKeyPressed(Keyboard::E))
			{
				fireRate = (2);
				handgun.gotIt();
				// Play a sound
				textureHotBar = TextureHolder::GetTexture(
					"graphics/handgun_thum.png");
				spriteHotBar.setTexture(textureHotBar);


			}
			// Has the player touched a handgun
			if (player.getPosition().intersects
			(c4Pickup.getPosition()) && c4Pickup.isSpawned())
			{
				player.bombHit(c4Pickup.gotIt());
				// Play a sound
				explosion.play();

			}

			// size up the health bar
			healthBar.setSize(Vector2f(player.getHealth() * 3, 70));

			// size up the health bar
			sprintBar.setSize(Vector2f(70, player.getSprint() * 3));

			// Increment the amount of time since the last HUD update
			timeSinceLastUpdate += dt;
			// Increment the number of frames since the last HUD calculation
			framesSinceLastHUDUpdate++;
			// Calculate FPS every fpsMeasurementFrameInterval frames
			if (framesSinceLastHUDUpdate > fpsMeasurementFrameInterval)
			{

				// Update game HUD text
				std::stringstream ssAmmo;
				std::stringstream ssScore;
				std::stringstream ssHiScore;
				std::stringstream ssWave;
				std::stringstream ssZombiesAlive;

				// Update the ammo text
				ssAmmo << bulletsInClip << "/" << bulletsSpare;
				ammoText.setString(ssAmmo.str());

				// Update the score text
				ssScore << "Score:" << score;
				scoreText.setString(ssScore.str());

				// Update the high score text
				ssHiScore << "Hi Score:" << hiScore;
				hiScoreText.setString(ssHiScore.str());

				// Update the wave
				ssWave << "Wave:" << wave;
				waveNumberText.setString(ssWave.str());

				// Update the high score text
				ssZombiesAlive << "Goons:" << numEnemiesAlive;
				zombiesRemainingText.setString(ssZombiesAlive.str());

				framesSinceLastHUDUpdate = 0;
				timeSinceLastUpdate = Time::Zero;
			}// End HUD update

		}// End updating the scene

		 /*
		 **************
		 Draw the scene
		 **************
		 */

		if (state == State::PLAYING)
		{
			window.clear();

			// set the mainView to be displayed in the window
			// And draw everything related to it
			window.setView(mainView);


			// Draw the background
			window.draw(background, &textureBackground);

			window.draw(TrapDoor.getSprite());

			// Draw the coin
			window.draw(spriteCoin);

			// Draw the zombies
			for (int i = 0; i < numEnemies; i++)
			{
				window.draw(enemies[i].getSprite());
			}

			for (int i = 0; i < 100; i++)
			{
				if (bullets[i].isInFlight())
				{
					window.draw(bullets[i].getShape());
				}
			}

			if (wave == 3)
			{
				window.draw(boss->getSprite());


			}
			

			// Draw the player
			window.draw(player.getSprite());

			

			// Draw the pickups is currently spawned
			if (ammoPickup.isSpawned())
			{
				window.draw(ammoPickup.getSprite());
			}
			if (healthPickup.isSpawned())
			{
				window.draw(healthPickup.getSprite());
			}
			if (shotgun.isSpawned())
			{
				window.draw(shotgun.getSprite());
			}
			if (c4Pickup.isSpawned())
			{
				window.draw(c4Pickup.getSprite());
			}
			

			//Draw the crosshair
			window.draw(spriteCrosshair);

			// Switch to the HUD view
			window.setView(hudView);

			
			//draw health bar
			window.draw(healthBar);
			window.draw(sprintBar);
			window.draw(spriteUI);
			


			// Draw all the HUD elements
			window.draw(spriteAmmoIcon);
			window.draw(spriteHotBar);
			window.draw(ammoText);
			window.draw(scoreText);
			window.draw(hiScoreText);
			window.draw(waveNumberText);
			window.draw(zombiesRemainingText);

			if (gameEnd)
			{
				window.draw(passedGame);

			}
		}

		if (state == State::LEVELING_UP)
		{
			window.draw(spriteGameOver);
			window.draw(levelUpText);
		}

		if (state == State::PAUSED)
		{
			window.draw(pausedText);
		}

		if (state == State::GAME_OVER)
		{
			window.draw(spriteGameOver);
			window.draw(gameOverText);
			window.draw(scoreText);
			window.draw(hiScoreText);
		}

		window.display();

	}// End game loop

	return 0;
}