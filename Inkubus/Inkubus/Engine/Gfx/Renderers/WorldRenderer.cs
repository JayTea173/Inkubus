﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Inkubus.Engine.Gfx.Renderers
{
    using GameObjects;
    class WorldRenderer : Renderer
    {
        protected World world;
        public WorldRenderer(World _world)
        {
            world = _world;
        }

        public override void Render(Actor actor)
        {
            //if (sprite == null)
            //    return;

            for (int y = 0; y < world.Height; y++)
            {
                for (int x = 0; x < world.Width; x++)
                {
                    WorldTile currentTile = world.tiles[y * world.Width + x];
                    var size = currentTile.sprite.Size;
                    var sizehalf = size * 0.5f;

                    Vector2 cameraPosition = Camera.current.GetPosition();
                    Matrix4 modelView = Matrix4.CreateScale(currentTile.sprite.Size3) * Matrix4.CreateTranslation((float)x * size[0] + sizehalf[0] - (world.Width * sizehalf[0]) - cameraPosition.X, (float)y * size[1] + sizehalf[1] - (world.Height * sizehalf[1]) - cameraPosition.Y, -1f);
                    GL.UniformMatrix4(21, false, ref modelView);
                    currentTile.shader.Use();
                    currentTile.sprite.Bind(0, 0);
                    SpriteRenderer.quadMesh.Bind();
                    SpriteRenderer.quadMesh.Render();
                }
            }
        }
    }
}
