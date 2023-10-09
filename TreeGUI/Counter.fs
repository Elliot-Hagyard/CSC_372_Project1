namespace TreeGUI

module TreeViewer =
    open Avalonia.Controls
    open Avalonia.FuncUI
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
  
    open Avalonia
    open Avalonia.Controls
    open Avalonia.Layout
    open Avalonia.FuncUI.DSL

    open Avalonia.Media
    open Avalonia.Controls.Shapes

    type point = {
        x : float
        y : float
    }

    let createText text point = 
        TextBlock.create [
                TextBlock.width 64
                TextBlock.horizontalAlignment HorizontalAlignment.Center // Is this necessary?
                // TextBlock.verticalAlignment // Is this necessary?
                TextBlock.text text

                Canvas.left point.x
                Canvas.top point.y
            ]
    
    let createRect rot point =
        // rot is 45.0 or -45.0
        // alignment?
        Rectangle.create [
            Rectangle.width 2.5
            Rectangle.height 50.0
            Rectangle.fill (SolidColorBrush (Color.Parse "black"))
            Rectangle.renderTransform(RotateTransform rot)

            Canvas.left point.x
            Canvas.top point.y
            
        ]
            
    // point is the xy-coords of current node
    // let rec drawTree tree point = 
    //     match tree with
    //     | Empty -> []
    //     | Tree t -> 
    //         match (t.left, t.right) with
    //         | (Empty, Empty) -> [createText $"{t.value}" x y]
    //         | (_, Empty) -> [createText $"{t.value}" createRect x y 45.0;]@drawTree t.left@drawTree t.right]
    //         | (Empty, _) -> [createText ${t.value}; createRect point{x=x; y=y} -45.0]@drawTree t.left@drawTree t.right]
    //         | (_, _) -> [createText ${t.value}; createRect x y 45.0; createRect x y -45.0]@drawTree t.left@drawTree t.right]

    let view tree =
        Component(fun ctx ->
            let state = ctx.useState 0
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    
                    Canvas.create [ // Remember to initialize position

                        Canvas.height 400.0
                        Canvas.background Brushes.Transparent
                        Canvas.children [drawTree tree]

                ]

            ]
        )


 // DockPanel.children [
                //     Button.create [
                //         Button.width 64
                //         Button.horizontalAlignment HorizontalAlignment.Center
                //         Button.horizontalContentAlignment HorizontalAlignment.Center
                //         Button.content "Reset"
                //         Button.onClick (fun _ -> state.Set 0)
                //         Button.dock Dock.Bottom
                //     ]
                //     Button.create [
                //         Button.width 64
                //         Button.horizontalAlignment HorizontalAlignment.Center
                //         Button.horizontalContentAlignment HorizontalAlignment.Center
                //         Button.content "Build"
                //         Button.onClick (fun _ -> state.Current - 1 |> state.Set)
                //         Button.dock Dock.Bottom
                //     ]
                //     Button.create [
                //         Button.width 64
                //         Button.horizontalAlignment HorizontalAlignment.Center
                //         Button.horizontalContentAlignment HorizontalAlignment.Center
                //         Button.content "+"
                //         Button.onClick (fun _ -> state.Current + 1 |> state.Set)
                //         Button.dock Dock.Bottom
                //     ]
                //     TextBlock.create [
                //         TextBlock.dock Dock.Top
                //         TextBlock.fontSize 48.0
                //         TextBlock.horizontalAlignment HorizontalAlignment.Center
                //         TextBlock.text (string state.Current)
                //     ]
                // ]