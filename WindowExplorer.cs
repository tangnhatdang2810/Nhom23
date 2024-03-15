using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowExplorer
{
    public partial class Form1 : Form
    {
        private string currentPath;
        private string copiedFilePath;
        private string cutFilePath;
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
            InitializeListView();
        }
        private void InitializeTreeView()
        { 
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                driveNode.Tag = drive.RootDirectory.FullName;
                treeView1.Nodes.Add(driveNode);
            }
        }
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Name", 150);
            listView1.Columns.Add("Size", 100);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Modified Date", 150);
        }
        private void PopulateListView(string path)
        {
            listView1.Items.Clear();
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                ListViewItem item = new ListViewItem(dir.Name);
                item.SubItems.Add("");
                item.SubItems.Add("Folder");
                item.SubItems.Add(dir.LastWriteTime.ToString());
                item.Tag = dir.FullName;
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in directory.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Length.ToString());
                item.SubItems.Add("File");
                item.SubItems.Add(file.LastWriteTime.ToString());
                item.Tag = file.FullName;
                listView1.Items.Add(item);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PopulateListView(e.Node.Tag.ToString());
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string selectedPath = listView1.SelectedItems[0].Tag.ToString();
                if (File.Exists(selectedPath)) 
                {
                    try
                    {
                        System.Diagnostics.Process.Start(selectedPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở tệp tin: " + ex.Message);
                    }
                }
                else if (Directory.Exists(selectedPath)) 
                {
                    PopulateListView(selectedPath);
                }
            }
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                copiedFilePath = listView1.SelectedItems[0].Tag.ToString();
            }
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                cutFilePath = listView1.SelectedItems[0].Tag.ToString();
            }
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].Tag.ToString();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else if (Directory.Exists(filePath))
                {
                    Directory.Delete(filePath, true);
                }
                PopulateListView(GetSelectedFolderPath());
            }

        }
        private string GetSelectedFolderPath()
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                return selectedNode.Tag.ToString();
            }
            return null;
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PasteItem();
        }
        private void PasteItem()
        {
            string destinationFolderPath = GetSelectedFolderPathListView();

            if (string.IsNullOrEmpty(destinationFolderPath))
            {
                MessageBox.Show("Please select a destination folder.");
                return;
            }

            if (!string.IsNullOrEmpty(copiedFilePath))
            {
                if (File.GetAttributes(copiedFilePath).HasFlag(FileAttributes.Directory))
                {
                    string newFolderPath = Path.Combine(destinationFolderPath, Path.GetFileName(copiedFilePath));
                    CopyDirectory(copiedFilePath, newFolderPath);
                }
                else
                {
                    string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(copiedFilePath));
                    File.Copy(copiedFilePath, destinationFilePath);
                }
            }
            else if (!string.IsNullOrEmpty(cutFilePath))
            {
                if (File.GetAttributes(cutFilePath).HasFlag(FileAttributes.Directory))
                {
                    string newFolderPath = Path.Combine(destinationFolderPath, Path.GetFileName(cutFilePath));
                    Directory.Move(cutFilePath, newFolderPath);
                }
                else
                {
                    string destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(cutFilePath));
                    File.Move(cutFilePath, destinationFilePath);
                }
            }
            copiedFilePath = null;
            cutFilePath = null;
            PopulateListView(destinationFolderPath);
        }
        private string GetSelectedFolderPathListView()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return null;
            }

            string selectedFolderPath = listView1.SelectedItems[0].Tag.ToString();
            if (!Directory.Exists(selectedFolderPath))
            {
                selectedFolderPath = Path.GetDirectoryName(selectedFolderPath);
            }
            return selectedFolderPath;
        }
        private void CopyDirectory(string sourceDirPath, string destDirPath)
        {
            Directory.CreateDirectory(destDirPath);
            foreach (string filePath in Directory.GetFiles(sourceDirPath))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(destDirPath, fileName);
                File.Copy(filePath, destFilePath);
            }
            foreach (string subDirPath in Directory.GetDirectories(sourceDirPath))
            {
                string subDirName = Path.GetFileName(subDirPath);
                string destSubDirPath = Path.Combine(destDirPath, subDirName);
                CopyDirectory(subDirPath, destSubDirPath);
            }
        }
        private void paToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedFolderPath = GetSelectedFolderPath();
            if (!string.IsNullOrEmpty(selectedFolderPath))
            {
                string newFolderName = "New Folder";
                int count = 1;
                string newFolderPath = Path.Combine(selectedFolderPath, newFolderName);
                while (Directory.Exists(newFolderPath))
                {
                    newFolderName = "New Folder (" + count + ")";
                    newFolderPath = Path.Combine(selectedFolderPath, newFolderName);
                    count++;
                }
                Directory.CreateDirectory(newFolderPath);
                PopulateListView(selectedFolderPath);
            }
        }
    }
}
