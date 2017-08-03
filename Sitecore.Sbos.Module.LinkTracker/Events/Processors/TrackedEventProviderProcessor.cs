using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace Sitecore.Sbos.Module.LinkTracker.Events.Processors
{
    public abstract class TrackedEventProviderProcessor
    {
        protected abstract string DefinitionItemPath { get; }
        protected abstract ID TemplateId { get; }
        protected abstract int Index { get; }

        public void Process(PipelineArgs args)
        {
            var defItems = this.GetDefinitionItems();

            string webRooPath = this.GetWebRootPath("Sitecore.Sbos.Module.LinkTracker");

            if (!string.IsNullOrEmpty(webRooPath))
            {
                if (defItems != null)
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    XmlNodeList nodeList = xdoc.GetElementsByTagName("Combobox");

                    if (nodeList.Count > 1)
                    {
                        XmlElement goalElement = (XmlElement) nodeList[this.Index];
                        goalElement.IsEmpty = true;

                        XmlElement listItemEmpty = xdoc.CreateElement("ListItem");

                        listItemEmpty.SetAttribute("Value", string.Empty);
                        listItemEmpty.SetAttribute("Header", string.Empty);
                        listItemEmpty.RemoveAttribute("xmlns");

                        goalElement.AppendChild(listItemEmpty);

                        foreach (var item in defItems)
                        {
                            var itemName = item.DisplayName;
                            var itemId = item.ID;

                            XmlElement listItem = xdoc.CreateElement("ListItem");

                            listItem.SetAttribute("Value", itemId.ToString());
                            listItem.SetAttribute("Header", itemName);
                            listItem.RemoveAttribute("xmlns");

                            goalElement.AppendChild(listItem);
                        }
                        xdoc.Save(webRooPath + Data.Constants.LinkTrackerConstants.ExternalFormPath);
                    }
                }
            }
        }

        private List<Item> GetDefinitionItems()
        {
            var context = Configuration.Factory.GetDatabase("master");
            Item item = context.SelectSingleItem(this.DefinitionItemPath);
            List<Item> items = item.Axes.GetDescendants().Where(x => x.TemplateID.Equals(this.TemplateId)).ToList();
            return items;
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