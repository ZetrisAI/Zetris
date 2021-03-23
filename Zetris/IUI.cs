using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetris {
    public interface IUI {
        void SetActive(bool state);
        void SetConfidence(string confidence);
        void SetThinkingTime(long time);
    }
}
