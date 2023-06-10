using System.ComponentModel;
using Gtk;
using GtkSource;

namespace Studio.Visual;

public class MultilineTextInput : ScrolledWindow
{
    private SourceView textView;
    private TextBuffer textBuffer;
    private LanguageManager manager;

    public string Text
    {
        get { return textBuffer.Text; }
        set { textBuffer.Text = value; }
    }

    public MultilineTextInput()
    {
        textView = new SourceView();
        textBuffer = textView.Buffer;
        textView.WrapMode = WrapMode.Word;
        textView.LeftMargin = 5;
        textView.RightMargin = 5;
        
        manager = new LanguageManager();

        Add(textView);
    }

    public void SetLanguage(string file)
    {
        try
        {
            Language language = manager.GuessLanguage(file, null);
            Console.WriteLine(language.Name);

            textView.HighlightCurrentLine = true;
            textView.ShowLineNumbers = true;
            textView.Buffer.Language = language;
        }
        catch
        {
            Language language = manager.GuessLanguage("text", null);
            
            textView.HighlightCurrentLine = false;
            textView.ShowLineNumbers = false;
            textView.Buffer.Language = language;
        }
    }
}