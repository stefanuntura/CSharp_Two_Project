# C-2_Project

<h1>Description of branches</h1>

<h3>Main branch:</h3>
<ul>
  <li>Production product with no bugs and errors</li>
  <li>Based on pull requests from "Development branch"</li>
  <li>Github owner approves the pull requests to the main branch</li>
</ul>
<h3>Development branch:</h3>
<ul>
  <li>Almost production level app, might have small bugs</li>
  <li>Based on feature branches</li>
  <li>Requires at least 1 reviewer from a contributor to merge</li>
</ul>
<h3>Feature branch</h3>
<ul>
  <li>Name of branch must start with "DEV-". Example name: "DEV-1: Menu button"</li>
</ul>
<h3>Documentation branch</h3>
<ul>
  <li>This branch contains all of the necessary documentation like the start document and the ERD.</li>
</ul>

<h1>Commit rules:</h1>
<ul>
  <li>Write down relevant commit titles and messages</li>
  <li>No swear words</li>
  <li>Commit messages should start with *DEV-*, example: "DEV-1: Add Player class"</li>
  <li>Commit messages and description should start with a verb in present tense, example:
    "Add button on menu page"
  </li>
</ul>
<h1>Installation Guide:</h1>

This section handles the installation of the necessary software to be able to run the game and will contain steps on how to do so.  This application was built using Monogame 3.8 and Visual Studio 2019 (VS) Community edition. but setting up a development environment is not required to run the appliation.

1. Download Visual Studio 2019 Community Edition with the following components:
   - .NET cross-platform development - For Desktop OpenGL and DirectX platforms
   - Mobile Development with .NET - For Android and iOS platforms
   - Universal Windows Platform development - For Windows 10 and Xbox UWP platforms
   - .Net Desktop Development - For Desktop OpenGL and DirectX platforms to target normal .NET Framework
2.  Install MonoGame extension for Visual Studio 2019
   - In VS, navigate to "Extensions -> Manage Extensions" in the menu bar.
   - Search for "MonoGame" in top right search window.
   - Install "MonoGame project templates".
3. Open 'Graduation.sln" and run the game!
