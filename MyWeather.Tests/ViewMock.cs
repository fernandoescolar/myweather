namespace MyWeather.Mvvm.Base
{
	using MyWeather.Mvvm.Events;
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class ViewMock<TViewModel> : Mock<IView<TViewModel>>, IView<TViewModel>
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public ViewMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
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
			private readonly ViewMock<TViewModel> parent;
			internal CountCallers(ViewMock<TViewModel> parent)
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
				private readonly ViewMock<TViewModel> parent;
				private readonly int count;
				internal CountCallerMethods(ViewMock<TViewModel> parent, int count)
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
			private readonly ViewMock<TViewModel> parent;
			internal CountCalls(ViewMock<TViewModel> parent)
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
				private readonly ViewMock<TViewModel> parent;
				private readonly int position;
				internal CountCallsMethods(ViewMock<TViewModel> parent, int position)
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
		public class Handlers
		{
			private readonly ViewMock<TViewModel> parent;
			internal Handlers(ViewMock<TViewModel> parent)
			{
				this.parent = parent;
			}
			public Handlers EventAggregator(Func<IEventAggregator> action)
			{
				this.parent.HandleGet("EventAggregator", action);
				return this;
			}
			public Handlers EventAggregator(Action<IEventAggregator> action)
			{
				this.parent.HandleSet("EventAggregator", action);
				return this;
			}
			public Handlers ViewModel(Func<TViewModel> action)
			{
				this.parent.HandleGet("ViewModel", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly ViewMock<TViewModel> parent;
			internal Verifications(ViewMock<TViewModel> parent)
			{
				this.parent = parent;
			}
			public Verifications EventAggregator()
			{
				this.parent.AddGetVerification("EventAggregator");
				return this;
			}
			public Verifications EventAggregator(IEventAggregator value)
			{
				this.parent.AddSetVerification("EventAggregator", value);
				return this;
			}
			public Verifications ViewModel()
			{
				this.parent.AddGetVerification("ViewModel");
				return this;
			}
		}
		public class Verifiers
		{
			private readonly ViewMock<TViewModel> parent;
			internal Verifiers(ViewMock<TViewModel> parent)
			{
				this.parent = parent;
			}
			public Verifiers EventAggregator()
			{
				this.parent.VerifyGet("EventAggregator");
				return this;
			}
			public Verifiers EventAggregator(IEventAggregator value)
			{
				this.parent.VerifySet("EventAggregator", value);
				return this;
			}
			public Verifiers ViewModel()
			{
				this.parent.VerifyGet("ViewModel");
				return this;
			}
		}
	}
}
