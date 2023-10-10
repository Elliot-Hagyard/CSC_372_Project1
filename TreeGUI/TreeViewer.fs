(*
File: TreeViewer.fs
Authors: Elliot Hagyard and M. Fay Garcia
Purpose: Implement an Avalonia application to visualize a BST
*)
namespace TreeGUI

module TreeViewer =
    // Open the packages we will need to build the GUI
    open Avalonia
    open Avalonia.Controls
    open Avalonia.FuncUI
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open Avalonia.Media
    open Avalonia.Controls.Shapes

    // We define this module ourselves
    open Tree

    type Point = {
        x : float
        y : float
    }

    // F# has a weird way of handling radians/degrees...
    let angle = 80.0 * (System.Math.PI/180.0)
    let fromRad = (180.0/System.Math.PI)

    let sin x = System.Math.Sin x
    let cos x = System.Math.Cos x
    
    let width = 30.0
    let height = 20.0
    let rect_height = 50.0

    // Increment the position of the next GUI element depending on the branch of the tree
    let left point rot = {x = point.x - rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    let right point rot = {x = point.x + rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    
    // Create a text box representing a node
    let createText text point =
        TextBlock.create [
                TextBlock.width (float (String.length text)*10.0)
                TextBlock.height height
                TextBlock.text text

                Canvas.left (point.x - (float (String.length text)*5.0) )
                Canvas.top (point.y + height/2.0)
            ]

    // Create a line between two nodes
    let createRect rot point =
        Rectangle.create [
            Rectangle.width 1.0
            Rectangle.height rect_height

            TextBlock.verticalAlignment VerticalAlignment.Center
            TextBlock.horizontalAlignment HorizontalAlignment.Center

            Rectangle.fill (SolidColorBrush (Color.Parse "black"))
            Rectangle.renderTransform(RotateTransform (fromRad*rot))

            Canvas.left (point.x - rect_height*sin(rot)/2.0)
            Canvas.top  (point.y + rect_height*cos(rot)/2.0)
        ]

    // Draw the tree
    let rec drawTree tree point curAngle =
        match tree with
        | Empty -> [] : Types.IView list
        | Tree t -> 
            match (t.left, t.right) with
            // If the L/R subtrees are empty, print the value at the node
            | (Empty, Empty) -> [createText $"{t.value}" point]

            // If one of the subtrees are empty, recurse on the non-empty one
            | (_, Empty) -> 
                [createText $"{t.value}" point;
                createRect curAngle point;] @ drawTree t.left (left point curAngle) (curAngle/2.0)
            | (Empty, _) -> [createText $"{t.value}" point;
                createRect -curAngle point] @ drawTree t.right (right point curAngle) (curAngle/2.0)
            
            // If neither subtree is empty, recurse on both subtrees
            | (_, _) -> 
                [createText $"{t.value}" point; 
                createRect -curAngle point; 
                createRect curAngle point;] @ drawTree t.left (left point (curAngle)) (curAngle/2.0)
                @ drawTree t.right (right point (curAngle)) (curAngle/2.0)
    
    // Draw the GUI components that visualize the BST
    let view =        
        Component(fun ctx ->
            let state = ctx.useState 0
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    Canvas.create [ // Remember to initialize position
                        Canvas.height 850.0
                        Canvas.background Brushes.Transparent

                        // EDIT "tree" HERE TO TEST BST VISUALIZER APP
                        Canvas.children (([] :  Types.IView list) @ drawTree t1 {x=0.0; y=200.0} angle)
                    ]
                ]
            ]
        )
    
    (*Insertion methods and TreeViewer application tests*)
    (*TREE OPTIONS: Choose one and replace "tree" in ln. 104 with the variable name.
                    The default test variable is t1*)

    // TREE 1 should be balanced
    let l1 = [10; 5; 4; 20; 100; 15; 6; -5; 120;]
    let t1 = addToTree l1 Empty


    // TREE 2 should also be balanced
    let mutable t2 = Empty

    // We're also testing insertion here
    t2 <- insert 10 t2
    t2 <- insert 5 t2
    t2 <- insert 4 t2
    t2 <- insert 20 t2
    t2 <- insert 30 t2
    t2 <- insert 15 t2
    t2 <- insert 12 t2
    t2 <- insert 9 t2
    

    // TREE 3 should be unbalanced and biased to the left
    let l3 = [10;9;8;7;6;5]
    let t3 = addToTree l3 Empty


    // TREE 4 should be unbalanced and biased to the right
    let l4 = [1;2;3;4;5;6;7]
    let t4 = addToTree l4 Empty