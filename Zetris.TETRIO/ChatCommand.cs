using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetris.TETRIO {
    abstract class ChatCommandBase {
        public readonly string HelpText;
        public readonly string[] Hints;

        public ChatCommandBase(string help, string[] hints = null) {
            HelpText = help;
            Hints = hints ?? new string[0];
        }

        public abstract object Process(string[] args);
    }

    class ChatCommand : ChatCommandBase {
        readonly Func<object> Processor;

        public ChatCommand(string help, Func<object> processor)
        : base(help) => Processor = processor;

        public override object Process(string[] args)
            => args.Length == 0 ? Processor() : new { };
    }

    class ChatCommand<T> : ChatCommandBase where T : struct {
        readonly Func<string, T?> CustomConvert;
        readonly Func<T, object> Processor;

        public ChatCommand(string help, string hint, Func<string, T?> custom, Func<T, object> processor)
        : base(help, new string[1] { hint }) {
            CustomConvert = custom;
            Processor = processor;
        }

        public override object Process(string[] args) {
            if (args.Length != 1) return new { response = "Invalid parameters." };

            T arg;
            try {
                arg = (T)Convert.ChangeType(args[0], typeof(T));

            } catch (FormatException) {
                T? attempt = CustomConvert(args[0]);

                if (!attempt.HasValue)
                    return new { response = "Invalid parameters." };

                arg = attempt.Value;
            }

            return Processor(arg);
        }
    }
}
