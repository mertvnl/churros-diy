using Game.Models;

namespace Game.Interfaces
{
    public interface IFryable
    {
        FryingData FryingData { get; }
        
        void Fry();
        void StopFrying();
    }
}

