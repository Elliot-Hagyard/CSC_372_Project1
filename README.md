# F# Project

## Authors

M. Fay Garcia
Elliot Hagyard

## Descriptive Overview

In F#, this project implments both a Binary Search Tree in and a rudimentary application
to visualize the tree. The application utilizes the Avalonia library to build a GUI.

## Running the Code

To run the code, you need to have the dotnet runtime environment installed.
To install on Mac with Homebrew, run the following command in your terminal:

```bash

brew install --cask dotnet

```

Once installed, change into the TreeGUI directory and run the console application
using the following command:

```bash

dotnet run

```

This will open the BST Visualizer and initialize it with the default tree.

To run the BST Visualizer on a different test tree:
    1. Quit out of the Visualizer application (not the terminal)
    2. Open "TreeViewer.fs" and follow the instructions in ln. 100
    3. Use the "dotnet run" command in your terminal again

To run the basic tree tests, run "TestTree.fsx" using the following command in your terminal:

```bash

dotnet fsi TestTree.fsx

```

You can open "TestTree.fsx" to view the inputs and expected outputs.