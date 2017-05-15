Demo

Assumption:
1. If you want to follow the demo, please install Habitat sample site.

Steps:

1. Login to the Sitecore Client using your prefered browser.

![image](https://cloud.githubusercontent.com/assets/2329372/26075898/d4ffd252-396b-11e7-8b89-5388328049e4.png)

2. In Sitecore Launchpad click on **Desktop**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075886/d458f270-396b-11e7-831d-c481e88a2dd2.png)

3. In Sitecore Desktop Click “Sitecore Icon” and then select Content Editor

![image](https://cloud.githubusercontent.com/assets/2329372/26075893/d48daed4-396b-11e7-89c8-30397cdb69f3.png)

4. In Sitecore Content tree in the left panel click on **sitecore/content/Habitat/Global/Teasers**. 
5. Since we are using Sitecore Habitat and Link Manager only work with external link we need to access Global where Teasers folder is located.

![image](https://cloud.githubusercontent.com/assets/2329372/26075895/d4aad1e4-396b-11e7-80fc-fd9ed4987092.png)

6. Inside Teasers folder are Teaser component which is using external link in their content.
Click About Habitat(component) in the Content panel right below **Quick Info** click **Content** and scroll down look for **Teaser Link** and on Teaser Link kindly click **Insert external link**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075890/d484b81a-396b-11e7-8db5-cb68f2542717.png)

![image](https://cloud.githubusercontent.com/assets/2329372/26075961/ffcfea8a-396b-11e7-8024-e37433499533.png)

7. Inside **Insert External Link** dialog in the bottom part you can see we added Trigger Goal using a CheckBox and also Goal using ComboBox.
8. Trigger Goal: if you Check ✔ the checkBox it will trigger **True** and if not it will trigger **False**. If True you want to trigger the goal; If False you don't want to trigger the goal.
9. Goal: The comboBox create a list of Goals and it also uses a javascript that allows the user to **AutoComplete**.
10. If you click **Insert** it will create a **OnClick** from About Habitat Link.
11. In this Image example I triggered the Goal  by checking the CheckBox ✔ and Select a Goal name **Brochures Request** and click **Insert**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075891/d486b426-396b-11e7-9dda-3dbf73b64a1b.png)

12. After click **Insert** in Insert External Link. Click **Save** to save the changes. And Click Publish to publish the changes you made from the **Web**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075889/d4809c26-396b-11e7-8dd3-4e428a56f267.png)

13. After clicking Publish button select **Publish Item** to publish the item you changes and the Publish dialog will pop out.

![image](https://cloud.githubusercontent.com/assets/2329372/26075903/d59924d4-396b-11e7-8e43-8ab3c4452c3e.png)

14. In the **Publishing** select **Smart publish** and check ✔ the Publish subitems and Publish related items and then click **Publish** Button and click Yes.
15. After Publishing you will see a dialog box that indicates the item you publish and since I just update 1 Item only.
16. After Successfully publishing your changes. To to your Instance example http://Sitecore101/ and in this Demo we are using Sitecore Habitat. You can locate **About Habitat** below Sitecore Habitat Home Page in the footer section.

![image](https://cloud.githubusercontent.com/assets/2329372/26075900/d5191f78-396b-11e7-86f1-47ca2e6ae547.png)
![image](https://cloud.githubusercontent.com/assets/2329372/26075897/d4d74e7c-396b-11e7-9856-9a648eacc017.png)

17. To check if the Teaser Link Button added OnClick in the html structure please use developer tool in Google Chrome F12. use inspect button to inspect the Link.

![image](https://cloud.githubusercontent.com/assets/2329372/26075892/d488aaba-396b-11e7-9537-3ea55aac54dd.png)

18.Then Let’s Start Testing. Just Click the About Habitat Link which is **Example available on Github** and the Goal dialog will pop out just click **OK**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075887/d45e52b0-396b-11e7-8bc0-8b645fc65e4d.png)

19. To Check if the Goal is really Trigger you can look at the Sitecore Habitat Information Bar in the Right panel of the Site.

![image](https://cloud.githubusercontent.com/assets/2329372/26075885/d4575d70-396b-11e7-9ed3-8cc067a5ffce.png)

20. After clicking the Information Bar just click **Refresh** and Click **Onsite Behavior**.

![image](https://cloud.githubusercontent.com/assets/2329372/26075896/d4b1e0a6-396b-11e7-8688-b8a8c73572d5.png)

21. You can check the Triggered goals in Onsite Behavior. Since we triggered **Brochures Request in the Sitecore Client** it will list here in the Triggered goals.

![image](https://cloud.githubusercontent.com/assets/2329372/26075888/d478b7a4-396b-11e7-9a9e-86384715d6d6.png)

22. End
