namespace MyWeather.Mvvm.InversionOfControl
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class ContainerStub : Stub<IContainer>, IContainer
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;

		public ContainerStub()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
		}

		public object Resolve(Type type)
		{
			object result;
			this.InvokeMember("Resolve", new object[] { type }, out result);
			return result;
		}
		public object Resolve(Type type, string name)
		{
			object result;
			this.InvokeMember("Resolve", new object[] { type, name }, out result);
			return result;
		}
		public TInterface Resolve<TInterface>()
		{
			TInterface result;
			this.InvokeMember("Resolve<TInterface>", new object[] {  }, out result);
			return result;
		}
		public TInterface Resolve<TInterface>(string name)
		{
			TInterface result;
			this.InvokeMember("Resolve<TInterface>", new object[] { name }, out result);
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
		public class CountCallers
		{
			private readonly ContainerStub parent;
			internal CountCallers(ContainerStub parent)
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
			public CountCallers Resolve(Type type)
			{
				this.parent.CalledWith("Resolve", type);
				return this;
			}
			public CountCallers Resolve()
			{
				this.parent.Called("Resolve");
				return this;
			}
			public CountCallers Resolve<TInterface>()
			{
				this.parent.Called("Resolve<TInterface>");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly ContainerStub parent;
				private readonly int count;
				internal CountCallerMethods(ContainerStub parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods Resolve(Type type)
				{
					this.parent.CalledWith(this.count, "Resolve", type);
					return this;
				}
				public CountCallerMethods Resolve()
				{
					this.parent.Called(this.count, "Resolve");
					return this;
				}
				public CountCallerMethods Resolve(Type type, string name)
				{
					this.parent.CalledWith(this.count, "Resolve", type, name);
					return this;
				}
				public CountCallerMethods Resolve<TInterface>()
				{
					this.parent.Called(this.count, "Resolve<TInterface>");
					return this;
				}
				public CountCallerMethods Resolve<TInterface>(string name)
				{
					this.parent.CalledWith(this.count, "Resolve<TInterface>", name);
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly ContainerStub parent;
			internal CountCalls(ContainerStub parent)
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
				private readonly ContainerStub parent;
				private readonly int position;
				internal CountCallsMethods(ContainerStub parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation Resolve()
				{
					return this.parent.GetCall(this.position, "Resolve");
				}
				public MemberInvocation Resolve<TInterface>()
				{
					return this.parent.GetCall(this.position, "Resolve<TInterface>");
				}
			}
		}
		public class Handlers
		{
			private readonly ContainerStub parent;
			internal Handlers(ContainerStub parent)
			{
				this.parent = parent;
			}
			public Handlers Resolve(Func<Type, object> action)
			{
				this.parent.Handle<Type, object>("Resolve", action);
				return this;
			}
			public Handlers Resolve(Func<Type, string, object> action)
			{
				this.parent.Handle<Type, string, object>("Resolve", action);
				return this;
			}
			public Handlers Resolve<TInterface>(Func<TInterface> action)
			{
				this.parent.Handle<TInterface>("Resolve<TInterface>", action);
				return this;
			}
			public Handlers Resolve<TInterface>(Func<string, TInterface> action)
			{
				this.parent.Handle<string, TInterface>("Resolve<TInterface>", action);
				return this;
			}
		}
	}
}
