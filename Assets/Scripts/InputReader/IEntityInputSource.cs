using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.InputReader
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get; }
        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeAction();
    }
}
