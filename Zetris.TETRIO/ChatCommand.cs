using System;
using System.Collections.Generic;

namespace Zetris.TETRIO {
    abstract class ChatCommandBase {
        public const string InvalidParameters = "Invalid parameters.";

        public readonly bool EvenWhileActive;
        public readonly string HelpText;
        public readonly string[] Hints;

        public ChatCommandBase(string help, string[] hints = null, bool evenWhileActive = false) {
            HelpText = help;
            Hints = hints?? new string[0];
            EvenWhileActive = evenWhileActive;
        }

        public abstract string Process(string[] args);
    }

    class ChatCommand: ChatCommandBase {
        readonly Func<string> Processor;

        public ChatCommand(string help, Func<string> processor, bool evenWhileActive = false)
        : base(help, evenWhileActive: evenWhileActive) => Processor = processor;

        public override string Process(string[] args)
            => args.Length == 0? Processor() : null;
    }

    class ChatCommand<T>: ChatCommandBase where T: struct {
        readonly Func<string, T?> CustomConvert;
        readonly Func<T, string> Processor;
        readonly Func<string> Readback;

        string GetReadback() => Readback?.Invoke()?? InvalidParameters;

        public ChatCommand(string help, string hint, Func<string, T?> custom, Func<T, string> processor, Func<string> readback = null, bool evenWhileActive = false)
        : base(help, new string[1] { hint }, evenWhileActive) {
            CustomConvert = custom;
            Processor = processor;
            Readback = readback;
        }

        public override string Process(string[] args) {
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

        public OnOffChatCommand(string help, Func<bool, string> processor, Func<string> readback = null, bool evenWhileActive = false)
        : base(help, "on/off", OnOffToBool, processor, readback, evenWhileActive) {}
    }
}
