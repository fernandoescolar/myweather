namespace MyWeather.Mvvm.Navigation
{
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;

	public class NavigationServiceSpy : Spy<INavigationService>, INavigationService
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;

		public NavigationServiceSpy()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
		}

		public bool CanGoBack()
		{
			bool result;
			this.InvokeMember("CanGoBack", new object[] {  }, out result);
			return result;
		}
		public bool CanGoForward()
		{
			bool result;
			this.InvokeMember("CanGoForward", new object[] {  }, out result);
			return result;
		}
		public void Clean()
		{
			this.InvokeMember("Clean", new object[] {  });
		}
		public Type GetPrevious()
		{
			Type result;
			this.InvokeMember("GetPrevious", new object[] {  }, out result);
			return result;
		}
		public object GetState()
		{
			object result;
			this.InvokeMember("GetState", new object[] {  }, out result);
			return result;
		}
		public void GoBack()
		{
			this.InvokeMember("GoBack", new object[] {  });
		}
		public void GoForward()
		{
			this.InvokeMember("GoForward", new object[] {  });
		}
		public void Navigate(Type type, object parameter)
		{
			this.InvokeMember("Navigate", new object[] { type, parameter });
		}
		public void RemoveBackEntry()
		{
			this.InvokeMember("RemoveBackEntry", new object[] {  });
		}
		public void SetState(object state)
		{
			this.InvokeMember("SetState", new object[] { state });
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
			private readonly NavigationServiceSpy parent;
			internal CountCallers(NavigationServiceSpy parent)
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
			public CountCallers CanGoBack()
			{
				this.parent.Called("CanGoBack");
				return this;
			}
			public CountCallers CanGoForward()
			{
				this.parent.Called("CanGoForward");
				return this;
			}
			public CountCallers Clean()
			{
				this.parent.Called("Clean");
				return this;
			}
			public CountCallers GetPrevious()
			{
				this.parent.Called("GetPrevious");
				return this;
			}
			public CountCallers GetState()
			{
				this.parent.Called("GetState");
				return this;
			}
			public CountCallers GoBack()
			{
				this.parent.Called("GoBack");
				return this;
			}
			public CountCallers GoForward()
			{
				this.parent.Called("GoForward");
				return this;
			}
			public CountCallers Navigate(Type type, object parameter)
			{
				this.parent.CalledWith("Navigate", type, parameter);
				return this;
			}
			public CountCallers Navigate()
			{
				this.parent.Called("Navigate");
				return this;
			}
			public CountCallers RemoveBackEntry()
			{
				this.parent.Called("RemoveBackEntry");
				return this;
			}
			public CountCallers SetState(object state)
			{
				this.parent.CalledWith("SetState", state);
				return this;
			}
			public CountCallers SetState()
			{
				this.parent.Called("SetState");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly NavigationServiceSpy parent;
				private readonly int count;
				internal CountCallerMethods(NavigationServiceSpy parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods CanGoBack()
				{
					this.parent.Called(this.count, "CanGoBack");
					return this;
				}
				public CountCallerMethods CanGoForward()
				{
					this.parent.Called(this.count, "CanGoForward");
					return this;
				}
				public CountCallerMethods Clean()
				{
					this.parent.Called(this.count, "Clean");
					return this;
				}
				public CountCallerMethods GetPrevious()
				{
					this.parent.Called(this.count, "GetPrevious");
					return this;
				}
				public CountCallerMethods GetState()
				{
					this.parent.Called(this.count, "GetState");
					return this;
				}
				public CountCallerMethods GoBack()
				{
					this.parent.Called(this.count, "GoBack");
					return this;
				}
				public CountCallerMethods GoForward()
				{
					this.parent.Called(this.count, "GoForward");
					return this;
				}
				public CountCallerMethods Navigate(Type type, object parameter)
				{
					this.parent.CalledWith(this.count, "Navigate", type, parameter);
					return this;
				}
				public CountCallerMethods Navigate()
				{
					this.parent.Called(this.count, "Navigate");
					return this;
				}
				public CountCallerMethods RemoveBackEntry()
				{
					this.parent.Called(this.count, "RemoveBackEntry");
					return this;
				}
				public CountCallerMethods SetState(object state)
				{
					this.parent.CalledWith(this.count, "SetState", state);
					return this;
				}
				public CountCallerMethods SetState()
				{
					this.parent.Called(this.count, "SetState");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly NavigationServiceSpy parent;
			internal CountCalls(NavigationServiceSpy parent)
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
				private readonly NavigationServiceSpy parent;
				private readonly int position;
				internal CountCallsMethods(NavigationServiceSpy parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation CanGoBack()
				{
					return this.parent.GetCall(this.position, "CanGoBack");
				}
				public MemberInvocation CanGoForward()
				{
					return this.parent.GetCall(this.position, "CanGoForward");
				}
				public MemberInvocation Clean()
				{
					return this.parent.GetCall(this.position, "Clean");
				}
				public MemberInvocation GetPrevious()
				{
					return this.parent.GetCall(this.position, "GetPrevious");
				}
				public MemberInvocation GetState()
				{
					return this.parent.GetCall(this.position, "GetState");
				}
				public MemberInvocation GoBack()
				{
					return this.parent.GetCall(this.position, "GoBack");
				}
				public MemberInvocation GoForward()
				{
					return this.parent.GetCall(this.position, "GoForward");
				}
				public MemberInvocation Navigate()
				{
					return this.parent.GetCall(this.position, "Navigate");
				}
				public MemberInvocation RemoveBackEntry()
				{
					return this.parent.GetCall(this.position, "RemoveBackEntry");
				}
				public MemberInvocation SetState()
				{
					return this.parent.GetCall(this.position, "SetState");
				}
			}
		}
	}
}
