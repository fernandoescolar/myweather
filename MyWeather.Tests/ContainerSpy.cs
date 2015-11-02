namespace MyWeather.Mvvm.InversionOfControl
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class ContainerSpy : Spy<IContainer>, IContainer
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public ContainerSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
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
		public class CountCallers
		{
			private readonly ContainerSpy parent;
			internal CountCallers(ContainerSpy parent)
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
				private readonly ContainerSpy parent;
				private readonly int count;
				internal CountCallerMethods(ContainerSpy parent, int count)
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
			private readonly ContainerSpy parent;
			internal CountCalls(ContainerSpy parent)
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
				private readonly ContainerSpy parent;
				private readonly int position;
				internal CountCallsMethods(ContainerSpy parent, int position)
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
	}
}
