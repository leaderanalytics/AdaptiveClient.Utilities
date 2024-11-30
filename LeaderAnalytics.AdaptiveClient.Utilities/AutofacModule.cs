namespace LeaderAnalytics.AdaptiveClient.Utilities;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<ServiceManifestFactory>().InstancePerLifetimeScope().PropertiesAutowired();
    }
}
