namespace MyWeather.Mvvm.Events
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class EventAggregatorSpy : Spy<IEventAggregator>, IEventAggregator
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public EventAggregatorSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
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
		public class CountCallers
		{
			private readonly EventAggregatorSpy parent;
			internal CountCallers(EventAggregatorSpy parent)
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
				private readonly EventAggregatorSpy parent;
				private readonly int count;
				internal CountCallerMethods(EventAggregatorSpy parent, int count)
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
			private readonly EventAggregatorSpy parent;
			internal CountCalls(EventAggregatorSpy parent)
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
				private readonly EventAggregatorSpy parent;
				private readonly int position;
				internal CountCallsMethods(EventAggregatorSpy parent, int position)
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
	}
}
