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

var riassuntoFrame = new FrameView(
    new Rect(1, 0, 43, 20),
     $"Riassunto al {DateTime.Now.ToShortDateString()}");
var movimentiFrame = new FrameView(new Rect(44, 0, 64, 20), "Movimenti");
var operazioniLabel = new Label(44, 20, "        [A] Aggiungi    [E] Elimina    [RETURN] Modifica        ");
operazioniLabel.ColorScheme = Colors.Error;
// Add some controls, 
win.Add(
// The ones laid out like an australopithecus, with Absolute positions:
    riassuntoFrame,
    movimentiFrame,
    operazioniLabel
    );

Application.Run();
Application.Shutdown();