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

# 3.2 Commit and Push the Project to GitHub

Select Team Explorer, then click "Changes" to view a list of modified/added files.  The contents of your new project should be listed.

Enter a brief commit message in the box provided, then click "Commit all".  

Click the "Home" icon on the small toolbar above the Team Explorer, then select "Sync" from the menu.  The commit you just added should be listed in
the "Outgoing Commits" section; click the "Push" link to push the commit to GitHub.