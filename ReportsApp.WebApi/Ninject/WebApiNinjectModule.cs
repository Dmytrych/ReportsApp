using Ninject.Modules;
using ReportsApp.Authentication.Dto;
using ReportsApp.WebApi.ApplicationSettings;
using ReportsApp.WebApi.Authentication;
using ReportsApp.WebApi.ControllerExecution;
using ReportsApp.WebApi.Controllers;
using ReportsApp.WebApi.Controllers.Converters;
using ReportsApp.WebApi.Controllers.Domain;
using ReportsApp.WebApi.Controllers.Domain.Dto;
using ReportsApp.WebApi.Controllers.Domain.StudentRepository;
using ReportsApp.WebApi.Controllers.Domain.UserRepository;
using ReportsApp.WebApi.Controllers.Reports;
using ReportsApp.WebApi.Controllers.Services;
using ReportsApp.WebApi.Dto;
using ReportsApp.WebApi.Routing;
using ReportsApp.WebApi.Server;

namespace ReportsApp.WebApi.Ninject
{
    public class WebApiNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerEndpointListener>().To<ServerEndpointListener>();
            Bind<IRequestHandler>().To<RequestHandler>();
            Bind<IApplicationSettingsProvider>().To<ApplicationSettingsProvider>();
            Bind<IApiChainCreator>().To<ApiChainCreator>();
            Bind<IApplicationRouter>().To<ApplicationRouter>();
            Bind<IControllerActionResolver>().To<ControllerActionResolver>();
            Bind<IApplicationAuthenticator>().To<ApplicationAuthenticator>();
            Bind<IControllerExecutionApiChainElement>().To<ControllerExecutionApiChainElement>();
            BindReporting();
            BindServices();
            BindConverters();
            BindControllers();
            BindDbContext();
            BindRepositories();
        }

        private void BindReporting()
        {
            Bind<IReportsSpecification>().To<ReportsSpecification>();
            Bind<IReportsBuilder<string>>().To<TextReportsBuilder>();
            Bind<IReportsGenerationManager>().To<ReportsGenerationManager>();
        }

        private void BindRepositories()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IStudentRepository>().To<StudentRepository>();
            Bind<IExternalUserRepository>().To<ExternalUserRepository>();
        }

        private void BindDbContext()
        {
            var dbContext = new MsSqlDatabaseContext();
            Bind<IUserContext>().ToConstant(dbContext);
            Bind<IStudentContext>().ToConstant(dbContext);
            Bind<IDormitoryContext>().ToConstant(dbContext);
        }

        private void BindControllers()
        {
            Bind<IApiController>().To<ReportsApiController>();
            Bind<IApiController>().To<StudentApiController>();
            Bind<IApiController>().To<AuthApiController>();
            Bind<IApiController>().To<FacultyApiController>();
        }

        private void BindConverters()
        {
            Bind<IDtoConverter<StudentClientDto, Student>>().To<StudentDtoConverter>();
            Bind<IDtoConverter<User, UserClientDto>>().To<UserDtoConverter>();
        }

        private void BindServices()
        {
            Bind<IReportsGenerationService>().To<ReportsGenerationService>();
            Bind<IStudentService>().To<StudentService>();
            Bind<IAuthService>().To<AuthService>();
            Bind<IDormitoryService>().To<DormitoryService>();
        }
    }
}