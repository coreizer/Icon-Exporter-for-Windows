/*
 * Copyright (c) 2019 Coreizer
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace IconExtract
{
   using System;
   using System.Drawing;
   using System.Drawing.Imaging;
   using System.Windows.Forms;

   public partial class frmMain : Form
   {
      public frmMain()
      {
         InitializeComponent();
      }

      private void IconExtract(string fileName)
      {
         var SFD = new SaveFileDialog() {
            Filter = "PNG (*.png)|*.png",
            FileName = $"{DateTime.Now.Ticks}-icon.png"
         };

         if (SFD.ShowDialog(this) == DialogResult.OK) {
            var extractIcon = Icon.ExtractAssociatedIcon(fileName);
            var iconMap = new Bitmap(extractIcon.ToBitmap());
            iconMap.Save(SFD.FileName, ImageFormat.Png);
            iconMap.Dispose();
         }
      }

      private void ButtonExport_Click(object sender, EventArgs e)
      {
         try {
            var OFD = new OpenFileDialog() {
               Filter = "Executable files (*.exe)|*.exe|*.All files|*.*",
               RestoreDirectory = false,
            };

            if (OFD.ShowDialog(this) == DialogResult.OK) {
               IconExtract(OFD.FileName);
            }
         }
         catch (Exception ex) {
            MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void frmMain_DragDrop(object sender, DragEventArgs e)
      {
         var filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);
         foreach (var path in filePaths) {
            IconExtract(path);
         }
      }

      private void frmMain_DragEnter(object sender, DragEventArgs e)
      {
         e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ?
               DragDropEffects.All
             : DragDropEffects.None;
      }
   }
}
