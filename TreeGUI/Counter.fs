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
            TextBlock.horizontalAlignment HorizontalAlignment.Center
            TextBlock.verticalAlignment VerticalAlignment.Top
            //TODO
            TextBlock.text text
        ]
                        
    let createRect rot point = 
            Rectangle.create [
                Rectangle.width 5.0
                Rectangle.height 100.0
                Rectangle.fill (SolidColorBrush (Color.Parse "black"))
                
                Rectangle.renderTransform (
                    RotateTransform 45.0
                )
                Canvas.left point.x
                Canvas.top point.y
            ]
    // Point is the current point
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
        Component(fun ctx ->
            let state = ctx.useState 0
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    TextBlock.create [
                        TextBlock.width 64
                        TextBlock.horizontalAlignment HorizontalAlignment.Center
                        TextBlock.verticalAlignment VerticalAlignment.Top
                        TextBlock.text "Test"
                    ]
                    Canvas.create [
                        Canvas.height 400.00
                        Canvas.background Brushes.Transparent
                        Canvas.children [
                            Rectangle.create [
                                Rectangle.width 5.0
                                Rectangle.height 100.0
                                Rectangle.fill (SolidColorBrush (Color.Parse "black"))
                                
                                Rectangle.renderTransform (
                                    RotateTransform 45.0
                                )

                                Canvas.left 20.0
                                Canvas.top 20.0
                            ]
                        ]
                    ]
                ]

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
            ]
        )
