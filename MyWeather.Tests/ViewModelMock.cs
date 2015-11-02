namespace MyWeather.Mvvm.Base
{
	using MyWeather.Mvvm.Events;
	using MyWeather.Mvvm.Navigation;
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	public class ViewModelMock<TModel> : Mock<IViewModel<TModel>>, IViewModel<TModel>
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public ViewModelMock()
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
		}
		public TModel Model
		{
			get { TModel result; this.InvokeGetProperty("Model", out result); return result; }
		}
		public INavigationService NavigationService
		{
			get { INavigationService result; this.InvokeGetProperty("NavigationService", out result); return result; }
		}
		public List<PropertyChangedEventHandler> PropertyChangedHandlers { get; private set; }
		public event PropertyChangedEventHandler PropertyChanged
		{
			add { if (this.PropertyChangedHandlers == null) this.PropertyChangedHandlers = new List<PropertyChangedEventHandler>(); this.PropertyChangedHandlers.Add(value); }
			remove { if (this.PropertyChangedHandlers != null) this.PropertyChangedHandlers.Remove(value); }
		}
		public void Load(object parameter)
		{
			this.InvokeMember("Load", new object[] { parameter });
		}
		public void LoadState(Dictionary<string, object> state)
		{
			this.InvokeMember("LoadState", new object[] { state });
		}
		public void SaveState(Dictionary<string, object> state)
		{
			this.InvokeMember("SaveState", new object[] { state });
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
			private readonly ViewModelMock<TModel> parent;
			internal CountCallers(ViewModelMock<TModel> parent)
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
			public CountCallers ModelGetter()
			{
				this.parent.CalledGet("Model");
				return this;
			}
			public CountCallers NavigationServiceGetter()
			{
				this.parent.CalledGet("NavigationService");
				return this;
			}
			public CountCallers Load(object parameter)
			{
				this.parent.CalledWith("Load", parameter);
				return this;
			}
			public CountCallers Load()
			{
				this.parent.Called("Load");
				return this;
			}
			public CountCallers LoadState(Dictionary<string, object> state)
			{
				this.parent.CalledWith("LoadState", state);
				return this;
			}
			public CountCallers LoadState()
			{
				this.parent.Called("LoadState");
				return this;
			}
			public CountCallers SaveState(Dictionary<string, object> state)
			{
				this.parent.CalledWith("SaveState", state);
				return this;
			}
			public CountCallers SaveState()
			{
				this.parent.Called("SaveState");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly ViewModelMock<TModel> parent;
				private readonly int count;
				internal CountCallerMethods(ViewModelMock<TModel> parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods EventAggregatorGetter()
				{
					this.parent.CalledGet(this.count, "EventAggregator");
					return this;
				}
				public CountCallerMethods ModelGetter()
				{
					this.parent.CalledGet(this.count, "Model");
					return this;
				}
				public CountCallerMethods NavigationServiceGetter()
				{
					this.parent.CalledGet(this.count, "NavigationService");
					return this;
				}
				public CountCallerMethods Load(object parameter)
				{
					this.parent.CalledWith(this.count, "Load", parameter);
					return this;
				}
				public CountCallerMethods Load()
				{
					this.parent.Called(this.count, "Load");
					return this;
				}
				public CountCallerMethods LoadState(Dictionary<string, object> state)
				{
					this.parent.CalledWith(this.count, "LoadState", state);
					return this;
				}
				public CountCallerMethods LoadState()
				{
					this.parent.Called(this.count, "LoadState");
					return this;
				}
				public CountCallerMethods SaveState(Dictionary<string, object> state)
				{
					this.parent.CalledWith(this.count, "SaveState", state);
					return this;
				}
				public CountCallerMethods SaveState()
				{
					this.parent.Called(this.count, "SaveState");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly ViewModelMock<TModel> parent;
			internal CountCalls(ViewModelMock<TModel> parent)
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
				private readonly ViewModelMock<TModel> parent;
				private readonly int position;
				internal CountCallsMethods(ViewModelMock<TModel> parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation EventAggregatorGetter()
				{
					return this.parent.GetCall(this.position, "EventAggregator");
				}
				public MemberInvocation ModelGetter()
				{
					return this.parent.GetCall(this.position, "Model");
				}
				public MemberInvocation NavigationServiceGetter()
				{
					return this.parent.GetCall(this.position, "NavigationService");
				}
				public MemberInvocation Load()
				{
					return this.parent.GetCall(this.position, "Load");
				}
				public MemberInvocation LoadState()
				{
					return this.parent.GetCall(this.position, "LoadState");
				}
				public MemberInvocation SaveState()
				{
					return this.parent.GetCall(this.position, "SaveState");
				}
			}
		}
		public class Handlers
		{
			private readonly ViewModelMock<TModel> parent;
			internal Handlers(ViewModelMock<TModel> parent)
			{
				this.parent = parent;
			}
			public Handlers EventAggregator(Func<IEventAggregator> action)
			{
				this.parent.HandleGet("EventAggregator", action);
				return this;
			}
			public Handlers Model(Func<TModel> action)
			{
				this.parent.HandleGet("Model", action);
				return this;
			}
			public Handlers NavigationService(Func<INavigationService> action)
			{
				this.parent.HandleGet("NavigationService", action);
				return this;
			}
			public Handlers Load(Action<object> action)
			{
				this.parent.Handle<object>("Load", action);
				return this;
			}
			public Handlers LoadState(Action<Dictionary<string, object>> action)
			{
				this.parent.Handle<Dictionary<string, object>>("LoadState", action);
				return this;
			}
			public Handlers SaveState(Action<Dictionary<string, object>> action)
			{
				this.parent.Handle<Dictionary<string, object>>("SaveState", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly ViewModelMock<TModel> parent;
			internal Verifications(ViewModelMock<TModel> parent)
			{
				this.parent = parent;
			}
			public Verifications EventAggregator()
			{
				this.parent.AddGetVerification("EventAggregator");
				return this;
			}
			public Verifications Model()
			{
				this.parent.AddGetVerification("Model");
				return this;
			}
			public Verifications NavigationService()
			{
				this.parent.AddGetVerification("NavigationService");
				return this;
			}
			public Verifications Load(object parameter)
			{
				this.parent.AddVerification("Load", parameter);
				return this;
			}
			public Verifications LoadState(Dictionary<string, object> state)
			{
				this.parent.AddVerification("LoadState", state);
				return this;
			}
			public Verifications SaveState(Dictionary<string, object> state)
			{
				this.parent.AddVerification("SaveState", state);
				return this;
			}
		}
		public class Verifiers
		{
			private readonly ViewModelMock<TModel> parent;
			internal Verifiers(ViewModelMock<TModel> parent)
			{
				this.parent = parent;
			}
			public Verifiers EventAggregator()
			{
				this.parent.VerifyGet("EventAggregator");
				return this;
			}
			public Verifiers Model()
			{
				this.parent.VerifyGet("Model");
				return this;
			}
			public Verifiers NavigationService()
			{
				this.parent.VerifyGet("NavigationService");
				return this;
			}
			public Verifiers Load(object parameter)
			{
				this.parent.Verify("Load", parameter);
				return this;
			}
			public Verifiers LoadState(Dictionary<string, object> state)
			{
				this.parent.Verify("LoadState", state);
				return this;
			}
			public Verifiers SaveState(Dictionary<string, object> state)
			{
				this.parent.Verify("SaveState", state);
				return this;
			}
		}
	}
}
