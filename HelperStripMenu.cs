using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services
{
    public static class HelperStripMenu
    {
        public static IEnumerable<ToolStripMenuItem> GetToolStripMenuItemsRecursive(ToolStripItemCollection items)
        {
            var allItems = new List<ToolStripMenuItem>();
            foreach (var item in items.OfType<ToolStripMenuItem>())
            {
                allItems.Add(item);
                allItems.AddRange(GetToolStripMenuItemsRecursive(item.DropDownItems));
            }
            return allItems;
        }

        public static IEnumerable<ToolStripItem> GetStatusStripItems(ToolStripItemCollection items)
        {
            var allItems = new List<ToolStripItem>();
            foreach (var item in items.OfType<ToolStripItem>())
            {
                allItems.Add(item);
            }
            return allItems;
        }
    }
}
