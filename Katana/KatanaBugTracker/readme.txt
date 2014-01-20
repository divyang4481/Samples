Nancy Razor
Microsoft.Owin.Host.SystemWeb

How the Startup class is discovered:

- If the web.config file contains an appSetting with key=“owin:AppStartup”, the loader uses the setting value. The value must be a valid .NET-type name.
- If the assembly contains the attribute [assembly: OwinStartup(typeof(MyStartup))], the loader will use the type specified in the attribute value.
- If neither of these conditions are true, the loader will scan the loaded assemblies looking for a type named Startup with a method that matches the signature void Configure(IAppBuilder app).

