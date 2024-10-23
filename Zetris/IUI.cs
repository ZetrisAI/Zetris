namespace Zetris {
    public interface IUI {
        bool Active { get; set; }
        void SetConfidence(string confidence);
        void SetThinkingTime(long time);
    }
}
