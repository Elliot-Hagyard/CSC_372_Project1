using System;
using System.Collections.Generic;
using System.Windows.Forms;

// Form is a type of class which allows you build a user interface

namespace BSTVisualizer {
    public partial class GUI : Form {
        private TextBox inputTextBox;
        private Button buildButton;
        private TreeViewer treeViewer;

        public MainForm() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            // Create the gui box
            this.Text = "BST Visualizer";
            this.Size = new System.Drawing.Size(400, 400);

            // Create input text box
            inputTextBox = new TextBox();
            inputTextBox.Location = new System.Drawing.Point(10, 10);
            inputTextBox.Size = new System.Drawing.Size(200, 20);
            this.Controls.Add(inputTextBox);

            // Make build button
            buildButton = new Button();
            buildButton.Text = "Build BST";
            buildButton.Location = new System.Drawing.Point(220, 10);
            buildButton.Click += new EventHandler(BuildButton_Click);
            this.Controls.Add(buildButton);

            // Create Tree Viewer
            treeViewer = new TreeViewer();
            treeViewer.Location = new System.Drawing.Point(10, 40);
            treeViewer.Size = new System.Drawing.Size(360, 200);
            this.Controls.Add(treeViewer);
        }

        private void BuildButton_Click(object sender, EventArgs e) {
            // Parse the input text as a list of integers
            if (int.TryParse(inputTextBox.Text, out int[] values)) {
                // Call F# function to build BST and display it

            }
            else {
                MessageBox.Show("Your input was invalid. Please try again.");
            }
        }

        [STAThread]

        public static void Main() {
        Application.EnableVisualStyles();
        Application.Run(new GUI());
        }
    }
}

