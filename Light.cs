using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph6
{
    public class Light
    {
        public MyPoint position = new MyPoint(0, 0, 0);
        public Light(MyPoint position)
        {
            this.position = position;
        }

        public Light(float x, float y, float z)
        {
            position = new MyPoint(x, y, z);
        }
    }
}
