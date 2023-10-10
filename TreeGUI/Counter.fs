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
    let rect_height = 80.0
    let shrink_factor = 2.5
    let angle = 80.0 * (System.Math.PI/180.0)
    let min_angle = 10.0 * (System.Math.PI/180.0) 
    let fromRad = (180.0/System.Math.PI)
    let left point  rot = {x = point.x - rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    let right point rot = {x = point.x + rect_height*sin(rot); y = point.y + rect_height*cos(rot) + height}
    let sin x = System.Math.Sin (x)
    let cos x = System.Math.Cos (x)
    
    
    let createText text point =
        TextBlock.create [
                TextBlock.width (float (String.length text)*10.0)
                TextBlock.height height
                // TextBlock.verticalAlignment // Is this necessary?
                TextBlock.text text
                Canvas.left (point.x - (float (String.length text)*5.0) )
                Canvas.top (point.y + height)
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
        let ang = max curAngle min_angle
        match tree with
        | Empty -> [] : Types.IView list
        | Tree t -> 
            match (t.left, t.right) with
            | (Empty, Empty) -> [createText $"{t.value}" point]
            | (_, Empty) -> [createText $"{t.value}" point; createRect ang point;] @ drawTree t.left (left point ang) (ang/shrink_factor)
            | (Empty, _) -> [createText $"{t.value}" point; createRect -ang point] @ drawTree t.right (right point ang) (ang/shrink_factor)
            | (_, _) -> 

                [createText $"{t.value}" point; 
                createRect -ang point; 
                createRect ang point;
                ]@ drawTree t.left (left point (ang)) (ang/shrink_factor) @ drawTree t.right (right point (ang)) (ang/shrink_factor)
    


    let view =
        let mutable input =  ""
        let example_list = [10; 5; 4; 20; 30; 15; 12; 9]
        let example_tree = addToTree example_list Empty
        Component(fun ctx ->
            let state = ctx.useState example_tree
            DockPanel.create [
                DockPanel.verticalAlignment VerticalAlignment.Top
                DockPanel.horizontalAlignment HorizontalAlignment.Center
                DockPanel.children [
                    Canvas.create [ // Remember to initialize position
                        Canvas.height 850.0
                        Canvas.background Brushes.Transparent
                        Canvas.children (([] :  Types.IView list) @ drawTree state.Current {x=0.0; y=100.0} angle)
                    ]
                    StackPanel.create [
                        StackPanel.dock Dock.Bottom
                        StackPanel.verticalAlignment VerticalAlignment.Bottom
                        StackPanel.children [
                            TextBox.create [
                                TextBox.horizontalAlignment HorizontalAlignment.Left
                                TextBox.width 100.0
                                TextBox.height 50.0
                                TextBox.text input
                                TextBox.onTextChanged (fun newText -> input <- newText)
                            ]
                            Button.create [
                                TextBox.width 100.0
                                Button.dock Dock.Bottom
                                Button.content "Insert"
                                Button.onClick (fun _ -> 
                                    match System.Int32.TryParse input with
                                    | (true, num) -> state.Set(insert num state.Current)
                                    | _ -> ()
                                )
                            ]
                            Button.create [
                                TextBox.width 100.0
                                Button.content "Delete"
                                Button.dock Dock.Bottom
                                Button.onClick (fun _ -> 
                                    match System.Int32.TryParse input with
                                    | (true, num) -> state.Set(delete num state.Current)
                                    | _ -> ()
                                )
                            ]

                            Button.create [
                                TextBox.width 100.0
                                Button.content "Clear"
                                Button.dock Dock.Bottom
                                Button.onClick (fun _ -> state.Set(Empty))
                            ]
                        ]
                    ]
                    StackPanel.create [
                        StackPanel.children [
                            TextBlock.create [
                                TextBlock.width 200.0
                                TextBlock.height height
                                    // TextBlock.verticalAlignment // Is this necessary?
                                TextBlock.text $"Height {Tree.height state.Current}"
                            ]
                        ]
                    ]
                ]

            ]
        )