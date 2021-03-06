﻿namespace SnakeApplication
{
    class SnakePartDirection
    {
        Direction currentDirection = Direction.Left;

        public Direction GetCurrentDirection() 
        {
            return currentDirection;
        }

        public Direction GetOppositeDirection()
        {
            switch (currentDirection)
            {
                case Direction.Up:
                    return Direction.Down;

                case Direction.Down:
                    return Direction.Up;

                case Direction.Left:
                    return Direction.Right;

                case Direction.Right:
                    return Direction.Left;

                default:
                    return Direction.Right;
            }
        }

        public void SetCurrentDirection(Direction direction) 
        {
            currentDirection = direction;
        }
    }
}
