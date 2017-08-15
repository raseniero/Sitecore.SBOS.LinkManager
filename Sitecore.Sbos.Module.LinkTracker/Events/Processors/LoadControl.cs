using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public class LoadControl
    {
        public void Process(SaveArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            ReloadControl();
        }
        public List<Item> GetDefinitionItems(string path, string tempId)
        {
            var context = Configuration.Factory.GetDatabase("master");
            Item item = context.SelectSingleItem(path);
            List<Item> items = item.Axes.GetDescendants().Where(x => x.TemplateID.ToString() == tempId).ToList();
            return items;
        }

        public void ReloadControl()
        {
            string webRooPath = this.GetWebRootPath("Sitecore.Sbos.Module.LinkTracker");

            string[] PathElement = new string[4];
            PathElement[1] = "/sitecore/system/Marketing Control Panel/Goals";
            PathElement[2] = "/sitecore/system/Settings/Analytics/Page Events";
            PathElement[3] = "/sitecore/system/Marketing Control Panel/Campaigns";

            string[] TempId = new string[4];
            TempId[1] = "{475E9026-333F-432D-A4DC-52E03B75CB6B}";
            TempId[2] = "{059CFBDF-49FC-4F14-A4E5-B63E1E1AFB1E}";
            TempId[3] = "{94FD1606-139E-46EE-86FF-BC5BF3C79804}";

            for (int i = 1; i <= 3; i++)
            {
                var ItemsOn = GetDefinitionItems(PathElement[i].ToString(), TempId[i].ToString());

                if (ItemsOn != null)
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    XmlNodeList nodeList = xdoc.GetElementsByTagName("Combobox");

                    if (nodeList.Count > 1)
                    {
                        XmlElement ElementList = (XmlElement)nodeList[i];
                        ElementList.IsEmpty = true;

                        foreach (var item in ItemsOn)
                        {
                            var itemName = item.Name;
                            var itemId = item.ID;

                            XmlElement listItem = xdoc.CreateElement("ListItem");

                            listItem.SetAttribute("Value", itemId.ToString());
                            listItem.SetAttribute("Header", itemName);
                            listItem.RemoveAttribute("xmlns");

                            ElementList.AppendChild(listItem);
                        }
                        xdoc.Save(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    }
                }
            }
        }

        private string GetWebRootPath(string assembly)
        {
            string assemblyLoc = Assembly.Load(assembly).CodeBase;

            if (!string.IsNullOrEmpty(assemblyLoc))
            {
                assemblyLoc = assemblyLoc.Replace("file:///", "");

                string webRootPath = assemblyLoc.Replace(Data.Constants.LinkTrackerConstants.AssemblyLinkTrackerPath,
                    "");

                return webRootPath;
            }

            return string.Empty;
        }
    }
}