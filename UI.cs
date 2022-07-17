using NStack;
using Terminal.Gui;


// A sub class of FrameView to show the list of journal entries
public class JournalView : FrameView
{
    private ListView listView;
    private ScaleContext db;
    public JournalView(ScaleContext db, Rect frame, ustring title = null, View[] views = null, Border border = null) : base(frame, title, views, border)
    {
        this.db = db;
        // Create a listbox to show the journal entries
        listView = new ListView(new Rect(1, 0, frame.Width, frame.Height), db.JournalEntries.ToList());
        Add(listView);
    }

    public override bool ProcessColdKey(KeyEvent e)
    {

        if (e.Key == Key.A)
        {
            // Add a new journal entry
            if (AddNewJournalEntry() == true)
            {
                // Refresh the list of journal entries
                listView.SetSource(db.JournalEntries.ToList());
                listView.SetNeedsDisplay();
            }

            return true;
        }
        else if (e.Key == Key.E)
        {
            // Delete the selected journal entry
            var entry = listView.SelectedItem as JournalEntry;
            if (entry != null)
            {
                db.JournalEntries.Remove(entry);
                db.SaveChanges();
                listView.Items = db.JournalEntries.ToList();
            }
        }
        else if (e.Key == Key.Return)
        {
            // Edit the selected journal entry
            var entry = listView.SelectedItem as JournalEntry;
            if (entry != null)
            {
                var entryFrm = new EntryView(new Rect(1, 0, frame.Width, frame.Height), "Modifica movimento", entry);
                entryFrm.ProcessColdKey = (e) =>
                {
                    if (e.Key == Key.Return)
                    {
                        db.SaveChanges();
                        listView.Items = db.JournalEntries.ToList();
                    }
                };
                entryFrm.Show();
            }
        }

        return base.ProcessColdKey(e);
    }

    private bool AddNewJournalEntry()
    {
        var entryFrm = new EntryView(new Rect(1, 0, frame.Width, frame.Height), "Nuovo movimento");
        entryFrm.ProcessColdKey = (e) =>
        {
            if (e.Key == Key.Return)
            {
                db.JournalEntries.Add(entryFrm.JournalEntry);
                db.SaveChanges();
                listView.Items = db.JournalEntries.ToList();
            }
        };
        entryFrm.Show();
        return true;
    }


    private void Listbox_ItemDoubleClicked(object sender, ListBoxItemEventArgs e)
    {
        // Show the journal entry in a dialog
        var journal = (Journal)e.Item.Data;
        var dialog = new JournalDialog(journal);
        dialog.Show();
    }

    private void Listbox_ItemSelected(object sender, ListBoxItemEventArgs e)
    {
        // Show the journal entry in a dialog
        var journal = (Journal)e.Item.Data;
        var dialog = new JournalDialog(journal);
        dialog.Show();
    }

    private void Listbox_KeyPressed(object sender, KeyEventArgs e)
    {
        // Show the journal entry in a dialog
        var journal = (Journal)e.Item.Data;
        var dialog = new JournalDialog(journal);
        dialog.Show();
    }
}



