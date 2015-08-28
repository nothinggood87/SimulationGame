using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace UniverseSimV1
{
    static class TileTypes
    {
        private const string baseLocation = @"C:\Repos\Sim\UniverseSimV1\UniverseSimV1\resources\";
        public const int textureSize = 16;
        public enum textures
        {
            space = 0,
            water,
            rock,
            player
        }
        private static Image GetImage(int Id)
        {
            Image getImage = new Image();
            getImage.Height = textureSize;
            getImage.Width = textureSize;
            getImage.Source = GetSource(Id);
            return getImage;
        }
        private static ImageSource GetSource(int Id) => new BitmapImage(new Uri(baseLocation + textureSize + @"\" + Enum.GetName(typeof(textures), Id) + ".bmp"));
        public static Image GetTexture(Tile tile)
        {
            if(tile.IsPlayer)
            {
                return GetTexture((int)textures.player);
            }
            if(tile.mass == (int)textures.player)
            {
                return GetTexture((int)textures.rock);
            }
            return GetTexture(tile.mass);
        }
        public static Image GetTexture(int Id)
        {
            if(Id > (int)textures.player)
            {
                return getTexture[(int)textures.rock];
            }
            return GetImage(Id);
        }
        //public static Image GetTexture(int Id) => getTexture[Id];
        private static Image[] getTexture => new Image[4]
        {
            GetImage((int)textures.space),
            GetImage((int)textures.water),
            GetImage((int)textures.rock),
            GetImage((int)textures.player)
        };
    }
}
