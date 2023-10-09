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

    open Tree
    type Point = {
        x : float
        y : float
    }

    let left point = {x = point.x - 20.0; y = point.y + 20.0}
    let right point  = {x = point.x + 20.0; y = point.y + 20.0}
    
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
            
    let rec drawTree tree point =
        match tree with
        | Empty -> [] : Types.IView list
        | Tree t -> 
            match (t.left, t.right) with
            | (Empty, Empty) -> [createText $"{t.value}" point]
            | (_, Empty) -> [createText $"{t.value}" point; createRect 45.0 point;] @ drawTree t.left (left point)
            | (Empty, _) -> [createText $"{t.value}" point; createRect -45.0 point] @ drawTree t.right (right point)
            | (_, _) -> [createText $"{t.value}" point; createRect -45.0 point; createRect 45.0 point;]@ drawTree t.left (left point) @ drawTree t.right (right point)
    


    let view =
        let mutable c_equivalent = Empty
        c_equivalent <- insert 10 c_equivalent
        c_equivalent <- insert 5 c_equivalent
        c_equivalent <- insert 4 c_equivalent
        c_equivalent <- insert 20 c_equivalent
        Component(fun ctx ->
            let state = ctx.useState 0
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    
                    Canvas.create [ // Remember to initialize position

                        Canvas.height 400.0
                        Canvas.background Brushes.Transparent
                        Canvas.children (([] :  Types.IView list) @ drawTree c_equivalent {x=0.0; y=0.0})

                ]
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