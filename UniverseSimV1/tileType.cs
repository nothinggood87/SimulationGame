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
        public const int textureSize = 8;
        private enum textures
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
        private static Image GetTexture(Tile tile) => GetTexture(GetProcessedTextureId(tile));
        private static int GetProcessedTextureId(Tile tile)
        {
            if (tile.IsPlayer)
            {
                return (int)textures.player;
            }
            if (tile.mass > (int)textures.rock)
            {
                return (int)textures.rock;
            }
            return tile.mass;
        }
        public static Image GetTexture(int ProcessedId) => GetImage(ProcessedId);
        private static Image[] getTexture { get; } = new Image[4]
        {
            GetImage((int)textures.space),
            GetImage((int)textures.water),
            GetImage((int)textures.rock),
           GetImage((int)textures.player)
        };
        public static int[,] GetProcessedIds(Map map)
        {
            int[,] IdsMap = new int[map.Width,map.Height];
            for(int i = 0;i < map.Height;i++)
            {
                for (int j = 0; j < map.Width; j++)
                {
                    IdsMap[i, j] = GetProcessedTextureId(map.map[i,j]);
                }
            }
            return IdsMap;
        }
    }
}
