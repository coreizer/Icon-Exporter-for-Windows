/*
 * Copyright (c) 2018 AlphaNyne
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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace IconExporter
{
  public partial class FrmMain : Form
  {
    public FrmMain()
    {
      InitializeComponent();
    }

    private void ExportIcons(string fileName)
    {
      SaveFileDialog SFD = new SaveFileDialog() {
        Filter = "PNG (*.png)|*.png",
        FileName = $"{DateTime.Now.Ticks}-icon.png"
      };

      if (SFD.ShowDialog(this) == DialogResult.OK) {
        Icon export = Icon.ExtractAssociatedIcon(fileName);
        Bitmap icon = new Bitmap(export.ToBitmap());
        icon.Save(SFD.FileName, ImageFormat.Png);
        icon.Dispose();
      }
    }

    private void ButtonExport_Click(object sender, EventArgs e)
    {
      try {
        OpenFileDialog OFD = new OpenFileDialog() {
          Filter = "Executable files (*.exe)|*.exe|*.All files|*.*",
          RestoreDirectory = false,
        };

        if (OFD.ShowDialog(this) == DialogResult.OK) {
          ExportIcons(OFD.FileName);
        }
      }
      catch (Exception ex) {
        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
