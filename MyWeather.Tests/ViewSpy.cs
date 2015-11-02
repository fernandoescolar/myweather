namespace MyWeather.Mvvm.Base
{
	using MyWeather.Mvvm.Events;
	using MyWeather.Tests;
	using System.Collections.Generic;

	public class ViewSpy<TViewModel> : Spy<IView<TViewModel>>, IView<TViewModel>
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public ViewSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
		}

		public IEventAggregator EventAggregator
		{
			get { IEventAggregator result; this.InvokeGetProperty("EventAggregator", out result); return result; }
			set { this.InvokeSetProperty("EventAggregator", value); }
		}
		public TViewModel ViewModel
		{
			get { TViewModel result; this.InvokeGetProperty("ViewModel", out result); return result; }
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
			private readonly ViewSpy<TViewModel> parent;
			internal CountCallers(ViewSpy<TViewModel> parent)
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
			public CountCallers EventAggregatorGetter()
			{
				this.parent.CalledGet("EventAggregator");
				return this;
			}
			public CountCallers EventAggregatorSetter()
			{
				this.parent.CalledSet("EventAggregator");
				return this;
			}
			public CountCallers EventAggregatorSetter(IEventAggregator value)
			{
				this.parent.CalledSetWith("EventAggregator", value);
				return this;
			}
			public CountCallers ViewModelGetter()
			{
				this.parent.CalledGet("ViewModel");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly ViewSpy<TViewModel> parent;
				private readonly int count;
				internal CountCallerMethods(ViewSpy<TViewModel> parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods EventAggregatorGetter()
				{
					this.parent.CalledGet(this.count, "EventAggregator");
					return this;
				}
				public CountCallerMethods EventAggregatorSetter()
				{
					this.parent.CalledSet(this.count, "EventAggregator");
					return this;
				}
				public CountCallerMethods EventAggregatorSetter(IEventAggregator value)
				{
					this.parent.CalledSetWith(this.count, "EventAggregator", value);
					return this;
				}
				public CountCallerMethods ViewModelGetter()
				{
					this.parent.CalledGet(this.count, "ViewModel");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly ViewSpy<TViewModel> parent;
			internal CountCalls(ViewSpy<TViewModel> parent)
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
				private readonly ViewSpy<TViewModel> parent;
				private readonly int position;
				internal CountCallsMethods(ViewSpy<TViewModel> parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation EventAggregatorGetter()
				{
					return this.parent.GetCall(this.position, "EventAggregator");
				}
				public MemberInvocation EventAggregatorSetter()
				{
					return this.parent.GetCall(this.position, "EventAggregator");
				}
				public MemberInvocation ViewModelGetter()
				{
					return this.parent.GetCall(this.position, "ViewModel");
				}
			}
		}
	}
}
