namespace MyWeather.Mvvm.Events
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class EventSubscriptionStub : Stub<IEventSubscription>, IEventSubscription
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;

		public EventSubscriptionStub()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
		}

		public Type TypeOfEvent
		{
			get { Type result; this.InvokeGetProperty("TypeOfEvent", out result); return result; }
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
			private readonly EventSubscriptionStub parent;
			internal CountCallers(EventSubscriptionStub parent)
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
			public CountCallers TypeOfEventGetter()
			{
				this.parent.CalledGet("TypeOfEvent");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly EventSubscriptionStub parent;
				private readonly int count;
				internal CountCallerMethods(EventSubscriptionStub parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods TypeOfEventGetter()
				{
					this.parent.CalledGet(this.count, "TypeOfEvent");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly EventSubscriptionStub parent;
			internal CountCalls(EventSubscriptionStub parent)
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
				private readonly EventSubscriptionStub parent;
				private readonly int position;
				internal CountCallsMethods(EventSubscriptionStub parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation TypeOfEventGetter()
				{
					return this.parent.GetCall(this.position, "TypeOfEvent");
				}
			}
		}
		public class Handlers
		{
			private readonly EventSubscriptionStub parent;
			internal Handlers(EventSubscriptionStub parent)
			{
				this.parent = parent;
			}
			public Handlers TypeOfEvent(Func<Type> action)
			{
				this.parent.HandleGet("TypeOfEvent", action);
				return this;
			}
		}
	}
}
