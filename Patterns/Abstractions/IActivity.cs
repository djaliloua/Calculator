namespace Patterns.Abstractions
{
    public interface IActivity
    {
        bool IsActivity { get; set; }
        void ShowActivity();
        void HideActivity();
    }
}
