using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Logic
{
    [Serializable]
    class PlayerData
    {
        public int score = 0;
        public string achi= "新手";
        public int win = 0;
        public int fail = 0;

        public PlayerData()
        {
            score = 100;
        }
    }
}
