using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore.Data.Items;
using Sitecore.Pipelines;
using Sitecore.Sbos.Module.LinkTracker.Goals.Interfaces;
using System;
using System.Reflection;
using System.Web;

namespace Sitecore.Sbos.Module.LinkTracker.Goals
{
    public class GoalProvider : IGoalProviderProcessor
    {
        public void Process(PipelineArgs args)
        {
            SetGoalFieldValue();
        }
        public List<Item> GetGoalItems(string path)
        {
            Sitecore.Data.Database context = Sitecore.Configuration.Factory.GetDatabase("master");
            Item item = context.SelectSingleItem(path);
            List<Item> items = item.Axes.GetDescendants().Where(x => x.TemplateID.ToString() == "{475E9026-333F-432D-A4DC-52E03B75CB6B}").ToList();
            return items;

        }

        public void SetGoalFieldValue()
        {
            var goalItems = GetGoalItems(Data.Constants.LinkTrackerConstants.SitecoreGoalPath);

            string webRooPath = GetWebRootPath("Sitecore.Sbos.Module.LinkTracker");

            if (!string.IsNullOrEmpty(webRooPath))
            {
                if (goalItems != null)
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    XmlNodeList nodeList = xdoc.GetElementsByTagName("Combobox");

                    if (nodeList.Count > 1)
                    {
                        XmlElement goalElement = (XmlElement)nodeList[1];
                        goalElement.IsEmpty = true;

                        XmlElement listItemEmpty = xdoc.CreateElement("ListItem");

                        listItemEmpty.SetAttribute("Value", string.Empty);
                        listItemEmpty.SetAttribute("Header", string.Empty);
                        listItemEmpty.RemoveAttribute("xmlns");

                        goalElement.AppendChild(listItemEmpty);

                        foreach (var item in goalItems)
                        {
                            var itemName = item.DisplayName;
                            var itemID = item.ID;

                            XmlElement listItem = xdoc.CreateElement("ListItem");

                            listItem.SetAttribute("Value", itemID.ToString());
                            listItem.SetAttribute("Header", itemName.ToString());
                            listItem.RemoveAttribute("xmlns");

                            goalElement.AppendChild(listItem);
                        }
                        xdoc.Save(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    }
                }
            }
        }

        public string GetWebRootPath(string assembly)
        {
            string assemblyLoc = Assembly.Load(assembly).CodeBase;

            if (!string.IsNullOrEmpty(assemblyLoc))
            {
                assemblyLoc = assemblyLoc.Replace("file:///", "");
                string webRootPath = assemblyLoc.Replace(Data.Constants.LinkTrackerConstants.AssemblyLinkTrackerPath, "");

                return webRootPath ?? string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}