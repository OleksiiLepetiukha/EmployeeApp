﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValidateControls
{
    internal class Validation
    {       
            public static bool ValidateControl(Control control, string controlName)
            {
                if (control is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show($"{controlName} should not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox.Focus();
                    textBox.BackColor = Color.Red;
                    return false;
                    } else
                {
                    textBox.BackColor = Color.White;
                }
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show($"Please select a value for {controlName}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBox.Focus();
                        comboBox.BackColor = Color.Red;
                        return false;
                    } else
                {
                    comboBox.BackColor = Color.White;
                }
                }

                return true;
            }
        
    }

}
