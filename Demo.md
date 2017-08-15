Demo

Assumption:
1. If you want to follow the demo, please install Sitecore Habitat sample site.

Steps:

1. Login to the Sitecore Client using your prefered browser.

![image](https://cloud.githubusercontent.com/assets/2329372/26075898/d4ffd252-396b-11e7-8b89-5388328049e4.png)

2. In Sitecore Launchpad click on **Desktop**.

![image](https://user-images.githubusercontent.com/2329372/29325656-16fda4fc-821b-11e7-8270-b68963439398.png)

3. In Sitecore Desktop Click “Sitecore Icon” and then select Content Editor

![image](https://user-images.githubusercontent.com/2329372/29325732-5c1b6f2e-821b-11e7-853a-5d5af02a77d8.png)

4. In Sitecore Content tree in the left panel click on **sitecore/content/Habitat/Global/Teasers**. 
5. Since we are using Sitecore Habitat and Link Manager only work with external link we need to access Global where Teasers folder is located.

![image](https://user-images.githubusercontent.com/2329372/29325830-c23e4c86-821b-11e7-9d1b-486beee888f2.png)

6. Inside Teasers folder are Teaser component which is using external link in their content.
Click About Habitat(component) in the Content panel right below **Quick Info** click **Content** and scroll down look for **Teaser Link** and on Teaser Link kindly click **Insert external link**.

![image](https://user-images.githubusercontent.com/2329372/29325901-07469a22-821c-11e7-9d69-1064416fe7da.png)

![image](https://user-images.githubusercontent.com/2329372/29325882-f0a51b40-821b-11e7-8d84-85a4fdbfc580.png)

7. Inside “Insert External Link” dialog in the bottom part you can see we added Trigger Goal, Trigger Page Event, and Trigger Campaign using a CheckBox and also List of Goal, Page Event, and Campaign using ComboBox.
8.Trigger Goal: if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the goal.
Goal: The comboBox create a list of Goals and it also uses a javascript that allows the user to “AutoComplete”.
9.Page Event:if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the Page Event.
10.Campaign: if you Check ✔ the checkBox it will trigger “True” and if not it will trigger “False”. If True you want to trigger the goal; If False you don't want to trigger the Campaign.
11.If you click “Insert” it will create a “OnClick” from About Habitat Link.
In this Image example I triggered the Goal, Page Event, Campaign  by checking the CheckBox ✔ and Select a Goal name **Brochures Request**, Page Event name **Begin Transaction**, and lastly Campaign name **Register Page** and click **Insert**.

![image](https://user-images.githubusercontent.com/2329372/29325940-27164816-821c-11e7-998d-7aa05417b440.png)

12. After click **Insert** in Insert External Link. Click **Save** to save the changes. And Click Publish to publish the changes you made from the **Web**.

13. After clicking Publish button select **Publish Item** to publish the item you changes and the Publish dialog will pop out.

![image](https://user-images.githubusercontent.com/2329372/29326022-859bcb0e-821c-11e7-9778-6343299506d1.png)

14. In the **Publishing** select **Smart publish** and check ✔ the Publish subitems and Publish related items and then click **Publish** Button and click Yes.

![image](https://user-images.githubusercontent.com/2329372/29326104-d8b00a80-821c-11e7-8a43-4aa5e6390759.png)

15. After Publishing you will see a dialog box that indicates the item you publish and since I just update 1 Item only.

![image](https://user-images.githubusercontent.com/2329372/29326135-f9731ad2-821c-11e7-8167-bf1d22a6f556.png)

16. After Successfully publishing your changes. To to your Instance example http://Sitecore101/ and in this Demo we are using Sitecore Habitat. You can locate **About Habitat** below Sitecore Habitat Home Page in the footer section.

![image](https://user-images.githubusercontent.com/2329372/29326266-6846902e-821d-11e7-94aa-8f4404400bdd.png)

17. To check if the Teaser Link Button added OnClick in the html structure please use developer tool in Google Chrome F12. use inspect button to inspect the Link.

![image](https://user-images.githubusercontent.com/2329372/29326301-97eda2e0-821d-11e7-866f-a57c670bba5e.png)

19. To Check if the Goal, Page Event, and Campaign is really Trigger you can look at the Sitecore Habitat Information Bar in the Right panel of the Site.

![image](https://user-images.githubusercontent.com/2329372/29326347-c172053e-821d-11e7-946c-698d4bf2f5f8.png)

20. After clicking the Information Bar just click **Refresh** and Click **Onsite Behavior** to see trigger Goal and Page Event.

![image](https://user-images.githubusercontent.com/2329372/29326373-db4bce0e-821d-11e7-8dfc-159c4865cde0.png)

21. You can check the Triggered goals, page event you can check it at in Sitecore Habitat Onsite Behavior and if you triggered campaign you can check it at in Referrer. Since we triggered Goal, Page event and Campaign this will register in Interaction.

![image](https://user-images.githubusercontent.com/2329372/29326417-fd57ee10-821d-11e7-9f2f-ef20160de85c.png)

![image](https://user-images.githubusercontent.com/2329372/29326445-1c154d5c-821e-11e7-87ce-893f7a37e3f9.png)

![image](https://user-images.githubusercontent.com/2329372/29326461-2ccd2796-821e-11e7-887a-95ca048f6e1b.png)

22. End

## Supports
+ For support please email the author or [create an issue](https://github.com/raseniero/Sitecore.SBOS.LinkManager/issues/new).
+ Tested on Sitecore CMS 8.2 Update 1 or later

THIS MODULE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT SUPPORT, WARRANTIES OR CONDITIONS OF ANY KIND.