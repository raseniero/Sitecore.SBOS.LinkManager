# Sitecore.SBOS.LinkTracker (BETA) 
This module extends the Link Manager to add a capability to set a link click to trigger a Page Event. 

## Getting Started
Below are the steps to get up and running with the module.

1. Clone the repository.
2. Open the Solution file in Visual Studio.

![image](https://cloud.githubusercontent.com/assets/2329372/25974770/9af4acea-365f-11e7-8edc-99dd9be43455.png)

3. Right-click the Project Solution and then click **Restore NuGet Packages** to load all References.

![image](https://cloud.githubusercontent.com/assets/2329372/25974805/ca97d292-365f-11e7-8a6e-3e1cab739dd1.png)

4. Build the solution to compile all code files.
5. Publish the compile codes your existing Sitecore Instance. Right-click the project solution and then select **Publish**.

![image](https://cloud.githubusercontent.com/assets/2329372/25974813/d0080af8-365f-11e7-9061-0883c2876ad7.png)

6. Inside Publish dialog click “Profile” tab select “Custom” to create new custom profile and enter Profile name after click “Next >” Button to continue in connection tab.

![image](https://cloud.githubusercontent.com/assets/2329372/25974819/d4a63f76-365f-11e7-9f9f-2dab298bb1e7.png)

7. Inside Connection tab in the Publish method select “File System” and on Target Location enter your Sitecore Instance then click “Next >” Button to continue in Settings tab.

![image](https://cloud.githubusercontent.com/assets/2329372/25974826/d9aa458a-365f-11e7-9c43-09c45b2b2f85.png)

8. Inside Settings tab in the Configuration select “Debug” and then click “Next >” Button to continue in Preview tab.

![image](https://cloud.githubusercontent.com/assets/2329372/25974829/dd9a68e6-365f-11e7-8de9-e6375b815800.png)

9. Inside Preview tab set the Dropdown list to the Custom Profile you created and then Click publish to deploy all files inside your Sitecore Instance.

![image](https://cloud.githubusercontent.com/assets/2329372/25974834/e36c3038-365f-11e7-9574-e1e633015868.png)

10. End

## Demonstration 

If you want to follow a step-by-step demo please see [Demo page](https://github.com/raseniero/Sitecore.SBOS.LinkManager/blob/master/Demo.md). This is assume that you have Habitat running.

## Supports
+ For support please email the author or [create an issue](https://github.com/raseniero/Sitecore.SBOS.ReferrerUrlParameters/issues/new).
+ Tested on Sitecore CMS 8.2 or later

THIS MODULE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT SUPPORT, WARRANTIES OR CONDITIONS OF ANY KIND.
