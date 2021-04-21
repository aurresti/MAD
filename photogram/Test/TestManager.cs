using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.Photogram.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region Option A : configuration via sourcecode

            IKernel kernel = new StandardKernel();

            kernel.Bind<IImageService>().To<Es.Udc.DotNet.Photogram.Model.ImageService.ImageService>();

            kernel.Bind<IImageDao>().
                To<ImagenDaoEntityFramework>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["PhotogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion Option A : configuration via sourcecode

            #region Option B: configuration via external XML configuration file

            // The kernel should automatically load extensions at startup
            //NinjectSettings settings = new NinjectSettings() { LoadExtensions = false };
            //IKernel kernel = new StandardKernel(settings, new Ninject.Extensions.Xml.XmlExtensionModule());

            //kernel.Load("Ninject_Config.xml");

            #endregion Option B: configuration via external XML configuration file

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}