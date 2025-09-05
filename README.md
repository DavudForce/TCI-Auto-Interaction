# TCI Auto-Interaction

TCI Auto-Interaction is a practical tool designed to help users monitor their TCI (Telecommunication Company of Iran) account efficiently. The application connects to the TCI website and extracts key data, alerting you if any monitored values exceed the limits you’ve set in the settings.

Currently, the process requires the user to log in at startup and wait for about 30 seconds for the system to initialize. After that, the application automatically fetches data at the intervals specified in your settings, ensuring timely notifications without further manual steps.

## Features
- Connects to TCI website and extracts account data
- Alerts user if any data exceeds defined limits
- Automatic data extraction after startup
- Customizable schedule intervals in settings
- Customizable warning styles in settings

## How to download
- Download the latest version from GitHub Releases.
- Goto https://sendgb.com/cmMQUeBVbw1 and enter the password.
- copy the "server" folder (inside the downloaded folder) into the same directory that the executable located in.
- Make sure you have .NET8 (or later) Runtime installed.
- Run the application.

## How to Use
- Open the application.
- Log in with your TCI account details.
    - Please pay attention to **do not** press enter key during this procces. If enter key is pressed, you may need to close and re-open the application.
- Press "OK" button on the bottom of the application.
- After that you can see the button's text is now changed into "Retrieve Data". You can press it **after initialization** to manually extract data
- Wait about 30 seconds for initialization. Please note that the window must remain visible during this time, but it can be minimized.
- After initialization, the app will automatically fetch data at the interval set in Settings (The interval is in hours. Default is 6).
- You can now minimize the application to system tray if you enabled this feauture in the Settings and still get notifications
  ### How to enable "minimize to system tray" feauture:
  - After application is initialized, press "Settings" button on the top-right corner next to connection status text.
  - In the settings window, check "Minimize to system tray" checkbox and close the window.
  - If you minimize the application, you will see a notification saying that the application is minimized to system tray.
  - To open the window back, duble-click on the worldsphere with a computer icon on lower left corner of your desktop (Press small triagle icon "**^**" if not present)
- If any monitored value exceeds your defined limit, a notification will alert you based on your warning style defined on Settings (Warn me very ... dropdown list).

## Settings Overview
This is a more detailed explanation of each item you see on the Settings window. Please note that this lits is in order from top to down on what each item does. ("n" represents numberical values):

- **Today's download is grather than n MB**: Defines limit for internet download in a day in MBs. The application will warn you if today's download is grather than this value.
- **Today's upload is grather than n MB**: Defines limit for internet upload in a day in MBs. The application will warn you if today's upload is grather than this value.
- **n Days left from my active internet service**: Warns you if days left from your active internet service grather or equal to this value.
- **n Days left from my timed internet service**: Warns you if days left from your timed internet service grather or equal to this value. **Timed internet service**: The service that applies to specific times of the day. For example, from 1 AM to 12 PM for 20 days 90 MB total traffic.
- **My billing is grather than n rials**: Warns you if your internet/phone bills is grather than this value.
- **Days remaining and data remaining don't match with n% tolerance**: This application extracts how much of your active package time remains and how much of your active pakcage is used and takes absolute value of their subrtaction from eachother. If the resulting value is grather than `0 + tolerance` it warns the user. Tolerance specifies how much the two value can differ frm eachother.
- **Check internet status every n hours**: Sets interval btween each data extraction in hours. Lower is faster but 6-12 hours is ideal in most cases.
- **Minimize to system tray**: Determinates if the application can enter system tray after minimization.
- **Warm me very ...**: The user can specify how they going to be warned when they exceed one of the limits they defined.
-   ### What each element in "Warm me very ..." does:
    - **Politely**: Displays an Information style notification to user with a cute emojy in the end. Does not have any sounds.
    - **Normally**: Displays an Warning style notification to user with a warning sound.
    - **Aggressively**: Find this out yourself!
    - **Godmode**: Still under development and does nothing. **if you select this, you will not see any warnings!**

