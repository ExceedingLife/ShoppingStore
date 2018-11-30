using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.DataAccess
{
    public class US_State : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        private string _abbreviations;
        public string Abbreviations
        {
            get => _abbreviations;
            set
            {
                _abbreviations = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //if(PropertyChanged!=null)PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        //private List<US_State> _allusstates;
        //public List<US_State> AllUSStates
        //{
        //    get => _allusstates;
        //    set
        //    {
        //        _allusstates = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        public US_State()
        {
            Name = null;
            Abbreviations = null;
        }
        public US_State(string ab, string name)
        {
            Name = name;
            Abbreviations = ab;
        }
        public override string ToString()
        {
            return String.Format("{0} - {1}", Abbreviations, Name);
        }
    }
    //Static class of all 50 US_state's
    public static class StatesArray
    {
        private static List<US_State> _get_states;
        public static List<US_State> Get_States
        {
            get => _get_states;
            set
            {
                _get_states = value;
                //INotifyPropertyChanged();
            }
        }
        
        static StatesArray()
        {
            _get_states = new List<US_State>(50);
            _get_states.Add(new US_State("AL", "Alabama"));
            _get_states.Add(new US_State("AK", "Alaska"));
            _get_states.Add(new US_State("AZ", "Arizona"));
            _get_states.Add(new US_State("AR", "Arkansas"));
            _get_states.Add(new US_State("CA", "California"));
            _get_states.Add(new US_State("CO", "Colorado"));
            _get_states.Add(new US_State("CT", "Connecticut"));
            _get_states.Add(new US_State("DE", "Delaware"));
            _get_states.Add(new US_State("DC", "District Of Columbia"));
            _get_states.Add(new US_State("FL", "Florida"));
            _get_states.Add(new US_State("GA", "Georgia"));
            _get_states.Add(new US_State("HI", "Hawaii"));
            _get_states.Add(new US_State("ID", "Idaho"));
            _get_states.Add(new US_State("IL", "Illinois"));
            _get_states.Add(new US_State("IN", "Indiana"));
            _get_states.Add(new US_State("IA", "Iowa"));
            _get_states.Add(new US_State("KS", "Kansas"));
            _get_states.Add(new US_State("KY", "Kentucky"));
            _get_states.Add(new US_State("LA", "Louisiana"));
            _get_states.Add(new US_State("ME", "Maine"));
            _get_states.Add(new US_State("MD", "Maryland"));
            _get_states.Add(new US_State("MA", "Massachusetts"));
            _get_states.Add(new US_State("MI", "Michigan"));
            _get_states.Add(new US_State("MN", "Minnesota"));
            _get_states.Add(new US_State("MS", "Mississippi"));
            _get_states.Add(new US_State("MO", "Missouri"));
            _get_states.Add(new US_State("MT", "Montana"));
            _get_states.Add(new US_State("NE", "Nebraska"));
            _get_states.Add(new US_State("NV", "Nevada"));
            _get_states.Add(new US_State("NH", "New Hampshire"));
            _get_states.Add(new US_State("NJ", "New Jersey"));
            _get_states.Add(new US_State("NM", "New Mexico"));
            _get_states.Add(new US_State("NY", "New York"));
            _get_states.Add(new US_State("NC", "North Carolina"));
            _get_states.Add(new US_State("ND", "North Dakota"));
            _get_states.Add(new US_State("OH", "Ohio"));
            _get_states.Add(new US_State("OK", "Oklahoma"));
            _get_states.Add(new US_State("OR", "Oregon"));
            _get_states.Add(new US_State("PA", "Pennsylvania"));
            _get_states.Add(new US_State("RI", "Rhode Island"));
            _get_states.Add(new US_State("SC", "South Carolina"));
            _get_states.Add(new US_State("SD", "South Dakota"));
            _get_states.Add(new US_State("TN", "Tennessee"));
            _get_states.Add(new US_State("TX", "Texas"));
            _get_states.Add(new US_State("UT", "Utah"));
            _get_states.Add(new US_State("VT", "Vermont"));
            _get_states.Add(new US_State("VA", "Virginia"));
            _get_states.Add(new US_State("WA", "Washington"));
            _get_states.Add(new US_State("WV", "West Virginia"));
            _get_states.Add(new US_State("WI", "Wisconsin"));
            _get_states.Add(new US_State("WY", "Wyoming"));
        }
        //public static US_State[] AllFiftyStates()
        //{
        //    return states.ToArray();
        //}
    }
}
