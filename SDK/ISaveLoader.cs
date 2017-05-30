using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    public interface ISaveLoader
    {
        void Save(object figures, int childWidhtSize, int childHeightSize);

        object Load();
    }
}
