Demo

Assumption:
1. If you want to follow the demo, please install Habitat sample site.

Steps:

1. Login to the Sitecore Client using your prefered browser.

![image](https://cloud.githubusercontent.com/assets/2329372/26075898/d4ffd252-396b-11e7-8b89-5388328049e4.png)

2. In Sitecore Launchpad click on **Desktop**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075886/d458f270-396b-11e7-831d-c481e88a2dd2.png)

3. In Sitecore Desktop Click “Sitecore Icon” and then select Content Editor

![image]()

4. In Sitecore Content tree in the left panel click on **sitecore/content/Habitat/Global/Teasers**. 
5. Since we are using Sitecore Habitat and Link Manager only work with external link we need to access Global where Teasers folder is located.

![image]()

6. Inside Teasers folder are Teaser component which is using external link in their content.
Click About Habitat(component) in the Content panel right below **Quick Info** click **Content** and scroll down look for **Teaser Link** and on Teaser Link kindly click **Insert external link**.

![image]()

![image]()

7. Inside “Insert External Link” dialog in the bottom part you can see we added Trigger Goal, Trigger Page Event, and Trigger Campaign using a CheckBox and also List of Goal, Page Event, and Campaign using ComboBox.
8.Trigger Goal: if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the goal.
Goal: The comboBox create a list of Goals and it also uses a javascript that allows the user to “AutoComplete”.
9.Page Event:if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the Page Event.
10.Campaign: if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the Campaign.
11.If you click “Insert” it will create a “OnClick” from About Habitat Link.
In this Image example I triggered the Goal, Page Event, Campaign  by checking the CheckBox ✔ and Select a Goal name **Brochures Request**, Page Event name **Begin Transaction**, and lastly Campaign name **Register Page** and click **Insert**.

![image]()

12. After click **Insert** in Insert External Link. Click **Save** to save the changes. And Click Publish to publish the changes you made from the **Web**.

![image]()

13. After clicking Publish button select **Publish Item** to publish the item you changes and the Publish dialog will pop out.

![image]()

14. In the **Publishing** select **Smart publish** and check ✔ the Publish subitems and Publish related items and then click **Publish** Button and click Yes.
15. After Publishing you will see a dialog box that indicates the item you publish and since I just update 1 Item only.
16. After Successfully publishing your changes. To to your Instance example http://Sitecore101/ and in this Demo we are using Sitecore Habitat. You can locate **About Habitat** below Sitecore Habitat Home Page in the footer section.

![image]()
![image]()

17. To check if the Teaser Link Button added OnClick in the html structure please use developer tool in Google Chrome F12. use inspect button to inspect the Link.

![image]()

19. To Check if the Goal, Page Event, and Campaign is really Trigger you can look at the Sitecore Habitat Information Bar in the Right panel of the Site.

![image]()

20. After clicking the Information Bar just click **Refresh** and Click **Onsite Behavior**.

![image]()

21. You can check the Triggered goals, page event you can check it at in Sitecore Habitat Onsite Behavior and if you triggered campaign you can check it at in Referrer. Since we triggered Goal, Page event and Campaign this will register in Interaction.

![image]()

22. End
