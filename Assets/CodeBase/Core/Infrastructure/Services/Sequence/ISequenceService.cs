using System;

namespace CodeBase.Core.Infrastructure.Services.Sequence
{
    public interface ISequenceService : IService
    {
        void CallLater(Action action, float delay);
    }
}