using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Customized.Model;
using Customized.View;
using Xamarin.Forms;

namespace Customized.PageModel
{
    public class ClockinPageModel : INotifyPropertyChanged
    {
        //IMPLEMENT INTERFACES BEGIN
        public event PropertyChangedEventHandler PropertyChanged;

        //IMPLEMENT INTERFACES END

        //lets create series begin
        ObservableCollection<WorkItem> _workItems;
        public ObservableCollection<WorkItem> WorkItems
        {
            get => _workItems;
            set
            {
                _workItems = value;
                OnPropertyChanged(nameof(WorkItems));
            }
        }
        //lets create series end
        




        //COMMAND DECLARATIONS BEGIN
        public ICommand ClockinCommand { get; set; }
        public ICommand ClockoutCommand { get; set; }

        //COMMAND DECLARATIONS END














        //PROPERTY DECLARATIONS BEGIN

        private DateTime _clockIn;
        public DateTime Clockin
        {
            get => _clockIn;
            set
            {
                _clockIn = value;
                OnPropertyChanged(nameof(Clockin));
               

            }
        }



        private DateTime _clockOut;
        public DateTime Clockout
        {
            get => _clockOut;
            set
            {
                _clockOut = value;
                OnPropertyChanged(nameof(Clockout));
            }
        }




        private bool _isClockedIn;
        public bool IsClockedIn
        {
            get => _isClockedIn;
            set
            {
                _isClockedIn = value;
                OnPropertyChanged(nameof(IsClockedIn));
            }
        }

        //PROPERTY DECLARATIONS END 











        




        //METHOD DECLARATIONS BEGIN
        public void ClockinMethod()
        {
            if (IsClockedIn == false)
            {
            Clockin = DateTime.Now;
            IsClockedIn = !IsClockedIn;
            }
            
            
        }

        public int total;
        public void ClockoutMethod()
        {
            if (IsClockedIn == true)
            {
                Clockout = DateTime.Now;
                WorkItems.Insert(0, new WorkItem
                {
                    End = Clockout,
                    Start = Clockin,

                });

                IsClockedIn = false;
                
            }

            else
            {
                ClockoutCommand = null;
            }

        }



        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        //METHOD DECLARATIONS END 















        //TO DISPLAY TIME EACH SECOND BEGIN
        DateTime _dateTime;

        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateTime"));

            }
        }
        //TO DISPLAY TIME EACH SECOND END










        //CONSTRUCTOR BEGIN
        public ClockinPageModel()
        {

            WorkItems = new ObservableCollection<WorkItem>();

            ClockinCommand = new Command(ClockinMethod);
            ClockoutCommand = new Command(ClockoutMethod);
            

            //TIME EACH SECOND UPDATE BEGIN
            DateTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                DateTime = DateTime.Now;
                return true;
            });
            //TIME EACH SECOND UPDATE END
        }
        //CONSTRUCTOR END
    }
}
