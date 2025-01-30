<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<TargetFramework>net8.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
		<OpenApiSpecLocation>swagger.json</OpenApiSpecLocation>
		<ClientClassName>ApiClient</ClientClassName>
		<ClientNamespace>Api</ClientNamespace>		
		<!-- NSwagExe should be installed globally or in the project directory -->
	</PropertyGroup>

	<Target Name="generateClient" BeforeTargets="CoreCompile" Inputs="$(OpenApiSpecLocation)" Outputs="$(ClientOutputDirectory)\$(ClientClassName).cs">
		<Exec Command="$(NSwagExe) openapi2csclient /input:$(OpenApiSpecLocation)  /classname:$(ClientClassName) /namespace:$(ClientNamespace) /output:$(ClientClassName).cs" ConsoleToMSBuild="true">
			<Output TaskParameter="ConsoleOutput" PropertyName="OutputOfExec" />
		</Exec>
	</Target>
	<Target Name="forceReGenerationOnRebuild" AfterTargets="CoreClean">
		<Delete Files="$(ClientOutputDirectory)\$(ClientClassName).cs"></Delete>
	</Target>
	<ItemGroup>
	  <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
	  <PackageReference Include="NSwag.MSBuild" Version="14.2.0">
	    <PrivateAssets>all</PrivateAssets>
	    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
	  </PackageReference>
	</ItemGroup>

</Project>
