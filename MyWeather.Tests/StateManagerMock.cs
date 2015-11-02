namespace MyWeather.Mvvm.Base
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class StateManagerMock : Mock<IStateManager>, IStateManager
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public StateManagerMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
		}

		public Dictionary<string, Dictionary<string, object>> States
		{
			get { Dictionary<string, Dictionary<string, object>> result; this.InvokeGetProperty("States", out result); return result; }
		}
		public Task RestoreAsync()
		{
			Task result;
			this.InvokeMember("RestoreAsync", new object[] {  }, out result);
			return result;
		}
		public Task SaveAsync()
		{
			Task result;
			this.InvokeMember("SaveAsync", new object[] {  }, out result);
			return result;
		}
		public CountCallers HasBeenCalled()
		{
			return this.countCallers;
		}
		public CountCalls GetCalls()
		{
			return this.countCalls;
		}
		public Handlers AddHandlers()
		{
			return this.handlers;
		}
		public Verifications AddVerifications()
		{
			return this.verifications;
		}
		public Verifiers Verify()
		{
			return this.verifiers;
		}
		public class CountCallers
		{
			private readonly StateManagerMock parent;
			internal CountCallers(StateManagerMock parent)
			{
				this.parent = parent;
			}
			public CountCallerMethods Once()
			{
				return new CountCallerMethods(this.parent, 1);
			}
			public CountCallerMethods Twice()
			{
				return new CountCallerMethods(this.parent, 2);
			}
			public CountCallerMethods Thrice()
			{
				return new CountCallerMethods(this.parent, 3);
			}
			public CountCallerMethods Times(int times)
			{
				return new CountCallerMethods(this.parent, times);
			}
			public CountCallers StatesGetter()
			{
				this.parent.CalledGet("States");
				return this;
			}
			public CountCallers RestoreAsync()
			{
				this.parent.Called("RestoreAsync");
				return this;
			}
			public CountCallers SaveAsync()
			{
				this.parent.Called("SaveAsync");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly StateManagerMock parent;
				private readonly int count;
				internal CountCallerMethods(StateManagerMock parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods StatesGetter()
				{
					this.parent.CalledGet(this.count, "States");
					return this;
				}
				public CountCallerMethods RestoreAsync()
				{
					this.parent.Called(this.count, "RestoreAsync");
					return this;
				}
				public CountCallerMethods SaveAsync()
				{
					this.parent.Called(this.count, "SaveAsync");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly StateManagerMock parent;
			internal CountCalls(StateManagerMock parent)
			{
				this.parent = parent;
			}
			public CountCallsMethods First()
			{
				return new CountCallsMethods(this.parent, 0);
			}
			public CountCallsMethods Second()
			{
				return new CountCallsMethods(this.parent, 1);
			}
			public CountCallsMethods Third()
			{
				return new CountCallsMethods(this.parent, 2);
			}
			public CountCallsMethods At(int position)
			{
				return new CountCallsMethods(this.parent, position);
			}
			public class CountCallsMethods
			{
				private readonly StateManagerMock parent;
				private readonly int position;
				internal CountCallsMethods(StateManagerMock parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation StatesGetter()
				{
					return this.parent.GetCall(this.position, "States");
				}
				public MemberInvocation RestoreAsync()
				{
					return this.parent.GetCall(this.position, "RestoreAsync");
				}
				public MemberInvocation SaveAsync()
				{
					return this.parent.GetCall(this.position, "SaveAsync");
				}
			}
		}
		public class Handlers
		{
			private readonly StateManagerMock parent;
			internal Handlers(StateManagerMock parent)
			{
				this.parent = parent;
			}
			public Handlers States(Func<Dictionary<string, Dictionary<string, object>>> action)
			{
				this.parent.HandleGet("States", action);
				return this;
			}
			public Handlers RestoreAsync(Func<Task> action)
			{
				this.parent.Handle<Task>("RestoreAsync", action);
				return this;
			}
			public Handlers SaveAsync(Func<Task> action)
			{
				this.parent.Handle<Task>("SaveAsync", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly StateManagerMock parent;
			internal Verifications(StateManagerMock parent)
			{
				this.parent = parent;
			}
			public Verifications States()
			{
				this.parent.AddGetVerification("States");
				return this;
			}
			public Verifications RestoreAsync()
			{
				this.parent.AddVerification("RestoreAsync", new object[0]);
				return this;
			}
			public Verifications SaveAsync()
			{
				this.parent.AddVerification("SaveAsync", new object[0]);
				return this;
			}
		}
		public class Verifiers
		{
			private readonly StateManagerMock parent;
			internal Verifiers(StateManagerMock parent)
			{
				this.parent = parent;
			}
			public Verifiers States()
			{
				this.parent.VerifyGet("States");
				return this;
			}
			public Verifiers RestoreAsync()
			{
				this.parent.Verify("RestoreAsync", new object[0]);
				return this;
			}
			public Verifiers SaveAsync()
			{
				this.parent.Verify("SaveAsync", new object[0]);
				return this;
			}
		}
	}
}
