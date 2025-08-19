#pragma once
#include "Enemy.h"

using namespace sf;


int createBackground(VertexArray& rVA, IntRect arena);
Enemy* createGang(int numEnemies, IntRect arena);
Enemy* FinalBoss(IntRect arena);