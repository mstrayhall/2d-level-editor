using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2DLevelEditor
{
    class Tile
    {
        private string imageFile;
        private string tileType;

        public Tile()
        {
            imageFile = "";
            tileType = "Empty";
        }

        public string ImageFile
        {
            get
            {
                return imageFile;
            }
            set
            {
                imageFile = value;
            }
        }
        public string TileType
        {
            get
            {
                return tileType;
            }
            set
            {
                tileType = value;
            }
        }
    }
}
