﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeApplication
{
    class Snake : IDrawable
    {
        private readonly bool debug = true;
        private readonly SnakeMover snakeMover;

        private LinkedList<SnakePart> snakeParts;

        public Snake(int length)
        {
            snakeMover = new SnakeMover(this);
            snakeParts = new LinkedList<SnakePart>();

            for (int i = 0; i < length; i++) 
            {
                AddSnakePart();
            }
        }

        public int GetSnakeLength() 
        {
            return snakeParts.Count();
        }

        public SnakePart GetSnakeHead()
        {
            return snakeParts.ElementAt(snakeParts.Count-1);
        }

        public SnakePart GetSnakePart(int i)
        {
            return snakeParts.ElementAt(i);
        }

        public void AddSnakePart() 
        {
            if (snakeParts.Count() == 0)
            {
                snakeParts.AddFirst(new SnakePart());
                snakeParts.First.Value.SetSnakePartType(SnakePartType.Head);
            }
            else 
            {
                snakeParts.AddFirst(new SnakePart());
                if (snakeParts.First.Next.Value.GetSnakePartType() == SnakePartType.Tail) 
                {
                    snakeParts.First.Next.Value.SetSnakePartType(SnakePartType.Body);
                }
            }
        }

        public void EatFood(Food food) 
        {
            GameStateManager.AddPointsToScore(food.MValue);
            snakeMover.ShouldGrow = true;

            #region Debug Tools
            if (debug) 
            {
                Console.WriteLine("Snake ate " + food.MFoodType + " for a value of " + food.MValue + ".");
                Console.WriteLine("Snake now has a score of " + GameStateManager.GetScore() + " and a length of " + snakeParts.Count() + ".");
            }
            #endregion Debug Tools
        }

        public void AddSnakeToMap(MapManager mm)
        {
            SnakePart snakeTail = snakeParts.First();
            mm.PlaceItemOnTile(mm.GetCenterTile(), snakeTail);

            for (LinkedListNode<SnakePart> currentNode = snakeParts.First; currentNode != null;) 
            {
                LinkedListNode<SnakePart> nextNode = currentNode.Next;
                if (nextNode != null) 
                {
                    Tile tile = mm.GetTileInFrontOfSnakePart(currentNode.Value);
                    mm.PlaceItemOnTile(tile, nextNode.Value);
                }
                currentNode = nextNode;
            }
        }

        public void Update(MapManager mm) 
        {
            snakeMover.MoveSnakeParts(snakeParts, mm);
        }

        public void Draw(MapManager mm, Graphics gfx)
        {
            foreach (SnakePart sp in snakeParts) 
            {
                sp.Draw(mm, gfx);
            }
        }
    }
}
