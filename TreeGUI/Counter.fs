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
    type Orientation = 
    | Left
    | Right
    
    let width = 30.0
    let height = 20.0
    let rect_height = 50.0
    let mutable angle = 45.0
    let left point = {x = point.x - rect_height*sin(angle); y = point.y + rect_height*cos(angle) + height}
    let right point  = {x = point.x + rect_height*sin(angle); y = point.y + rect_height*cos(angle) + height}
    let angleInRadians degree = System.Math.PI / 180.0 * degree
    let sin = angleInRadians >> System.Math.Sin
    let cos = angleInRadians >> System.Math.Cos
    

    let createText text point =
        TextBlock.create [
                TextBlock.width (float (String.length text)*10.0)
                TextBlock.height height
                TextBlock.verticalAlignment VerticalAlignment.Center
                TextBlock.horizontalAlignment HorizontalAlignment.Left // Is this necessary?
                // TextBlock.verticalAlignment // Is this necessary?
                TextBlock.text text
                Canvas.left (point.x)
                Canvas.top (point.y + height/2.0)
            ]

    
    let createRect rot point =
        // rot is 45.0 or -45.0
        // alignment?
        Rectangle.create [
            Rectangle.width 1.0
            Rectangle.height rect_height
            TextBlock.verticalAlignment VerticalAlignment.Center
            TextBlock.horizontalAlignment HorizontalAlignment.Center
            Rectangle.fill (SolidColorBrush (Color.Parse "black"))
            Rectangle.renderTransform(RotateTransform rot)
            Canvas.left (point.x - rect_height*sin(rot)/2.0)
            Canvas.top  (point.y + height/2.0 + rect_height*cos(rot)/2.0)
            
        ]
            
    let rec drawTree tree point =
        match tree with
        | Empty -> [] : Types.IView list
        | Tree t -> 
            match (t.left, t.right) with
            | (Empty, Empty) -> [createText $"{t.value}" point]
            | (_, Empty) -> [createText $"{t.value}" point; createRect angle point;] @ drawTree t.left (left point)
            | (Empty, _) -> [createText $"{t.value}" point; createRect -angle point] @ drawTree t.right (right point)
            | (_, _) -> [createText $"{t.value}" point; createRect -angle point; createRect angle point;]@ drawTree t.left (left point) @ drawTree t.right (right point)
    


    let view =
        let mutable c_equivalent = Empty
        c_equivalent <- insert 10 c_equivalent
        c_equivalent <- insert 5 c_equivalent
        c_equivalent <- insert 4 c_equivalent
        c_equivalent <- insert 20 c_equivalent
        c_equivalent <- insert 100 c_equivalent
        c_equivalent <- insert 15 c_equivalent
        c_equivalent <- insert 6 c_equivalent
        Component(fun ctx ->
            let state = ctx.useState 0
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    
                    Canvas.create [ // Remember to initialize position
                        Canvas.height 850.0
                        Canvas.background Brushes.Transparent
                        Canvas.children (([] :  Types.IView list) @ drawTree c_equivalent {x=0.0; y=200.0})
                        ]
                    ]

            ]
        )