partial class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] readonly Solution Solution;
    [MinVer] MinVer MinVer;
    AbsolutePath ProjectDirectory => SourceDirectory / "Cli";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath PublishDirectory => RootDirectory / "publish";
    AbsolutePath ReleaseDirectory => RootDirectory / "release";
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestDirectory => RootDirectory / "tests";
    AbsolutePath TestResultsDirectory => TestDirectory / "results";
    IEnumerable<string> Projects => Solution.AllProjects.Select(x => x.Name);
    string Framework => "net8.0";
    string Runtime => "win-x64";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();
            SourceDirectory.GlobDirectories("**/{obj,bin}")
                .DeleteDirectories();
            PublishDirectory.DeleteDirectory();
            ReleaseDirectory.DeleteDirectory();
        });

    Target Restore => _ => _
    .After(Clean)
        .Executes(() => DotNetRestore(_ => _
                .SetForce(true)
                .SetProjectFile(Solution.Directory)));

    Target Compile => _ => _
        .DependsOn(Clean, Restore)
        .Executes(() =>
            DotNetBuild(_ => _
                .EnableNoLogo()
                .EnableNoRestore()
                .SetConfiguration(Configuration)
                .SetRuntime(Runtime)
                .SetProjectFile(ProjectDirectory)
            ));

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
            DotNetTest(_ => _
                .EnableNoLogo()
                .EnableNoRestore()
                .EnableNoBuild()
                .SetConfiguration(Configuration)
                .SetProjectFile(TestDirectory)
            // .SetResultsDire@Octory(TestResultsDirectory)
            // .SetProcessArgumentConfigurator(_ => _
            //     .Add("-- --coverage --coverage-output-format cobertura --results-directory ./results"))
            ));

    Target Publish => _ => _
        .DependsOn(Compile)
        .Executes(() =>
            DotNetPublish(_ => _
                .EnableNoLogo()
                .EnableNoRestore()
                .EnableNoBuild()
                .SetConfiguration(Configuration)
                .SetRuntime(Runtime)
                .SetProject(ProjectDirectory)
                .SetOutput(PublishDirectory)
            // .SetPublishSingleFile(true)
            // .SetSelfContained(false)
            ));


    Target Release => _ => _
        .TriggeredBy(Publish)
        .Produces(ReleaseDirectory)
        .Unlisted()
        .Executes(() =>
        {
            MinVer = MinVerTasks.MinVer(_ => _
                .SetAutoIncrement(MinVerVersionPart.Minor)
                .SetDefaultPreReleaseIdentifiers("preview.0")
                .SetTagPrefix("v")
            ).Result;
            Log.Information(MinVer.Version);

            var packDir = PublishDirectory;
            var outputDir = ReleaseDirectory;
            Log.Information("Velopack --packDir: {0}", packDir);
            Log.Information("Velopack --outputDir: {0}", outputDir);
            Vpk.Invoke($"pack --packId typetype --packVersion {MinVer.Version} --packDir {packDir} --mainExe typetype.exe --packTitle typetype --outputDir {outputDir}");
        });
}
