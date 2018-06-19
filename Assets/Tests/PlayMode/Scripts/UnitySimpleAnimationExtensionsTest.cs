using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UniRx
{
    public class UnitySimpleAnimationExtensionsTest
    {
        public class TestBase
        {
            protected SimpleAnimation SimpleAnimation { get; private set; }

            private GameObject GoCamera { get; set; }

            private GameObject GoCube { get; set; }

            [SetUp]
            public void SetUp()
            {
                GoCamera = new GameObject("Camera");
                GoCamera.AddComponent<Camera>();
                GoCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                SimpleAnimation = GoCube.AddComponent<SimpleAnimation>();
                SimpleAnimation.playAutomatically = false;
                SimpleAnimation.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                SimpleAnimation.AddClip(Resources.Load<AnimationClip>("Animations/Move"), "Move");
                SimpleAnimation.AddClip(Resources.Load<AnimationClip>("Animations/Scale"), "Scale");
            }

            [TearDown]
            public void TearDown()
            {
                Object.Destroy(GoCamera);
                Object.Destroy(GoCube);
            }
        }

        public class PlayAsObservableTest : TestBase
        {
            [UnityTest]
            public IEnumerator Test()
            {
                SimpleAnimation.PlayAsObservable("Move").Subscribe(_ => Assert.Pass());
                yield return new WaitForSeconds(2.0f);
                Assert.Fail();
            }
        }

        public class OnPlayAsObservableTest : TestBase
        {
            [UnityTest]
            public IEnumerator Test()
            {
                SimpleAnimation.OnPlayAsObservable("Move").Subscribe(_ => Assert.Pass());
                SimpleAnimation.Play("Move");
                yield return new WaitForSeconds(2.0f);
                Assert.Fail();
            }
        }

        public class OnStopAsObservableTest : TestBase
        {
            [UnityTest]
            public IEnumerator Test()
            {
                SimpleAnimation.OnStopAsObservable("Move").Subscribe(_ => Assert.Pass());
                SimpleAnimation.Play("Move");
                yield return new WaitForSeconds(2.0f);
                Assert.Fail();
            }
        }
    }
}