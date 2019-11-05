using System;
using System.Collections.Generic;

namespace Zetris {
    static class CachedMethod {
        public delegate void InvalidatedEventHandler();
        public static event InvalidatedEventHandler Invalidated;

        public static void InvalidateAll() => Invalidated?.Invoke();
    }

    public class CachedMethod<TResult> {
        Func<TResult> Method;
        TResult Cache;
        bool Cached = false;

        public CachedMethod(Func<TResult> method) {
            Method = method;
            CachedMethod.Invalidated += Invalidate;
        }

        public TResult Call() {
            if (Cached) return Cache;

            Cached = true;
            return Cache = Method.Invoke();
        }

        public void Invalidate() => Cached = false;
    }

    public class CachedMethod<T, TResult> {
        Func<T, TResult> Method;
        Dictionary<T, TResult> Cache = new Dictionary<T, TResult>();

        public CachedMethod(Func<T, TResult> method) {
            Method = method;
            CachedMethod.Invalidated += Invalidate;
        }

        public TResult Call(T arg) {
            if (!Cache.ContainsKey(arg))
                Cache.Add(arg, Method.Invoke(arg));
            
            return Cache[arg];
        }

        public void Invalidate() => Cache.Clear();
    }
}
