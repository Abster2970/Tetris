using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class GameSettings
    {
        public static int CellSize { get; set; } = 35;
        public static int GameBoardWidth { get; set; } = 13;
        public static int GameBoardHeight { get; set; } = 22;
        public static int PreviewWidth { get; set; } = 5;
        public static int PreviewHeight { get; set; } = 6;
    }
}
