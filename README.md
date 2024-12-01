<div align="center">
   <h1>🎄 Welcome to the PXL-JAM 2024🎄</h1>
</div>

🙏 This repository is proudly part of the **2024 F# Advent Calendar**, hosted by Sergey Tihon - 🙏

The PXL PAM 2024 is a fun and engaging way

- to learn programming - it's perfect for beginners and experienced developers alike,
- team up and collaborate with other developers or take challenge for yourself
- and share your creations
- and... finally: You can **win a PXL-Clock Mk1** 🎉

🎁 Yes, you read that right! We're giving away a PXL-Clock Mk1 to some of the creators we liked most. 🎁

<div align="center">
   <h2>🎁 <strong>Win a PXL-Clock - Watch How on YouTube</strong> 🎁</h2>
   <a href="https://www.youtube.com/watch?v=ZAw_u59ueyA"><img src="https://img.youtube.com/vi/ZAw_u59ueyA/0.jpg" alt="Watch the video" style="width: 70%;"></a>
</div>

In the meantime, get started with PXL apps and unleash your creativity. Dive into the code, explore, and have fun building something awesome.

📣 **Spread the holiday cheer!**

Share this repository and help us make this a memorable event for the dotnet community.

## Terms and Conditions

All the information you can read in the README.md file and also in the [Questions and Answers](QuestionsAndAnswers.md) file is valid and part of the Terms and Conditions.
For more details, please refer to the [Terms and Conditions](TermsAndConditions.md) section.

## Questions and Answers

We have a separate [Questions and Answers](QuestionsAndAnswers.md) section that covers common queries about the PXL-JAM 2024. If you have any questions, check it out!

## Hands On!

To programm PXL-Apps, you need to set up your development environment. Here’s how to get started:

### Prerequisites

**Mandatory**

- [**.NET 8 SDK**](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

**Optional (Recommended)**

- [**Visual Studio Code (VSCode)**](https://code.visualstudio.com/)
- [**Ionide-fsharp Extension for VSCode**](https://marketplace.visualstudio.com/items?itemName=Ionide.Ionide-fsharp)

### Fork the Repository

Best practice is to fork this repository to your GitHub account. This way, you can experiment with the code and save your changes, and maybe there will be some surprises along the way. 🎁

### Prepare the Sprites

Check out the sprites 🖼️ in `./PxlApps/assets` (like `pizzaMampf.png`) and swap them with your own custom artwork to personalize your app.

### Create Your First App

A PXL-App consists of two parts:
   - one or more F# script (or many of them) that contain the code for your app,
   - optionally some assets like images.

To set up your first app, follow these steps:

- Under the `./PxlApps/04_Submissions` folder, create a file with your GitHub account name and the name of your app

  Pattern: `{GHAccountName}_{UniqueAppName}.fsx`
  Example: `SchlenkR_MyFirstApp.fsx`

You can just copy and rename the example file `./PxlApps/04_Submissions/SchlenkR_MyFirstApp.fsx` to get started.

- If you have any assets (images), put them in the folder `./PxlApps/assets/submissions`- and name them:
  
  Pattern: `{GHAccountName}_{UniqueAppName}...`
  Example: `SchlenkR_MyImage.png`

### 🚀 Start the Simulator

Before running any apps, you’ll need to start the simulator.

⚠️ **Important:** Only one simulator should be running at a time.

1. Open the list of build tasks in VSCode:
   - Press `Ctrl+Shift+B` (Windows/Linux) or `Cmd+Shift+B` (macOS).
2. Select **Start Simulator** from the list.

As an alternative for the VSCode build task, just run `./start-simulator.sh` (Mac) or `./start-simulator.ps1` (Windows) in your terminal.

### Run an App

- Ensure the simulator is running (see above).
- Open an app file (e.g. `./PxlApps/RoundClock.fsx`) in VSCode.
- Select the entire content of the file and run it by pressing `Alt+Enter` (Windows) or `Cmd+Enter` (Mac).

You can modify the code, open new files, and re-run apps as often as you like. Simply re-evaluate the **entire file** (that's the mose easy way.)

In case the simulator does not what you expect (e.g. you were in sleep mode), just restart the simulator.

### Submit Your App

When you’re ready to submit your app, create a pull request (PR) with your changes. We’ll review your app, provide feedback or merge it.

Follow-up PRs (updates) for your app in case you want to improve it are welcome until the end of the PXL-JAM 2024.

## Resources

### Tutorials and Examples

Explore the demo apps and tutorials in the `./PxlApps` directory.

### Programming Pitfalls

Have a look at the [./PxlApps/01_Basics/XX_Pitfalls.fsx] file to see some common pitfalls and how to avoid or fix them.


## Additional Information

**Resources**

- We offer videos and tutorials to help you set up everything.
- Getting the simulator up and running is straightforward.
- Examples are available to assist you in learning.

**Community Support**

- Don’t hesitate to ask for help!
- The community is friendly and eager to support fellow participants.

**Stay Updated**

- Follow Us: Keep up with the latest news by following us on Bluesky or similar platforms.
- Repository Announcements: Important updates will be posted in the repository.

We can’t wait to see your amazing creations! This is a fantastic opportunity to learn, experiment, and showcase your talent. Happy coding!

---

🎉 Enjoy experimenting!

🌟 Thank You for Being a Part of This Journey!

📣 If you like this Jam - please share it with others - thank you :)
