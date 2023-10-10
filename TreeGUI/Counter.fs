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
    | Middle
    
    let width = 30.0
    let height = 20.0
    let rect_height = 50.0
    let angle = 80.0 * (System.Math.PI/180.0)
    let fromRad = (180.0/System.Math.PI)
    let left point  rot = {x = point.x - rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    let right point rot = {x = point.x + rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    let sin x = System.Math.Sin ( x)
    let cos x = System.Math.Cos ( x)
    
    
    let createText text point =
        TextBlock.create [
                TextBlock.width (float (String.length text)*10.0)
                TextBlock.height height
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
            Rectangle.renderTransform(RotateTransform (fromRad*rot))
            Canvas.left (point.x - rect_height*sin(rot)/2.0)
            Canvas.top  (point.y + rect_height*cos(rot)/2.0)
        ]
            
    let rec drawTree tree point curAngle =
        match tree with
        | Empty -> [] : Types.IView list
        | Tree t -> 
            match (t.left, t.right) with
            | (Empty, Empty) -> [createText $"{t.value}" point]
            | (_, Empty) -> [createText $"{t.value}" point; createRect -curAngle point;] @ drawTree t.left (left point curAngle) (curAngle/2.0)
            | (Empty, _) -> [createText $"{t.value}" point; createRect curAngle point] @ drawTree t.right (right point curAngle) (curAngle/2.0)
            | (_, _) -> 
                printfn $"{point}"
                [createText $"{t.value}" point; 
                createRect -curAngle point; 
                createRect curAngle point;
                ]@ drawTree t.left (left point (curAngle)) (curAngle/2.0) @ drawTree t.right (right point (curAngle)) (curAngle/2.0)
    


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
                        Canvas.children (([] :  Types.IView list) @ drawTree c_equivalent {x=0.0; y=200.0} angle)
                        ]
                    ]

            ]
        )