using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BulkUpdate
{
    public partial class bulkoperations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTreeView();
        }

        private void LoadTreeView()
        {
            Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            Item rootItem = masterDb.GetItem("/sitecore/content");

            if (rootItem != null)
            {
                PopulateTreeView(rootItem, null);
            }
        }

        private void PopulateTreeView(Item parentItem, TreeNode parentNode)
        {
            foreach (Item child in parentItem.Children)
            {
                TreeNode node = new TreeNode(child.Name, child.ID.ToString());

                if (parentNode == null)
                {
                    ItemTreeView.Nodes.Add(node);
                }
                else
                {
                    parentNode.ChildNodes.Add(node);
                }

                PopulateTreeView(child, node);
            }
        }

        protected void CopyItems(object sender, EventArgs e)
        {
            PerformBulkAction("copy");
        }

        protected void MoveItems(object sender, EventArgs e)
        {
            PerformBulkAction("move");
        }

        protected void DeleteItems(object sender, EventArgs e)
        {
            PerformBulkAction("delete");
        }

        private void PerformBulkAction(string action)
        {
            Database masterDb = Sitecore.Configuration.Factory.GetDatabase("master");
            List<string> selectedItemIds = GetSelectedItems();

            using (new SecurityDisabler()) // Bypass security restrictions
            {
                foreach (string itemId in selectedItemIds)
                {
                    Item item = masterDb.GetItem(new ID(itemId));
                    if (item == null) continue;

                    switch (action)
                    {
                        case "copy":
                            Item parent = item.Parent;
                            item.CopyTo(parent, item.Name + "_copy");
                            break;

                        case "move":
                            Item newParent = masterDb.GetItem("/sitecore/content/NewLocation"); // Set your target location
                            if (newParent != null)
                            {
                                item.MoveTo(newParent);
                            }
                            break;

                        case "delete":
                            item.Delete();
                            break;
                    }
                }
            }
        }

        private List<string> GetSelectedItems()
        {
            List<string> selectedItems = new List<string>();
            foreach (TreeNode node in ItemTreeView.CheckedNodes)
            {
                selectedItems.Add(node.Value);
            }
            return selectedItems;
        }
    }
}