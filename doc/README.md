# 1. Environment Setup

This session requires [Visual Studio 2017](https://www.visualstudio.com/thank-you-downloading-visual-studio/?sku=Community&rel=15).  

Download and execute the installer from the link above, and when prompted to select a workload, select ".NET desktop development".  You can 
optionally select other workloads if you choose, however for this session we'll be making a console application which only requires this one.

The installation should take about 10 minutes.  

# 1.1 Install the GitHub Extension for Visual Studio

When setup is complete, launch Visual Studio and from the "Tools" menu at the top select "Extensions and Updates" to launch the Extensions and
Updates dialog.

Select "Online" from the menu to the left of the dialog, then select and download the GitHub Extension for Visual Studio.  If it doesn't appear in
the list, search for "GitHub" in the box to the right to search for it.

After downloading the extension, close all Visual Studio windows to automatically start the installation.

# 2. Create a GitHub.com Account

GitHub is a service that provides online storage for your Git repositories.  To create an account, navigate to [GitHub](https://github.com/) and
create an account, if you don't already have one.  There's nothing tricky about signing up, however you will need to verify your email address 
before we can link to other services.

# 2.2 Connect to GitHub from Visual Stucio

Re-open Visual Studio (if it isn't open already), then click the "Team Explorer" tab near the bottom right of the screen.  Under the "Manage Connections"
heading on the pane on the right of the screen, click "Connect" under the GitHub section and log in to your account.

# 3. Create a New Repository

The Team Explorer tab in Visual Studio should now display a few options under the GitHub section.  Select "Create" to create a new repository for
our project.  A dialog titled "Create a GitHub Repository" will appear; enter a name and description of your choosing, then click "Create".

A message will be displayed stating that the repository has been created, and a small link titled "Create a new project or solution" will appear
in the same area.  Click on it to open the "New Project" dialog.

# 3.1 Create a New Project

Within the New Project dialog, select "Templates\Visual C#" from the list on the left, then select "Console App (.NET Framework)".  Give the project
a name of your choosing, then click "OK" to create the project.

# 3.2 Add a Unit Test Project

Right click "Solution" in the root of the solution tree in the Solution Exporer, then select "Add" -> "New Project".

Within the New Project dialog, select "Templates\Visual C#\Test", then select "Unit Test Project (.NET Framework)"

# 3.3 Move Project Folders

The "add new solution" functionality places the new solution into a subdirectory of the repository root; this isn't necessary.  Fix this by saving and closing 
Visual Studio, then navigate to the repository folder and cut (ctrl+x) and paste (ctrl+v) the subdirectory into the root.

If done correctly, the root of your repository should contain a ```.sln``` file and two folders; one for your application and one for your tests, as well as
your readme, .gitignore and a couple of other miscellaneous things._

# 3.4 Commit and Push the Project to GitHub

Select Team Explorer, then click "Changes" to view a list of modified/added files.  The contents of your new project should be listed.

Enter a brief commit message in the box provided, then click "Commit all".  

Click the "Home" icon on the small toolbar above the Team Explorer, then select "Sync" from the menu.  The commit you just added should be listed in
the "Outgoing Commits" section; click the "Push" link to push the commit to GitHub.

# 4 Add References

Expand the Unit Test project, then right click "References" and choose "Manage NuGet Packages..." from the pop-up menu.  The NuGet Package Manager dialog will appear.

Begin by uninstalling the two existing references; we'll be using the Xunit framework as opposed to MSTest.

Next, add the necessary Xunit packages "xunit", "xunit.runner.console", and "xunit.runner.visualstudio".

Add the "OpenCover" and "Moq" packages to handle code coverage and mockups, respectively.

# 4.1 Test the Tests

Replace the content of the automatically generated "UnitTest1.cs" file with the following:

```c#
using Xunit;

namespace UnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(1, 2);
        }
    }
}

```

This will create two unit tests; one passing and one failing.

Run the tests by opening the "Test" menu and selecting "Run" > "All Tests".  Verify that one passing and one failing test are reported.

# 5 Set up Continuous Integration

Now that we have a project scaffolded and our test runner is working we want to set up Continuous Integration with AppVeyor.

Upon a push to the master branch of the repository AppVeyor will clone the repo, build the project and tests, run the tests using the Xunit console
runner, then use OpenCover to generate a code coverage report.

To get all this working we need to connect our GitHub account to [AppVeyor](https://www.appveyor.com/) and [Codecov](https://codecov.io/).  Make sure you
are logged in to your GitHub account, then visit both sites and log in using your GitHub account.  You'll be prompted to accept permissions; use your best
judgment.

Finally, once logged in to AppVeyor, click "Projects" near the top of the page, then click "New Project" and select your repository from the list.

# 5.1 Configure AppVeyor Integration

Next, we need to add instructions for AppVeyor to follow.  We do this by adding an ```appveyor.yml``` file to the root of our repository.  Create this file
and paste the following contents into it:

```yaml
version: 1.0.0.{build}
before_build:
- cmd: nuget restore
build:
  project: ConsoleApp1.sln
  verbosity: minimal
test_script:
- .\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:"C:\projects\lets-code-test\UnitTestProject1\bin\Debug\UnitTestProject1.dll -noshadow -appveyor" -returntargetcode -filter:"+[*]*" -excludebyattribute:*.ExcludeFromCodeCoverage* -hideskipped:All -output:.\coverage.xml
- "SET PATH=C:\\Python34;C:\\Python34\\Scripts;%PATH%"
- pip install codecov
- codecov -f "coverage.xml"
```

The first couple of lines are pretty standard; set the version, perform a nuget restore to pull in all of your referenced packages, and build the solution.

The ```test_script``` section is where things get very specific to the testing environment we are using.

The first line of the script executes OpenCover and forces it to use the Xunit console runner to execute the tests, then outputs a coverage summary to ```coverage.xml```.

Potentially tricky aspects of this line are the OpenCover version (check your packages folder) and the path to your test dll.  AppVeyor clones your repo to the folder ```C:\projects\<your repo name>\```, so 
be sure to replace "lets-code-test" in the line above with the name of your repo.

Commit your changes and push your repo to GitHub to test.  If everything works as expected, your AppVeyor build should fail due to the failing test we wrote.

# 5.2 Fix your Tests

Modify the ```Assert()``` method call in your ```FailingTest``` method to make the test pass, then commit and push your changes.

AppVeyor should run again and should pass.

# 5.3 View your Code Coverage

Navigate to [codecov.io](http://codecov.io) and your coverage results should automatically come in from AppVeyor.

# 6 Code Something!

We'll do this part live during the meetup.