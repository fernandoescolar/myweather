namespace MyWeather.Core.Services
{
	using MyWeather.Core.Model;
	using MyWeather.Tests;
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public class WeatherServiceMock : Mock<IWeatherService>, IWeatherService
	{
		private readonly CountCallers countCallers;
		private readonly CountCalls countCalls;
		private readonly Handlers handlers;
		private readonly Verifications verifications;
		private readonly Verifiers verifiers;

		public WeatherServiceMock()
		{
			this.countCallers = new CountCallers(this);
			this.countCalls = new CountCalls(this);
			this.handlers = new Handlers(this);
			this.verifications = new Verifications(this);
			this.verifiers = new Verifiers(this);
		}

		public Task<WeatherInfo> GetCityWeatherAsync(string cityName)
		{
			Task<WeatherInfo> result;
			this.InvokeMember("GetCityWeatherAsync", new object[] { cityName }, out result);
			return result;
		}
		public Task<CurrentWeather> GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
		{
			Task<CurrentWeather> result;
			this.InvokeMember("GetCoordinateCurrentWeatherAsync", new object[] { latitude, longitude }, out result);
			return result;
		}
		public Task<WeatherInfo> GetCoordinateWeatherAsync(double latitude, double longitude)
		{
			Task<WeatherInfo> result;
			this.InvokeMember("GetCoordinateWeatherAsync", new object[] { latitude, longitude }, out result);
			return result;
		}
		public Task<CurrentWeather> GetCurrentCityWeatherAsync(string cityName)
		{
			Task<CurrentWeather> result;
			this.InvokeMember("GetCurrentCityWeatherAsync", new object[] { cityName }, out result);
			return result;
		}
		public Task<CurrentWeather> GetCurrentWeatherAsync()
		{
			Task<CurrentWeather> result;
			this.InvokeMember("GetCurrentWeatherAsync", new object[] {  }, out result);
			return result;
		}
		public Task<WeatherInfo> GetWeatherAsync()
		{
			Task<WeatherInfo> result;
			this.InvokeMember("GetWeatherAsync", new object[] {  }, out result);
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
			private readonly WeatherServiceMock parent;
			internal CountCallers(WeatherServiceMock parent)
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
			public CountCallers GetCityWeatherAsync(string cityName)
			{
				this.parent.CalledWith("GetCityWeatherAsync", cityName);
				return this;
			}
			public CountCallers GetCityWeatherAsync()
			{
				this.parent.Called("GetCityWeatherAsync");
				return this;
			}
			public CountCallers GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
			{
				this.parent.CalledWith("GetCoordinateCurrentWeatherAsync", latitude, longitude);
				return this;
			}
			public CountCallers GetCoordinateCurrentWeatherAsync()
			{
				this.parent.Called("GetCoordinateCurrentWeatherAsync");
				return this;
			}
			public CountCallers GetCoordinateWeatherAsync(double latitude, double longitude)
			{
				this.parent.CalledWith("GetCoordinateWeatherAsync", latitude, longitude);
				return this;
			}
			public CountCallers GetCoordinateWeatherAsync()
			{
				this.parent.Called("GetCoordinateWeatherAsync");
				return this;
			}
			public CountCallers GetCurrentCityWeatherAsync(string cityName)
			{
				this.parent.CalledWith("GetCurrentCityWeatherAsync", cityName);
				return this;
			}
			public CountCallers GetCurrentCityWeatherAsync()
			{
				this.parent.Called("GetCurrentCityWeatherAsync");
				return this;
			}
			public CountCallers GetCurrentWeatherAsync()
			{
				this.parent.Called("GetCurrentWeatherAsync");
				return this;
			}
			public CountCallers GetWeatherAsync()
			{
				this.parent.Called("GetWeatherAsync");
				return this;
			}
			public class CountCallerMethods
			{
				private readonly WeatherServiceMock parent;
				private readonly int count;
				internal CountCallerMethods(WeatherServiceMock parent, int count)
				{
					this.parent = parent;
					this.count = count;
				}
				public CountCallerMethods GetCityWeatherAsync(string cityName)
				{
					this.parent.CalledWith(this.count, "GetCityWeatherAsync", cityName);
					return this;
				}
				public CountCallerMethods GetCityWeatherAsync()
				{
					this.parent.Called(this.count, "GetCityWeatherAsync");
					return this;
				}
				public CountCallerMethods GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
				{
					this.parent.CalledWith(this.count, "GetCoordinateCurrentWeatherAsync", latitude, longitude);
					return this;
				}
				public CountCallerMethods GetCoordinateCurrentWeatherAsync()
				{
					this.parent.Called(this.count, "GetCoordinateCurrentWeatherAsync");
					return this;
				}
				public CountCallerMethods GetCoordinateWeatherAsync(double latitude, double longitude)
				{
					this.parent.CalledWith(this.count, "GetCoordinateWeatherAsync", latitude, longitude);
					return this;
				}
				public CountCallerMethods GetCoordinateWeatherAsync()
				{
					this.parent.Called(this.count, "GetCoordinateWeatherAsync");
					return this;
				}
				public CountCallerMethods GetCurrentCityWeatherAsync(string cityName)
				{
					this.parent.CalledWith(this.count, "GetCurrentCityWeatherAsync", cityName);
					return this;
				}
				public CountCallerMethods GetCurrentCityWeatherAsync()
				{
					this.parent.Called(this.count, "GetCurrentCityWeatherAsync");
					return this;
				}
				public CountCallerMethods GetCurrentWeatherAsync()
				{
					this.parent.Called(this.count, "GetCurrentWeatherAsync");
					return this;
				}
				public CountCallerMethods GetWeatherAsync()
				{
					this.parent.Called(this.count, "GetWeatherAsync");
					return this;
				}
			}
		}
		public class CountCalls
		{
			private readonly WeatherServiceMock parent;
			internal CountCalls(WeatherServiceMock parent)
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
				private readonly WeatherServiceMock parent;
				private readonly int position;
				internal CountCallsMethods(WeatherServiceMock parent, int position)
				{
					this.parent = parent;
					this.position = position;
				}
				public MemberInvocation GetCityWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetCityWeatherAsync");
				}
				public MemberInvocation GetCoordinateCurrentWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetCoordinateCurrentWeatherAsync");
				}
				public MemberInvocation GetCoordinateWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetCoordinateWeatherAsync");
				}
				public MemberInvocation GetCurrentCityWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetCurrentCityWeatherAsync");
				}
				public MemberInvocation GetCurrentWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetCurrentWeatherAsync");
				}
				public MemberInvocation GetWeatherAsync()
				{
					return this.parent.GetCall(this.position, "GetWeatherAsync");
				}
			}
		}
		public class Handlers
		{
			private readonly WeatherServiceMock parent;
			internal Handlers(WeatherServiceMock parent)
			{
				this.parent = parent;
			}
			public Handlers GetCityWeatherAsync(Func<string, Task<WeatherInfo>> action)
			{
				this.parent.Handle<string, Task<WeatherInfo>>("GetCityWeatherAsync", action);
				return this;
			}
			public Handlers GetCoordinateCurrentWeatherAsync(Func<double, double, Task<CurrentWeather>> action)
			{
				this.parent.Handle<double, double, Task<CurrentWeather>>("GetCoordinateCurrentWeatherAsync", action);
				return this;
			}
			public Handlers GetCoordinateWeatherAsync(Func<double, double, Task<WeatherInfo>> action)
			{
				this.parent.Handle<double, double, Task<WeatherInfo>>("GetCoordinateWeatherAsync", action);
				return this;
			}
			public Handlers GetCurrentCityWeatherAsync(Func<string, Task<CurrentWeather>> action)
			{
				this.parent.Handle<string, Task<CurrentWeather>>("GetCurrentCityWeatherAsync", action);
				return this;
			}
			public Handlers GetCurrentWeatherAsync(Func<Task<CurrentWeather>> action)
			{
				this.parent.Handle<Task<CurrentWeather>>("GetCurrentWeatherAsync", action);
				return this;
			}
			public Handlers GetWeatherAsync(Func<Task<WeatherInfo>> action)
			{
				this.parent.Handle<Task<WeatherInfo>>("GetWeatherAsync", action);
				return this;
			}
		}
		public class Verifications
		{
			private readonly WeatherServiceMock parent;
			internal Verifications(WeatherServiceMock parent)
			{
				this.parent = parent;
			}
			public Verifications GetCityWeatherAsync(string cityName)
			{
				this.parent.AddVerification("GetCityWeatherAsync", cityName);
				return this;
			}
			public Verifications GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
			{
				this.parent.AddVerification("GetCoordinateCurrentWeatherAsync", latitude, longitude);
				return this;
			}
			public Verifications GetCoordinateWeatherAsync(double latitude, double longitude)
			{
				this.parent.AddVerification("GetCoordinateWeatherAsync", latitude, longitude);
				return this;
			}
			public Verifications GetCurrentCityWeatherAsync(string cityName)
			{
				this.parent.AddVerification("GetCurrentCityWeatherAsync", cityName);
				return this;
			}
			public Verifications GetCurrentWeatherAsync()
			{
				this.parent.AddVerification("GetCurrentWeatherAsync", new object[0]);
				return this;
			}
			public Verifications GetWeatherAsync()
			{
				this.parent.AddVerification("GetWeatherAsync", new object[0]);
				return this;
			}
		}
		public class Verifiers
		{
			private readonly WeatherServiceMock parent;
			internal Verifiers(WeatherServiceMock parent)
			{
				this.parent = parent;
			}
			public Verifiers GetCityWeatherAsync(string cityName)
			{
				this.parent.Verify("GetCityWeatherAsync", cityName);
				return this;
			}
			public Verifiers GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
			{
				this.parent.Verify("GetCoordinateCurrentWeatherAsync", latitude, longitude);
				return this;
			}
			public Verifiers GetCoordinateWeatherAsync(double latitude, double longitude)
			{
				this.parent.Verify("GetCoordinateWeatherAsync", latitude, longitude);
				return this;
			}
			public Verifiers GetCurrentCityWeatherAsync(string cityName)
			{
				this.parent.Verify("GetCurrentCityWeatherAsync", cityName);
				return this;
			}
			public Verifiers GetCurrentWeatherAsync()
			{
				this.parent.Verify("GetCurrentWeatherAsync", new object[0]);
				return this;
			}
			public Verifiers GetWeatherAsync()
			{
				this.parent.Verify("GetWeatherAsync", new object[0]);
				return this;
			}
		}
	}
}
