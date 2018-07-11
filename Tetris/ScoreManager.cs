using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class ScoreManager
    {
        private static List<int> _scores;

        static ScoreManager()
        {
            _scores = new List<int>();
        }

        public static void AddScore(int score)
        {
            _scores.Add(score);
        }

        public static int GetMax()
        {
            return _scores.Count > 0 ? _scores.Max() : 0;
        }

        public static void Save()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("score.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, _scores);
            }
        }

        public static void Load()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            using (FileStream fs = new FileStream("score.dat", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                    _scores = (List<int>)formatter.Deserialize(fs);
            }
        }
    }
}
