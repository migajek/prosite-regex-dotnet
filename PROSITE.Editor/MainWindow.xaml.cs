using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Rendering;
using PROSITE.Core;
using PROSITE.Core.Parsers;

namespace PROSITE.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpressionCompiler _expressionCompiler = new ExpressionCompiler();
        private CompiledExpression currentExpression = null;
        private ExpressionTester tester= new ExpressionTester();
        public MainWindow()
        {
            InitializeComponent();            
            tbData.TextChanged += (sender, args) => HighlightMatches();
            Title = "PROSITE regular expression tester";
        }

        private void tbExpression_TextChanged(object sender, TextChangedEventArgs e)
        {            
            HighlightMatches();            
        }

        private void HighlightMatches()
        {
            if (!IsInitialized)
                return;
            
            lblError.Content = "";
            try
            {
                currentExpression = _expressionCompiler.ParseExpression(tbExpression.Text);                
            }
            catch (ParserException ex)
            {
                lblError.Content = ex.Message;
            }
           
            if (currentExpression == null || String.IsNullOrEmpty(tbData.Text))
                return;

            tbData.TextArea.TextView.LineTransformers.Clear();
            foreach (var documentLine in tbData.Document.Lines)
            {
                var matches = tester.GetMatches(currentExpression, tbData.Document.GetText(documentLine));                
                tbData.TextArea.TextView.LineTransformers.Add(new ColorizeAvalonEdit(matches, documentLine.LineNumber));    
            }
            
        }
    }

    public class ColorizeAvalonEdit : DocumentColorizingTransformer
    {
        private IEnumerable<Match> matches;
        private int lineNumber;

        public ColorizeAvalonEdit(IEnumerable<Match> matches, int lineNumber)
        {
            this.matches = matches;
            this.lineNumber = lineNumber;
        }

        protected override void ColorizeLine(DocumentLine line)
        {
            if (line.LineNumber != lineNumber)
                return;
            int lineStartOffset = line.Offset;
          
            foreach (var item in matches.Select((m,i) => new {m,i}))
            {
                try
                {                    
                    var match = item.m;
                    base.ChangeLinePart(
                        lineStartOffset + match.Position, // startOffset
                        lineStartOffset + match.Position + match.MatchedText.Length, // endOffset
                        (VisualLineElement element) =>
                        {

                            element.TextRunProperties.SetBackgroundBrush(Brushes.Yellow);
                        });
                }
                catch (Exception)
                {
                    
                }
            }
            
        }
    }
}
