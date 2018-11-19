using System;
using System.Diagnostics;
using System.Reflection;

using UnityEngine;
using UnityEngine.UI;

public static class StopwatchExtensions
{
    public delegate void TestFunction();
    public static long RunTest(this Stopwatch stopwatch, TestFunction testFunction)
    {
        stopwatch.Reset();
        stopwatch.Start();

        testFunction();

        return stopwatch.ElapsedMilliseconds;
    }
}

public class TestClass
{
    public int IntField;
    public int IntProperty { get; set; }
    public void VoidMethod() { }
    public bool field;
}

public class PerformanceTests : MonoBehaviour
{
    public int NumIterations = 10000000;

    private string report="";
    public bool field;
    public nested Nested;
    public float value;
    public bool bool1;
    [System.Serializable]
    public class nested
    {
        public float value;
        public nested2 _nested2;
        public bool bool1;
        [System.Serializable]
        public class nested2
        {
            public float value;
            public neste3 _nested3;
            [System.Serializable]
            public class neste3
            {
                public float value;
            }
        }
        public bool DirectMethod()
        {
            return bool1;
        }
    }
    public bool DirectMethod()
    {
        return bool1;
    }
    delegate bool MathAction();
    void Start()
    {
        var stopwatch = new Stopwatch();

        //var testClassInstance = new TestClass();
    //    bool bool1 = true;

        //System.Type type = typeof(bool);
        //System.Type type2 = typeof(string);

        var speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                Nested.DirectMethod();
            }
        });
        
       // print("Direct Method: " + speed);
        MathAction ma = Nested.DirectMethod;
        speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                bool1 = ma();
            }
        });
        print("Delegated Method: " + speed);
        speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                bool1 = DirectMethod();
            }
        });
        print("Direct Method: " + speed);
        speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                bool1 = Nested.DirectMethod();
            }
        });
        print("Nested Direct Method: " + speed);
        speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                    bool1 = Nested.bool1;
            }
        });
        print("Nested Direct field verification: " + speed);
                                                                                                                         
        speed = stopwatch.RunTest(() =>
        {
            for (long i = 0; i < NumIterations; ++i)
            {
                bool1 = Nested.bool1;
            }
        });
        print("Nested Direct field verification: " + speed);

        //var nestedVariableClassAcess = stopwatch.RunTest(() =>
        //{
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }
        //    if (testClassInstance.field == true) { }

        //});
        //var Nested3Value = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        Nested._nested2._nested3.value += 1f;
        //    }
        //});

        //var Nested0Value = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        value += 1f;
        //    }
        //});

        //var Nested1Value = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        Nested.value += 1f;
        //    }
        //});

        //var Nested2Value = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        Nested._nested2.value += 1f;
        //    }
        //});

        //var emptyFor = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //    }
        //});

        //var CompareType = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        if (type == type2)
        //        {
        //        }
        //    }
        //});

        //var CompareBool = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        if (bool1 == bool2) { }
        //    }
        //});

        //var CommpareString = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        if (string1 == string2) { }
        //    }
        //});

        //var CompareSplit = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        string[] splitedString = stringToSplit.Split('.');
        //    }
        //});

        //var getTypeTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        // testClassType = typeof(TestClass);
        //    }
        //});

        //var getFieldTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        // intFieldInfo = testClassType.GetField("IntField");
        //    }
        //});

        //var getPropertyTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        // intPropertyInfo = testClassType.GetProperty("IntProperty");
        //    }
        //});

        //var getMethodTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        // voidMethodInfo = testClassType.GetMethod("VoidMethod");
        //    }
        //});

        //var readFieldReflectionTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        // intValue = (int)intFieldInfo.GetValue(testClassInstance);
        //    }
        //});

        //var readPropertyReflectionTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        //  intValue = (int)intPropertyInfo.GetValue(testClassInstance, null);
        //    }
        //});

        //var readFieldDirectTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < 0; ++i)
        //    {
        //        //   intValue = testClassInstance.IntField;
        //    }
        //});

        //var readPropertyDirectTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        //  intValue = testClassInstance.IntProperty;
        //    }
        //});

        //var writeFieldReflectionTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        // intFieldInfo.SetValue(testClassInstance, 5);
        //    }
        //});

        //var writePropertyReflectionTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        // intPropertyInfo.SetValue(testClassInstance, 5, null);
        //    }
        //});

        //var writeFieldDirectTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        //testClassInstance.IntField = intValue;
        //    }
        //});

        //var writePropertyDirectTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        //testClassInstance.IntProperty = intValue;
        //    }
        //});

        //var callMethodReflectionTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        //voidMethodInfo.Invoke(testClassInstance, null);
        //    }
        //});

        //var callMethodDirectTime = stopwatch.RunTest(() =>
        //{
        //    for (long i = 0; i < NumIterations; ++i)
        //    {
        //        // testClassInstance.VoidMethod();
        //    }
        //});

        //report = "Test,Reflection Time,Direct Time\n"
        //            + "Nested0Value," + Nested0Value + ",0\n"
        //                        + "Nested1Value," + Nested1Value + ",0\n"
        //                                    + "Nested2Value," + Nested2Value + ",0\n"
        //                                                + "Nested3Value," + Nested3Value + ",0\n"
        //    + "rootVariableAcess," + rootVariableAcess + ",0\n"
        //    + "nestedVariableClassAcess," + nestedVariableClassAcess + ",0\n"
        //    + "CompareSplit," + CompareSplit + ",0\n"
        //    + "emptyFor," + emptyFor + ",0\n"
        //    + "CompareType," + CompareType + ",0\n"
        //    + "CompareBool," + CompareBool + ",0\n"
        //    + "CommpareString," + CommpareString + ",0\n"
        //    + "Get Type," + getTypeTime + ",0\n"
        //    + "Get Field," + getFieldTime + ",0\n"
        //    + "Get Property," + getPropertyTime + ",0\n"
        //    + "Get Method," + getMethodTime + ",0\n"
        //    + "Read Field," + readFieldReflectionTime + "," + readFieldDirectTime + "\n"
        //    + "Read Property," + readPropertyReflectionTime + "," + readPropertyDirectTime + "\n"
        //    + "Write Field," + writeFieldReflectionTime + "," + writeFieldDirectTime + "\n"
        //    + "Write Property," + writePropertyReflectionTime + "," + writePropertyDirectTime + "\n"
        //    + "Call Method," + callMethodReflectionTime + "," + callMethodDirectTime + "\n";
    }

    void OnGUI()
    {
        var drawRect = new Rect(0, 0, Screen.width, Screen.height);
        GUI.TextArea(drawRect, report);
    }
}