using System;

namespace UsingInvokeMethodActivityInCode
{
    public class TestClass
    {
        public void Method()
        {
            Console.WriteLine("Hello, message from Method()");
        }

        public void Method(string message1, string message2)
        {
            Console.WriteLine($"Hello, your message 1 is {message1} message 2 is {message2}");
        }

        public string MethodWithReturn(string message1, string message2)
        {
            return $"Message1: {message1} Message2: {message2}";
        }

        public void MethodWtihRef(string message1, string message2, ref string resultMessage)
        {
            resultMessage = $"Message1: {message1} Message2: {message2}";
        }

        public void Method<T1, T2>(T1 param1, T2 param2)
        {
            Console.WriteLine($"Type T1 {typeof(T1)} , Type T2 {typeof(T2)}, T1 Value {param1}, T2 Value {param2}");
        }

        public static string StaticMethod(string message1, string message2)
        {
            return $"Message1: {message1} Message2: {message2}";
        }
    }
}