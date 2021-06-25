using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.CommentService;
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
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };

            IKernel kernel = new StandardKernel(settings);

            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            kernel.Bind<IUserService>().
                To<UserService>();

            kernel.Bind<IImageDao>().
                To<ImageDaoEntityFramework>();

            kernel.Bind<IImageService>().
                To<ImageService>();

            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            kernel.Bind<ICategoryService>().
                To<CategoryService>();

            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            kernel.Bind<ICommentService>().
                To<CommentService>();

            string connectionString =
                ConfigurationManager.ConnectionStrings["PhotogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);

            return kernel;
        }

        /// <summary>
        /// Configures the Ninject kernel from an external module file.
        /// </summary>
        /// <param name="moduleFilename">The module filename.</param>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel(string moduleFilename)
        {
            NinjectSettings settings = new NinjectSettings() { LoadExtensions = true };
            IKernel kernel = new StandardKernel(settings);

            kernel.Load(moduleFilename);

            return kernel;
        }

        public static void ClearNInjectKernel(IKernel kernel)
        {
           kernel.Dispose();
        }
    }
}