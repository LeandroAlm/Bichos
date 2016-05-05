using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bichos
{
    public enum Direction
    {
        RIGHT = 0, LEFT, DOWN, UP
    }

    class CJ
    {
        bool is_moving = false;
        Texture2D image;
        public Vector2 position=new Vector2(10,10);
        Direction direction;
        int spriteWidth, spriteHeight;
        float v;
        float distance;
        private Vector2 lastMovement;
        float spriteColumn = 0f;

        public CJ(ContentManager content, Vector2 position)
        {
            this.position = position;
            this.direction = Direction.RIGHT;
            image = content.Load<Texture2D>("CJ");

            spriteWidth = image.Width / 3;
            spriteHeight = image.Height / 4;

            v = (64 - spriteWidth) * .5f;
        }

        public void Move(Vector2 movement)
        {
            lastMovement = movement;
            if (movement.X == 1)
                direction = Direction.RIGHT;
            else if (movement.X == -1)
                direction = Direction.LEFT;
            else if (movement.Y == 1)
                direction = Direction.DOWN;
            else
                direction = Direction.UP;

            distance = 0f;
            is_moving = true;
        }

        public void Update(GameTime gameTime)
        {
            if (is_moving)
            {
                distance += 4;
                spriteColumn = (spriteColumn + 0.25f);
                if (distance >= 64)
                {
                    spriteColumn = 0f;
                    distance = 0;
                    is_moving = false;
                    position = position + lastMovement;
                }
            }
        }

        public bool isMoving()
        {
            return is_moving;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position: new Vector2(position.X * 64 + v, position.Y * 64) + distanceToVector(),
            color: Color.White, sourceRectangle: new Rectangle(((int)spriteColumn % 3) * spriteWidth, spriteHeight * (int)direction, spriteWidth, spriteHeight));
        }

        public Vector2 distanceToVector()
        {
            switch (direction)
            {
                case Direction.RIGHT:
                    return Vector2.UnitX * distance;
                case Direction.LEFT:
                    return -Vector2.UnitX * distance;
                case Direction.DOWN:
                    return Vector2.UnitY * distance;
                case Direction.UP:
                    return -Vector2.UnitY * distance;
            }
            return Vector2.Zero;
        }

        public Vector2 Position()
        {
            return position;
        }

        public float V()
        {
            return v;
        }
    }
}

