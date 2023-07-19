using Game.Models;

namespace Game.Interfaces
{
    public interface IFryable
    {
        FryingData FryingData { get; }

        void StartFrying();
        void Fry();
        void StopFrying();
    }
}

