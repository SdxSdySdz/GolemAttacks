using System.Collections;
using UnityEngine;

namespace CodeBase.Core.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}