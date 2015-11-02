namespace MyWeather.Mvvm.Events
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class EventAggregatorMock : Mock<IEventAggregator>, IEventAggregator
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public EventAggregatorMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
		}

		public void Notify<TEvent>(TEvent data)
		{
			this.InvokeMember("Notify<TEvent>", new object[] { data });
		}
		public IEventSubscription Subscribe<TEvent>(Action<TEvent> handler)
		{
			IEventSubscription result;
			this.InvokeMember("Subscribe<TEvent>", new object[] { handler }, out result);
			return result;
		}
		public IEventSubscription Subscribe(Type type, Action<object> handler)
		{
			IEventSubscription result;
			this.InvokeMember("Subscribe", new object[] { type, handler }, out result);
			return result;
		}
		public IEventSubscription SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
		{
			IEventSubscription result;
			this.InvokeMember("SubscribeAsync<TEvent>", new object[] { handler }, out result);
			return result;
		}
		public IEventSubscription SubscribeAsync(Type type, Func<object, Task> handler)
		{
			IEventSubscription result;
			this.InvokeMember("SubscribeAsync", new object[] { type, handler }, out result);
			return result;
		}
		public void Unsubscribe(IEventSubscription eventSubscription)
		{
			this.InvokeMember("Unsubscribe", new object[] { eventSubscription });
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
			private readonly EventAggregatorMock parent;
			internal CountCallers(EventAggregatorMock parent)
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
			public CountCallers Notify<TEvent>(TEvent data)
			{
				this.parent.CalledWith("Notify<TEvent>", data);
				return this;
			}
			public CountCallers Notify<TEvent>()
			{
				this.parent.Called("Notify<TEvent>");
				return this;
			}
			public CountCallers Subscribe<TEvent>(Action<TEvent> handler)
			{
				this.parent.CalledWith("Subscribe<TEvent>", handler);
				return this;
			}
			public CountCallers Subscribe<TEvent>()
			{
				this.parent.Called("Subscribe<TEvent>");
				return this;
			}
			public CountCallers Subscribe(Type type, Action<object> handler)
			{
				this.parent.CalledWith("Subscribe", type, handler);
				return this;
			}
			public CountCallers Subscribe()
			{
				this.parent.Called("Subscribe");
				return this;
			}
			public CountCallers SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
			{
				this.parent.CalledWith("SubscribeAsync<TEvent>", handler);
				return this;
			}
			public CountCallers SubscribeAsync<TEvent>()
			{
				this.parent.Called("SubscribeAsync<TEvent>");
				return this;
			}
			public CountCallers SubscribeAsync(Type type, Func<object, Task> handler)
			{
				this.parent.CalledWith("SubscribeAsync", type, handler);
				return this;
			}
			public CountCallers SubscribeAsync()
			{
				this.parent.Called("SubscribeAsync");
				return this;
			}
			public CountCallers Unsubscribe(IEventSubscription eventSubscription)
			{
				this.parent.CalledWith("Unsubscribe", eventSubscription);
				return this;
			}
			public CountCallers Unsubscribe()
			{
				this.parent.Called("Unsubscribe");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly EventAggregatorMock parent;
				private readonly int count;
				internal CountCallerMethods(EventAggregatorMock parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods Notify<TEvent>(TEvent data)
				{
					this.parent.CalledWith(this.count, "Notify<TEvent>", data);
					return this;
				}
				public CountCallerMethods Notify<TEvent>()
				{
					this.parent.Called(this.count, "Notify<TEvent>");
					return this;
				}
				public CountCallerMethods Subscribe<TEvent>(Action<TEvent> handler)
				{
					this.parent.CalledWith(this.count, "Subscribe<TEvent>", handler);
					return this;
				}
				public CountCallerMethods Subscribe<TEvent>()
				{
					this.parent.Called(this.count, "Subscribe<TEvent>");
					return this;
				}
				public CountCallerMethods Subscribe(Type type, Action<object> handler)
				{
					this.parent.CalledWith(this.count, "Subscribe", type, handler);
					return this;
				}
				public CountCallerMethods Subscribe()
				{
					this.parent.Called(this.count, "Subscribe");
					return this;
				}
				public CountCallerMethods SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
				{
					this.parent.CalledWith(this.count, "SubscribeAsync<TEvent>", handler);
					return this;
				}
				public CountCallerMethods SubscribeAsync<TEvent>()
				{
					this.parent.Called(this.count, "SubscribeAsync<TEvent>");
					return this;
				}
				public CountCallerMethods SubscribeAsync(Type type, Func<object, Task> handler)
				{
					this.parent.CalledWith(this.count, "SubscribeAsync", type, handler);
					return this;
				}
				public CountCallerMethods SubscribeAsync()
				{
					this.parent.Called(this.count, "SubscribeAsync");
					return this;
				}
				public CountCallerMethods Unsubscribe(IEventSubscription eventSubscription)
				{
					this.parent.CalledWith(this.count, "Unsubscribe", eventSubscription);
					return this;
				}
				public CountCallerMethods Unsubscribe()
				{
					this.parent.Called(this.count, "Unsubscribe");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly EventAggregatorMock parent;
			internal CountCalls(EventAggregatorMock parent)
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
				private readonly EventAggregatorMock parent;
				private readonly int position;
				internal CountCallsMethods(EventAggregatorMock parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation Notify<TEvent>()
				{
					return this.parent.GetCall(this.position, "Notify<TEvent>");
				}
				public MemberInvocation Subscribe<TEvent>()
				{
					return this.parent.GetCall(this.position, "Subscribe<TEvent>");
				}
				public MemberInvocation Subscribe()
				{
					return this.parent.GetCall(this.position, "Subscribe");
				}
				public MemberInvocation SubscribeAsync<TEvent>()
				{
					return this.parent.GetCall(this.position, "SubscribeAsync<TEvent>");
				}
				public MemberInvocation SubscribeAsync()
				{
					return this.parent.GetCall(this.position, "SubscribeAsync");
				}
				public MemberInvocation Unsubscribe()
				{
					return this.parent.GetCall(this.position, "Unsubscribe");
				}
			}
		}
		public class Handlers
		{
			private readonly EventAggregatorMock parent;
			internal Handlers(EventAggregatorMock parent)
			{
				this.parent = parent;
			}
			public Handlers Notify<TEvent>(Action<TEvent> action)
			{
				this.parent.Handle<TEvent>("Notify<TEvent>", action);
				return this;
			}
			public Handlers Subscribe<TEvent>(Func<Action<TEvent>, IEventSubscription> action)
			{
				this.parent.Handle<Action<TEvent>, IEventSubscription>("Subscribe<TEvent>", action);
				return this;
			}
			public Handlers Subscribe(Func<Type, Action<object>, IEventSubscription> action)
			{
				this.parent.Handle<Type, Action<object>, IEventSubscription>("Subscribe", action);
				return this;
			}
			public Handlers SubscribeAsync<TEvent>(Func<Func<TEvent, Task>, IEventSubscription> action)
			{
				this.parent.Handle<Func<TEvent, Task>, IEventSubscription>("SubscribeAsync<TEvent>", action);
				return this;
			}
			public Handlers SubscribeAsync(Func<Type, Func<object, Task>, IEventSubscription> action)
			{
				this.parent.Handle<Type, Func<object, Task>, IEventSubscription>("SubscribeAsync", action);
				return this;
			}
			public Handlers Unsubscribe(Action<IEventSubscription> action)
			{
				this.parent.Handle<IEventSubscription>("Unsubscribe", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly EventAggregatorMock parent;
			internal Verifications(EventAggregatorMock parent)
			{
				this.parent = parent;
			}
			public Verifications Notify<TEvent>(TEvent data)
			{
				this.parent.AddVerification("Notify<TEvent>", data);
				return this;
			}
			public Verifications Subscribe<TEvent>(Action<TEvent> handler)
			{
				this.parent.AddVerification("Subscribe<TEvent>", handler);
				return this;
			}
			public Verifications Subscribe(Type type, Action<object> handler)
			{
				this.parent.AddVerification("Subscribe", type, handler);
				return this;
			}
			public Verifications SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
			{
				this.parent.AddVerification("SubscribeAsync<TEvent>", handler);
				return this;
			}
			public Verifications SubscribeAsync(Type type, Func<object, Task> handler)
			{
				this.parent.AddVerification("SubscribeAsync", type, handler);
				return this;
			}
			public Verifications Unsubscribe(IEventSubscription eventSubscription)
			{
				this.parent.AddVerification("Unsubscribe", eventSubscription);
				return this;
			}
		}
		public class Verifiers
		{
			private readonly EventAggregatorMock parent;
			internal Verifiers(EventAggregatorMock parent)
			{
				this.parent = parent;
			}
			public Verifiers Notify<TEvent>(TEvent data)
			{
				this.parent.Verify("Notify<TEvent>", data);
				return this;
			}
			public Verifiers Subscribe<TEvent>(Action<TEvent> handler)
			{
				this.parent.Verify("Subscribe<TEvent>", handler);
				return this;
			}
			public Verifiers Subscribe(Type type, Action<object> handler)
			{
				this.parent.Verify("Subscribe", type, handler);
				return this;
			}
			public Verifiers SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
			{
				this.parent.Verify("SubscribeAsync<TEvent>", handler);
				return this;
			}
			public Verifiers SubscribeAsync(Type type, Func<object, Task> handler)
			{
				this.parent.Verify("SubscribeAsync", type, handler);
				return this;
			}
			public Verifiers Unsubscribe(IEventSubscription eventSubscription)
			{
				this.parent.Verify("Unsubscribe", eventSubscription);
				return this;
			}
		}
	}
}
