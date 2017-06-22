namespace Library.CommonServices.Utils
{
    public class WinListItem
    {
        public string Text { get; private set;}
        public string Value{ get; private set;}
        public WinListItem(string text,string value)
        {
            Text = text;
            Value = value;
        }
    }
}
