using Es.Udc.DotNet.Photogram.Model.UserDao;
using Es.Udc.DotNet.Photogram.Model.UserService;
using Es.Udc.DotNet.Photogram.Model.ImageDao;
using Es.Udc.DotNet.Photogram.Model.ImageService;
using Es.Udc.DotNet.Photogram.Model.CategoryDao;
using Es.Udc.DotNet.Photogram.Model.CategoryService;
using Es.Udc.DotNet.Photogram.Model.CommentDao;
using Es.Udc.DotNet.Photogram.Model.CommentService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.Photogram.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /* UserDao */
            kernel.Bind<IUserDao>().
                To<UserDaoEntityFramework>();

            /* UserService */
            kernel.Bind<IUserService>().
                To<UserService>();

            /* ImageDao */
            kernel.Bind<IImageDao>().
                To<ImageDaoEntityFramework>();

            /* ImageService */
            kernel.Bind<IImageService>().
                To<ImageService>();

            /* CategoryDao */
            kernel.Bind<ICategoryDao>().
                To<CategoryDaoEntityFramework>();

            /* CategoryService */
            kernel.Bind<ICategoryService>().
                To<CategoryService>();

            /* CommentDao */
            kernel.Bind<ICommentDao>().
                To<CommentDaoEntityFramework>();

            /* CommentService */
            kernel.Bind<ICommentService>().
                To<CommentService>();

            /* DbContext */
            string connectionString =
                ConfigurationManager.ConnectionStrings["PhotogramEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                ToSelf().
                InSingletonScope().
                WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}