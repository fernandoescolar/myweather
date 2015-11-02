namespace MyWeather.Tests
{
	using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public abstract class Mock<T> : Stub<T> where T : class
    {
        private readonly List<MemberInvocation> memberInvocationVerifiers;

        protected Mock() : base()
        {
            this.memberInvocationVerifiers = new List<MemberInvocation>();
        }

        public void VerifyAll()
        {
            if (this.memberInvocationVerifiers.Count <= 0) Assert.Fail("There is not any verification to check");

            foreach (var memberInvocation in this.memberInvocationVerifiers)
            {
                this.Verify(memberInvocation);
            }
        }

        internal void Verify(string methodName, params object[] args)
        {
            this.Verify(new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true });
        }

        internal void VerifyGet(string propertyName)
        {
            this.Verify(new MemberInvocation { Name = FormatPropertyGetName(propertyName), Parameters = new object[0], IsGetter = true });
        }

        internal void VerifySet(string propertyName, object arg)
        {
            this.Verify(new MemberInvocation { Name = FormatPropertySetName(propertyName), Parameters = new object[] { arg }, IsSetter = true });
        }

        internal void AddVerification(string methodName, params object[] args)
        {
            this.AddVerification(new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true });
        }

        internal void AddGetVerification(string propertyName)
        {
            this.AddVerification(new MemberInvocation { Name = FormatPropertyGetName(propertyName), Parameters = new object[0], IsGetter = true });
        }

        internal void AddSetVerification(string propertyName, object arg)
        {
            this.AddVerification(new MemberInvocation { Name = FormatPropertySetName(propertyName), Parameters = new object[] { arg }, IsSetter = true });
        }

        protected override void OnReset()
        {
            base.OnReset();
            this.memberInvocationVerifiers.Clear();
        }

        private void AddVerification(MemberInvocation memberInvocation)
        {
            this.memberInvocationVerifiers.Add(memberInvocation);
        }

        private void Verify(MemberInvocation memberInvocation)
        {
            var verified = false;
            var candidateVerifiers = this.MemberInvocations.Where(v => v.Name == memberInvocation.Name && v.Parameters.Length == memberInvocation.Parameters.Length);
            foreach (var candidate in candidateVerifiers)
            {
                var allPropertiesAreEqual = true;
                for (var i = 0; i < candidate.Parameters.Length; i++)
                {
                    if (candidate.Parameters[i] != memberInvocation.Parameters[i])
                    {
                        allPropertiesAreEqual = false;
                        break;
                    }
                }

                if (allPropertiesAreEqual)
                {
                    verified = true;
                    break;
                }
            }

            Assert.IsTrue(verified, "Verifying '" + memberInvocation.Name + "' invocation");
        }
    }

    public abstract class Stub<T> : Spy<T> where T : class
    {
        private readonly List<IMemberInvocationHandler> memberInvocationHandlers;

        protected Stub() : base()
        {
            this.memberInvocationHandlers = new List<IMemberInvocationHandler>();
        }

        internal void Handle(string methodName, Action action)
        {
            Action<object[]> callback = args => { action(); };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback));
        }

        internal void Handle<T1>(string methodName, Action<T1> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                action(arg1);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1)));
        }

        internal void Handle<T1, T2>(string methodName, Action<T1, T2> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                action(arg1, arg2);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2)));
        }

        internal void Handle<T1, T2, T3>(string methodName, Action<T1, T2, T3> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                action(arg1, arg2, arg3);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3)));
        }

        internal void Handle<T1, T2, T3, T4>(string methodName, Action<T1, T2, T3, T4> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                action(arg1, arg2, arg3, arg4);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4)));
        }

        internal void Handle<T1, T2, T3, T4, T5>(string methodName, Action<T1, T2, T3, T4, T5> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                action(arg1, arg2, arg3, arg4, arg5);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6>(string methodName, Action<T1, T2, T3, T4, T5, T6> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                action(arg1, arg2, arg3, arg4, arg5, arg6);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                var arg9 = (T9)args[8];
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string methodName, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            Action<object[]> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                var arg9 = (T9)args[8];
                var arg10 = (T10)args[9];
                action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10)));
        }

        internal void Handle<TResult>(string methodName, Func<TResult> action)
        {
            Func<object[], TResult> callback = args => action();
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback));
        }

        internal void Handle<T1, TResult>(string methodName, Func<T1, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                return action(arg1);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1)));
        }

        internal void Handle<T1, T2, TResult>(string methodName, Func<T1, T2, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                return action(arg1, arg2);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2)));
        }

        internal void Handle<T1, T2, T3, TResult>(string methodName, Func<T1, T2, T3, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                return action(arg1, arg2, arg3);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3)));
        }

        internal void Handle<T1, T2, T3, T4, TResult>(string methodName, Func<T1, T2, T3, T4, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                return action(arg1, arg2, arg3, arg4);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4)));
        }

        internal void Handle<T1, T2, T3, T4, T5, TResult>(string methodName, Func<T1, T2, T3, T4, T5, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                return action(arg1, arg2, arg3, arg4, arg5);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, TResult>(string methodName, Func<T1, T2, T3, T4, T5, T6, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                return action(arg1, arg2, arg3, arg4, arg5, arg6);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, TResult>(string methodName, Func<T1, T2, T3, T4, T5, T6, T7, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                return action(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string methodName, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                return action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(string methodName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                var arg9 = (T9)args[8];
                return action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9)));
        }

        internal void Handle<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(string methodName, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> action)
        {
            Func<object[], TResult> callback = args =>
            {
                var arg1 = (T1)args[0];
                var arg2 = (T2)args[1];
                var arg3 = (T3)args[2];
                var arg4 = (T4)args[3];
                var arg5 = (T5)args[4];
                var arg6 = (T6)args[5];
                var arg7 = (T7)args[6];
                var arg8 = (T8)args[7];
                var arg9 = (T9)args[8];
                var arg10 = (T10)args[9];
                return action(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            };
            this.memberInvocationHandlers.Add(new MemberInvocationHandler<TResult>(methodName, callback, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10)));
        }

        internal void HandleGet<TResult>(string propertyName, Func<TResult> action)
        {
            var methodName = FormatPropertyGetName(propertyName);
            this.Handle<TResult>(methodName, action);
        }

        internal void HandleSet<TResult>(string propertyName, Action<TResult> action)
        {
            var methodName = FormatPropertySetName(propertyName);
            this.Handle<TResult>(methodName, action);
        }

        protected override bool OnInvokeMember<TResult>(MemberInvocation invocation, out TResult result)
        {
            var baseResult = base.OnInvokeMember<TResult>(invocation, out result);
            var candidateHandlers = this.memberInvocationHandlers.Where(h => h.MethodName == invocation.Name && h.ParametersTypes.Length == invocation.Parameters.Length).Reverse();
            if (candidateHandlers.Any())
            {
                foreach (var candidate in candidateHandlers)
                {
                    var isOk = true;
                    for (var i = 0; i < invocation.Parameters.Length; i++)
                    {
                        if (!candidate.ParametersTypes[i].IsAssignableFrom(invocation.Parameters[i].GetType()))
                        {
                            isOk = false;
                            break;
                        }
                    }

                    if (isOk)
                    {
                        candidate.Execute(invocation.Parameters);
                        result = (TResult)candidate.Result;
                        return true;
                    }
                }
            }

            return baseResult;
        }

        protected override bool OnInvokeMember<TResult>(MemberInvocation invocation, out Task<TResult> result)
        {
            var isOk = this.OnInvokeMember<Task<TResult>>(invocation, out result);
            if (result == null)
            {
                result = Task.FromResult(default(TResult));
            }

            return isOk;
        }

        protected override void OnReset()
        {
            base.OnReset();
            this.memberInvocationHandlers.Clear();
        }
    }

    public abstract class Spy<T> where T : class
    {
        private readonly List<MemberInvocation> memberInvocations;

        protected List<MemberInvocation> MemberInvocations { get { return this.memberInvocations; } }

        protected Spy()
        {
            if (!typeof(T).GetTypeInfo().IsInterface)
                throw new ArgumentException(string.Format("{0} must be an Interface", typeof(T).Name));

            this.memberInvocations = new List<MemberInvocation>();
        }

        internal void Called(string methodName)
        {
            Assert.IsTrue(this.memberInvocations.Any(m => m.Name == methodName), "Checking '" + methodName + "' has been called");
        }

        internal void Called(int counter, string methodName)
        {
            Assert.AreEqual(counter, this.memberInvocations.Count(m => m.Name == methodName), "Checking '" + methodName + "' has been called " + counter + " times");
        }

		internal void CalledGet(string propertyName)
        {
			var methodName = FormatPropertyGetName(propertyName);
            this.Called(methodName);
        }

		internal void CalledGet(int counter, string propertyName)
        {
			var methodName = FormatPropertyGetName(propertyName);
            this.Called(counter, methodName);
        }

		internal void CalledSet(string propertyName)
        {
			var methodName = FormatPropertySetName(propertyName);
            this.Called(methodName);
        }

		internal void CalledSet(int counter, string propertyName)
        {
			var methodName = FormatPropertySetName(propertyName);
            this.Called(counter, methodName);
        }

		internal void CalledSetWith(string propertyName, object value)
        {
			var methodName = FormatPropertySetName(propertyName);
            this.CalledWith(methodName, value);
        }

		internal void CalledSetWith(int counter, string propertyName, object value)
        {
			var methodName = FormatPropertySetName(propertyName);
            this.CalledWith(counter, methodName, value);
        }

        internal void CalledWith(string methodName, params object[] args)
        {
            var memberInvocation = new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true };
            Assert.IsTrue(this.memberInvocations.Any(m => memberInvocation.Equals(m)), "Checking '" + methodName + "' has been called with parameters");
        }

        internal void CalledWith(int counter, string methodName, params object[] args)
        {
            var memberInvocation = new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true };
            Assert.AreEqual(counter, this.memberInvocations.Count(m => memberInvocation.Equals(m)), "Checking '" + methodName + "' has been called " + counter + " times with parameters");
        }

        internal MemberInvocation GetCall(int counter, string methodName)
        {
            return this.memberInvocations.Where(m => methodName == m.Name).Skip(counter).FirstOrDefault();
        }

        public void Reset()
        {
            this.OnReset();
        }

        protected virtual bool OnInvokeMember<TResult>(MemberInvocation invocation, out TResult result)
        {
            result = default(TResult);
            return true;
        }

        protected virtual bool OnInvokeMember<TResult>(MemberInvocation invocation, out Task<TResult> result)
        {
            result = Task.FromResult(default(TResult));
            return true;
        }

        protected virtual void OnReset()
        {
            memberInvocations.Clear();
        }

        protected void InvokeMember(string methodName, object[] args)
        {
            if (this.TryInvokeMember(methodName, args))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        protected void InvokeMember<TResult>(string methodName, object[] args, out TResult result)
        {
            if (this.TryInvokeMember(methodName, args, out result))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        protected void InvokeMember<TResult>(string methodName, object[] args, out Task<TResult> result)
        {
            if (this.TryInvokeMember(methodName, args, out result))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        protected void InvokeGetProperty<TResult>(string propertyName, out TResult result)
        {
            if (this.TryInvokeGetProperty(propertyName, out result))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        protected void InvokeSetProperty(string propertyName, object arg)
        {
            if (this.TryInvokeSetProperty(propertyName, arg))
            {
                return;
            }

            throw new InvalidOperationException();
        }

        private bool TryInvokeMember(string methodName, object[] args)
        {
            var memberInvocation = new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true };
            bool result;
            return this.TryInvokeMember<bool>(memberInvocation, out result);
        }

        private bool TryInvokeMember<TResult>(string methodName, object[] args, out TResult result)
        {
            var memberInvocation = new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true };
            return this.TryInvokeMember(memberInvocation, out result);
        }

        private bool TryInvokeMember<TResult>(string methodName, object[] args, out Task<TResult> result)
        {
            var memberInvocation = new MemberInvocation { Name = methodName, Parameters = args, IsMethod = true };
            return this.TryInvokeMember(memberInvocation, out result);
        }

        private bool TryInvokeGetProperty<TResult>(string propertyName, out TResult result)
        {
            var memberInvocation = new MemberInvocation { Name = FormatPropertyGetName(propertyName), Parameters = new object[0], IsGetter = true };
            return this.TryInvokeMember(memberInvocation, out result);
        }

        private bool TryInvokeSetProperty(string propertyName, object arg)
        {
            var memberInvocation = new MemberInvocation { Name = FormatPropertySetName(propertyName), Parameters = new object[] { arg }, IsSetter = true };
            bool result;
            return this.TryInvokeMember(memberInvocation, out result);
        }

        private bool TryInvokeMember<TResult>(MemberInvocation memberInvocation, out TResult result)
        {
            var done = false;
            result = default(TResult);
            try
            {
                done = this.OnInvokeMember(memberInvocation, out result);
                if (done)
                {
                    this.memberInvocations.Add(memberInvocation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error invoking '{0}': {1}", memberInvocation.ToString(), ex.Message));
            }

            return done;
        }

        private bool TryInvokeMember<TResult>(MemberInvocation memberInvocation, out Task<TResult> result)
        {
            var done = false;
            result = default(Task<TResult>);
            try
            {
                done = this.OnInvokeMember(memberInvocation, out result);
                if (done)
                {
                    this.memberInvocations.Add(memberInvocation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error invoking '{0}': {1}", memberInvocation.ToString(), ex.Message));
            }

            return done;
        }

        protected static string FormatPropertyGetName(string propertyName)
        {
            return string.Format("$Get_{0}", propertyName);
        }

        protected static string FormatPropertySetName(string propertyName)
        {
            return string.Format("$Set_{0}", propertyName);
        }
    }

    public class MemberInvocation : IEquatable<MemberInvocation>
    {
        private const string UnknownTypeName = "[Unknown]";
        private const string MethodTypeName = "[Method]";
        private const string PropertyGetTypeName = "[PropertyGet]";
        private const string PropertySetTypeName = "[PropertySet]";
        public string Name { get; set; }

        public object[] Parameters { get; set; }

        public bool IsMethod { get; set; }

        public bool IsGetter { get; set; }

        public bool IsSetter { get; set; }

        public override string ToString()
        {
            var type = UnknownTypeName;
            if (IsMethod) type = MethodTypeName;
            if (IsGetter) type = PropertyGetTypeName;
            if (IsSetter) type = PropertySetTypeName;

            return string.Format("{0}.{1}", type, Name);
        }

        public bool Equals(MemberInvocation other)
        {
            if (other.Name != this.Name) return false;
            if (other.Parameters == null && this.Parameters == null) return true;
            if (other.Parameters == null || this.Parameters == null) return false;
            if (other.Parameters.Length != this.Parameters.Length) return false;

            for (var i = 0; i < this.Parameters.Length; i++)
            {
                if (other.Parameters[i] != this.Parameters[i]) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            var b = obj as MemberInvocation;
            if (b != null)
            {
                return this.Equals(b);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public interface IMemberInvocationHandler
    {
        string MethodName { get; }
        Type[] ParametersTypes { get; }

        object Result { get; }

        void Execute(object[] args);
    }

    public class MemberInvocationHandler : IMemberInvocationHandler
    {
        private readonly Action<object[]> callback;

        public string MethodName { get; private set; }

        public Type[] ParametersTypes { get; private set; }

        public object Result { get { return true; } }

        public MemberInvocationHandler(string methodName, Action<object[]> callback, params Type[] parametersTypes)
        {
            this.MethodName = methodName;
            this.ParametersTypes = parametersTypes ?? new Type[0];
            this.callback = callback;
        }

        public void Execute(object[] args)
        {
            this.callback(args);
        }
    }

    public class MemberInvocationHandler<TResult> : IMemberInvocationHandler
    {
        private readonly Func<object[], TResult> callback;

        public string MethodName { get; private set; }

        public Type[] ParametersTypes { get; private set; }

        public object Result { get; private set; }

        public MemberInvocationHandler(string methodName, Func<object[], TResult> callback, params Type[] parametersTypes)
        {
            this.MethodName = methodName;
            this.ParametersTypes = parametersTypes ?? new Type[0];
            this.callback = callback;
        }

        public void Execute(object[] args)
        {
            this.Result = this.callback(args);
        }
    }
}

