using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetris {
    public interface IUI {
        bool Active { get; set; }
        void SetConfidence(string confidence);
        void SetThinkingTime(long time);
    }
}
