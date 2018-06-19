using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UniRx
{
    [PublicAPI]
    public static class UnitySimpleAnimationExtensions
    {
        private const string DefaultStateName = "Default";

        public static IObservable<Unit> PlayAsObservable(this SimpleAnimation self, string stateName = DefaultStateName, bool forceRewind = false)
        {
            if (forceRewind)
            {
                self.Rewind(stateName);
            }
            var observable = self.OnStopAsObservable(stateName);
            self.Play(stateName);
            return observable;
        }

        public static IObservable<Unit> OnPlayAsObservable(this SimpleAnimation self, string stateName = DefaultStateName)
        {
            return Observable
                .Merge(
                    // ストリームの一発目から値を持っている場合
                    Observable.EveryUpdate()
                        .Select(__ => self[stateName].time)
                        .Take(1)
                        .Where(x => x > 0.0f)
                        .AsUnitObservable(),
                    // ストリームが 0.0f から変化した場合
                    Observable.EveryUpdate()
                        .Select(__ => self[stateName].time)
                        .Pairwise()
                        .Where(pair => Mathf.Approximately(pair.Previous, 0.0f) && pair.Current > 0.0f)
                        .AsUnitObservable()
                )
                .Share();
        }

        public static IObservable<Unit> OnStopAsObservable(this SimpleAnimation self, string stateName = DefaultStateName)
        {
            return self.OnPlayAsObservable(stateName)
                .SelectMany(
                    _ =>
                        Observable.EveryUpdate()
                            .Select(__ => self[stateName].time)
                            .Pairwise()
                            .Where(pair => Mathf.Approximately(pair.Previous, pair.Current))
                            .Take(1)
                )
                .AsUnitObservable()
                .Share();
        }
    }
}