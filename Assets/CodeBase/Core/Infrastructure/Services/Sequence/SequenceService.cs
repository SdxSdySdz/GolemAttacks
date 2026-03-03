using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Services.Sequence
{
    public class SequenceService : ISequenceService
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SequenceService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void CallLater(Action action, float delay)
        {
            _coroutineRunner.StartCoroutine(CallAfterRoutine(action, delay));
        }

        private IEnumerator CallAfterRoutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}