(*
File: Program.fs
Authors: Elliot Hagyard and M. Fay Garcia
Purpose: Initialize the BST visualizer application
*)
namespace TreeGUI

// Open the packages we need to use
open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Input
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts

// Initialize the main application window
type MainWindow() as this =
    inherit HostWindow()
    do
        // Define the attributes of the window
        base.Title <- "TreeGUI"
        base.Width <- 1000.0
        base.Height <- 1000.0
        this.Content <- TreeViewer.view


// Here we define a type which inherits Application to override some of its functions
type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme(baseUri = null, Mode = FluentThemeMode.Light))

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

// This module builds and initializes our application
module Program =
    // Here we have defined the entry point of the application
    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)