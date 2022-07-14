using System;
using System.Linq;

using Terminal.Gui;
using NStack;

Application.Init();
var top = Application.Top;

// Creates the top-level window to show
var win = new Window("SCALE - v. 0.2.0")
{
    X = 0,
    Y = 1, // Leave one row for the toplevel menu

    // By using Dim.Fill(), it will automatically resize without manual intervention
    Width = Dim.Fill(),
    Height = Dim.Fill()
};

top.Add(win);

// Creates a menubar, the item "New" has a help menu.
var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ("_File", new MenuItem [] {
                new MenuItem ("_Esci", "", () => { if (Quit ()) top.Running = false; })
            }),
            new MenuBarItem ("_Edit", new MenuItem [] {
                new MenuItem ("_Copy", "", null),
                new MenuItem ("C_ut", "", null),
                new MenuItem ("_Paste", "", null)
            })
        });
top.Add(menu);

static bool Quit()
{
    var n = MessageBox.Query(
        50,
        7,
        "Esci da SCALE",
        "Sei sicuro di volere chiudere l'applicazione?",
        "Ok",
        "Annulla");
    return n == 0;
}

using var db = new ScaleContext();

// Add some controls, 
win.Add(
    // The ones laid out like an australopithecus, with Absolute positions:
    new Label(3, 10, $"Database path: {db.DbPath}."),
    new Label(3, 15, $"Total balance: {db.TotalBalance()}"),
    new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar")
);

Application.Run();
Application.Shutdown();