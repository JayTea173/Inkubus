using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.GameObjects
{
    using Graphics;
    using Graphics.Renderers;
    using Graphics.Shaders;
    class World : Actor
    {
        public WorldTile[] tiles;
        protected int width;
        protected int height;
        protected WorldRenderer renderer;

        public int Width
        {
            get
            {
                return width;
            }

        }

        public int Height
        {
            get
            {
                return height;
            }

        }

        public World(int _width, int _height)
        {
            width = _width;
            height = _height;
            renderer = new WorldRenderer(this);
            ResizeWorld();
        }
        public void ResizeWorld()
        {
            tiles = new WorldTile[width * height];
        }

        public void Fill(WorldTile tile)
        {
            for (int i = 0; i < (width * height); i++)
            {
                tiles[i] = tile;
            }
        }
        public void Render()
        {
            renderer.Render(this);
        }
    }

    class WorldTile
    {
        public Sprite sprite;
        public ShaderProgram shader;

        public WorldTile(Sprite _sprite, ShaderProgram _shader)
        {
            sprite = _sprite;
            shader = _shader;
        }
    }
}
