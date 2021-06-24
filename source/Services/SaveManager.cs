using System;
using System.Collections.Generic;
using System.Text;

namespace TownOfUs.Services
{
    public interface ISaveManager
    {
        public void Save(GameConfiguration config, int slot);
        public GameConfiguration Load(int slot);
    }

    public class FileSaveManager : ISaveManager
    {
        public GameConfiguration Load(int slot)
        {
            throw new NotImplementedException();
        }

        public void Save(GameConfiguration config, int slot)
        {
            throw new NotImplementedException();
        }
    }
}