## Release notes:
v1.3.11-alpha is a pre-release that maked in 2.5 days and contains enourmous amounts of bugs (Which is much of them aren't fatal errors, just minor errors). But they will be fixed on next versions and new feautures will be added. Future versions will include UI/UX improvements and will improve login and include server-based CAPTCHA solver.

## Screenshots:
-  At application startup, you will see this wondow.
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/34f6256b-ddee-410b-8857-139df9539251" />

- Scroll down until you see this login form.
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/9d7aa050-8b20-4604-b5d1-da0d7337444f" />

- Fill the required fields and press OK button and pay attention to **do not** press enter key during this proccess. If enter key is pressed, you may need to manually close, and re-open the app.
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/129f0851-5217-4897-9218-04e3a248b067" />

- You will see the window change to something similar to this and you will notice the "OK" button now saying "Retrieve data".
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/efaf2f81-0cd6-4ecc-b474-ebae7c7224cc" />

- And you will see a notification box on bottom right corner of your desktop. Press the × button to close it.
<img width="334" height="154" alt="image" src="https://github.com/user-attachments/assets/e82497ed-c5cb-4f12-b052-b4bbd09f7486" />

- From now on, you need to wait for 30 seconds. The window must remain visible but you can minimize it.
- After 30 seconds you will see a notification saying the application is started. Close it using the × icon.
<img width="334" height="115" alt="image" src="https://github.com/user-attachments/assets/38fe2570-2c07-482c-b4ec-cce50bd1db92" />

- You can now monitor your status in this window
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/3fcb8bf9-3def-4306-9e3c-f6d42a85714f" />

- Scroll down to see further details
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/bdaab9f2-b105-4a48-948c-3e6c331a466b" />

- To open settings window, press the "Settings" button on the top right of the application, next to internet status text.
<img width="721" height="400" alt="image" src="https://github.com/user-attachments/assets/ca1df99b-7ed8-4b3d-b2b5-8674e4ed0990" />

- You will see this window appearing. Their purose is described in detail [here](#settings-overview)
<img width="308" height="325" alt="image" src="https://github.com/user-attachments/assets/d3f90409-00b5-4f76-9c41-01e4306bc1d9" />

- If you exceed any of your limits, the application will warn you about it. For example, this figure shows a warning notification that will appear when you exceed your daily download limit.
<img width="334" height="114" alt="image" src="https://github.com/user-attachments/assets/72a33371-1ccf-4a8c-80e5-6868dadcbff4" />

- if "Minimize to system tray" is enable in your settings, you can minimize the application and it will move to system tray mode.
<img width="408" height="41" alt="image" src="https://github.com/user-attachments/assets/24d02594-8b5c-4720-b32d-7819e639f8f1" />

- if it's not present, press "^" button on your desktop
<img width="385" height="40" alt="image" src="https://github.com/user-attachments/assets/c8f619f5-7872-4230-92d0-72406115973c" />

- and duble click on this icon to open the app
<img width="121" height="120" alt="image" src="https://github.com/user-attachments/assets/bd99cd2e-74db-46fd-9c40-a79dfd8bc7f9" />

## Common issues & FAQ
Contact information is place in the end of this file.

**sometimes when the program starts, it only shows a white window with the "OK" button on below**: This issue is mostly server-side error and you simply cannot do anything about it yourself. But sometimes it can be caused my slow connecions. Eather way, when you encounter this problem, [contact](#contact-us) to app developers and describe them your issue and what did you did and did the application work at another time you was uing it.

**Sometimes it shows a message box saying "Cannot make a connection because target machine actively refused it" (or soemthing similar)**: This message shows up because server is down or it is stoped manually. Eather way, you cannot do anyhting by yourself. [Contact](#contact-us) to app developers and inform them of this issue.

**Sometimes it shows a message box saying that settings file is missing**: This is because the file that the Application is storing it's settings on is missing or re-named. But another common cause it that the application has not access to that directory. In those cases, you can eather run the application as administrator, or you can allow it in your prefered defender application. If the file is missing or re-named, you can press "Yes" button on the message box to create a new Settings file with default values. After restoring, your settings will be reseted and you may need to manually re-valuate them. This issue is usually harmless.

**When I open the program, in the navigation section, it says that I can't reach the site**: This issue is usually because of your internet connection quality. However, sometimes using a VPN can cause this problem too. Try disconnecting from VPN and trying again.

## Contact us
If you having any other issue, please contact us via this E-Mail: keypumbs.team@gmail.com
