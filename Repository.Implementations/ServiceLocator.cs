using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class ServiceLocator
    {
		private static IKernel _kernel;
		public static void RegisterAll(IKernel kernel)
		{

			_kernel = kernel;
			_kernel.Register(Component.For(typeof(IAuthorRepository))
				.ImplementedBy(typeof(AuthorRepository)));

			_kernel.Register(Component.For(typeof(INewsRepository))
				.ImplementedBy(typeof(NewsRepository)));
		}

		public static T Get<T>()
		{
			return _kernel.Resolve<T>();
		}
    }
}
