namespace MyWeather.Mvvm.Base
{
	using MyWeather.Tests;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class StateManagerSpy : Spy<IStateManager>, IStateManager
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public StateManagerSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
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
		public class CountCallers
		{
			private readonly StateManagerSpy parent;
			internal CountCallers(StateManagerSpy parent)
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
				private readonly StateManagerSpy parent;
				private readonly int count;
				internal CountCallerMethods(StateManagerSpy parent, int count)
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
			private readonly StateManagerSpy parent;
			internal CountCalls(StateManagerSpy parent)
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
				private readonly StateManagerSpy parent;
				private readonly int position;
				internal CountCallsMethods(StateManagerSpy parent, int position)
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
	}
}
