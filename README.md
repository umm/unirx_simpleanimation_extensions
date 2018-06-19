# UniRx SimpleAnimation extensions

## What

* Extension methods of `IObservable<T>` for [SimpleAnimation](https://github.com/umm-projects/simple_animation)

## Requirement

* Unity 2018.1
* .NET 4.x / C# 6.0

## Install

```shell
yarn add "umm-projects/unirx_simpleanimation_extensions#^1.0.0"
```

## Usage

### Observe Play, Stop animation

```csharp
GetComponent<SimpleAnimation>()
    .OnPlayAsObservable("ClipName")
    .Subscribe(_ => Debug.Log("ClipName played!"));

GetComponent<SimpleAnimation>()
    .OnStopAsObservable("ClipName")
    .Subscribe(_ => Debug.Log("ClipName stopped!"));
```

### Wait for animation to end while playing animation

```csharp
GetComponent<SimpleAnimation>()
    .PlayAsObservable("ClipName")
    .Subscribe(_ => Debug.Log("ClipName played!"));
```

## License

Copyright (c) 2018 Tetsuya Mori

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)

