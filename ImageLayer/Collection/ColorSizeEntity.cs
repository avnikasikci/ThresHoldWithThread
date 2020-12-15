using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLayer.Collection
{
    public class ColorSizeEntity
    {
        public int ThreadId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int StartWidth { get; set; }
        public int StartHeight { get; set; }
        public int ThreadsCount { get; set; }
        public bool IsLast { get; set; }
    }
}
