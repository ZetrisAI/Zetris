using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetris.TETRIO {
    abstract class ChatCommandBase {
        public static object InvalidParameters => new { response = "Invalid parameters." };

        public readonly string HelpText;
        public readonly string[] Hints;

        public ChatCommandBase(string help, string[] hints = null) {
            HelpText = help;
            Hints = hints ?? new string[0];
        }

        public abstract object Process(string[] args);
    }

    class ChatCommand: ChatCommandBase {
        readonly Func<object> Processor;

        public ChatCommand(string help, Func<object> processor)
        : base(help) => Processor = processor;

        public override object Process(string[] args)
            => args.Length == 0 ? Processor() : new { };
    }

    class ChatCommand<T>: ChatCommandBase where T: struct {
        readonly Func<string, T?> CustomConvert;
        readonly Func<T, object> Processor;
        readonly Func<object> Readback;

        object GetReadback() => Readback?.Invoke()?? InvalidParameters;

        public ChatCommand(string help, string hint, Func<string, T?> custom, Func<T, object> processor, Func<object> readback = null)
        : base(help, new string[1] { hint }) {
            CustomConvert = custom;
            Processor = processor;
            Readback = readback;
        }

        public override object Process(string[] args) {
            if (args.Length == 0) return GetReadback();

            if (args.Length != 1) return InvalidParameters;

            T arg;
            try {
                arg = (T)Convert.ChangeType(args[0], typeof(T));

            } catch (FormatException) {
                T? attempt = CustomConvert(args[0]);

                if (!attempt.HasValue)
                    return InvalidParameters;

                arg = attempt.Value;
            }

            return Processor(arg)?? GetReadback();
        }
    }

    class OnOffChatCommand: ChatCommand<bool> {
        static bool? OnOffToBool(string s)
            => new Dictionary<string, bool>() {{"on", true}, {"off", false}}.TryGetValue(s.ToLower(), out bool result) ? (bool?)result : null;

        public OnOffChatCommand(string help, Func<bool, object> processor, Func<object> readback = null)
        : base(help, "on/off", OnOffToBool, processor, readback) {}
    }
}
