namespace Infrastructure
{
	using Castle.MicroKernel;
	using Castle.MicroKernel.Registration;
	using Repository.Implementations;
	using Repository.Interfaces;

	internal static class ServiceLocator
	{

		public static void RegisterAll(IKernel kernel)
		{
			_kernel = kernel;
			_kernel.Register(
			   Component.For(typeof(IAuthorRepository))
						.ImplementedBy(typeof(AuthorRepository)).LifestylePerWebRequest());
			_kernel.Register(
			   Component.For(typeof(INewsRepository))
						.ImplementedBy(typeof(NewsRepository)).LifestylePerWebRequest());
		}

		public static T Get<T>()
		{
			return _kernel.Resolve<T>();
		}

		private static IKernel _kernel;
	}
}