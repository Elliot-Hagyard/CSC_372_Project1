﻿namespace TreeGUI

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
                        Canvas.height 100.00
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
