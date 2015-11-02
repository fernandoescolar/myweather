namespace MyWeather.Mvvm.Events
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class EventSubscriptionMock : Mock<IEventSubscription>, IEventSubscription
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public EventSubscriptionMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
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
			private readonly EventSubscriptionMock parent;
			internal CountCallers(EventSubscriptionMock parent)
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
				private readonly EventSubscriptionMock parent;
				private readonly int count;
				internal CountCallerMethods(EventSubscriptionMock parent, int count)
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
			private readonly EventSubscriptionMock parent;
			internal CountCalls(EventSubscriptionMock parent)
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
				private readonly EventSubscriptionMock parent;
				private readonly int position;
				internal CountCallsMethods(EventSubscriptionMock parent, int position)
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
			private readonly EventSubscriptionMock parent;
			internal Handlers(EventSubscriptionMock parent)
			{
				this.parent = parent;
			}
			public Handlers TypeOfEvent(Func<Type> action)
			{
				this.parent.HandleGet("TypeOfEvent", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly EventSubscriptionMock parent;
			internal Verifications(EventSubscriptionMock parent)
			{
				this.parent = parent;
			}
			public Verifications TypeOfEvent()
			{
				this.parent.AddGetVerification("TypeOfEvent");
				return this;
			}
		}
		public class Verifiers
		{
			private readonly EventSubscriptionMock parent;
			internal Verifiers(EventSubscriptionMock parent)
			{
				this.parent = parent;
			}
			public Verifiers TypeOfEvent()
			{
				this.parent.VerifyGet("TypeOfEvent");
				return this;
			}
		}
	}
}
